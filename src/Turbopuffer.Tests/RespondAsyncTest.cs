using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Turbopuffer;
using Turbopuffer.Core;
using Turbopuffer.Exceptions;

namespace Turbopuffer.Tests;

public class RespondAsyncTest : TestBase, IDisposable
{
    readonly TimeSpan _savedPollInterval;

    public RespondAsyncTest()
    {
        // Make the polling loop tight in tests so we don't sit on Task.Delay.
        _savedPollInterval = RespondAsync.PollInterval;
        RespondAsync.PollInterval = TimeSpan.Zero;
    }

    public void Dispose()
    {
        RespondAsync.PollInterval = _savedPollInterval;
        GC.SuppressFinalize(this);
    }

    record class BlankParams : ParamsBase
    {
        internal override void AddHeadersToRequest(
            HttpRequestMessage request,
            ClientOptions options
        ) => ParamsBase.AddDefaultHeaders(request, options);

        public override Uri Url(ClientOptions _) => new("http://localhost/something");
    }

    record class ParamsWithPreferHeader : ParamsBase
    {
        internal override void AddHeadersToRequest(
            HttpRequestMessage request,
            ClientOptions options
        )
        {
            ParamsBase.AddDefaultHeaders(request, options);
            request.Headers.Add("Prefer", "wait=10");
        }

        public override Uri Url(ClientOptions _) => new("http://localhost/something");
    }

    // Builds a mocked HttpMessageHandler that returns the given responses
    // in order across successive SendAsync calls, paired with a client wired
    // to it.
    static (Mock<HttpMessageHandler> mock, TurbopufferClient client) Setup(
        params HttpResponseMessage[] responses
    )
    {
        var mock = new Mock<HttpMessageHandler>();
        var seq = mock.Protected()
            .SetupSequence<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            );
        foreach (var r in responses)
            seq = seq.ReturnsAsync(r);
        return (mock, ClientWith(mock.Object));
    }

    static TurbopufferClient ClientWith(HttpMessageHandler handler, TimeSpan? timeout = null) =>
        new()
        {
            HttpClient = new HttpClient(handler),
            MaxRetries = 0,
            Timeout = timeout,
        };

    static Task<HttpResponse> Send<T>(TurbopufferClient client)
        where T : ParamsBase, new() =>
        client.WithRawResponse.Execute(
            new HttpRequest<T> { Method = HttpMethod.Get, Params = new() },
            TestContext.Current.CancellationToken
        );

    static HttpResponseMessage AsyncAcceptedResponse(string location)
    {
        var resp = new HttpResponseMessage(HttpStatusCode.Accepted)
        {
            Content = new StringContent("ignored"),
        };
        resp.Headers.Add("Preference-Applied", "respond-async");
        resp.Headers.Add("Location", location);
        return resp;
    }

    static HttpResponseMessage JsonResponse(HttpStatusCode statusCode, string body) =>
        new(statusCode) { Content = new StringContent(body, Encoding.UTF8, "application/json") };

    static HttpResponseMessage OkResponse(string body = "{}") =>
        new(HttpStatusCode.OK) { Content = new StringContent(body) };

    [Fact]
    public async Task SendsPreferHeader()
    {
        var (mock, client) = Setup(OkResponse());

        await Send<BlankParams>(client);

        mock.Protected()
            .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    Enumerable.Single(req.Headers.GetValues("Prefer")) == "respond-async"
                ),
                ItExpr.IsAny<CancellationToken>()
            );
    }

    [Fact]
    public async Task RetainsCallerSuppliedPreferHeader()
    {
        var (mock, client) = Setup(OkResponse());

        await Send<ParamsWithPreferHeader>(client);

        mock.Protected()
            .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    Enumerable.Single(req.Headers.GetValues("Prefer")) == "wait=10"
                ),
                ItExpr.IsAny<CancellationToken>()
            );
    }

    [Fact]
    public async Task PassesThroughSyncResponse()
    {
        var (_, client) = Setup(OkResponse("ok-body"));

        using var resp = await Send<BlankParams>(client);

        Assert.Equal(HttpStatusCode.OK, resp.StatusCode);
        Assert.Equal("ok-body", await resp.ReadAsString(TestContext.Current.CancellationToken));
    }

    [Fact]
    public async Task PassesThroughUnrelated202()
    {
        // 202 without Preference-Applied: should NOT trigger polling.
        var (mock, client) = Setup(
            new HttpResponseMessage(HttpStatusCode.Accepted)
            {
                Content = new StringContent("not-our-business"),
            }
        );

        using var resp = await Send<BlankParams>(client);

        Assert.Equal(HttpStatusCode.Accepted, resp.StatusCode);
        Assert.Equal(
            "not-our-business",
            await resp.ReadAsString(TestContext.Current.CancellationToken)
        );
        mock.Protected()
            .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            );
    }

    [Fact]
    public async Task PollsUntilSuccess()
    {
        var (mock, client) = Setup(
            AsyncAcceptedResponse("/v1/operations/op-abc"),
            JsonResponse(HttpStatusCode.OK, """{"status":"running"}"""),
            JsonResponse(
                HttpStatusCode.OK,
                """{"status":"finished","result":{"success":{"foo":1}}}"""
            )
        );

        using var resp = await Send<BlankParams>(client);

        Assert.Equal(HttpStatusCode.OK, resp.StatusCode);
        Assert.Equal(
            """{"foo":1}""",
            await resp.ReadAsString(TestContext.Current.CancellationToken)
        );
        mock.Protected()
            .Verify(
                "SendAsync",
                Times.Exactly(2),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get
                    && req.RequestUri == new Uri("http://localhost/v1/operations/op-abc")
                    && !req.Headers.GetValues("Prefer").Contains("respond-async")
                ),
                ItExpr.IsAny<CancellationToken>()
            );
    }

    [Fact]
    public async Task PollsUntilError()
    {
        var (_, client) = Setup(
            AsyncAcceptedResponse("/v1/operations/op-err"),
            JsonResponse(HttpStatusCode.OK, """{"status":"running"}"""),
            JsonResponse(
                HttpStatusCode.OK,
                """{"status":"finished","result":{"error":{"status_code":404,"detail":{"message":"namespace not found"}}}}"""
            )
        );

        var ex = await Assert.ThrowsAsync<TurbopufferNotFoundException>(() =>
            Send<BlankParams>(client)
        );
        Assert.Equal(HttpStatusCode.NotFound, ex.StatusCode);
        Assert.Equal("""{"message":"namespace not found"}""", ex.ResponseBody);
    }

    [Fact]
    public async Task ThrowsOnMissingLocationHeader()
    {
        var response = new HttpResponseMessage(HttpStatusCode.Accepted)
        {
            Content = new StringContent("ignored"),
        };
        response.Headers.Add("Preference-Applied", "respond-async");
        var (_, client) = Setup(response);

        var ex = await Assert.ThrowsAsync<TurbopufferException>(() => Send<BlankParams>(client));
        Assert.Contains("Location", ex.Message);
    }

    [Theory]
    [InlineData("https://evil.example.com/v1/ops/op-x")]
    [InlineData("//evil.example.com/v1/ops/op-x")]
    [InlineData("http://api.turbopuffer.com/v1/ops/op-x")]
    public async Task RejectsBadLocation(string badLocation)
    {
        var (_, client) = Setup(AsyncAcceptedResponse(badLocation));

        var ex = await Assert.ThrowsAsync<TurbopufferException>(() => Send<BlankParams>(client));
        Assert.Contains("Location", ex.Message);
    }

    [Fact]
    public async Task ThrowsOnMalformedPollBody()
    {
        var (_, client) = Setup(
            AsyncAcceptedResponse("/v1/operations/op-bad"),
            JsonResponse(HttpStatusCode.OK, """{"status":"finished"}""")
        );

        var ex = await Assert.ThrowsAsync<TurbopufferException>(() => Send<BlankParams>(client));
        Assert.Contains("malformed poll response", ex.Message);
    }

    [Fact]
    public async Task PollUsesAuthHeaders()
    {
        var (mock, client) = Setup(
            AsyncAcceptedResponse("/v1/operations/op-headers"),
            JsonResponse(HttpStatusCode.OK, """{"status":"finished","result":{"success":{}}}""")
        );

        using var _ = await Send<BlankParams>(client);

        mock.Protected()
            .Verify(
                "SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(req =>
                    req.Method == HttpMethod.Get
                    && req.RequestUri == new Uri("http://localhost/v1/operations/op-headers")
                    && req.Headers.Contains("Authorization")
                ),
                ItExpr.IsAny<CancellationToken>()
            );
    }

    [Fact]
    public async Task PollTimeoutThrows()
    {
        var mock = new Mock<HttpMessageHandler>();
        mock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .Returns<HttpRequestMessage, CancellationToken>(
                (req, _) =>
                    req.RequestUri!.AbsolutePath == "/something"
                        ? Task.FromResult(AsyncAcceptedResponse("/v1/operations/op-slow"))
                        : Task.FromResult(
                            JsonResponse(HttpStatusCode.OK, """{"status":"running"}""")
                        )
            );

        var client = ClientWith(mock.Object, TimeSpan.FromMilliseconds(50));

        await Assert.ThrowsAsync<TurbopufferIOException>(() => Send<BlankParams>(client));
    }
}
