using System;
using System.Net.Http;
using Turbopuffer.Client.Exceptions;

namespace Turbopuffer.Client.Core;

/// <summary>
/// A class representing the SDK client configuration.
/// </summary>
public record struct ClientOptions()
{
    /// <summary>
    /// The default value used for <see cref="MaxRetries"/>.
    /// </summary>
    public static readonly int DefaultMaxRetries = 4;

    /// <summary>
    /// The default value used for <see cref="Timeout"/>.
    /// </summary>
    public static readonly TimeSpan DefaultTimeout = TimeSpan.FromMinutes(1);

    /// <summary>
    /// The HTTP client to use for making requests in the SDK.
    ///
    /// <para>Note: The HttpClient has a built-in timeout, which defaults to 100 seconds.
    /// When passing a custom HttpClient, this timeout may conflict with the SDK's
    /// own timeout handler and cause premature cancellation.</para>
    /// </summary>
    public HttpClient HttpClient { get; set; } =
        // Automatic decompression is disabled so that the SDK fully controls the
        // Accept-Encoding header (see <see cref="Compression"/>). Gzip responses
        // are decompressed manually in HttpResponse.ReadAsStream.
        new(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.None })
        {
            Timeout = global::System.Threading.Timeout.InfiniteTimeSpan,
        };

    Lazy<string> _baseUrl = new(() =>
        Environment.GetEnvironmentVariable("TURBOPUFFER_BASE_URL") ?? EnvironmentUrl.Production
    );

    /// <summary>
    /// The base URL to use for every request.
    ///
    /// <para>Defaults to the production environment: <see cref="EnvironmentUrl.Production"/></para>
    /// </summary>
    public string BaseUrl
    {
        readonly get
        {
            var baseUrl = _baseUrl.Value;

            if (baseUrl.Contains("{region}"))
            {
                if (Region == null)
                {
                    throw new TurbopufferInvalidDataException(
                        string.Format("{0} cannot be null", nameof(Region)),
                        new ArgumentNullException(nameof(Region))
                    );
                }
                baseUrl = baseUrl.Replace("{region}", Region);
            }

            return baseUrl;
        }
        set { _baseUrl = new(() => value); }
    }

    /// <summary>
    /// Whether to validate response bodies before returning them.
    ///
    /// <para>Defaults to false, which means the shape of the response body will not be validated upfront.
    /// Instead, validation will only occur for the parts of the response body that are accessed.</para>
    ///
    /// <para>Note that when set to true, the response body is only validated if the response is
    /// deserialized. Methods that don't eagerly deserialize the response, such as those on
    /// <see cref="ITurbopufferClient.WithRawResponse"/>, don't perform validation until deserialization
    /// is triggered.</para>
    /// </summary>
    public bool ResponseValidation { get; set; } = false;

    /// <summary>
    /// The maximum number of times to retry failed requests, with a short exponential backoff between requests.
    ///
    /// <para>
    /// Only the following error types are retried:
    /// <list type="bullet">
    ///   <item>Connection errors (for example, due to a network connectivity problem)</item>
    ///   <item>408 Request Timeout</item>
    ///   <item>409 Conflict</item>
    ///   <item>429 Rate Limit</item>
    ///   <item>5xx Internal</item>
    /// </list>
    /// </para>
    ///
    /// <para>The API may also explicitly instruct the SDK to retry or not retry a request.</para>
    ///
    /// <para>Defaults to 4 when null. Set to 0 to
    /// disable retries, which also ignores API instructions to retry.</para>
    /// </summary>
    public int? MaxRetries { get; set; } = null;

    /// <summary>
    /// Sets the maximum time allowed for a complete HTTP call, not including retries.
    ///
    /// <para>This includes resolving DNS, connecting, writing the request body, server processing, as
    /// well as reading the response body.</para>
    ///
    /// <para>Defaults to <c>TimeSpan.FromMinutes(1)</c> when null.</para>
    /// </summary>
    public TimeSpan? Timeout { get; set; } = null;

    /// <summary>
    /// API key used for authentication
    /// </summary>
    Lazy<string> _apiKey = new(() =>
        Environment.GetEnvironmentVariable("TURBOPUFFER_API_KEY")
        ?? throw new TurbopufferInvalidDataException(
            string.Format("{0} cannot be null", nameof(ApiKey)),
            new ArgumentNullException(nameof(ApiKey))
        )
    );

    /// <summary>
    /// API key used for authentication
    /// </summary>
    public string ApiKey
    {
        readonly get { return _apiKey.Value; }
        set { _apiKey = new(() => value); }
    }

    /// <summary>
    /// The turbopuffer region to use.
    /// </summary>
    Lazy<string?> _region = new(() => Environment.GetEnvironmentVariable("TURBOPUFFER_REGION"));

    /// <summary>
    /// The turbopuffer region to use.
    /// </summary>
    public string? Region
    {
        readonly get { return _region.Value; }
        set { _region = new(() => value); }
    }

    public string? DefaultNamespace { get; set; }

    /// <summary>
    /// Whether to gzip-compress requests and advertise gzip compression on responses.
    ///
    /// <para>Defaults to false. When set to true, the SDK gzip-compresses request bodies
    /// (sending <c>Content-Encoding: gzip</c>) and lets the default <see cref="HttpClient"/>
    /// advertise and transparently decompress gzip responses. When false, the SDK sends
    /// <c>Accept-Encoding: identity</c> to prevent the server from returning compressed
    /// responses and leaves request bodies uncompressed.</para>
    /// </summary>
    public bool Compression { get; set; } = false;
}
