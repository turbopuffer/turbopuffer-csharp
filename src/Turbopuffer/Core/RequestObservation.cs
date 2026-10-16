using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Turbopuffer.Exceptions;
using Net = System.Net;

namespace Turbopuffer.Core;

/// <summary>
/// Drives a request's <see cref="RequestScope"/> through its attempts. Generated code
/// calls it at fixed points of each request, and only when the request is observed.
/// </summary>
sealed class RequestObservation
{
    readonly RequestScope _scope;

    // The caller's token, to tell their cancellation apart from timeouts.
    readonly CancellationToken _cancellationToken;

    int _attempt;
    AttemptState _current = new();
    HttpRequestMessage? _request;
    HttpResponseMessage? _response;
    Exception? _attemptError;
    ErrorKind? _attemptErrorKind;
    int _finished;

    RequestObservation(RequestScope scope, CancellationToken cancellationToken)
    {
        _scope = scope;
        _cancellationToken = cancellationToken;
    }

    /// <summary>
    /// The current attempt's timings, which custom code may add marks to.
    /// </summary>
    internal Timings Timings
    {
        get { return _current.Timings; }
    }

    /// <summary>
    /// Starts observing a request, returning null when the client has no observer or the
    /// observer leaves the request unobserved.
    /// </summary>
    internal static RequestObservation? Start(
        ClientOptions options,
        HttpMethod method,
        ParamsBase parameters,
        CancellationToken cancellationToken
    )
    {
        var observer = options.RequestObserver;
        if (observer == null)
        {
            return null;
        }

        string url;
        try
        {
            url = parameters.RelativeUrl(options);
        }
        catch (Exception)
        {
            // The request fails the same way when it's built.
            return null;
        }

        RequestScope? scope;
        try
        {
            scope = observer.Start(new(method, url));
        }
        catch (Exception e)
        {
            LogObserverException(e);
            return null;
        }
        return scope == null ? null : new(scope, cancellationToken);
    }

    /// <summary>
    /// Finishes the request with the exception it failed with, if it fails.
    /// </summary>
    internal async Task<HttpResponse> Track(Task<HttpResponse> execution)
    {
        try
        {
            return await execution.ConfigureAwait(false);
        }
        catch (Exception e)
        {
            this.Fail(e, _response, AttemptPhase.Unknown, _cancellationToken);
            throw;
        }
    }

    /// <summary>
    /// Returns the body content of the request, timing its serialization when observed.
    /// </summary>
    internal static HttpContent? Serialize(RequestObservation? observation, ParamsBase parameters)
    {
        if (observation == null)
        {
            return parameters.BodyContent();
        }
        var start = Timings.Now();
        var content = parameters.BodyContent();
        if (content != null)
        {
            observation.Timings.SerializeStart = start;
            observation.Timings.SerializeEnd = Timings.Now();
        }
        return content;
    }

    internal void StartAttempt(int attempt)
    {
        // The first attempt's state was made with the observation.
        if (attempt > 0)
        {
            _current.Complete();
            _current = new();
        }
        _attempt = attempt;
        _request = null;
        _response = null;
        _current.Timings.AttemptStart = Timings.Now();
    }

    internal void BeforeSend(HttpRequestMessage request)
    {
        _current.Timings.RequestReady = Timings.Now();
        _request = request;
#if NET
        // Lets the SDK's own handler attribute connection events to this attempt.
        ConnectionObserver.Current.Value = _current;
#endif
        try
        {
            _scope.AttemptBegin(_attempt, request);
        }
        catch (Exception e)
        {
            LogObserverException(e);
        }
    }

    internal void Responded(HttpResponseMessage response)
    {
        var attempt = _current;
        attempt.Timings.ResponseHeaders = Timings.Now();
        attempt.Complete();
        _response = response;
        this.AttemptEnd(new(_attempt, _request!, response, null, null, true, attempt.Timings));
    }

    internal void AttemptFailed(Exception error)
    {
        var attempt = _current;
        attempt.Complete();
        _response = null;
        // An attempt that failed while building its request was never sent.
        if (_request == null)
        {
            return;
        }
        var kind = Classify(error, attempt.Phase(), _cancellationToken.IsCancellationRequested);
        _attemptError = error;
        _attemptErrorKind = kind;
        this.AttemptEnd(
            new(_attempt, _request, null, error, kind, ReachedServer(kind), attempt.Timings)
        );
    }

    /// <summary>
    /// Replaces the response the request ends with, such as with the result of polling for an
    /// accepted request.
    /// </summary>
    internal void ReplaceResponse(HttpResponseMessage response)
    {
        _response = response;
    }

    /// <summary>
    /// Marks the response body as read, unless custom code already did.
    /// </summary>
    internal void BodyRead()
    {
        if (_finished == 0)
        {
            this.Timings.BodyEnd ??= Timings.Now();
        }
    }

    /// <summary>
    /// Reads the whole response body before it's deserialized, so that reading and
    /// deserializing are timed apart.
    /// </summary>
    internal async Task<Stream> ReadBody(Stream body, CancellationToken cancellationToken)
    {
        MemoryStream buffer = new();
        await body.CopyToAsync(buffer, 81920, cancellationToken).ConfigureAwait(false);
        buffer.Position = 0;
        this.BodyRead();
        if (_finished == 0)
        {
            this.Timings.DeserializeStart = Timings.Now();
        }
        return buffer;
    }

    internal void Finish(HttpResponseMessage response, object? parsed) =>
        this.End(response, parsed, null, null);

    /// <summary>
    /// Finishes the request with the exception it failed with. <paramref name="phase"/> is how
    /// far the request had got when the exception wasn't raised by an attempt.
    /// </summary>
    internal void Fail(
        Exception error,
        HttpResponseMessage? response,
        AttemptPhase phase,
        CancellationToken cancellationToken
    )
    {
        ErrorKind? kind = null;
        // An error response is described by the response itself.
        if (error is not TurbopufferApiException)
        {
            var canceled =
                cancellationToken.IsCancellationRequested
                || _cancellationToken.IsCancellationRequested;
            if (ReferenceEquals(error, _attemptError))
            {
                kind = _attemptErrorKind;
            }
            // Failing after the whole body was read means it couldn't be deserialized.
            else if (this.Timings.DeserializeStart != null && !canceled)
            {
                kind = ErrorKind.Other;
            }
            else
            {
                kind = Classify(error, phase, canceled);
            }
        }
        this.End(response, null, error, kind);
    }

    void End(HttpResponseMessage? response, object? parsed, Exception? error, ErrorKind? kind)
    {
        if (Interlocked.Exchange(ref _finished, 1) != 0)
        {
            return;
        }
        var attempt = _current;
        attempt.Complete();
        var timings = attempt.Timings;
        if (timings.DeserializeStart != null)
        {
            timings.DeserializeEnd = Timings.Now();
        }
        try
        {
            _scope.Finish(new(_attempt, response, error, kind, parsed, timings));
        }
        catch (Exception e)
        {
            LogObserverException(e);
        }
    }

    void AttemptEnd(AttemptResult result)
    {
        try
        {
            _scope.AttemptEnd(result);
        }
        catch (Exception e)
        {
            LogObserverException(e);
        }
    }

    static void LogObserverException(Exception e) =>
        Trace.TraceWarning("Request observer threw an exception: {0}", e);

    static bool? ReachedServer(ErrorKind kind) =>
        kind is ErrorKind.PoolTimeout or ErrorKind.ConnectTimeout or ErrorKind.ConnectError
            ? false
            : null;

    internal static ErrorKind Classify(Exception error, AttemptPhase phase, bool canceled)
    {
        var timeout = false;
        for (var e = error; e != null; e = e.InnerException)
        {
            if (e is OperationCanceledException && canceled)
            {
                return ErrorKind.Canceled;
            }
            if (e is OperationCanceledException or TimeoutException)
            {
                timeout = true;
            }
            // What the exception says beats what the attempt recorded.
            else if (IsConnectFailure(e))
            {
                phase = AttemptPhase.Connect;
            }
            else if (IsReadFailure(e))
            {
                phase = AttemptPhase.Read;
            }
        }
        return (phase, timeout) switch
        {
            (AttemptPhase.Pool, true) => ErrorKind.PoolTimeout,
            (AttemptPhase.Connect, true) => ErrorKind.ConnectTimeout,
            (AttemptPhase.Connect, false) => ErrorKind.ConnectError,
            (AttemptPhase.Write, true) => ErrorKind.WriteTimeout,
            (AttemptPhase.Read, true) => ErrorKind.ReadTimeout,
            (AttemptPhase.Read, false) => ErrorKind.ReadError,
            (_, true) => ErrorKind.Timeout,
            _ => ErrorKind.Other,
        };
    }

    static bool IsConnectFailure(Exception e) =>
        e switch
        {
#if NET8_0_OR_GREATER
            HttpRequestException
            {
                HttpRequestError: HttpRequestError.NameResolutionError
                    or HttpRequestError.ConnectionError
                    or HttpRequestError.SecureConnectionError
                    or HttpRequestError.ProxyTunnelError
            } => true,
#endif
            SocketException
            {
                SocketErrorCode: SocketError.ConnectionRefused
                    or SocketError.HostNotFound
                    or SocketError.HostUnreachable
                    or SocketError.NetworkUnreachable
                    or SocketError.TryAgain
                    or SocketError.NoData
            } => true,
            AuthenticationException => true,
            Net::WebException
            {
                Status: Net::WebExceptionStatus.ConnectFailure
                    or Net::WebExceptionStatus.NameResolutionFailure
                    or Net::WebExceptionStatus.ProxyNameResolutionFailure
                    or Net::WebExceptionStatus.SecureChannelFailure
                    or Net::WebExceptionStatus.TrustFailure
            } => true,
            _ => false,
        };

    static bool IsReadFailure(Exception e) =>
        e switch
        {
#if NET8_0_OR_GREATER
            HttpRequestException
            {
                HttpRequestError: HttpRequestError.ResponseEnded or HttpRequestError.InvalidResponse
            } => true,
#endif
            Net::WebException
            {
                Status: Net::WebExceptionStatus.ReceiveFailure
                    or Net::WebExceptionStatus.ConnectionClosed
            } => true,
            _ => false,
        };
}

/// <summary>
/// How far an attempt got.
/// </summary>
enum AttemptPhase
{
    Unknown,

    /// <summary>
    /// Waiting for a connection from the pool.
    /// </summary>
    Pool,
    Connect,
    Write,
    Read,
}

/// <summary>
/// One attempt of an observed request. The SDK's own handler records connection events on
/// it from other tasks, so those hold the lock and stop once the attempt completes.
/// </summary>
sealed class AttemptState
{
    readonly object _lock = new();

#if NET
    bool _done;

    // Set when the attempt went through the SDK's own handler, so its connection is observed.
    bool _transportObserved;

    // The connection the attempt last wrote to.
    ObservedConnection? _connection;

    int _writing;

    long? _pendingConnectStart;
#endif

    internal Timings Timings { get; } = new();

    internal void Complete()
    {
#if NET
        lock (_lock)
        {
            _done = true;
        }
#endif
    }

    internal AttemptPhase Phase()
    {
        lock (_lock)
        {
            if (this.Timings.ResponseHeaders != null)
            {
                return AttemptPhase.Read;
            }
#if NET
            if (_connection != null)
            {
                return _writing > 0 ? AttemptPhase.Write : AttemptPhase.Read;
            }
            if (this.Timings.ConnectStart != null && this.Timings.ConnectEnd == null)
            {
                return AttemptPhase.Connect;
            }
            if (_transportObserved)
            {
                return AttemptPhase.Pool;
            }
#endif
            return AttemptPhase.Unknown;
        }
    }

#if NET
    internal void TransportObserved()
    {
        lock (_lock)
        {
            _transportObserved = true;
        }
    }

    /// <summary>
    /// Records that the attempt asked for a new connection, which may end up serving another
    /// request.
    /// </summary>
    internal void ConnectStarted(long at)
    {
        lock (_lock)
        {
            _pendingConnectStart = at;
            if (!_done && _connection == null)
            {
                this.Timings.ConnectStart = at;
                this.Timings.ConnectEnd = null;
            }
        }
    }

    /// <summary>
    /// Records that the connection the attempt asked for is ready, returning when it started.
    /// </summary>
    internal long Connected(long at)
    {
        lock (_lock)
        {
            if (!_done && _connection == null && this.Timings.ConnectStart != null)
            {
                this.Timings.ConnectEnd = at;
            }
            return _pendingConnectStart ?? at;
        }
    }

    internal void Writing(ObservedConnection connection, long at)
    {
        lock (_lock)
        {
            if (_done)
            {
                return;
            }
            _writing++;
            if (_connection == connection)
            {
                return;
            }
            // A new connection for the attempt replaces what an earlier one recorded.
            _connection = connection;
            var reused = !connection.Claim();
            this.Timings.Reused = reused;
            this.Timings.ConnectStart = reused ? null : connection.ConnectStart;
            this.Timings.ConnectEnd = reused ? null : connection.ConnectEnd;
            this.Timings.SendStart = at;
            this.Timings.SendEnd = null;
        }
    }

    internal void Wrote(long at, bool succeeded)
    {
        lock (_lock)
        {
            if (_done)
            {
                return;
            }
            _writing--;
            if (succeeded)
            {
                this.Timings.SendEnd = at;
            }
        }
    }
#endif
}

/// <summary>
/// The default HTTP client of client options, created on first use. It observes connections
/// only when the options have a request observer by then, so clients without one get the
/// platform's usual handler.
/// </summary>
sealed class DefaultHttpClient
{
    /// <summary>
    /// Serves options that skipped their initializers, such as default instances.
    /// </summary>
    internal static readonly DefaultHttpClient Fallback = new();

    readonly object _lock = new();
    HttpClient? _client;

    /// <summary>
    /// Whether the client was created for observed requests, or null until it's created.
    /// </summary>
    internal bool? Observed { get; private set; }

    internal HttpClient Get(bool observed, Net::DecompressionMethods decompression)
    {
        var client = Volatile.Read(ref _client);
        if (client != null)
        {
            return client;
        }
        lock (_lock)
        {
            if (_client == null)
            {
                HttpMessageHandler handler = observed
                    ? ConnectionObserver.CreateHandler(decompression)
                    : new HttpClientHandler() { AutomaticDecompression = decompression };
                this.Observed = observed;
                Volatile.Write(ref _client, new(handler) { Timeout = Timeout.InfiniteTimeSpan });
            }
            return _client!;
        }
    }
}

/// <summary>
/// Observes the connections of the SDK's default HTTP client on .NET, attributing their
/// events to the attempts that use them.
///
/// <para>A connection is opened on behalf of the request that asked for it, but may serve
/// another one. So connection timings are kept on the connection and given to the first
/// attempt that writes to it, which is also how reuse is told. Writes happen on the
/// attempt's own async flow, which carries the attempt. Only HTTP/1.x connections are
/// observed because HTTP/2 writes requests from a shared task.</para>
/// </summary>
static class ConnectionObserver
{
    /// <summary>
    /// Returns the handler of the SDK's default HTTP client for observed requests, which
    /// observes connections where the platform allows.
    /// </summary>
    internal static HttpMessageHandler CreateHandler(Net::DecompressionMethods decompression)
    {
#if NET
        // Mobile platforms prefer their native handlers, which aren't observable.
        if (
            SocketsHttpHandler.IsSupported
            && !OperatingSystem.IsAndroid()
            && !OperatingSystem.IsIOS()
            && !OperatingSystem.IsTvOS()
            && !OperatingSystem.IsMacCatalyst()
        )
        {
            return new ObservingHandler(
                new SocketsHttpHandler()
                {
                    AutomaticDecompression = decompression,
                    ConnectCallback = ConnectAsync,
                    PlaintextStreamFilter = FilterAsync,
                }
            );
        }
#endif
        return new HttpClientHandler() { AutomaticDecompression = decompression };
    }

#if NET
    internal static readonly AsyncLocal<AttemptState?> Current = new();

    static readonly HttpRequestOptionsKey<AttemptState> AttemptKey = new(
        "Turbopuffer.Core.AttemptState"
    );

    static AttemptState? AttemptOf(HttpRequestMessage request) =>
        request.Options.TryGetValue(AttemptKey, out var attempt) ? attempt : null;

    // Connects like SocketsHttpHandler does by default, but notes when connecting started.
    static async ValueTask<Stream> ConnectAsync(
        SocketsHttpConnectionContext context,
        CancellationToken cancellationToken
    )
    {
        AttemptOf(context.InitialRequestMessage)?.ConnectStarted(Timings.Now());
        Socket socket = new(SocketType.Stream, ProtocolType.Tcp) { NoDelay = true };
        try
        {
            await socket.ConnectAsync(context.DnsEndPoint, cancellationToken).ConfigureAwait(false);
            return new NetworkStream(socket, ownsSocket: true);
        }
        catch
        {
            socket.Dispose();
            throw;
        }
    }

    // Runs once the connection is ready, after any TLS handshake.
    static ValueTask<Stream> FilterAsync(
        SocketsHttpPlaintextStreamFilterContext context,
        CancellationToken cancellationToken
    )
    {
        var attempt = AttemptOf(context.InitialRequestMessage);
        // Connections of unobserved clients aren't wrapped, so they cost nothing.
        if (attempt == null || context.NegotiatedHttpVersion.Major != 1)
        {
            return new(context.PlaintextStream);
        }
        var now = Timings.Now();
        var start = attempt.Connected(now);
        return new(new ObservedConnection(context.PlaintextStream, start, now));
    }

    sealed class ObservingHandler : DelegatingHandler
    {
        internal ObservingHandler(HttpMessageHandler innerHandler)
            : base(innerHandler) { }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken
        )
        {
            var attempt = Current.Value;
            if (attempt != null && request.Version.Major == 1)
            {
                attempt.TransportObserved();
                request.Options.Set(AttemptKey, attempt);
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
#endif
}

#if NET
/// <summary>
/// An HTTP/1.x connection of the SDK's default HTTP client, which marks when the attempt
/// using it writes its request.
/// </summary>
sealed class ObservedConnection : Stream
{
    readonly Stream _inner;
    int _claimed;

    internal ObservedConnection(Stream inner, long connectStart, long connectEnd)
    {
        _inner = inner;
        this.ConnectStart = connectStart;
        this.ConnectEnd = connectEnd;
    }

    internal long ConnectStart { get; }

    internal long ConnectEnd { get; }

    /// <summary>
    /// Returns true the first time it's called, when the connection serves its first
    /// attempt.
    /// </summary>
    internal bool Claim() => Interlocked.Exchange(ref _claimed, 1) == 0;

    public override bool CanRead => _inner.CanRead;

    public override bool CanSeek => false;

    public override bool CanWrite => _inner.CanWrite;

    public override bool CanTimeout => _inner.CanTimeout;

    public override int ReadTimeout
    {
        get => _inner.ReadTimeout;
        set => _inner.ReadTimeout = value;
    }

    public override int WriteTimeout
    {
        get => _inner.WriteTimeout;
        set => _inner.WriteTimeout = value;
    }

    public override long Length => throw new NotSupportedException();

    public override long Position
    {
        get => throw new NotSupportedException();
        set => throw new NotSupportedException();
    }

    public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();

    public override void SetLength(long value) => throw new NotSupportedException();

    public override void Flush() => _inner.Flush();

    public override Task FlushAsync(CancellationToken cancellationToken) =>
        _inner.FlushAsync(cancellationToken);

    public override int Read(byte[] buffer, int offset, int count) =>
        _inner.Read(buffer, offset, count);

    public override int Read(Span<byte> buffer) => _inner.Read(buffer);

    public override Task<int> ReadAsync(
        byte[] buffer,
        int offset,
        int count,
        CancellationToken cancellationToken
    ) => _inner.ReadAsync(buffer, offset, count, cancellationToken);

    public override ValueTask<int> ReadAsync(
        Memory<byte> buffer,
        CancellationToken cancellationToken = default
    ) => _inner.ReadAsync(buffer, cancellationToken);

    public override void Write(byte[] buffer, int offset, int count) =>
        this.Write(buffer.AsSpan(offset, count));

    public override void Write(ReadOnlySpan<byte> buffer)
    {
        var attempt = ConnectionObserver.Current.Value;
        if (attempt == null)
        {
            _inner.Write(buffer);
            return;
        }
        attempt.Writing(this, Timings.Now());
        var succeeded = false;
        try
        {
            _inner.Write(buffer);
            succeeded = true;
        }
        finally
        {
            attempt.Wrote(Timings.Now(), succeeded);
        }
    }

    public override Task WriteAsync(
        byte[] buffer,
        int offset,
        int count,
        CancellationToken cancellationToken
    ) => this.WriteAsync(buffer.AsMemory(offset, count), cancellationToken).AsTask();

    public override ValueTask WriteAsync(
        ReadOnlyMemory<byte> buffer,
        CancellationToken cancellationToken = default
    )
    {
        var attempt = ConnectionObserver.Current.Value;
        return attempt == null
            ? _inner.WriteAsync(buffer, cancellationToken)
            : this.WriteObservedAsync(attempt, buffer, cancellationToken);
    }

    async ValueTask WriteObservedAsync(
        AttemptState attempt,
        ReadOnlyMemory<byte> buffer,
        CancellationToken cancellationToken
    )
    {
        attempt.Writing(this, Timings.Now());
        var succeeded = false;
        try
        {
            await _inner.WriteAsync(buffer, cancellationToken).ConfigureAwait(false);
            succeeded = true;
        }
        finally
        {
            attempt.Wrote(Timings.Now(), succeeded);
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _inner.Dispose();
        }
        base.Dispose(disposing);
    }

    public override async ValueTask DisposeAsync()
    {
        await _inner.DisposeAsync().ConfigureAwait(false);
        await base.DisposeAsync().ConfigureAwait(false);
    }
}
#endif
