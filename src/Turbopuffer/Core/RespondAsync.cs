using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Turbopuffer.Exceptions;

namespace Turbopuffer.Core;

/// <summary>
/// Transparent polling for tpuf APIs that accept <c>Prefer: respond-async</c>.
///
/// <para>Every outgoing request is stamped with <c>Prefer: respond-async</c>. If the server
/// applies the preference (responds with <c>202 Accepted</c> + <c>Preference-Applied: respond-async</c>),
/// the SDK polls the operation URL from the <c>Location</c> header until the operation finishes
/// and returns the final response as if the call had been synchronous.</para>
/// </summary>
internal static class RespondAsync
{
    const string PreferHeader = "Prefer";
    const string PreferenceAppliedHeader = "Preference-Applied";
    const string LocationHeader = "Location";
    const string RespondAsyncValue = "respond-async";

    // Interval between successive polls. Internal var so tests can override.
    internal static TimeSpan PollInterval = TimeSpan.FromSeconds(1);

    // Per-poll request timeout cap.
    static readonly TimeSpan PollRequestTimeout = TimeSpan.FromSeconds(60);

    internal static void PrepareRequest(HttpRequestMessage request)
    {
        if (request.Headers.Contains(PreferHeader))
        {
            return;
        }
        request.Headers.Add(PreferHeader, RespondAsyncValue);
    }

    internal static async Task<HttpResponse> MaybePoll(
        HttpResponse response,
        Uri requestUrl,
        ITurbopufferClientWithRawResponse client,
        CancellationToken cancellationToken
    )
    {
        if (!RespondAsyncApplied(response))
        {
            return response;
        }

        Uri location;
        using (response)
        {
            location = ExtractLocation(response, requestUrl);
        }

        var timeout = new Timeout(client.Timeout ?? ClientOptions.DefaultTimeout);
        var pollRequest = new HttpRequest<PollParams>
        {
            Method = HttpMethod.Get,
            Params = new PollParams { Location = location },
        };

        while (true)
        {
            if (timeout.Remaining() == TimeSpan.Zero)
            {
                throw new TurbopufferIOException("request timed out");
            }

            var pollClient = client.WithOptions(o => o with { Timeout = timeout.PollTimeout() });
            var pollResponse = await pollClient
                .Execute(pollRequest, cancellationToken)
                .ConfigureAwait(false);
            using (pollResponse)
            {
                var resolved = await ResolvePollResponse(pollResponse, cancellationToken)
                    .ConfigureAwait(false);
                if (resolved != null)
                {
                    return resolved;
                }
            }

            await Task.Delay(timeout.SleepDuration(), cancellationToken).ConfigureAwait(false);
        }
    }

    sealed record class PollParams : ParamsBase
    {
        internal required Uri Location { get; init; }

        public override Uri Url(ClientOptions _options) => Location;

        internal override void AddHeadersToRequest(
            HttpRequestMessage request,
            ClientOptions options
        )
        {
            ParamsBase.AddDefaultHeaders(request, options);
            request.Headers.TryAddWithoutValidation(PreferHeader, "");
        }
    }

    static bool RespondAsyncApplied(HttpResponse response)
    {
        if (response.StatusCode != HttpStatusCode.Accepted)
        {
            return false;
        }
        if (!response.TryGetHeaderValues(PreferenceAppliedHeader, out var values))
        {
            return false;
        }
        var applied = Enumerable.FirstOrDefault(values);
        return applied != null
            && string.Equals(applied.Trim(), RespondAsyncValue, StringComparison.OrdinalIgnoreCase);
    }

    static Uri ExtractLocation(HttpResponse response, Uri requestUrl)
    {
        var raw = response.TryGetHeaderValues(LocationHeader, out var values)
            ? Enumerable.FirstOrDefault(values)
            : null;
        if (string.IsNullOrEmpty(raw))
        {
            throw new TurbopufferException(
                "server returned async response without a 'Location' header"
            );
        }

        Uri location;
        try
        {
            // Resolve the Location against the original request URL.
            location = new Uri(requestUrl, raw);
        }
        catch (UriFormatException e)
        {
            throw new TurbopufferException($"malformed 'Location' header: {raw}", e);
        }

        // Reject a Location pointing at a different origin, to prevent API key exfiltration.
        if (!SameOrigin(location, requestUrl))
        {
            throw new TurbopufferException(
                $"'Location' origin does not match request origin: {raw}"
            );
        }
        return location;
    }

    static bool SameOrigin(Uri a, Uri b) =>
        string.Equals(a.Scheme, b.Scheme, StringComparison.OrdinalIgnoreCase)
        && string.Equals(a.Host, b.Host, StringComparison.OrdinalIgnoreCase)
        && a.Port == b.Port;

    static async Task<HttpResponse?> ResolvePollResponse(
        HttpResponse response,
        CancellationToken cancellationToken
    )
    {
        PollBody? body;
        try
        {
            using var stream = await response.ReadAsStream(cancellationToken).ConfigureAwait(false);
            body = await JsonSerializer
                .DeserializeAsync<PollBody>(stream, PollJsonOptions, cancellationToken)
                .ConfigureAwait(false);
        }
        catch (JsonException e)
        {
            throw new TurbopufferException("malformed poll response", e);
        }

        if (body?.Status == "running")
        {
            return null;
        }
        if (body?.Status != "finished" || body.Result is null)
        {
            throw new TurbopufferException("malformed poll response");
        }

        var (statusCode, payload) = body.Result switch
        {
            { Success: { } success, Error: null } => ((int)HttpStatusCode.OK, success.GetRawText()),
            { Success: null, Error: { } err } => (err.StatusCode, err.Detail.GetRawText()),
            _ => throw new TurbopufferException("malformed poll response"),
        };

        var responseMessage = new HttpResponseMessage((HttpStatusCode)statusCode)
        {
            Content = new ByteArrayContent(Encoding.UTF8.GetBytes(payload)),
        };
        responseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        return new HttpResponse { RawMessage = responseMessage };
    }

    static readonly JsonSerializerOptions PollJsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    record PollBody
    {
        public required string Status { get; init; }
        public PollResult? Result { get; init; }
    }

    record PollResult(JsonElement? Success, PollError? Error);

    record PollError
    {
        [JsonPropertyName("status_code")]
        public required int StatusCode { get; init; }
        public required JsonElement Detail { get; init; }
    }

    sealed class Timeout
    {
        readonly DateTime _deadline;

        internal Timeout(TimeSpan timeout) =>
            _deadline =
                timeout == System.Threading.Timeout.InfiniteTimeSpan
                    ? DateTime.MaxValue
                    : DateTime.UtcNow + timeout;

        internal TimeSpan Remaining() => Max(_deadline - DateTime.UtcNow, TimeSpan.Zero);

        // Floor at 1ms because HttpClient treats 0 as "no timeout".
        internal TimeSpan PollTimeout() =>
            Max(Min(Remaining(), PollRequestTimeout), TimeSpan.FromMilliseconds(1));

        internal TimeSpan SleepDuration() => Min(Remaining(), PollInterval);

        static TimeSpan Min(TimeSpan a, TimeSpan b) => a < b ? a : b;

        static TimeSpan Max(TimeSpan a, TimeSpan b) => a > b ? a : b;
    }
}
