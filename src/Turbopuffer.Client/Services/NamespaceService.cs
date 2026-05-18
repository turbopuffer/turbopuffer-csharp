using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Turbopuffer.Client.Core;
using Turbopuffer.Client.Exceptions;
using Turbopuffer.Client.Models.Namespaces;

namespace Turbopuffer.Client.Services;

/// <inheritdoc/>
public sealed class NamespaceService : INamespaceService
{
    readonly Lazy<INamespaceServiceWithRawResponse> _withRawResponse;

    /// <inheritdoc/>
    public INamespaceServiceWithRawResponse WithRawResponse
    {
        get { return _withRawResponse.Value; }
    }

    readonly ITurbopufferClient _client;

    /// <inheritdoc/>
    public INamespaceService WithOptions(Func<ClientOptions, ClientOptions> modifier)
    {
        return new NamespaceService(this._client.WithOptions(modifier));
    }

    public NamespaceService(ITurbopufferClient client)
    {
        _client = client;

        _withRawResponse = new(() => new NamespaceServiceWithRawResponse(client.WithRawResponse));
    }

    /// <inheritdoc/>
    public async Task<NamespaceBranchFromResponse> BranchFrom(
        NamespaceBranchFromParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        using var response = await this
            .WithRawResponse.BranchFrom(parameters, cancellationToken)
            .ConfigureAwait(false);
        return await response.Deserialize(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<NamespaceCopyFromResponse> CopyFrom(
        NamespaceCopyFromParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        using var response = await this
            .WithRawResponse.CopyFrom(parameters, cancellationToken)
            .ConfigureAwait(false);
        return await response.Deserialize(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<NamespaceDeleteAllResponse> DeleteAll(
        NamespaceDeleteAllParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        using var response = await this
            .WithRawResponse.DeleteAll(parameters, cancellationToken)
            .ConfigureAwait(false);
        return await response.Deserialize(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<NamespaceExplainQueryResponse> ExplainQuery(
        NamespaceExplainQueryParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        using var response = await this
            .WithRawResponse.ExplainQuery(parameters, cancellationToken)
            .ConfigureAwait(false);
        return await response.Deserialize(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<NamespaceHintCacheWarmResponse> HintCacheWarm(
        NamespaceHintCacheWarmParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        using var response = await this
            .WithRawResponse.HintCacheWarm(parameters, cancellationToken)
            .ConfigureAwait(false);
        return await response.Deserialize(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<NamespaceMetadata> Metadata(
        NamespaceMetadataParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        using var response = await this
            .WithRawResponse.Metadata(parameters, cancellationToken)
            .ConfigureAwait(false);
        return await response.Deserialize(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<NamespaceMultiQueryResponse> MultiQuery(
        NamespaceMultiQueryParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        using var response = await this
            .WithRawResponse.MultiQuery(parameters, cancellationToken)
            .ConfigureAwait(false);
        return await response.Deserialize(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<NamespaceQueryResponse> Query(
        NamespaceQueryParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        using var response = await this
            .WithRawResponse.Query(parameters, cancellationToken)
            .ConfigureAwait(false);
        return await response.Deserialize(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<NamespaceRecallResponse> Recall(
        NamespaceRecallParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        using var response = await this
            .WithRawResponse.Recall(parameters, cancellationToken)
            .ConfigureAwait(false);
        return await response.Deserialize(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<Dictionary<string, AttributeSchemaConfig>> Schema(
        NamespaceSchemaParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        using var response = await this
            .WithRawResponse.Schema(parameters, cancellationToken)
            .ConfigureAwait(false);
        return await response.Deserialize(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<NamespaceMetadata> UpdateMetadata(
        NamespaceUpdateMetadataParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        using var response = await this
            .WithRawResponse.UpdateMetadata(parameters, cancellationToken)
            .ConfigureAwait(false);
        return await response.Deserialize(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<Dictionary<string, AttributeSchemaConfig>> UpdateSchema(
        NamespaceUpdateSchemaParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        using var response = await this
            .WithRawResponse.UpdateSchema(parameters, cancellationToken)
            .ConfigureAwait(false);
        return await response.Deserialize(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<NamespaceWriteResponse> Write(
        NamespaceWriteParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        using var response = await this
            .WithRawResponse.Write(parameters, cancellationToken)
            .ConfigureAwait(false);
        return await response.Deserialize(cancellationToken).ConfigureAwait(false);
    }
}

/// <inheritdoc/>
public sealed class NamespaceServiceWithRawResponse : INamespaceServiceWithRawResponse
{
    readonly ITurbopufferClientWithRawResponse _client;

    /// <inheritdoc/>
    public INamespaceServiceWithRawResponse WithOptions(Func<ClientOptions, ClientOptions> modifier)
    {
        return new NamespaceServiceWithRawResponse(this._client.WithOptions(modifier));
    }

    public NamespaceServiceWithRawResponse(ITurbopufferClientWithRawResponse client)
    {
        _client = client;
    }

    /// <inheritdoc/>
    public async Task<HttpResponse<NamespaceBranchFromResponse>> BranchFrom(
        NamespaceBranchFromParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        parameters = parameters with
        {
            Namespace = parameters.Namespace ?? this._client.DefaultNamespace,
        };

        if (parameters.Namespace == null)
        {
            throw new TurbopufferInvalidDataException("'parameters.Namespace' cannot be null");
        }

        HttpRequest<NamespaceBranchFromParams> request = new()
        {
            Method = HttpMethod.Post,
            Params = parameters,
        };
        var response = await this._client.Execute(request, cancellationToken).ConfigureAwait(false);
        return new(
            response,
            async (token) =>
            {
                var deserializedResponse = await response
                    .Deserialize<NamespaceBranchFromResponse>(token)
                    .ConfigureAwait(false);
                if (this._client.ResponseValidation)
                {
                    deserializedResponse.Validate();
                }
                return deserializedResponse;
            }
        );
    }

    /// <inheritdoc/>
    public async Task<HttpResponse<NamespaceCopyFromResponse>> CopyFrom(
        NamespaceCopyFromParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        parameters = parameters with
        {
            Namespace = parameters.Namespace ?? this._client.DefaultNamespace,
        };

        if (parameters.Namespace == null)
        {
            throw new TurbopufferInvalidDataException("'parameters.Namespace' cannot be null");
        }

        HttpRequest<NamespaceCopyFromParams> request = new()
        {
            Method = HttpMethod.Post,
            Params = parameters,
        };
        var response = await this._client.Execute(request, cancellationToken).ConfigureAwait(false);
        return new(
            response,
            async (token) =>
            {
                var deserializedResponse = await response
                    .Deserialize<NamespaceCopyFromResponse>(token)
                    .ConfigureAwait(false);
                if (this._client.ResponseValidation)
                {
                    deserializedResponse.Validate();
                }
                return deserializedResponse;
            }
        );
    }

    /// <inheritdoc/>
    public async Task<HttpResponse<NamespaceDeleteAllResponse>> DeleteAll(
        NamespaceDeleteAllParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        parameters = parameters with
        {
            Namespace = parameters.Namespace ?? this._client.DefaultNamespace,
        };

        if (parameters.Namespace == null)
        {
            throw new TurbopufferInvalidDataException("'parameters.Namespace' cannot be null");
        }

        HttpRequest<NamespaceDeleteAllParams> request = new()
        {
            Method = HttpMethod.Delete,
            Params = parameters,
        };
        var response = await this._client.Execute(request, cancellationToken).ConfigureAwait(false);
        return new(
            response,
            async (token) =>
            {
                var deserializedResponse = await response
                    .Deserialize<NamespaceDeleteAllResponse>(token)
                    .ConfigureAwait(false);
                if (this._client.ResponseValidation)
                {
                    deserializedResponse.Validate();
                }
                return deserializedResponse;
            }
        );
    }

    /// <inheritdoc/>
    public async Task<HttpResponse<NamespaceExplainQueryResponse>> ExplainQuery(
        NamespaceExplainQueryParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        parameters = parameters with
        {
            Namespace = parameters.Namespace ?? this._client.DefaultNamespace,
        };

        if (parameters.Namespace == null)
        {
            throw new TurbopufferInvalidDataException("'parameters.Namespace' cannot be null");
        }

        HttpRequest<NamespaceExplainQueryParams> request = new()
        {
            Method = HttpMethod.Post,
            Params = parameters,
        };
        var response = await this._client.Execute(request, cancellationToken).ConfigureAwait(false);
        return new(
            response,
            async (token) =>
            {
                var deserializedResponse = await response
                    .Deserialize<NamespaceExplainQueryResponse>(token)
                    .ConfigureAwait(false);
                if (this._client.ResponseValidation)
                {
                    deserializedResponse.Validate();
                }
                return deserializedResponse;
            }
        );
    }

    /// <inheritdoc/>
    public async Task<HttpResponse<NamespaceHintCacheWarmResponse>> HintCacheWarm(
        NamespaceHintCacheWarmParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        parameters = parameters with
        {
            Namespace = parameters.Namespace ?? this._client.DefaultNamespace,
        };

        if (parameters.Namespace == null)
        {
            throw new TurbopufferInvalidDataException("'parameters.Namespace' cannot be null");
        }

        HttpRequest<NamespaceHintCacheWarmParams> request = new()
        {
            Method = HttpMethod.Get,
            Params = parameters,
        };
        var response = await this._client.Execute(request, cancellationToken).ConfigureAwait(false);
        return new(
            response,
            async (token) =>
            {
                var deserializedResponse = await response
                    .Deserialize<NamespaceHintCacheWarmResponse>(token)
                    .ConfigureAwait(false);
                if (this._client.ResponseValidation)
                {
                    deserializedResponse.Validate();
                }
                return deserializedResponse;
            }
        );
    }

    /// <inheritdoc/>
    public async Task<HttpResponse<NamespaceMetadata>> Metadata(
        NamespaceMetadataParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        parameters = parameters with
        {
            Namespace = parameters.Namespace ?? this._client.DefaultNamespace,
        };

        if (parameters.Namespace == null)
        {
            throw new TurbopufferInvalidDataException("'parameters.Namespace' cannot be null");
        }

        HttpRequest<NamespaceMetadataParams> request = new()
        {
            Method = HttpMethod.Get,
            Params = parameters,
        };
        var response = await this._client.Execute(request, cancellationToken).ConfigureAwait(false);
        return new(
            response,
            async (token) =>
            {
                var namespaceMetadata = await response
                    .Deserialize<NamespaceMetadata>(token)
                    .ConfigureAwait(false);
                if (this._client.ResponseValidation)
                {
                    namespaceMetadata.Validate();
                }
                return namespaceMetadata;
            }
        );
    }

    /// <inheritdoc/>
    public async Task<HttpResponse<NamespaceMultiQueryResponse>> MultiQuery(
        NamespaceMultiQueryParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        parameters = parameters with
        {
            Namespace = parameters.Namespace ?? this._client.DefaultNamespace,
        };

        if (parameters.Namespace == null)
        {
            throw new TurbopufferInvalidDataException("'parameters.Namespace' cannot be null");
        }

        HttpRequest<NamespaceMultiQueryParams> request = new()
        {
            Method = HttpMethod.Post,
            Params = parameters,
        };
        var response = await this._client.Execute(request, cancellationToken).ConfigureAwait(false);
        return new(
            response,
            async (token) =>
            {
                var deserializedResponse = await response
                    .Deserialize<NamespaceMultiQueryResponse>(token)
                    .ConfigureAwait(false);
                if (this._client.ResponseValidation)
                {
                    deserializedResponse.Validate();
                }
                return deserializedResponse;
            }
        );
    }

    /// <inheritdoc/>
    public async Task<HttpResponse<NamespaceQueryResponse>> Query(
        NamespaceQueryParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        parameters = parameters with
        {
            Namespace = parameters.Namespace ?? this._client.DefaultNamespace,
        };

        if (parameters.Namespace == null)
        {
            throw new TurbopufferInvalidDataException("'parameters.Namespace' cannot be null");
        }

        HttpRequest<NamespaceQueryParams> request = new()
        {
            Method = HttpMethod.Post,
            Params = parameters,
        };
        var response = await this._client.Execute(request, cancellationToken).ConfigureAwait(false);
        return new(
            response,
            async (token) =>
            {
                var deserializedResponse = await response
                    .Deserialize<NamespaceQueryResponse>(token)
                    .ConfigureAwait(false);
                if (this._client.ResponseValidation)
                {
                    deserializedResponse.Validate();
                }
                return deserializedResponse;
            }
        );
    }

    /// <inheritdoc/>
    public async Task<HttpResponse<NamespaceRecallResponse>> Recall(
        NamespaceRecallParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        parameters = parameters with
        {
            Namespace = parameters.Namespace ?? this._client.DefaultNamespace,
        };

        if (parameters.Namespace == null)
        {
            throw new TurbopufferInvalidDataException("'parameters.Namespace' cannot be null");
        }

        HttpRequest<NamespaceRecallParams> request = new()
        {
            Method = HttpMethod.Post,
            Params = parameters,
        };
        var response = await this._client.Execute(request, cancellationToken).ConfigureAwait(false);
        return new(
            response,
            async (token) =>
            {
                var deserializedResponse = await response
                    .Deserialize<NamespaceRecallResponse>(token)
                    .ConfigureAwait(false);
                if (this._client.ResponseValidation)
                {
                    deserializedResponse.Validate();
                }
                return deserializedResponse;
            }
        );
    }

    /// <inheritdoc/>
    public async Task<HttpResponse<Dictionary<string, AttributeSchemaConfig>>> Schema(
        NamespaceSchemaParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        parameters = parameters with
        {
            Namespace = parameters.Namespace ?? this._client.DefaultNamespace,
        };

        if (parameters.Namespace == null)
        {
            throw new TurbopufferInvalidDataException("'parameters.Namespace' cannot be null");
        }

        HttpRequest<NamespaceSchemaParams> request = new()
        {
            Method = HttpMethod.Get,
            Params = parameters,
        };
        var response = await this._client.Execute(request, cancellationToken).ConfigureAwait(false);
        return new(
            response,
            async (token) =>
            {
                var deserializedResponse = await response
                    .Deserialize<Dictionary<string, AttributeSchemaConfig>>(token)
                    .ConfigureAwait(false);
                if (this._client.ResponseValidation)
                {
                    foreach (var item in deserializedResponse.Values)
                    {
                        item.Validate();
                    }
                }
                return deserializedResponse;
            }
        );
    }

    /// <inheritdoc/>
    public async Task<HttpResponse<NamespaceMetadata>> UpdateMetadata(
        NamespaceUpdateMetadataParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        parameters = parameters with
        {
            Namespace = parameters.Namespace ?? this._client.DefaultNamespace,
        };

        if (parameters.Namespace == null)
        {
            throw new TurbopufferInvalidDataException("'parameters.Namespace' cannot be null");
        }

        HttpRequest<NamespaceUpdateMetadataParams> request = new()
        {
            Method = TurbopufferClientWithRawResponse.PatchMethod,
            Params = parameters,
        };
        var response = await this._client.Execute(request, cancellationToken).ConfigureAwait(false);
        return new(
            response,
            async (token) =>
            {
                var namespaceMetadata = await response
                    .Deserialize<NamespaceMetadata>(token)
                    .ConfigureAwait(false);
                if (this._client.ResponseValidation)
                {
                    namespaceMetadata.Validate();
                }
                return namespaceMetadata;
            }
        );
    }

    /// <inheritdoc/>
    public async Task<HttpResponse<Dictionary<string, AttributeSchemaConfig>>> UpdateSchema(
        NamespaceUpdateSchemaParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        parameters = parameters with
        {
            Namespace = parameters.Namespace ?? this._client.DefaultNamespace,
        };

        if (parameters.Namespace == null)
        {
            throw new TurbopufferInvalidDataException("'parameters.Namespace' cannot be null");
        }

        HttpRequest<NamespaceUpdateSchemaParams> request = new()
        {
            Method = HttpMethod.Post,
            Params = parameters,
        };
        var response = await this._client.Execute(request, cancellationToken).ConfigureAwait(false);
        return new(
            response,
            async (token) =>
            {
                var deserializedResponse = await response
                    .Deserialize<Dictionary<string, AttributeSchemaConfig>>(token)
                    .ConfigureAwait(false);
                if (this._client.ResponseValidation)
                {
                    foreach (var item in deserializedResponse.Values)
                    {
                        item.Validate();
                    }
                }
                return deserializedResponse;
            }
        );
    }

    /// <inheritdoc/>
    public async Task<HttpResponse<NamespaceWriteResponse>> Write(
        NamespaceWriteParams parameters,
        CancellationToken cancellationToken = default
    )
    {
        parameters = parameters with
        {
            Namespace = parameters.Namespace ?? this._client.DefaultNamespace,
        };

        if (parameters.Namespace == null)
        {
            throw new TurbopufferInvalidDataException("'parameters.Namespace' cannot be null");
        }

        HttpRequest<NamespaceWriteParams> request = new()
        {
            Method = HttpMethod.Post,
            Params = parameters,
        };
        var response = await this
            ._client.WithOptions(options => options with { MaxRetries = options.MaxRetries ?? 6 })
            .Execute(request, cancellationToken)
            .ConfigureAwait(false);
        return new(
            response,
            async (token) =>
            {
                var deserializedResponse = await response
                    .Deserialize<NamespaceWriteResponse>(token)
                    .ConfigureAwait(false);
                if (this._client.ResponseValidation)
                {
                    deserializedResponse.Validate();
                }
                return deserializedResponse;
            }
        );
    }
}
