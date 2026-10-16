using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Turbopuffer;
using Turbopuffer.Core;
using Turbopuffer.Exceptions;

namespace Turbopuffer.Tests.Core;

public class RequestObserverTest : TestBase
{
    [Fact]
    public async Task ObservesRequest()
    {
        var recorder = new Recorder();
        string? observedAttempt = null;
        var client = this.ObservedClient(
            recorder,
            new StubHandler(
                (request, _) =>
                {
                    observedAttempt = Enumerable.Single(
                        request.Headers.GetValues("X-Observed-Attempt")
                    );
                    return Task.FromResult(JsonResponse(HttpStatusCode.OK));
                }
            )
        );

        var parsed = await Deserialize(await Send(client));

        recorder.AssertEvents("start", "before 0", "after 0", "finish");
        Assert.Equal("0", observedAttempt);
        var info = Assert.Single(recorder.Infos);
        Assert.Equal(HttpMethod.Post, info.Method);
        Assert.Equal("/something", info.Url);

        var attempt = Assert.Single(recorder.Attempts);
        Assert.Equal(HttpStatusCode.OK, attempt.Response!.StatusCode);
        Assert.True(attempt.ReachedServer);
        Assert.Null(attempt.ErrorKind);

        var result = Assert.Single(recorder.Results);
        Assert.Equal(0, result.Retries);
        Assert.Equal(HttpStatusCode.OK, result.Response!.StatusCode);
        Assert.Null(result.Error);
        Assert.Same(parsed, result.Parsed);
        var timings = result.Timings;
        AssertOrdered(
            timings.AttemptStart,
            timings.SerializeStart,
            timings.SerializeEnd,
            timings.RequestReady,
            timings.ResponseHeaders,
            timings.BodyEnd,
            timings.DeserializeStart,
            timings.DeserializeEnd
        );
        // A custom HTTP client's connections aren't observed.
        Assert.Null(timings.ConnectStart);
        Assert.Null(timings.SendStart);
        Assert.Null(timings.Reused);
    }

    [Fact]
    public async Task ObservesRetries()
    {
        var recorder = new Recorder();
        var sentAttempts = new List<string>();
        var client = this.ObservedClient(
            recorder,
            new StubHandler(
                (request, _) =>
                {
                    sentAttempts.Add(
                        Enumerable.Single(request.Headers.GetValues("X-Observed-Attempt"))
                    );
                    // Fails the first attempt with an error that's retried.
                    var response = JsonResponse(
                        sentAttempts.Count == 1
                            ? HttpStatusCode.ServiceUnavailable
                            : HttpStatusCode.OK
                    );
                    response.Headers.Add("Retry-After-Ms", "1");
                    return Task.FromResult(response);
                }
            ),
            maxRetries: 1
        );

        await Deserialize(await Send(client));

        recorder.AssertEvents("start", "before 0", "after 0", "before 1", "after 1", "finish");
        Assert.Equal("0,1", string.Join(",", sentAttempts));
        Assert.Equal(HttpStatusCode.ServiceUnavailable, recorder.Attempts[0].Response!.StatusCode);
        Assert.Equal(1, recorder.Attempts[1].Attempt);
        var result = Assert.Single(recorder.Results);
        Assert.Equal(1, result.Retries);
        Assert.Equal(HttpStatusCode.OK, result.Response!.StatusCode);
    }

    [Fact]
    public async Task ObservesErrorResponse()
    {
        var recorder = new Recorder();
        var client = this.ObservedClient(
            recorder,
            new StubHandler((_, _) => Task.FromResult(JsonResponse(HttpStatusCode.BadRequest)))
        );

        var exception = await Assert.ThrowsAnyAsync<TurbopufferApiException>(() => Send(client));

        recorder.AssertEvents("start", "before 0", "after 0", "finish");
        var result = Assert.Single(recorder.Results);
        Assert.Same(exception, result.Error);
        Assert.Null(result.ErrorKind);
        Assert.Equal(HttpStatusCode.BadRequest, result.Response!.StatusCode);
        Assert.NotNull(result.Timings.BodyEnd);
    }

    [Fact]
    public async Task ObservesConnectionError()
    {
        var recorder = new Recorder();
        var client = this.ObservedClient(
            recorder,
            new StubHandler(
                (_, _) =>
                    throw new HttpRequestException(
                        "connection refused",
                        new SocketException((int)SocketError.ConnectionRefused)
                    )
            ),
            maxRetries: 1
        );

        var exception = await Assert.ThrowsAsync<TurbopufferIOException>(() => Send(client));

        recorder.AssertEvents("start", "before 0", "after 0", "before 1", "after 1", "finish");
        Assert.All(
            recorder.Attempts,
            attempt =>
            {
                Assert.Null(attempt.Response);
                Assert.Equal(ErrorKind.ConnectError, attempt.ErrorKind);
                Assert.False(attempt.ReachedServer);
            }
        );
        var result = Assert.Single(recorder.Results);
        Assert.Same(exception, result.Error);
        Assert.Equal(ErrorKind.ConnectError, result.ErrorKind);
        Assert.Null(result.Response);
        Assert.Equal(1, result.Retries);
    }

    [Fact]
    public async Task ObservesTimeout()
    {
        var recorder = new Recorder();
        var client = this.ObservedClient(
            recorder,
            new StubHandler(
                async (_, cancellationToken) =>
                {
                    await Task.Delay(Timeout.Infinite, cancellationToken);
                    throw new InvalidOperationException("unreachable");
                }
            ),
            timeout: TimeSpan.FromMilliseconds(50)
        );

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => Send(client));

        recorder.AssertEvents("start", "before 0", "after 0", "finish");
        var attempt = Assert.Single(recorder.Attempts);
        Assert.Equal(ErrorKind.Timeout, attempt.ErrorKind);
        Assert.Null(attempt.ReachedServer);
        Assert.Equal(ErrorKind.Timeout, Assert.Single(recorder.Results).ErrorKind);
    }

    [Fact]
    public async Task ObservesCancellation()
    {
        var recorder = new Recorder();
        using var cts = new CancellationTokenSource();
        var client = this.ObservedClient(
            recorder,
            new StubHandler(
                async (_, cancellationToken) =>
                {
                    cts.Cancel();
                    await Task.Delay(Timeout.Infinite, cancellationToken);
                    throw new InvalidOperationException("unreachable");
                }
            )
        );

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() => SendWith(client, cts.Token));

        Assert.Equal(ErrorKind.Canceled, Assert.Single(recorder.Attempts).ErrorKind);
        Assert.Equal(ErrorKind.Canceled, Assert.Single(recorder.Results).ErrorKind);
    }

    [Fact]
    public async Task ClassifiesDeserializeFailures()
    {
        var recorder = new Recorder();
        var client = this.ObservedClient(
            recorder,
            new StubHandler(
                (_, _) =>
                    Task.FromResult(
                        new HttpResponseMessage(HttpStatusCode.OK)
                        {
                            Content = new StringContent(
                                "not json",
                                Encoding.UTF8,
                                "application/json"
                            ),
                        }
                    )
            )
        );

        var response = await Send(client);
        await Assert.ThrowsAnyAsync<Exception>(() => Deserialize(response));

        Assert.Equal(ErrorKind.Other, Assert.Single(recorder.Results).ErrorKind);
    }

    [Fact]
    public async Task FinishesOnce()
    {
        var recorder = new Recorder();
        var client = this.ObservedClient(
            recorder,
            new StubHandler((_, _) => Task.FromResult(JsonResponse(HttpStatusCode.OK)))
        );

        var response = await Send(client);
        await Deserialize(response);
        try
        {
            // The body was already consumed, so this may fail.
            await Deserialize(response);
        }
        catch (Exception) { }

        Assert.Single(recorder.Results);
    }

    [Fact]
    public async Task IgnoresObserverExceptions()
    {
        await this.AssertSucceeds(new ThrowingObserver());
        await this.AssertSucceeds(new Recorder(throwOnStart: true));
    }

    async Task AssertSucceeds(RequestObserver observer)
    {
        var client = this.ObservedClient(
            observer,
            new StubHandler((_, _) => Task.FromResult(JsonResponse(HttpStatusCode.OK)))
        );

        var parsed = await Deserialize(await Send(client));

        Assert.Equal("value", parsed["key"]);
    }

    [Fact]
    public async Task DoesNothingWithoutObserver()
    {
        var client = this.client.WithOptions(options =>
            options with
            {
                HttpClient = new(
                    new StubHandler((_, _) => Task.FromResult(JsonResponse(HttpStatusCode.OK)))
                ),
                MaxRetries = 0,
            }
        );
        Assert.Null((await Send(client)).Observation);

        var recorder = new Recorder(observe: false);
        var unobserved = await Send(
            this.ObservedClient(
                recorder,
                new StubHandler((_, _) => Task.FromResult(JsonResponse(HttpStatusCode.OK)))
            )
        );
        await Deserialize(unobserved);
        Assert.Null(unobserved.Observation);
        recorder.AssertEvents("start");
    }

    [Fact]
    public void DefaultHttpClientIsPlainWithoutObserver()
    {
        var options = OptionsOf(this.client);
        Assert.Null(options.DefaultHttpClientObserved);

        var httpClient = options.HttpClient;

        Assert.False(options.DefaultHttpClientObserved);
        // Copies share the default client, even ones observed later.
        var copy = OptionsOf(
            this.client.WithOptions(o => o with { RequestObserver = new Recorder() })
        );
        Assert.Same(httpClient, copy.HttpClient);
        Assert.False(copy.DefaultHttpClientObserved);
    }

    [Fact]
    public void DefaultHttpClientObservesWithObserver()
    {
        var options = OptionsOf(
            this.client.WithOptions(o => o with { RequestObserver = new Recorder() })
        );

        var httpClient = options.HttpClient;

        Assert.True(options.DefaultHttpClientObserved);
        Assert.Same(httpClient, OptionsOf(this.client).HttpClient);
    }

    [Fact]
    public void CustomHttpClientIsKept()
    {
        using var custom = new HttpClient();
        var options = OptionsOf(
            this.client.WithOptions(o =>
                o with
                {
                    HttpClient = custom,
                    RequestObserver = new Recorder(),
                }
            )
        );

        Assert.Same(custom, options.HttpClient);
        Assert.Null(options.DefaultHttpClientObserved);
    }

    [Fact]
    public void DefaultOptionsHaveHttpClient()
    {
        Assert.NotNull(default(ClientOptions).HttpClient);
    }

#if NET
    [Fact]
    public async Task ObservesConnections()
    {
        using var server = new LocalServer();
        var recorder = new Recorder();
        // The default HTTP client, whose connections are observed.
        var client = this.client.WithOptions(options =>
            options with
            {
                MaxRetries = 0,
                RequestObserver = recorder,
            }
        );

        for (var i = 0; i < 2; i++)
        {
            await Deserialize(
                await client.WithRawResponse.Execute(
                    new HttpRequest<LocalParams>
                    {
                        Method = HttpMethod.Post,
                        Params = new(new Uri(server.Url, "/something")),
                    },
                    TestContext.Current.CancellationToken
                )
            );
        }

        var first = recorder.Results[0].Timings;
        Assert.False(first.Reused);
        AssertOrdered(
            first.RequestReady,
            first.ConnectStart,
            first.ConnectEnd,
            first.SendStart,
            first.SendEnd,
            first.ResponseHeaders,
            first.BodyEnd
        );

        var second = recorder.Results[1].Timings;
        Assert.True(second.Reused);
        Assert.Null(second.ConnectStart);
        Assert.Null(second.ConnectEnd);
        AssertOrdered(
            second.RequestReady,
            second.SendStart,
            second.SendEnd,
            second.ResponseHeaders
        );

        Assert.True(OptionsOf(client).DefaultHttpClientObserved);
    }
#endif

    static ClientOptions OptionsOf(ITurbopufferClient client)
    {
        ClientOptions options = default;
        client.WithOptions(o =>
        {
            options = o;
            return o;
        });
        return options;
    }

    ITurbopufferClient ObservedClient(
        RequestObserver observer,
        HttpMessageHandler handler,
        int maxRetries = 0,
        TimeSpan? timeout = null
    ) =>
        this.client.WithOptions(options =>
            options with
            {
                HttpClient = new(handler),
                MaxRetries = maxRetries,
                Timeout = timeout,
                RequestObserver = observer,
            }
        );

    static Task<HttpResponse> Send(ITurbopufferClient client) =>
        SendWith(client, TestContext.Current.CancellationToken);

    static Task<HttpResponse> SendWith(
        ITurbopufferClient client,
        CancellationToken cancellationToken
    ) =>
        client.WithRawResponse.Execute(
            new HttpRequest<BlankParams> { Method = HttpMethod.Post, Params = new() },
            cancellationToken
        );

    static Task<Dictionary<string, string>> Deserialize(HttpResponse response) =>
        new HttpResponse<Dictionary<string, string>>(
            response,
            token => response.Deserialize<Dictionary<string, string>>(token)
        ).Deserialize(TestContext.Current.CancellationToken);

    static HttpResponseMessage JsonResponse(HttpStatusCode status) =>
        new(status)
        {
            Content = new StringContent("{\"key\":\"value\"}", Encoding.UTF8, "application/json"),
        };

    static void AssertOrdered(params long?[] marks)
    {
        for (var i = 0; i < marks.Length; i++)
        {
            Assert.True(marks[i] != null, $"expected mark {i} to be set");
            Assert.True(i == 0 || marks[i] >= marks[i - 1], $"expected mark {i} to be in order");
        }
    }

    record class BlankParams : ParamsBase
    {
        internal override void AddHeadersToRequest(
            HttpRequestMessage _request,
            ClientOptions _options
        ) { }

        public override Uri Url(ClientOptions _options) => new("http://localhost/something");

        internal override HttpContent? BodyContent() =>
            new StringContent("{}", Encoding.UTF8, "application/json");
    }

    record class LocalParams : ParamsBase
    {
        readonly Uri _address;

        public LocalParams(Uri address)
        {
            _address = address;
        }

        internal override void AddHeadersToRequest(
            HttpRequestMessage _request,
            ClientOptions _options
        ) { }

        public override Uri Url(ClientOptions _options) => _address;

        internal override HttpContent? BodyContent() =>
            new StringContent("{}", Encoding.UTF8, "application/json");
    }

    sealed class StubHandler : HttpMessageHandler
    {
        readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _send;

        public StubHandler(
            Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> send
        )
        {
            _send = send;
        }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken
        ) => _send(request, cancellationToken);
    }

    sealed class Recorder : RequestObserver
    {
        readonly bool _observe;
        readonly bool _throwOnStart;

        public Recorder(bool observe = true, bool throwOnStart = false)
        {
            _observe = observe;
            _throwOnStart = throwOnStart;
        }

        public List<string> Events { get; } = new();
        public List<RequestInfo> Infos { get; } = new();
        public List<AttemptResult> Attempts { get; } = new();
        public List<RequestResult> Results { get; } = new();

        public void AssertEvents(params string[] expected) => Assert.Equal(expected, this.Events);

        public override RequestScope? Start(RequestInfo info)
        {
            if (_throwOnStart)
            {
                throw new InvalidOperationException("observer failure");
            }
            this.Events.Add("start");
            this.Infos.Add(info);
            return _observe ? new Scope(this) : null;
        }

        sealed class Scope : RequestScope
        {
            readonly Recorder _recorder;

            public Scope(Recorder recorder)
            {
                _recorder = recorder;
            }

            public override void AttemptBegin(int attempt, HttpRequestMessage request)
            {
                _recorder.Events.Add($"before {attempt}");
                request.Headers.Add("X-Observed-Attempt", attempt.ToString());
            }

            public override void AttemptEnd(AttemptResult result)
            {
                _recorder.Events.Add($"after {result.Attempt}");
                _recorder.Attempts.Add(result);
            }

            public override void Finish(RequestResult result)
            {
                _recorder.Events.Add("finish");
                _recorder.Results.Add(result);
            }
        }
    }

    sealed class ThrowingObserver : RequestObserver
    {
        public override RequestScope? Start(RequestInfo info) => new Scope();

        sealed class Scope : RequestScope
        {
            public override void AttemptBegin(int attempt, HttpRequestMessage request) =>
                throw new InvalidOperationException("observer failure");

            public override void AttemptEnd(AttemptResult result) =>
                throw new InvalidOperationException("observer failure");

            public override void Finish(RequestResult result) =>
                throw new InvalidOperationException("observer failure");
        }
    }
}

/// <summary>
/// A minimal HTTP/1.1 server on a local port that answers every request with the same JSON
/// body and keeps connections alive.
/// </summary>
sealed class LocalServer : IDisposable
{
    readonly TcpListener _listener = new(IPAddress.Loopback, 0);

    public LocalServer(string body = "{\"key\":\"value\"}")
    {
        _listener.Start();
        var port = ((IPEndPoint)_listener.LocalEndpoint).Port;
        this.Url = new($"http://127.0.0.1:{port}");
        var response = Encoding.ASCII.GetBytes(
            "HTTP/1.1 200 OK\r\nContent-Type: application/json\r\n"
                + $"Content-Length: {Encoding.UTF8.GetByteCount(body)}\r\n\r\n{body}"
        );
        _ = Task.Run(() => this.Accept(response));
    }

    /// <summary>
    /// The server's base URL.
    /// </summary>
    public Uri Url { get; }

    async Task Accept(byte[] response)
    {
        while (true)
        {
            TcpClient connection;
            try
            {
                connection = await _listener.AcceptTcpClientAsync();
            }
            catch (Exception)
            {
                // The server stopped.
                return;
            }
            _ = Task.Run(() => Serve(connection, response));
        }
    }

    static void Serve(TcpClient connection, byte[] response)
    {
        using var _ = connection;
        using var stream = connection.GetStream();
        var buffer = new byte[65536];
        var received = new StringBuilder();
        try
        {
            while (true)
            {
                var read = stream.Read(buffer, 0, buffer.Length);
                if (read == 0)
                {
                    return;
                }
                received.Append(Encoding.ASCII.GetString(buffer, 0, read));
                // Answers once a request's headers and body have arrived.
                while (TryTakeRequest(received))
                {
                    stream.Write(response, 0, response.Length);
                }
            }
        }
        catch (IOException) { }
    }

    static bool TryTakeRequest(StringBuilder received)
    {
        var text = received.ToString();
        var headersEnd = text.IndexOf("\r\n\r\n", StringComparison.Ordinal);
        if (headersEnd < 0)
        {
            return false;
        }
        var contentLength = 0;
        foreach (var line in text.Substring(0, headersEnd).Split('\n'))
        {
            if (line.StartsWith("Content-Length:", StringComparison.OrdinalIgnoreCase))
            {
                contentLength = int.Parse(line.Substring("Content-Length:".Length).Trim());
            }
        }
        var length = headersEnd + 4 + contentLength;
        if (text.Length < length)
        {
            return false;
        }
        received.Remove(0, length);
        return true;
    }

    public void Dispose() => _listener.Stop();
}
