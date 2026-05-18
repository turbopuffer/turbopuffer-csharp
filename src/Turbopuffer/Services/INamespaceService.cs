using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Services;

/// <summary>
/// NOTE: Do not inherit from this type outside the SDK unless you're okay with breaking
/// changes in non-major versions. We may add new methods in the future that cause
/// existing derived classes to break.
/// </summary>
public interface INamespaceService
{
    /// <summary>
    /// Returns a view of this service that provides access to raw HTTP responses
    /// for each method.
    /// </summary>
    INamespaceServiceWithRawResponse WithRawResponse { get; }

    /// <summary>
    /// Returns a view of this service with the given option modifications applied.
    ///
    /// <para>The original service is not modified.</para>
    /// </summary>
    INamespaceService WithOptions(Func<ClientOptions, ClientOptions> modifier);

    /// <summary>
    /// Creates an instant, copy-on-write clone of a namespace.
    /// </summary>
    Task<NamespaceBranchFromResponse> BranchFrom(
        NamespaceBranchFromParams parameters,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Copy all documents from another namespace into this one.
    /// </summary>
    Task<NamespaceCopyFromResponse> CopyFrom(
        NamespaceCopyFromParams parameters,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Delete namespace.
    /// </summary>
    Task<NamespaceDeleteAllResponse> DeleteAll(
        NamespaceDeleteAllParams parameters,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Explain a query plan.
    /// </summary>
    Task<NamespaceExplainQueryResponse> ExplainQuery(
        NamespaceExplainQueryParams parameters,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Signal turbopuffer to prepare for low-latency requests.
    /// </summary>
    Task<NamespaceHintCacheWarmResponse> HintCacheWarm(
        NamespaceHintCacheWarmParams parameters,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Get metadata about a namespace.
    /// </summary>
    Task<NamespaceMetadata> Metadata(
        NamespaceMetadataParams parameters,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Issue multiple concurrent queries filter or search documents.
    /// </summary>
    Task<NamespaceMultiQueryResponse> MultiQuery(
        NamespaceMultiQueryParams parameters,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Query, filter, full-text search and vector search documents.
    /// </summary>
    Task<NamespaceQueryResponse> Query(
        NamespaceQueryParams parameters,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Evaluate recall.
    /// </summary>
    Task<NamespaceRecallResponse> Recall(
        NamespaceRecallParams parameters,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Get namespace schema.
    /// </summary>
    Task<Dictionary<string, AttributeSchemaConfig>> Schema(
        NamespaceSchemaParams parameters,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Update metadata configuration for a namespace.
    /// </summary>
    Task<NamespaceMetadata> UpdateMetadata(
        NamespaceUpdateMetadataParams parameters,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Update namespace schema.
    /// </summary>
    Task<Dictionary<string, AttributeSchemaConfig>> UpdateSchema(
        NamespaceUpdateSchemaParams parameters,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Create, update, or delete documents.
    /// </summary>
    Task<NamespaceWriteResponse> Write(
        NamespaceWriteParams parameters,
        CancellationToken cancellationToken = default
    );
}

/// <summary>
/// A view of <see cref="INamespaceService"/> that provides access to raw
/// HTTP responses for each method.
/// </summary>
public interface INamespaceServiceWithRawResponse
{
    /// <summary>
    /// Returns a view of this service with the given option modifications applied.
    ///
    /// <para>The original service is not modified.</para>
    /// </summary>
    INamespaceServiceWithRawResponse WithOptions(Func<ClientOptions, ClientOptions> modifier);

    /// <summary>
    /// Returns a raw HTTP response for <c>post /v2/namespaces/{namespace}?stainless_overload=branchFrom</c>, but is otherwise the
    /// same as <see cref="INamespaceService.BranchFrom(NamespaceBranchFromParams, CancellationToken)"/>.
    /// </summary>
    Task<HttpResponse<NamespaceBranchFromResponse>> BranchFrom(
        NamespaceBranchFromParams parameters,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Returns a raw HTTP response for <c>post /v2/namespaces/{namespace}?stainless_overload=copyFrom</c>, but is otherwise the
    /// same as <see cref="INamespaceService.CopyFrom(NamespaceCopyFromParams, CancellationToken)"/>.
    /// </summary>
    Task<HttpResponse<NamespaceCopyFromResponse>> CopyFrom(
        NamespaceCopyFromParams parameters,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Returns a raw HTTP response for <c>delete /v2/namespaces/{namespace}</c>, but is otherwise the
    /// same as <see cref="INamespaceService.DeleteAll(NamespaceDeleteAllParams, CancellationToken)"/>.
    /// </summary>
    Task<HttpResponse<NamespaceDeleteAllResponse>> DeleteAll(
        NamespaceDeleteAllParams parameters,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Returns a raw HTTP response for <c>post /v2/namespaces/{namespace}/explain_query</c>, but is otherwise the
    /// same as <see cref="INamespaceService.ExplainQuery(NamespaceExplainQueryParams, CancellationToken)"/>.
    /// </summary>
    Task<HttpResponse<NamespaceExplainQueryResponse>> ExplainQuery(
        NamespaceExplainQueryParams parameters,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Returns a raw HTTP response for <c>get /v1/namespaces/{namespace}/hint_cache_warm</c>, but is otherwise the
    /// same as <see cref="INamespaceService.HintCacheWarm(NamespaceHintCacheWarmParams, CancellationToken)"/>.
    /// </summary>
    Task<HttpResponse<NamespaceHintCacheWarmResponse>> HintCacheWarm(
        NamespaceHintCacheWarmParams parameters,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Returns a raw HTTP response for <c>get /v2/namespaces/{namespace}/metadata</c>, but is otherwise the
    /// same as <see cref="INamespaceService.Metadata(NamespaceMetadataParams, CancellationToken)"/>.
    /// </summary>
    Task<HttpResponse<NamespaceMetadata>> Metadata(
        NamespaceMetadataParams parameters,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Returns a raw HTTP response for <c>post /v2/namespaces/{namespace}/query?stainless_overload=multiQuery</c>, but is otherwise the
    /// same as <see cref="INamespaceService.MultiQuery(NamespaceMultiQueryParams, CancellationToken)"/>.
    /// </summary>
    Task<HttpResponse<NamespaceMultiQueryResponse>> MultiQuery(
        NamespaceMultiQueryParams parameters,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Returns a raw HTTP response for <c>post /v2/namespaces/{namespace}/query</c>, but is otherwise the
    /// same as <see cref="INamespaceService.Query(NamespaceQueryParams, CancellationToken)"/>.
    /// </summary>
    Task<HttpResponse<NamespaceQueryResponse>> Query(
        NamespaceQueryParams parameters,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Returns a raw HTTP response for <c>post /v1/namespaces/{namespace}/_debug/recall</c>, but is otherwise the
    /// same as <see cref="INamespaceService.Recall(NamespaceRecallParams, CancellationToken)"/>.
    /// </summary>
    Task<HttpResponse<NamespaceRecallResponse>> Recall(
        NamespaceRecallParams parameters,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Returns a raw HTTP response for <c>get /v1/namespaces/{namespace}/schema</c>, but is otherwise the
    /// same as <see cref="INamespaceService.Schema(NamespaceSchemaParams, CancellationToken)"/>.
    /// </summary>
    Task<HttpResponse<Dictionary<string, AttributeSchemaConfig>>> Schema(
        NamespaceSchemaParams parameters,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Returns a raw HTTP response for <c>patch /v1/namespaces/{namespace}/metadata</c>, but is otherwise the
    /// same as <see cref="INamespaceService.UpdateMetadata(NamespaceUpdateMetadataParams, CancellationToken)"/>.
    /// </summary>
    Task<HttpResponse<NamespaceMetadata>> UpdateMetadata(
        NamespaceUpdateMetadataParams parameters,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Returns a raw HTTP response for <c>post /v1/namespaces/{namespace}/schema</c>, but is otherwise the
    /// same as <see cref="INamespaceService.UpdateSchema(NamespaceUpdateSchemaParams, CancellationToken)"/>.
    /// </summary>
    Task<HttpResponse<Dictionary<string, AttributeSchemaConfig>>> UpdateSchema(
        NamespaceUpdateSchemaParams parameters,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Returns a raw HTTP response for <c>post /v2/namespaces/{namespace}</c>, but is otherwise the
    /// same as <see cref="INamespaceService.Write(NamespaceWriteParams, CancellationToken)"/>.
    /// </summary>
    Task<HttpResponse<NamespaceWriteResponse>> Write(
        NamespaceWriteParams parameters,
        CancellationToken cancellationToken = default
    );
}
