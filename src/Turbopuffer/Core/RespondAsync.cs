using System;
using System.IO;
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
            await Drain(response, cancellationToken).ConfigureAwait(false);
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
                // The inner timeout tells request observers why the request failed.
                throw new TurbopufferIOException(
                    "request timed out",
                    new HttpRequestException("request timed out", new TimeoutException())
                );
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
                    // The request ends with the operation's result rather than the 202.
                    return response.Replace(resolved);
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

    // Reads the 202's body so that its request is timed like any other.
    static async Task Drain(HttpResponse response, CancellationToken cancellationToken)
    {
        try
        {
            await response.ReadAsString(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception e)
            when (e is IOException or HttpRequestException or TurbopufferIOException)
        {
            // The body is ignored, so failing to read it doesn't fail the request.
        }
    }

    static async Task<HttpResponseMessage?> ResolvePollResponse(
        HttpResponse response,
        CancellationToken cancellationToken
    )
    {
        PollBody? body;
        try
        {
            // Deserialized like a typed response so that the poll finishes when observed.
            body = await new HttpResponse<PollBody>(
                response,
                token => response.Deserialize<PollBody>(token)
            )
                .Deserialize(cancellationToken)
                .ConfigureAwait(false);
        }
        catch (Exception e) when (e is JsonException or TurbopufferInvalidDataException)
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
        return responseMessage;
    }

    record PollBody
    {
        [JsonPropertyName("status")]
        public required string Status { get; init; }

        [JsonPropertyName("result")]
        public PollResult? Result { get; init; }
    }

    record PollResult
    {
        [JsonPropertyName("success")]
        public JsonElement? Success { get; init; }

        [JsonPropertyName("error")]
        public PollError? Error { get; init; }
    }

    record PollError
    {
        [JsonPropertyName("status_code")]
        public required int StatusCode { get; init; }

        [JsonPropertyName("detail")]
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
