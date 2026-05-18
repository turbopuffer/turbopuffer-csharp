using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Turbopuffer.Client.Core;
using Turbopuffer.Client.Models;
using Turbopuffer.Client.Services;

namespace Turbopuffer.Client;

/// <summary>
/// A client for interacting with the Turbopuffer REST API.
///
/// <para>This client performs best when you create a single instance and reuse it
/// for all interactions with the REST API. This is because each client holds its
/// own connection pool and thread pools. Reusing connections and threads reduces
/// latency and saves memory.</para>
///
/// <para>NOTE: Do not inherit from this type outside the SDK unless you're okay with
/// breaking changes in non-major versions. We may add new methods in the future that
/// cause existing derived classes to break.</para>
/// </summary>
public interface ITurbopufferClient : IDisposable
{
    /// <inheritdoc cref="ClientOptions.HttpClient" />
    HttpClient HttpClient { get; init; }

    /// <inheritdoc cref="ClientOptions.BaseUrl" />
    string BaseUrl { get; init; }

    /// <inheritdoc cref="ClientOptions.ResponseValidation" />
    bool ResponseValidation { get; init; }

    /// <inheritdoc cref="ClientOptions.MaxRetries" />
    int? MaxRetries { get; init; }

    /// <inheritdoc cref="ClientOptions.Timeout" />
    TimeSpan? Timeout { get; init; }

    /// <summary>
    /// API key used for authentication
    /// </summary>
    string ApiKey { get; init; }

    /// <summary>
    /// The turbopuffer region to use.
    /// </summary>
    string? Region { get; init; }

    string? DefaultNamespace { get; init; }

    /// <summary>
    /// Returns a view of this service that provides access to raw HTTP responses
    /// for each method.
    /// </summary>
    ITurbopufferClientWithRawResponse WithRawResponse { get; }

    /// <summary>
    /// Returns a view of this service with the given option modifications applied.
    ///
    /// <para>The original service is not modified.</para>
    /// </summary>
    ITurbopufferClient WithOptions(Func<ClientOptions, ClientOptions> modifier);

    /// <summary>
    /// Returns a namespace-scoped service. All API calls made on the returned
    /// service operate on the given namespace by default, so callers don't
    /// need to set <c>Namespace</c> on every <c>NamespaceXxxParams</c>.
    /// </summary>
    INamespaceService Namespace(string @namespace);

    /// <summary>
    /// List namespaces.
    /// </summary>
    Task<ClientNamespacesPage> Namespaces(
        ClientNamespacesParams? parameters = null,
        CancellationToken cancellationToken = default
    );
}

/// <summary>
/// A view of <see cref="ITurbopufferClient"/> that provides access to raw HTTP responses for each method.
/// </summary>
public interface ITurbopufferClientWithRawResponse : IDisposable
{
    /// <inheritdoc cref="ClientOptions.HttpClient" />
    HttpClient HttpClient { get; init; }

    /// <inheritdoc cref="ClientOptions.BaseUrl" />
    string BaseUrl { get; init; }

    /// <inheritdoc cref="ClientOptions.ResponseValidation" />
    bool ResponseValidation { get; init; }

    /// <inheritdoc cref="ClientOptions.MaxRetries" />
    int? MaxRetries { get; init; }

    /// <inheritdoc cref="ClientOptions.Timeout" />
    TimeSpan? Timeout { get; init; }

    /// <summary>
    /// API key used for authentication
    /// </summary>
    string ApiKey { get; init; }

    /// <summary>
    /// The turbopuffer region to use.
    /// </summary>
    string? Region { get; init; }

    string? DefaultNamespace { get; init; }

    /// <summary>
    /// Returns a view of this service with the given option modifications applied.
    ///
    /// <para>The original service is not modified.</para>
    /// </summary>
    ITurbopufferClientWithRawResponse WithOptions(Func<ClientOptions, ClientOptions> modifier);

    /// <summary>
    /// Returns a namespace-scoped raw-response service. All API calls made on
    /// the returned service operate on the given namespace by default, so
    /// callers don't need to set <c>Namespace</c> on every
    /// <c>NamespaceXxxParams</c>.
    /// </summary>
    INamespaceServiceWithRawResponse Namespace(string @namespace);

    /// <summary>
    /// Returns a raw HTTP response for <c>get /v1/namespaces</c>, but is otherwise the
    /// same as <see cref="ITurbopufferClient.Namespaces(ClientNamespacesParams?, CancellationToken)"/>.
    /// </summary>
    Task<HttpResponse<ClientNamespacesPage>> Namespaces1(
        ClientNamespacesParams? parameters = null,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Sends a request to the Turbopuffer REST API.
    /// </summary>
    Task<HttpResponse> Execute<T>(
        HttpRequest<T> request,
        CancellationToken cancellationToken = default
    )
        where T : ParamsBase;
}
