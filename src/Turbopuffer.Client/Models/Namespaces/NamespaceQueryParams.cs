using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Client.Core;
using Turbopuffer.Client.Exceptions;

namespace Turbopuffer.Client.Models.Namespaces;

/// <summary>
/// Query, filter, full-text search and vector search documents.
///
/// <para>NOTE: Do not inherit from this type outside the SDK unless you're okay with
/// breaking changes in non-major versions. We may add new methods in the future that
/// cause existing derived classes to break.</para>
/// </summary>
public record class NamespaceQueryParams : ParamsBase
{
    readonly JsonDictionary _rawBodyData = new();
    public IReadOnlyDictionary<string, JsonElement> RawBodyData
    {
        get { return this._rawBodyData.Freeze(); }
    }

    public string? Namespace { get; init; }

    /// <summary>
    /// Aggregations to compute over all documents in the namespace that match the filters.
    /// </summary>
    public IReadOnlyDictionary<string, AggregateBy>? AggregateBy
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableClass<FrozenDictionary<string, AggregateBy>>(
                "aggregate_by"
            );
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawBodyData.Set<FrozenDictionary<string, AggregateBy>?>(
                "aggregate_by",
                value == null ? null : FrozenDictionary.ToFrozenDictionary(value)
            );
        }
    }

    /// <summary>
    /// The consistency level for a query.
    /// </summary>
    public NamespaceQueryParamsConsistency? Consistency
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableClass<NamespaceQueryParamsConsistency>(
                "consistency"
            );
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawBodyData.Set("consistency", value);
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
    /// List of attribute names to exclude from the response. All other attributes
    /// will be included in the response.
    /// </summary>
    public IReadOnlyList<string>? ExcludeAttributes
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableStruct<ImmutableArray<string>>(
                "exclude_attributes"
            );
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawBodyData.Set<ImmutableArray<string>?>(
                "exclude_attributes",
                value == null ? null : ImmutableArray.ToImmutableArray(value)
            );
        }
    }

    /// <summary>
    /// Exact filters for attributes to refine search results for. Think of it as
    /// a SQL WHERE clause.
    /// </summary>
    public Filter? Filters
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableClass<Filter>("filters");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawBodyData.Set("filters", value);
        }
    }

    /// <summary>
    /// Groups documents by the specified attributes (the "group key") before computing
    /// aggregates. Aggregates are computed separately for each group.
    /// </summary>
    public IReadOnlyList<GroupBy>? GroupBy
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableStruct<ImmutableArray<GroupBy>>("group_by");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawBodyData.Set<ImmutableArray<GroupBy>?>(
                "group_by",
                value == null ? null : ImmutableArray.ToImmutableArray(value)
            );
        }
    }

    /// <summary>
    /// Whether to include attributes in the response.
    /// </summary>
    public IncludeAttributes? IncludeAttributes
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableClass<IncludeAttributes>("include_attributes");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawBodyData.Set("include_attributes", value);
        }
    }

    /// <summary>
    /// Limits the documents returned by a query.
    /// </summary>
    public NamespaceQueryParamsLimit? Limit
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableClass<NamespaceQueryParamsLimit>("limit");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawBodyData.Set("limit", value);
        }
    }

    /// <summary>
    /// How to rank the documents in the namespace.
    /// </summary>
    public RankBy? RankBy
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableClass<RankBy>("rank_by");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawBodyData.Set("rank_by", value);
        }
    }

    /// <summary>
    /// The number of results to return.
    /// </summary>
    public long? TopK
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableStruct<long>("top_k");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawBodyData.Set("top_k", value);
        }
    }

    /// <summary>
    /// The encoding to use for vectors in the response.
    /// </summary>
    public ApiEnum<string, VectorEncoding>? VectorEncoding
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNullableClass<ApiEnum<string, VectorEncoding>>(
                "vector_encoding"
            );
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawBodyData.Set("vector_encoding", value);
        }
    }

    public NamespaceQueryParams() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public NamespaceQueryParams(NamespaceQueryParams namespaceQueryParams)
        : base(namespaceQueryParams)
    {
        this.Namespace = namespaceQueryParams.Namespace;

        this._rawBodyData = new(namespaceQueryParams._rawBodyData);
    }
#pragma warning restore CS8618

    public NamespaceQueryParams(
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
    NamespaceQueryParams(
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
    public static NamespaceQueryParams FromRawUnchecked(
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

    public virtual bool Equals(NamespaceQueryParams? other)
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
                + string.Format("/v2/namespaces/{0}/query", this.Namespace)
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
/// The consistency level for a query.
/// </summary>
[JsonConverter(
    typeof(JsonModelConverter<
        NamespaceQueryParamsConsistency,
        NamespaceQueryParamsConsistencyFromRaw
    >)
)]
public sealed record class NamespaceQueryParamsConsistency : JsonModel
{
    /// <summary>
    /// The query's consistency level.
    /// </summary>
    public ApiEnum<string, NamespaceQueryParamsConsistencyLevel>? Level
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<
                ApiEnum<string, NamespaceQueryParamsConsistencyLevel>
            >("level");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("level", value);
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        this.Level?.Validate();
    }

    public NamespaceQueryParamsConsistency() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public NamespaceQueryParamsConsistency(
        NamespaceQueryParamsConsistency namespaceQueryParamsConsistency
    )
        : base(namespaceQueryParamsConsistency) { }
#pragma warning restore CS8618

    public NamespaceQueryParamsConsistency(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    NamespaceQueryParamsConsistency(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="NamespaceQueryParamsConsistencyFromRaw.FromRawUnchecked"/>
    public static NamespaceQueryParamsConsistency FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class NamespaceQueryParamsConsistencyFromRaw : IFromRawJson<NamespaceQueryParamsConsistency>
{
    /// <inheritdoc/>
    public NamespaceQueryParamsConsistency FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => NamespaceQueryParamsConsistency.FromRawUnchecked(rawData);
}

/// <summary>
/// The query's consistency level.
/// </summary>
[JsonConverter(typeof(NamespaceQueryParamsConsistencyLevelConverter))]
public enum NamespaceQueryParamsConsistencyLevel
{
    /// <summary>
    /// Strong consistency. Requires a round-trip to object storage to fetch the latest writes.
    /// </summary>
    Strong,

    /// <summary>
    /// Eventual consistency. Does not require a round-trip to object storage, but
    /// may not see the latest writes.
    /// </summary>
    Eventual,
}

sealed class NamespaceQueryParamsConsistencyLevelConverter
    : JsonConverter<NamespaceQueryParamsConsistencyLevel>
{
    public override NamespaceQueryParamsConsistencyLevel Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<string>(ref reader, options) switch
        {
            "strong" => NamespaceQueryParamsConsistencyLevel.Strong,
            "eventual" => NamespaceQueryParamsConsistencyLevel.Eventual,
            _ => (NamespaceQueryParamsConsistencyLevel)(-1),
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        NamespaceQueryParamsConsistencyLevel value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            value switch
            {
                NamespaceQueryParamsConsistencyLevel.Strong => "strong",
                NamespaceQueryParamsConsistencyLevel.Eventual => "eventual",
                _ => throw new TurbopufferInvalidDataException(
                    string.Format("Invalid value '{0}' in {1}", value, nameof(value))
                ),
            },
            options
        );
    }
}

/// <summary>
/// Limits the documents returned by a query.
/// </summary>
[JsonConverter(typeof(NamespaceQueryParamsLimitConverter))]
public record class NamespaceQueryParamsLimit : ModelBase
{
    public object? Value { get; } = null;

    JsonElement? _element = null;

    public JsonElement Json
    {
        get
        {
            return this._element ??= JsonSerializer.SerializeToElement(
                this.Value,
                ModelBase.SerializerOptions
            );
        }
    }

    public NamespaceQueryParamsLimit(long value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public NamespaceQueryParamsLimit(NamespaceLimit value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public NamespaceQueryParamsLimit(JsonElement element)
    {
        this._element = element;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="long"/>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickLong(out var value)) {
    ///     // `value` is of type `long`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickLong([NotNullWhen(true)] out long? value)
    {
        value = this.Value as long?;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="NamespaceLimit"/>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickLimit(out var value)) {
    ///     // `value` is of type `NamespaceLimit`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickLimit([NotNullWhen(true)] out NamespaceLimit? value)
    {
        value = this.Value as NamespaceLimit;
        return value != null;
    }

    /// <summary>
    /// Calls the function parameter corresponding to the variant the instance was constructed with.
    ///
    /// <para>Use the <c>TryPick</c> method(s) if you don't need to handle every variant, or <see cref="Match"/>
    /// if you need your function parameters to return something.</para>
    ///
    /// <exception cref="TurbopufferInvalidDataException">
    /// Thrown when the instance was constructed with an unknown variant (e.g. deserialized from raw data
    /// that doesn't match any variant's expected shape).
    /// </exception>
    ///
    /// <example>
    /// <code>
    /// instance.Switch(
    ///     (long value) =&gt; {...},
    ///     (NamespaceLimit value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(Action<long> @long, Action<NamespaceLimit> limit)
    {
        switch (this.Value)
        {
            case long value:
                @long(value);
                break;
            case NamespaceLimit value:
                limit(value);
                break;
            default:
                throw new TurbopufferInvalidDataException(
                    "Data did not match any variant of NamespaceQueryParamsLimit"
                );
        }
    }

    /// <summary>
    /// Calls the function parameter corresponding to the variant the instance was constructed with and
    /// returns its result.
    ///
    /// <para>Use the <c>TryPick</c> method(s) if you don't need to handle every variant, or <see cref="Switch"/>
    /// if you don't need your function parameters to return a value.</para>
    ///
    /// <exception cref="TurbopufferInvalidDataException">
    /// Thrown when the instance was constructed with an unknown variant (e.g. deserialized from raw data
    /// that doesn't match any variant's expected shape).
    /// </exception>
    ///
    /// <example>
    /// <code>
    /// var result = instance.Match(
    ///     (long value) =&gt; {...},
    ///     (NamespaceLimit value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(Func<long, T> @long, Func<NamespaceLimit, T> limit)
    {
        return this.Value switch
        {
            long value => @long(value),
            NamespaceLimit value => limit(value),
            _ => throw new TurbopufferInvalidDataException(
                "Data did not match any variant of NamespaceQueryParamsLimit"
            ),
        };
    }

    public static implicit operator NamespaceQueryParamsLimit(long value) => new(value);

    public static implicit operator NamespaceQueryParamsLimit(NamespaceLimit value) => new(value);

    /// <summary>
    /// Validates that the instance was constructed with a known variant and that this variant is valid
    /// (based on its own <c>Validate</c> method).
    ///
    /// <para>This is useful for instances constructed from raw JSON data (e.g. deserialized from an API response).</para>
    ///
    /// <exception cref="TurbopufferInvalidDataException">
    /// Thrown when the instance does not pass validation.
    /// </exception>
    /// </summary>
    public override void Validate()
    {
        if (this.Value == null)
        {
            throw new TurbopufferInvalidDataException(
                "Data did not match any variant of NamespaceQueryParamsLimit"
            );
        }
        this.Switch((_) => { }, (limit) => limit.Validate());
    }

    public virtual bool Equals(NamespaceQueryParamsLimit? other) =>
        other != null
        && this.VariantIndex() == other.VariantIndex()
        && JsonElement.DeepEquals(this.Json, other.Json);

    public override int GetHashCode()
    {
        return 0;
    }

    public override string ToString() =>
        JsonSerializer.Serialize(
            FriendlyJsonPrinter.PrintValue(this.Json),
            ModelBase.ToStringSerializerOptions
        );

    int VariantIndex()
    {
        return this.Value switch
        {
            long _ => 0,
            NamespaceLimit _ => 1,
            _ => -1,
        };
    }
}

sealed class NamespaceQueryParamsLimitConverter : JsonConverter<NamespaceQueryParamsLimit>
{
    public override NamespaceQueryParamsLimit? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var element = JsonSerializer.Deserialize<JsonElement>(ref reader, options);
        try
        {
            var deserialized = JsonSerializer.Deserialize<NamespaceLimit>(element, options);
            if (deserialized != null)
            {
                deserialized.Validate();
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is TurbopufferInvalidDataException)
        {
            // ignore
        }

        try
        {
            return new(JsonSerializer.Deserialize<long>(element, options), element);
        }
        catch (Exception e) when (e is JsonException || e is TurbopufferInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(
        Utf8JsonWriter writer,
        NamespaceQueryParamsLimit value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value.Json, options);
    }
}
