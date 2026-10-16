using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Turbopuffer.Core;
using Turbopuffer.Exceptions;
using Turbopuffer.Models;
using Turbopuffer.Models.Namespaces;
using Turbopuffer.Tests.Core;

namespace Turbopuffer.Tests;

// Shares the respond-async poll interval with the other tests that change it.
[Collection(nameof(RespondAsync))]
public class TelemetryTest : IDisposable
{
    const string QueryPath = "/v2/namespaces/my-ns/query";
    const string QueryRoute = "POST /v2/namespaces/{ns}/query";

    const string QueryBody = """
        {"rows":[],"performance":{"approx_namespace_size":1,"cache_hit_ratio":1.0,"cache_temperature":"hot","exhaustive_search_count":0,"query_execution_ms":5,"server_total_ms":7}}
        """;

    readonly TimeSpan _savedPollInterval;

    public TelemetryTest()
    {
        _savedPollInterval = RespondAsync.PollInterval;
        RespondAsync.PollInterval = TimeSpan.Zero;
    }

    public void Dispose()
    {
        RespondAsync.PollInterval = _savedPollInterval;
        GC.SuppressFinalize(this);
    }

    [Theory]
    [InlineData("1", true)]
    [InlineData("true", true)]
    [InlineData(" ON ", true)]
    [InlineData("yes", true)]
    [InlineData("0", false)]
    [InlineData("false", false)]
    [InlineData("", false)]
    [InlineData(null, false)]
    [InlineData("sometimes", false)]
    public void ParsesEnvironmentValue(string? value, bool expected)
    {
        Assert.Equal(expected, ClientTelemetry.ParseEnabled(value));
    }

    [Fact]
    public void ExplicitValueBeatsEnvironment()
    {
        Assert.True(ClientTelemetry.ResolveEnabled(true));
        Assert.False(ClientTelemetry.ResolveEnabled(false));
    }

    [Theory]
    [InlineData("post", "/v2/namespaces/my-ns/query", QueryRoute)]
    [InlineData(
        "post",
        "/v2/namespaces/my-ns/query?stainless_overload=multiQuery",
        "POST /v2/namespaces/{ns}/query?stainless_overload=multiQuery"
    )]
    [InlineData("post", "/v2/namespaces/my%2Fns", "POST /v2/namespaces/{ns}")]
    [InlineData("get", "/v1/namespaces", "GET /v1/namespaces")]
    [InlineData(
        "get",
        "/v1/namespaces/my-ns/operations/abc123?stainless_overload=pollCopyFrom",
        "GET /v1/namespaces/{ns}/operations/{token}?stainless_overload=pollCopyFrom"
    )]
    [InlineData(
        "get",
        "https://gcp-us-central1.turbopuffer.com/v1/namespaces/my-ns/metadata",
        "GET /v1/namespaces/{ns}/metadata"
    )]
    public void TemplatesRoutes(string method, string url, string expected)
    {
        Assert.Equal(expected, ClientTelemetry.RouteTemplate(method, url));
    }

    [Fact]
    public void RecordsIntegerMicroseconds()
    {
        var buffer = new TelemetryBuffer();
        buffer.Record(
            "post",
            QueryPath,
            200,
            2,
            [("total", 1_234_567), ("compress", 0), ("deserialize", 999)],
            serverTotalMs: 7
        );

        var record = Assert.Single(Records(Take(buffer)));
        Assert.Equal(1234, record.GetProperty("total_us").GetInt64());
        Assert.Equal(0, record.GetProperty("deserialize_us").GetInt64());
        Assert.Equal(7000, record.GetProperty("server_total_us").GetInt64());
        Assert.Equal(2, record.GetProperty("retries").GetInt32());
        // A zero compress duration means the body wasn't compressed.
        Assert.False(record.TryGetProperty("compress_us", out _));
    }

    [Fact]
    public void EncodesCompactHeader()
    {
        var buffer = new TelemetryBuffer();
        buffer.Record("get", "/v1/namespaces", 200, 0, [("total", 5_000_000)], reused: true);

        var header = buffer.Take()!.Header;

        Assert.Matches(
            "^\\{\"version\":1,\"records\":\\[\\{\"route\":\"GET /v1/namespaces\",\"status\":200,\"total_us\":5000,\"reused\":true,\"age_ms\":\\d+\\}\\]\\}$",
            header
        );
    }

    [Fact]
    public void RecordsAreTakenWhenTheFirstAttemptBegins()
    {
        var buffer = new TelemetryBuffer();
        AddRecord(buffer, 200);
        var scope = new TelemetryObserver(buffer).Start(
            new RequestInfo(HttpMethod.Post, QueryPath)
        )!;

        // A request that fails before it's sent never takes them.
        var held = buffer.Take();
        Assert.NotNull(held);
        buffer.Restore(held);

        using var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost" + QueryPath);
        scope.AttemptBegin(0, request);
        Assert.True(request.Headers.Contains(ClientTelemetry.Header));
        Assert.Null(buffer.Take());
    }

    [Fact]
    public void FullBufferDropsOldestRecords()
    {
        var buffer = new TelemetryBuffer();
        for (var status = 0; status < ClientTelemetry.MaxBufferedRecords + 5; status++)
        {
            AddRecord(buffer, status);
        }

        var first = Take(buffer);
        Assert.Equal(5, first.GetProperty("dropped").GetInt32());
        var records = Records(first);
        Assert.Equal(ClientTelemetry.MaxRecordsPerHeader, records.Count);
        Assert.Equal(5, records[0].GetProperty("status").GetInt32());

        var second = Take(buffer);
        Assert.False(second.TryGetProperty("dropped", out _));
        Assert.Equal(
            5 + ClientTelemetry.MaxRecordsPerHeader,
            Records(second)[0].GetProperty("status").GetInt32()
        );
    }

    [Fact]
    public void RestorePutsRecordsBackInOrder()
    {
        var buffer = new TelemetryBuffer();
        for (var status = 0; status < 3; status++)
        {
            AddRecord(buffer, status);
        }
        var batch = buffer.Take()!;

        AddRecord(buffer, 99);
        buffer.Restore(batch);

        Assert.Equal(
            [0, 1, 2, 99],
            Records(Take(buffer)).Select(record => record.GetProperty("status").GetInt32())
        );
    }

    [Fact]
    public void RestoreDropsWhenFull()
    {
        var buffer = new TelemetryBuffer();
        AddRecord(buffer, 0);
        var batch = buffer.Take()!;
        for (var status = 1; status <= ClientTelemetry.MaxBufferedRecords; status++)
        {
            AddRecord(buffer, status);
        }

        buffer.Restore(batch);

        Assert.Equal(1, Take(buffer).GetProperty("dropped").GetInt32());
    }

    [Theory]
    [InlineData("abc")]
    [InlineData("1.5")]
    [InlineData("-0.1")]
    [InlineData("NaN")]
    [InlineData("")]
    public void IgnoresInvalidSampleRate(string value)
    {
        var buffer = new TelemetryBuffer();
        buffer.ObserveResponse(WithSampleRate(new(HttpStatusCode.OK), "0.5"));
        buffer.ObserveResponse(WithSampleRate(new(HttpStatusCode.OK), value));
        Assert.Equal(0.5, buffer.SampleRate);
    }

    [Fact]
    public void SamplesRecords()
    {
        var buffer = new TelemetryBuffer();
        buffer.ObserveResponse(WithSampleRate(new(HttpStatusCode.OK), "0.25"));

        buffer.Random = () => 0.3;
        AddRecord(buffer, 200);
        Assert.Null(buffer.Take());

        buffer.Random = () => 0.2;
        AddRecord(buffer, 200);
        Assert.Equal(
            0.25,
            Assert.Single(Records(Take(buffer))).GetProperty("sample_rate").GetDouble()
        );

        buffer.ObserveResponse(WithSampleRate(new(HttpStatusCode.OK), "0"));
        buffer.Random = () => 0;
        AddRecord(buffer, 200);
        Assert.Null(buffer.Take());
    }

    [Fact]
    public void ComputesDurations()
    {
        var newConnection = new Timings
        {
            AttemptStart = 0,
            RequestReady = 50,
            ConnectStart = 150,
            ConnectEnd = 400,
            SendStart = 410,
            SendEnd = 500,
            ResponseHeaders = 900,
        };
        newConnection.Extra["compress_start"] = 60;
        newConnection.Extra["compress_end"] = 100;
        var durations = ClientTelemetry
            .ComputeDurations(newConnection, 1000)
            .ToDictionary(d => d.Name, d => d.Nanoseconds);
        Assert.Equal(40, durations["compress"]);
        Assert.Equal(50, durations["pool"]);
        Assert.Equal(250, durations["connect"]);
        Assert.Equal(90, durations["send"]);
        Assert.Equal(400, durations["wait"]);

        var reusedConnection = new Timings
        {
            AttemptStart = 0,
            RequestReady = 100,
            SendStart = 130,
            SendEnd = 160,
            ResponseHeaders = 300,
        };
        durations = ClientTelemetry
            .ComputeDurations(reusedConnection, 400)
            .ToDictionary(d => d.Name, d => d.Nanoseconds);
        Assert.False(durations.ContainsKey("connect"));
        Assert.Equal(30, durations["pool"]);

        // Some clients finish sending the body after the response headers arrive.
        reusedConnection.SendEnd = 350;
        Assert.Contains(("wait", 0L), ClientTelemetry.ComputeDurations(reusedConnection, 400));

        // Untraced requests, such as through a custom HTTP client.
        Assert.Equal(
            [("total", 400L)],
            ClientTelemetry.ComputeDurations(new Timings { AttemptStart = 0 }, 400)
        );
    }

    [Fact]
    public void CopiesShareBuffer()
    {
        var apiKey = UniqueApiKey();
        var client = TelemetryClient(new StubHandler(_ => QueryResponse()), apiKey);
        var buffer = BufferOf(client);
        Assert.NotNull(buffer);

        Assert.Same(
            buffer,
            BufferOf(client.WithOptions(o => o with { Timeout = TimeSpan.FromSeconds(5) }))
        );
        Assert.Same(
            buffer,
            BufferOf(TelemetryClient(new StubHandler(_ => QueryResponse()), apiKey))
        );

        var otherKey = BufferOf(client.WithOptions(o => o with { ApiKey = UniqueApiKey() }));
        Assert.NotNull(otherKey);
        Assert.NotSame(buffer, otherKey);
        var otherUrl = BufferOf(
            client.WithOptions(o => o with { BaseUrl = "http://127.0.0.1:4011" })
        );
        Assert.NotNull(otherUrl);
        Assert.NotSame(buffer, otherUrl);
        Assert.Null(BufferOf(client.WithOptions(o => o with { Telemetry = false })));
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void DefaultHttpClientObservesOnlyWithTelemetry(bool telemetry)
    {
        var client = new TurbopufferClient()
        {
            BaseUrl = "http://localhost",
            ApiKey = UniqueApiKey(),
            Telemetry = telemetry,
        };

        var options = RawOptionsOf(client);
        _ = options.HttpClient;

        Assert.Equal(telemetry, options.DefaultHttpClientObserved);
        // Copies, such as those polling for accepted requests, share it.
        Assert.Same(options.HttpClient, RawOptionsOf(client.WithOptions(o => o)).HttpClient);
    }

    [Fact]
    public async Task RecordsRideOnNextRequest()
    {
        var handler = new StubHandler(_ => QueryResponse());
        var ns = TelemetryClient(handler).Namespace("my-ns");

        await ns.Query(new() { TopK = 1 }, TestContext.Current.CancellationToken);
        Assert.Null(SentRecords(handler.Requests[0]));

        await ns.Query(new() { TopK = 1 }, TestContext.Current.CancellationToken);
        var record = Assert.Single(SentRecords(handler.Requests[1])!);
        Assert.Equal(QueryRoute, record.GetProperty("route").GetString());
        Assert.Equal(200, record.GetProperty("status").GetInt32());
        Assert.Equal(7000, record.GetProperty("server_total_us").GetInt64());
        // Mocked handlers never open connections, so network phases are covered by a real server.
        foreach (var key in new[] { "total_us", "serialize_us", "deserialize_us" })
        {
            Assert.True(record.GetProperty(key).GetInt64() >= 0, key);
        }
        Assert.True(
            record.GetProperty("total_us").GetInt64()
                >= record.GetProperty("serialize_us").GetInt64()
                    + record.GetProperty("deserialize_us").GetInt64()
        );
        Assert.True(record.GetProperty("age_ms").GetInt64() >= 0);
        foreach (var key in new[] { "compress_us", "retries", "error", "sample_rate", "reused" })
        {
            Assert.False(record.TryGetProperty(key, out _), key);
        }
    }

    [Fact]
    public async Task DisabledClientSendsNothing()
    {
        var handler = new StubHandler(_ => QueryResponse());
        var ns = TelemetryClient(handler, telemetry: false).Namespace("my-ns");

        await ns.Query(new() { TopK = 1 }, TestContext.Current.CancellationToken);
        await ns.Query(new() { TopK = 1 }, TestContext.Current.CancellationToken);

        Assert.All(handler.Requests, request => Assert.Null(SentRecords(request)));
    }

    [Fact]
    public async Task CompressionIsRecorded()
    {
        var handler = new StubHandler(_ => QueryResponse());
        var ns = TelemetryClient(handler)
            .WithOptions(o => o with { Compression = true })
            .Namespace("my-ns");

        await ns.Query(new() { TopK = 1 }, TestContext.Current.CancellationToken);
        await ns.Query(new() { TopK = 1 }, TestContext.Current.CancellationToken);

        Assert.True(
            Assert.Single(SentRecords(handler.Requests[1])!).TryGetProperty("compress_us", out _)
        );
    }

    [Fact]
    public async Task ServerSampleRateIsApplied()
    {
        var handler = new StubHandler(_ => WithSampleRate(QueryResponse(), "0.25"));
        var client = TelemetryClient(handler);
        var buffer = BufferOf(client)!;
        var ns = client.Namespace("my-ns");

        buffer.Random = () => 0.2;
        await ns.Query(new() { TopK = 1 }, TestContext.Current.CancellationToken);
        buffer.Random = () => 0.3;
        await ns.Query(new() { TopK = 1 }, TestContext.Current.CancellationToken);
        await ns.Query(new() { TopK = 1 }, TestContext.Current.CancellationToken);

        Assert.Equal(
            0.25,
            Assert.Single(SentRecords(handler.Requests[1])!).GetProperty("sample_rate").GetDouble()
        );
        Assert.Null(SentRecords(handler.Requests[2]));
    }

    [Fact]
    public async Task ServerSampleRateOfZeroStopsRecording()
    {
        var handler = new StubHandler(_ => WithSampleRate(QueryResponse(), "0"));
        var client = TelemetryClient(handler);
        var ns = client.Namespace("my-ns");

        for (var i = 0; i < 3; i++)
        {
            await ns.Query(new() { TopK = 1 }, TestContext.Current.CancellationToken);
        }

        Assert.All(handler.Requests, request => Assert.Null(SentRecords(request)));
        Assert.Equal(0.0, BufferOf(client)!.SampleRate);
    }

    [Fact]
    public async Task BatchIsResentAfterConnectError()
    {
        var handler = new StubHandler(
            _ => QueryResponse(),
            _ => throw ConnectError(),
            _ => QueryResponse()
        );
        var ns = TelemetryClient(handler).Namespace("my-ns");

        await ns.Query(new() { TopK = 1 }, TestContext.Current.CancellationToken);
        await ns.Query(new() { TopK = 1 }, TestContext.Current.CancellationToken);

        var firstAttempt = SentHeader(handler.Requests[1]);
        Assert.NotNull(firstAttempt);
        Assert.Equal(firstAttempt, SentHeader(handler.Requests[2]));
    }

    [Fact]
    public async Task BatchIsNotResentAfterServerError()
    {
        var handler = new StubHandler(
            _ => QueryResponse(),
            _ => RetryableError(),
            _ => QueryResponse(),
            _ => QueryResponse()
        );
        var ns = TelemetryClient(handler).Namespace("my-ns");

        for (var i = 0; i < 3; i++)
        {
            await ns.Query(new() { TopK = 1 }, TestContext.Current.CancellationToken);
        }

        Assert.NotNull(SentRecords(handler.Requests[1]));
        Assert.Null(SentRecords(handler.Requests[2]));
        // The retried query reports how many retries it took.
        Assert.Equal(1, SentRecords(handler.Requests[3])![0].GetProperty("retries").GetInt32());
    }

    [Fact]
    public async Task CanceledRequestsAreRecorded()
    {
        using var cts = new CancellationTokenSource();
        var handler = new StubHandler(
            async (_, cancellationToken) =>
            {
                cts.Cancel();
                await Task.Delay(Timeout.Infinite, cancellationToken);
                throw new InvalidOperationException("unreachable");
            },
            (_, _) => Task.FromResult(QueryResponse())
        );
        var ns = TelemetryClient(handler).Namespace("my-ns");

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
            ns.Query(new() { TopK = 1 }, cts.Token)
        );
        await ns.Query(new() { TopK = 1 }, TestContext.Current.CancellationToken);

        var record = Assert.Single(SentRecords(handler.Requests[1])!);
        Assert.Equal(0, record.GetProperty("status").GetInt32());
        Assert.Equal("canceled", record.GetProperty("error").GetString());
    }

    [Fact]
    public async Task FailuresAreRecorded()
    {
        var handler = new StubHandler(
            async (_, cancellationToken) =>
            {
                await Task.Delay(Timeout.Infinite, cancellationToken);
                throw new InvalidOperationException("unreachable");
            },
            (_, _) => throw ConnectError(),
            (_, _) =>
                Task.FromResult(
                    new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent("{\"error\":\"bad request\"}"),
                    }
                ),
            (_, _) => Task.FromResult(QueryResponse())
        );
        var ns = TelemetryClient(handler)
            .WithOptions(o => o with { MaxRetries = 0, Timeout = TimeSpan.FromMilliseconds(50) })
            .Namespace("my-ns");

        await Assert.ThrowsAnyAsync<OperationCanceledException>(() =>
            ns.Query(new() { TopK = 1 }, TestContext.Current.CancellationToken)
        );
        await Assert.ThrowsAsync<TurbopufferIOException>(() =>
            ns.Query(new() { TopK = 1 }, TestContext.Current.CancellationToken)
        );
        await Assert.ThrowsAsync<TurbopufferBadRequestException>(() =>
            ns.Query(new() { TopK = 1 }, TestContext.Current.CancellationToken)
        );
        await ns.Query(new() { TopK = 1 }, TestContext.Current.CancellationToken);

        Assert.Null(SentRecords(handler.Requests[0]));
        Assert.Equal([(0, "timeout")], StatusesAndErrors(handler.Requests[1]));
        // The connect error never reached the server, so its batch is sent again with the new record.
        Assert.Equal([(0, "timeout"), (0, "connect")], StatusesAndErrors(handler.Requests[2]));
        Assert.Equal([(400, null)], StatusesAndErrors(handler.Requests[3]));
    }

    [Fact]
    public async Task QueryParamsAreNotRecorded()
    {
        var handler = new StubHandler(_ => JsonResponse(HttpStatusCode.OK, "{\"namespaces\":[]}"));
        var client = TelemetryClient(handler);

        await client.Namespaces(
            new() { Prefix = "secret-prefix" },
            TestContext.Current.CancellationToken
        );

        var record = Assert.Single(Records(Take(BufferOf(client)!)));
        Assert.Equal("GET /v1/namespaces", record.GetProperty("route").GetString());
    }

    [Fact]
    public async Task OverloadedRoutesKeepTheirQuery()
    {
        var handler = new StubHandler(_ => JsonResponse(HttpStatusCode.OK, "{\"results\":[]}"));
        var client = TelemetryClient(handler);

        await client
            .Namespace("my-ns")
            .MultiQuery(new() { Queries = [] }, TestContext.Current.CancellationToken);

        var record = Assert.Single(Records(Take(BufferOf(client)!)));
        Assert.Equal(
            "POST /v2/namespaces/{ns}/query?stainless_overload=multiQuery",
            record.GetProperty("route").GetString()
        );
    }

    [Fact]
    public async Task ParsingTwiceRecordsOnce()
    {
        var handler = new StubHandler(_ => QueryResponse());
        var client = TelemetryClient(handler);

        var response = await client
            .Namespace("my-ns")
            .WithRawResponse.Query(new() { TopK = 1 }, TestContext.Current.CancellationToken);
        await response.Deserialize(TestContext.Current.CancellationToken);
        try
        {
            await response.Deserialize(TestContext.Current.CancellationToken);
        }
        catch (Exception) { }

        Assert.Single(Records(Take(BufferOf(client)!)));
    }

    [Fact]
    public async Task RespondAsyncPollsAndRequestAreRecorded()
    {
        var handler = new StubHandler(
            _ => Accepted(),
            _ => JsonResponse(HttpStatusCode.OK, "{\"status\":\"running\"}"),
            _ =>
                JsonResponse(
                    HttpStatusCode.OK,
                    "{\"status\":\"finished\",\"result\":{\"success\":{\"status\":\"OK\"}}}"
                )
        );
        var client = TelemetryClient(handler);

        await client.Namespace("my-ns").DeleteAll(new(), TestContext.Current.CancellationToken);

        var records = AllRecords(handler, client);
        var polls = records.Take(records.Count - 1).ToList();
        var request = records.Last();
        Assert.Equal(
            [
                ("GET /v1/namespaces/{ns}/operations/{token}", 200),
                ("GET /v1/namespaces/{ns}/operations/{token}", 200),
            ],
            polls.Select(r =>
                (r.GetProperty("route").GetString(), r.GetProperty("status").GetInt32())
            )
        );
        // The request reports the final outcome, and its total spans the polling.
        Assert.Equal("DELETE /v2/namespaces/{ns}", request.GetProperty("route").GetString());
        Assert.Equal(200, request.GetProperty("status").GetInt32());
        Assert.False(request.TryGetProperty("error", out _));
        Assert.True(
            request.GetProperty("total_us").GetInt64()
                >= polls.Sum(r => r.GetProperty("total_us").GetInt64())
        );
        Assert.True(request.TryGetProperty("body_read_us", out _));
        Assert.True(request.TryGetProperty("deserialize_us", out _));
    }

    [Fact]
    public async Task RespondAsyncFailedOperationIsRecorded()
    {
        var handler = new StubHandler(
            _ => Accepted(),
            _ =>
                JsonResponse(
                    HttpStatusCode.OK,
                    "{\"status\":\"finished\",\"result\":{\"error\":{\"status_code\":404,\"detail\":{\"error\":\"gone\"}}}}"
                )
        );
        var client = TelemetryClient(handler);

        await Assert.ThrowsAsync<TurbopufferNotFoundException>(() =>
            client.Namespace("my-ns").DeleteAll(new(), TestContext.Current.CancellationToken)
        );

        var request = AllRecords(handler, client).Last();
        Assert.Equal("DELETE /v2/namespaces/{ns}", request.GetProperty("route").GetString());
        Assert.Equal(404, request.GetProperty("status").GetInt32());
        Assert.False(request.TryGetProperty("error", out _));
    }

    [Fact]
    public async Task RespondAsyncPollingTimeoutIsRecorded()
    {
        RespondAsync.PollInterval = TimeSpan.FromMilliseconds(10);
        var handler = new StubHandler(
            _ => Accepted(),
            _ => JsonResponse(HttpStatusCode.OK, "{\"status\":\"running\"}")
        );
        var client = TelemetryClient(handler);

        await Assert.ThrowsAsync<TurbopufferIOException>(() =>
            client
                .WithOptions(o => o with { Timeout = TimeSpan.FromMilliseconds(50) })
                .Namespace("my-ns")
                .DeleteAll(new(), TestContext.Current.CancellationToken)
        );

        var request = AllRecords(handler, client).Last();
        Assert.Equal("DELETE /v2/namespaces/{ns}", request.GetProperty("route").GetString());
        Assert.Equal(202, request.GetProperty("status").GetInt32());
        Assert.Equal("timeout", request.GetProperty("error").GetString());
    }

#if NET
    [Fact]
    public async Task RecordsConnectionPhases()
    {
        using var server = new LocalServer(QueryBody);
        var client = new TurbopufferClient()
        {
            BaseUrl = server.Url.ToString(),
            ApiKey = UniqueApiKey(),
            Telemetry = true,
        };
        var buffer = BufferOf(client)!;
        var ns = client.Namespace("my-ns");

        await ns.Query(new() { TopK = 1 }, TestContext.Current.CancellationToken);
        var first = Assert.Single(Records(Take(buffer)));
        await ns.Query(new() { TopK = 1 }, TestContext.Current.CancellationToken);
        var second = Assert.Single(Records(Take(buffer)));

        Assert.False(first.GetProperty("reused").GetBoolean());
        Assert.True(first.TryGetProperty("connect_us", out _));
        Assert.True(second.GetProperty("reused").GetBoolean());
        Assert.False(second.TryGetProperty("connect_us", out _));
        foreach (var record in new[] { first, second })
        {
            foreach (var key in new[] { "pool_us", "send_us", "wait_us", "body_read_us" })
            {
                Assert.True(record.GetProperty(key).GetInt64() >= 0, key);
            }
            Assert.True(
                record.GetProperty("wait_us").GetInt64()
                    + record.GetProperty("body_read_us").GetInt64()
                    <= record.GetProperty("total_us").GetInt64()
            );
        }
    }
#endif

    static TurbopufferClient TelemetryClient(
        StubHandler handler,
        string? apiKey = null,
        bool telemetry = true
    ) =>
        new TurbopufferClient()
        {
            HttpClient = new(handler),
            BaseUrl = "http://localhost",
            // Clients with the same API key share telemetry, so tests don't.
            ApiKey = apiKey ?? UniqueApiKey(),
            Telemetry = telemetry,
        };

    static string UniqueApiKey() => $"tpuf_{Guid.NewGuid():N}";

    /// <summary>
    /// Returns the telemetry buffer the client's requests record to, if any.
    /// </summary>
    internal static TelemetryBuffer? BufferOf(ITurbopufferClient client) =>
        (RawOptionsOf(client).RequestObserver as TelemetryObserver)?.Buffer;

    /// <summary>
    /// Returns the options the client's requests are made with, where telemetry is installed.
    /// </summary>
    static ClientOptions RawOptionsOf(ITurbopufferClient client)
    {
        ClientOptions options = default;
        client.WithRawResponse.WithOptions(o =>
        {
            options = o;
            return o;
        });
        return options;
    }

    static JsonElement Take(TelemetryBuffer buffer)
    {
        var batch = buffer.Take();
        Assert.NotNull(batch);
        return JsonDocument.Parse(batch.Header).RootElement;
    }

    static List<JsonElement> Records(JsonElement payload)
    {
        Assert.Equal(1, payload.GetProperty("version").GetInt32());
        return [.. payload.GetProperty("records").EnumerateArray()];
    }

    /// <summary>
    /// Returns the records sent on later requests, followed by those still buffered.
    /// </summary>
    static List<JsonElement> AllRecords(StubHandler handler, ITurbopufferClient client) =>
        [
            .. handler.Requests.SelectMany(request => SentRecords(request) ?? []),
            .. Records(Take(BufferOf(client)!)),
        ];

    static string? SentHeader(HttpRequestMessage request) =>
        request.Headers.TryGetValues(ClientTelemetry.Header, out var values)
            ? values.Single()
            : null;

    static List<JsonElement>? SentRecords(HttpRequestMessage request) =>
        SentHeader(request) is { } header ? Records(JsonDocument.Parse(header).RootElement) : null;

    static List<(int, string?)> StatusesAndErrors(HttpRequestMessage request) =>
        [
            .. SentRecords(request)!
                .Select(record =>
                    (
                        record.GetProperty("status").GetInt32(),
                        record.TryGetProperty("error", out var error) ? error.GetString() : null
                    )
                ),
        ];

    static void AddRecord(TelemetryBuffer buffer, int status) =>
        buffer.Record("post", QueryPath, status, 0, [("total", 5_000_000)]);

    static HttpResponseMessage JsonResponse(HttpStatusCode status, string body) =>
        new(status) { Content = new StringContent(body, Encoding.UTF8, "application/json") };

    static HttpResponseMessage QueryResponse() => JsonResponse(HttpStatusCode.OK, QueryBody);

    static HttpResponseMessage RetryableError()
    {
        var response = JsonResponse(HttpStatusCode.ServiceUnavailable, "{}");
        response.Headers.Add("Retry-After-Ms", "1");
        return response;
    }

    static HttpResponseMessage Accepted()
    {
        var response = new HttpResponseMessage(HttpStatusCode.Accepted)
        {
            Content = new StringContent("{}"),
        };
        response.Headers.Add("Preference-Applied", "respond-async");
        response.Headers.Add("Location", "/v1/namespaces/my-ns/operations/op-abc");
        return response;
    }

    static HttpResponseMessage WithSampleRate(HttpResponseMessage response, string rate)
    {
        response.Headers.TryAddWithoutValidation(ClientTelemetry.SampleRateHeader, rate);
        return response;
    }

    static HttpRequestException ConnectError() =>
        new("connection refused", new SocketException((int)SocketError.ConnectionRefused));

    /// <summary>
    /// Answers requests in turn, repeating the last answer, and keeps the requests it was sent.
    /// </summary>
    sealed class StubHandler : HttpMessageHandler
    {
        readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>>[] _answers;

        public StubHandler(params Func<HttpRequestMessage, HttpResponseMessage>[] answers)
            : this(
                [
                    .. answers.Select(answer =>
                        (Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>>)(
                            (request, _) => Task.FromResult(answer(request))
                        )
                    ),
                ]
            ) { }

        public StubHandler(
            params Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>>[] answers
        )
        {
            _answers = answers;
        }

        public List<HttpRequestMessage> Requests { get; } = [];

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken
        )
        {
            var answer = _answers[Math.Min(this.Requests.Count, _answers.Length - 1)];
            this.Requests.Add(request);
            return answer(request, cancellationToken);
        }
    }
}

/// <summary>
/// Tests that change the environment, which can't run alongside others.
/// </summary>
[CollectionDefinition(DisableParallelization = true)]
public class EnvironmentCollection { }

[Collection(typeof(EnvironmentCollection))]
public class TelemetryEnvironmentTest : IDisposable
{
    readonly string? _saved = Environment.GetEnvironmentVariable(ClientTelemetry.EnvVar);

    public void Dispose()
    {
        Environment.SetEnvironmentVariable(ClientTelemetry.EnvVar, _saved);
        GC.SuppressFinalize(this);
    }

    [Fact]
    public void DisabledByDefault()
    {
        Environment.SetEnvironmentVariable(ClientTelemetry.EnvVar, null);
        Assert.False(ClientTelemetry.ResolveEnabled(null));
        Assert.Null(TelemetryTest.BufferOf(NewClient()));
    }

    [Fact]
    public void EnvironmentEnablesClient()
    {
        Environment.SetEnvironmentVariable(ClientTelemetry.EnvVar, "1");
        var client = NewClient();
        var buffer = TelemetryTest.BufferOf(client);
        Assert.NotNull(buffer);

        // Copies keep the resolved setting instead of reading the environment again.
        Environment.SetEnvironmentVariable(ClientTelemetry.EnvVar, "0");
        Assert.Same(buffer, TelemetryTest.BufferOf(client.WithOptions(o => o)));
        Assert.Null(TelemetryTest.BufferOf(NewClient()));
    }

    [Fact]
    public void ExplicitValueBeatsEnvironment()
    {
        Environment.SetEnvironmentVariable(ClientTelemetry.EnvVar, "1");
        Assert.Null(TelemetryTest.BufferOf(NewClient(telemetry: false)));
        Environment.SetEnvironmentVariable(ClientTelemetry.EnvVar, "sometimes");
        Assert.Null(TelemetryTest.BufferOf(NewClient()));
        Assert.NotNull(TelemetryTest.BufferOf(NewClient(telemetry: true)));
    }

    static TurbopufferClient NewClient(bool? telemetry = null) =>
        new()
        {
            BaseUrl = "http://localhost",
            ApiKey = $"tpuf_{Guid.NewGuid():N}",
            Telemetry = telemetry,
        };
}
