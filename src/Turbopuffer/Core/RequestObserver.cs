using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;

namespace Turbopuffer.Core;

/// <summary>
/// Observes the requests a client makes, such as to record their timings. Custom code
/// installs one through <see cref="ClientOptions.RequestObserver"/>.
///
/// <para>Observers are called synchronously at fixed points of each request, so they must be
/// cheap. Anything an observer throws is logged and ignored, so it can never break a request.
/// An observer is shared by concurrent requests, so per request state belongs on the
/// <see cref="RequestScope"/> it returns.</para>
/// </summary>
abstract class RequestObserver
{
    /// <summary>
    /// Called once per request, before its first attempt. Returns a scope to observe the
    /// request, or null to leave it unobserved.
    /// </summary>
    public abstract RequestScope? Start(RequestInfo info);
}

/// <summary>
/// Observes one request across its attempts. Override what you need.
/// </summary>
abstract class RequestScope
{
    /// <summary>
    /// Called before each attempt is sent. May change the request's headers.
    /// </summary>
    public virtual void AttemptBegin(int attempt, HttpRequestMessage request) { }

    /// <summary>
    /// Called after each attempt, success or failure, before its response is disposed.
    /// </summary>
    public virtual void AttemptEnd(AttemptResult result) { }

    /// <summary>
    /// Called at most once, after the response is deserialized or when the request fails for
    /// good. Responses whose body the caller reads itself don't finish.
    /// </summary>
    public virtual void Finish(RequestResult result) { }
}

/// <summary>
/// Describes a request as it starts.
/// </summary>
sealed class RequestInfo
{
    internal RequestInfo(HttpMethod method, string url)
    {
        this.Method = method;
        this.Url = url;
    }

    public HttpMethod Method { get; }

    /// <summary>
    /// The URL the request was made with relative to the base URL, such as
    /// <c>/v1/things/abc</c>. It keeps any query that's part of the API path, but not query
    /// params.
    /// </summary>
    public string Url { get; }
}

/// <summary>
/// The outcome of one attempt of a request.
/// </summary>
sealed class AttemptResult
{
    internal AttemptResult(
        int attempt,
        HttpRequestMessage request,
        HttpResponseMessage? response,
        Exception? error,
        ErrorKind? errorKind,
        bool? reachedServer,
        Timings timings
    )
    {
        this.Attempt = attempt;
        this.Request = request;
        this.Response = response;
        this.Error = error;
        this.ErrorKind = errorKind;
        this.ReachedServer = reachedServer;
        this.Timings = timings;
    }

    /// <summary>
    /// The attempt's 0-based index.
    /// </summary>
    public int Attempt { get; }

    public HttpRequestMessage Request { get; }

    public HttpResponseMessage? Response { get; }

    /// <summary>
    /// The exception the attempt failed with when no response arrived.
    /// </summary>
    public Exception? Error { get; }

    public ErrorKind? ErrorKind { get; }

    /// <summary>
    /// False when the attempt certainly never reached the server, true when a response
    /// arrived, and null when unknown.
    /// </summary>
    public bool? ReachedServer { get; }

    public Timings Timings { get; }
}

/// <summary>
/// The final outcome of a request.
/// </summary>
sealed class RequestResult
{
    internal RequestResult(
        int retries,
        HttpResponseMessage? response,
        Exception? error,
        ErrorKind? errorKind,
        object? parsed,
        Timings timings
    )
    {
        this.Retries = retries;
        this.Response = response;
        this.Error = error;
        this.ErrorKind = errorKind;
        this.Parsed = parsed;
        this.Timings = timings;
    }

    /// <summary>
    /// The number of retries made, which is also the final attempt's index.
    /// </summary>
    public int Retries { get; }

    /// <summary>
    /// The response the request ended with, if any.
    /// </summary>
    public HttpResponseMessage? Response { get; }

    /// <summary>
    /// The exception the request failed with. For an error response it's the API exception,
    /// which has no error kind because the response describes what went wrong.
    /// </summary>
    public Exception? Error { get; }

    public ErrorKind? ErrorKind { get; }

    /// <summary>
    /// The deserialized response body, or null if it wasn't deserialized.
    /// </summary>
    public object? Parsed { get; }

    /// <summary>
    /// The final attempt's timings.
    /// </summary>
    public Timings Timings { get; }
}

/// <summary>
/// Classifies why an attempt or request failed.
/// </summary>
enum ErrorKind
{
    PoolTimeout,
    ConnectTimeout,
    ConnectError,
    WriteTimeout,
    ReadTimeout,
    ReadError,

    /// <summary>
    /// Any other timeout.
    /// </summary>
    Timeout,

    /// <summary>
    /// The caller canceled the request through its cancellation token.
    /// </summary>
    Canceled,
    Other,
}

/// <summary>
/// Timestamps in nanoseconds from <see cref="Now"/>, a monotonic clock, marking the progress
/// of one attempt. A timestamp stays null when its phase didn't happen or couldn't be
/// observed. Phases may overlap.
///
/// <para>Connection phases are only observed through the default HTTP client on .NET, and
/// stay null with a custom one.</para>
/// </summary>
sealed class Timings
{
    static readonly long NanosecondsPerTick =
        1_000_000_000 % Stopwatch.Frequency == 0 ? 1_000_000_000 / Stopwatch.Frequency : 0;

    static readonly double NanosecondsPerTickFraction = 1e9 / Stopwatch.Frequency;

    /// <summary>
    /// The attempt began, before the request was built.
    /// </summary>
    public long? AttemptStart { get; set; }

    /// <summary>
    /// Encoding the request body.
    /// </summary>
    public long? SerializeStart { get; set; }

    public long? SerializeEnd { get; set; }

    /// <summary>
    /// The request was built and is about to be handed to the HTTP client.
    /// </summary>
    public long? RequestReady { get; set; }

    /// <summary>
    /// Opening a new connection: DNS, TCP and TLS. Null when an existing connection was used.
    /// </summary>
    public long? ConnectStart { get; set; }

    public long? ConnectEnd { get; set; }

    /// <summary>
    /// Writing the request headers and body.
    /// </summary>
    public long? SendStart { get; set; }

    public long? SendEnd { get; set; }

    public long? ResponseHeaders { get; set; }

    /// <summary>
    /// The response body was fully read.
    /// </summary>
    public long? BodyEnd { get; set; }

    /// <summary>
    /// Deserializing the response body, which is read in full first. Only on the final attempt.
    /// </summary>
    public long? DeserializeStart { get; set; }

    public long? DeserializeEnd { get; set; }

    /// <summary>
    /// Whether the attempt used an existing connection, or null when unknown.
    /// </summary>
    public bool? Reused { get; set; }

    /// <summary>
    /// Marks added by custom code, such as code that compresses request bodies.
    /// </summary>
    public Dictionary<string, long> Extra { get; } = new();

    /// <summary>
    /// Records the current time in <see cref="Extra"/> under the given name.
    /// </summary>
    public void Mark(string name) => this.Extra[name] = Now();

    /// <summary>
    /// The current time in nanoseconds on the clock all timings use.
    /// </summary>
    public static long Now()
    {
        var ticks = Stopwatch.GetTimestamp();
        return NanosecondsPerTick != 0
            ? ticks * NanosecondsPerTick
            : (long)(ticks * NanosecondsPerTickFraction);
    }
}
