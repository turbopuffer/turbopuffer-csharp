using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Core;

namespace Turbopuffer.Models.Namespaces;

/// <summary>
/// Create, update, or delete documents.
///
/// <para>NOTE: Do not inherit from this type outside the SDK unless you're okay with
/// breaking changes in non-major versions. We may add new methods in the future that
/// cause existing derived classes to break.</para>
/// </summary>
public record class NamespaceWriteParams : ParamsBase
{
    readonly JsonDictionary _rawBodyData = new();
    public IReadOnlyDictionary<string, JsonElement> RawBodyData
    {
        get { return this._rawBodyData.Freeze(); }
    }

    public string? Namespace { get; init; }

    /// <summary>
    /// The namespace to create an instant, copy-on-write clone of.
    /// </summary>
    public BranchFromNamespaceParams? BranchFromNamespace
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableClass<BranchFromNamespaceParams>(
                "branch_from_namespace"
            );
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawBodyData.Set("branch_from_namespace", value);
        }
    }

    /// <summary>
    /// The namespace to copy documents from.
    /// </summary>
    public CopyFromNamespaceParams? CopyFromNamespace
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableClass<CopyFromNamespaceParams>(
                "copy_from_namespace"
            );
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawBodyData.Set("copy_from_namespace", value);
        }
    }

    /// <summary>
    /// The filter specifying which documents to delete.
    /// </summary>
    public JsonElement? DeleteByFilter
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableStruct<JsonElement>("delete_by_filter");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawBodyData.Set("delete_by_filter", value);
        }
    }

    /// <summary>
    /// Allow partial completion when filter matches too many documents.
    /// </summary>
    public bool? DeleteByFilterAllowPartial
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableStruct<bool>("delete_by_filter_allow_partial");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawBodyData.Set("delete_by_filter_allow_partial", value);
        }
    }

    /// <summary>
    /// A condition evaluated against the current value of each document targeted
    /// by a delete write. Only documents that pass the condition are deleted.
    /// </summary>
    public JsonElement? DeleteCondition
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableStruct<JsonElement>("delete_condition");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawBodyData.Set("delete_condition", value);
        }
    }

    public IReadOnlyList<ID>? Deletes
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableStruct<ImmutableArray<ID>>("deletes");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawBodyData.Set<ImmutableArray<ID>?>(
                "deletes",
                value == null ? null : ImmutableArray.ToImmutableArray(value)
            );
        }
    }

    /// <summary>
    /// Disables write throttling (HTTP 429 responses) during high-volume ingestion.
    /// </summary>
    public bool? DisableBackpressure
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableStruct<bool>("disable_backpressure");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawBodyData.Set("disable_backpressure", value);
        }
    }

    /// <summary>
    /// A function used to calculate vector similarity.
    /// </summary>
    public ApiEnum<string, DistanceMetric>? DistanceMetric
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableClass<ApiEnum<string, DistanceMetric>>(
                "distance_metric"
            );
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawBodyData.Set("distance_metric", value);
        }
    }

    /// <summary>
    /// The encryption configuration for a namespace.
    /// </summary>
    public Encryption? Encryption
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableClass<Encryption>("encryption");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawBodyData.Set("encryption", value);
        }
    }

    /// <summary>
    /// The patch and filter specifying which documents to patch.
    /// </summary>
    public PatchByFilter? PatchByFilter
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableClass<PatchByFilter>("patch_by_filter");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawBodyData.Set("patch_by_filter", value);
        }
    }

    /// <summary>
    /// Allow partial completion when filter matches too many documents.
    /// </summary>
    public bool? PatchByFilterAllowPartial
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableStruct<bool>("patch_by_filter_allow_partial");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawBodyData.Set("patch_by_filter_allow_partial", value);
        }
    }

    /// <summary>
    /// A list of documents in columnar format. Each key is a column name, mapped
    /// to an array of values for that column.
    /// </summary>
    public Columns? PatchColumns
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableClass<Columns>("patch_columns");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawBodyData.Set("patch_columns", value);
        }
    }

    /// <summary>
    /// A condition evaluated against the current value of each document targeted
    /// by a patch write. Only documents that pass the condition are patched.
    /// </summary>
    public JsonElement? PatchCondition
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableStruct<JsonElement>("patch_condition");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawBodyData.Set("patch_condition", value);
        }
    }

    public IReadOnlyList<Row>? PatchRows
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableStruct<ImmutableArray<Row>>("patch_rows");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawBodyData.Set<ImmutableArray<Row>?>(
                "patch_rows",
                value == null ? null : ImmutableArray.ToImmutableArray(value)
            );
        }
    }

    /// <summary>
    /// If true, return the IDs of affected rows (deleted, patched, upserted) in
    /// the response. For filtered and conditional writes, only IDs for writes that
    /// succeeded will be included.
    /// </summary>
    public bool? ReturnAffectedIds
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableStruct<bool>("return_affected_ids");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawBodyData.Set("return_affected_ids", value);
        }
    }

    /// <summary>
    /// The schema of the attributes attached to the documents.
    /// </summary>
    public IReadOnlyDictionary<string, AttributeSchema>? Schema
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableClass<FrozenDictionary<string, AttributeSchema>>(
                "schema"
            );
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawBodyData.Set<FrozenDictionary<string, AttributeSchema>?>(
                "schema",
                value == null ? null : FrozenDictionary.ToFrozenDictionary(value)
            );
        }
    }

    /// <summary>
    /// A list of documents in columnar format. Each key is a column name, mapped
    /// to an array of values for that column.
    /// </summary>
    public Columns? UpsertColumns
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableClass<Columns>("upsert_columns");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawBodyData.Set("upsert_columns", value);
        }
    }

    /// <summary>
    /// A condition evaluated against the current value of each document targeted
    /// by an upsert write. Only documents that pass the condition are upserted.
    /// </summary>
    public JsonElement? UpsertCondition
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableStruct<JsonElement>("upsert_condition");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawBodyData.Set("upsert_condition", value);
        }
    }

    public IReadOnlyList<Row>? UpsertRows
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableStruct<ImmutableArray<Row>>("upsert_rows");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawBodyData.Set<ImmutableArray<Row>?>(
                "upsert_rows",
                value == null ? null : ImmutableArray.ToImmutableArray(value)
            );
        }
    }

    public NamespaceWriteParams() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public NamespaceWriteParams(NamespaceWriteParams namespaceWriteParams)
        : base(namespaceWriteParams)
    {
        this.Namespace = namespaceWriteParams.Namespace;

        this._rawBodyData = new(namespaceWriteParams._rawBodyData);
    }
#pragma warning restore CS8618

    public NamespaceWriteParams(
        IReadOnlyDictionary<string, JsonElement> rawHeaderData,
        IReadOnlyDictionary<string, JsonElement> rawQueryData,
        IReadOnlyDictionary<string, JsonElement> rawBodyData
    )
    {
        this._rawHeaderData = new(rawHeaderData);
        this._rawQueryData = new(rawQueryData);
        this._rawBodyData = new(rawBodyData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    NamespaceWriteParams(
        FrozenDictionary<string, JsonElement> rawHeaderData,
        FrozenDictionary<string, JsonElement> rawQueryData,
        FrozenDictionary<string, JsonElement> rawBodyData,
        string namespace_
    )
    {
        this._rawHeaderData = new(rawHeaderData);
        this._rawQueryData = new(rawQueryData);
        this._rawBodyData = new(rawBodyData);
        this.Namespace = namespace_;
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="IFromRawJson{T}.FromRawUnchecked"/>
    public static NamespaceWriteParams FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawHeaderData,
        IReadOnlyDictionary<string, JsonElement> rawQueryData,
        IReadOnlyDictionary<string, JsonElement> rawBodyData,
        string namespace_
    )
    {
        return new(
            FrozenDictionary.ToFrozenDictionary(rawHeaderData),
            FrozenDictionary.ToFrozenDictionary(rawQueryData),
            FrozenDictionary.ToFrozenDictionary(rawBodyData),
            namespace_
        );
    }

    public override string ToString() =>
        JsonSerializer.Serialize(
            FriendlyJsonPrinter.PrintValue(
                new Dictionary<string, JsonElement>()
                {
                    ["Namespace"] = JsonSerializer.SerializeToElement(this.Namespace),
                    ["HeaderData"] = FriendlyJsonPrinter.PrintValue(
                        JsonSerializer.SerializeToElement(this._rawHeaderData.Freeze())
                    ),
                    ["QueryData"] = FriendlyJsonPrinter.PrintValue(
                        JsonSerializer.SerializeToElement(this._rawQueryData.Freeze())
                    ),
                    ["BodyData"] = FriendlyJsonPrinter.PrintValue(this._rawBodyData.Freeze()),
                }
            ),
            ModelBase.ToStringSerializerOptions
        );

    public virtual bool Equals(NamespaceWriteParams? other)
    {
        if (other == null)
        {
            return false;
        }
        return (this.Namespace?.Equals(other.Namespace) ?? other.Namespace == null)
            && this._rawHeaderData.Equals(other._rawHeaderData)
            && this._rawQueryData.Equals(other._rawQueryData)
            && this._rawBodyData.Equals(other._rawBodyData);
    }

    public override Uri Url(ClientOptions options)
    {
        return new UriBuilder(
            options.BaseUrl.ToString().TrimEnd('/')
                + string.Format("/v2/namespaces/{0}", this.Namespace)
        )
        {
            Query = this.QueryString(options),
        }.Uri;
    }

    internal override HttpContent? BodyContent()
    {
        return new StringContent(
            JsonSerializer.Serialize(this.RawBodyData, ModelBase.SerializerOptions),
            Encoding.UTF8,
            "application/json"
        );
    }

    internal override void AddHeadersToRequest(HttpRequestMessage request, ClientOptions options)
    {
        ParamsBase.AddDefaultHeaders(request, options);
        foreach (var item in this.RawHeaderData)
        {
            ParamsBase.AddHeaderElementToRequest(request, item.Key, item.Value);
        }
    }

    public override int GetHashCode()
    {
        return 0;
    }
}

/// <summary>
/// The patch and filter specifying which documents to patch.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<PatchByFilter, PatchByFilterFromRaw>))]
public sealed record class PatchByFilter : JsonModel
{
    /// <summary>
    /// Filter by attributes. Same syntax as the query endpoint.
    /// </summary>
    public required JsonElement Filters
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<JsonElement>("filters");
        }
        init { this._rawData.Set("filters", value); }
    }

    public required IReadOnlyDictionary<string, JsonElement> Patch
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<FrozenDictionary<string, JsonElement>>("patch");
        }
        init
        {
            this._rawData.Set<FrozenDictionary<string, JsonElement>>(
                "patch",
                FrozenDictionary.ToFrozenDictionary(value)
            );
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.Filters;
        _ = this.Patch;
    }

    public PatchByFilter() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public PatchByFilter(PatchByFilter patchByFilter)
        : base(patchByFilter) { }
#pragma warning restore CS8618

    public PatchByFilter(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    PatchByFilter(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="PatchByFilterFromRaw.FromRawUnchecked"/>
    public static PatchByFilter FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class PatchByFilterFromRaw : IFromRawJson<PatchByFilter>
{
    /// <inheritdoc/>
    public PatchByFilter FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        PatchByFilter.FromRawUnchecked(rawData);
}
