using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Core;
using Turbopuffer.Exceptions;

namespace Turbopuffer.Models.Namespaces;

/// <summary>
/// Detailed configuration for an attribute attached to a document.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<AttributeSchemaConfig, AttributeSchemaConfigFromRaw>))]
public sealed record class AttributeSchemaConfig : JsonModel
{
    /// <summary>
    /// The data type of the attribute. Valid values: string, int, uint, float, uuid,
    /// datetime, bool, []string, []int, []uint, []float, []uuid, []datetime, []bool,
    /// [DIMS]f16, [DIMS]f32, {}f16.
    /// </summary>
    public required string Type
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<string>("type");
        }
        init { this._rawData.Set("type", value); }
    }

    /// <summary>
    /// Whether to create an approximate nearest neighbor index for the attribute.
    /// Can be a boolean or a detailed configuration object.
    /// </summary>
    public Ann? Ann
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<Ann>("ann");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("ann", value);
        }
    }

    /// <summary>
    /// Whether to automatically embed this string attribute into a vector attribute.
    /// Can be a model name, a detailed configuration object, or `null` to remove
    /// an existing embedding configuration.
    /// </summary>
    public AttributeEmbed? Embed
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<AttributeEmbed>("embed");
        }
        init { this._rawData.Set("embed", value); }
    }

    /// <summary>
    /// Whether or not the attributes can be used in filters.
    /// </summary>
    public bool? Filterable
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<bool>("filterable");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("filterable", value);
        }
    }

    /// <summary>
    /// Whether this attribute can be used as part of a BM25 full-text search. Requires
    /// the `string` or `[]string` type, and by default, BM25-enabled attributes
    /// are not filterable. You can override this by setting `filterable: true`.
    /// </summary>
    public FullTextSearch? FullTextSearch
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<FullTextSearch>("full_text_search");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("full_text_search", value);
        }
    }

    /// <summary>
    /// Whether to enable Fuzzy filters on this attribute.
    /// </summary>
    public bool? Fuzzy
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<bool>("fuzzy");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("fuzzy", value);
        }
    }

    /// <summary>
    /// Whether to enable Glob filters on this attribute.
    /// </summary>
    public bool? Glob
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<bool>("glob");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("glob", value);
        }
    }

    /// <summary>
    /// Whether to enable Regex filters on this attribute.
    /// </summary>
    public bool? Regex
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<bool>("regex");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("regex", value);
        }
    }

    /// <summary>
    /// Whether to create a sparse kNN index for the attribute. Requires the `{}f16` type.
    /// </summary>
    public SparseKnn? SparseKnn
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<SparseKnn>("sparse_knn");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("sparse_knn", value);
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.Type;
        this.Ann?.Validate();
        this.Embed?.Validate();
        _ = this.Filterable;
        this.FullTextSearch?.Validate();
        _ = this.Fuzzy;
        _ = this.Glob;
        _ = this.Regex;
        this.SparseKnn?.Validate();
    }

    public AttributeSchemaConfig() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public AttributeSchemaConfig(AttributeSchemaConfig attributeSchemaConfig)
        : base(attributeSchemaConfig) { }
#pragma warning restore CS8618

    public AttributeSchemaConfig(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    AttributeSchemaConfig(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="AttributeSchemaConfigFromRaw.FromRawUnchecked"/>
    public static AttributeSchemaConfig FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }

    [SetsRequiredMembers]
    public AttributeSchemaConfig(string type)
        : this()
    {
        this.Type = type;
    }
}

class AttributeSchemaConfigFromRaw : IFromRawJson<AttributeSchemaConfig>
{
    /// <inheritdoc/>
    public AttributeSchemaConfig FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => AttributeSchemaConfig.FromRawUnchecked(rawData);
}

/// <summary>
/// Whether to create an approximate nearest neighbor index for the attribute. Can
/// be a boolean or a detailed configuration object.
/// </summary>
[JsonConverter(typeof(AnnConverter))]
public record class Ann : ModelBase
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

    public Ann(bool value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public Ann(AnnConfig value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public Ann(JsonElement element)
    {
        this._element = element;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="bool"/>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickBool(out var value)) {
    ///     // `value` is of type `bool`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickBool([NotNullWhen(true)] out bool? value)
    {
        value = this.Value as bool?;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="AnnConfig"/>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickConfig(out var value)) {
    ///     // `value` is of type `AnnConfig`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickConfig([NotNullWhen(true)] out AnnConfig? value)
    {
        value = this.Value as AnnConfig;
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
    ///     (bool value) =&gt; {...},
    ///     (AnnConfig value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(Action<bool> @bool, Action<AnnConfig> config)
    {
        switch (this.Value)
        {
            case bool value:
                @bool(value);
                break;
            case AnnConfig value:
                config(value);
                break;
            default:
                throw new TurbopufferInvalidDataException("Data did not match any variant of Ann");
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
    ///     (bool value) =&gt; {...},
    ///     (AnnConfig value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(Func<bool, T> @bool, Func<AnnConfig, T> config)
    {
        return this.Value switch
        {
            bool value => @bool(value),
            AnnConfig value => config(value),
            _ => throw new TurbopufferInvalidDataException("Data did not match any variant of Ann"),
        };
    }

    public static implicit operator Ann(bool value) => new(value);

    public static implicit operator Ann(AnnConfig value) => new(value);

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
            throw new TurbopufferInvalidDataException("Data did not match any variant of Ann");
        }
        this.Switch((_) => { }, (config) => config.Validate());
    }

    public virtual bool Equals(Ann? other) =>
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
            bool _ => 0,
            AnnConfig _ => 1,
            _ => -1,
        };
    }
}

sealed class AnnConverter : JsonConverter<Ann>
{
    public override Ann? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var element = JsonSerializer.Deserialize<JsonElement>(ref reader, options);
        try
        {
            var deserialized = JsonSerializer.Deserialize<AnnConfig>(element, options);
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
            return new(JsonSerializer.Deserialize<bool>(element, options), element);
        }
        catch (Exception e) when (e is JsonException || e is TurbopufferInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(Utf8JsonWriter writer, Ann value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.Json, options);
    }
}

/// <summary>
/// Configuration options for ANN (Approximate Nearest Neighbor) indexing.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<AnnConfig, AnnConfigFromRaw>))]
public sealed record class AnnConfig : JsonModel
{
    /// <summary>
    /// A function used to calculate vector similarity.
    /// </summary>
    public ApiEnum<string, DistanceMetric>? DistanceMetric
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<ApiEnum<string, DistanceMetric>>(
                "distance_metric"
            );
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("distance_metric", value);
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        this.DistanceMetric?.Validate();
    }

    public AnnConfig() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public AnnConfig(AnnConfig annConfig)
        : base(annConfig) { }
#pragma warning restore CS8618

    public AnnConfig(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    AnnConfig(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="AnnConfigFromRaw.FromRawUnchecked"/>
    public static AnnConfig FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class AnnConfigFromRaw : IFromRawJson<AnnConfig>
{
    /// <inheritdoc/>
    public AnnConfig FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        AnnConfig.FromRawUnchecked(rawData);
}

/// <summary>
/// Whether to create a sparse kNN index for the attribute. Requires the `{}f16` type.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<SparseKnn, SparseKnnFromRaw>))]
public sealed record class SparseKnn : JsonModel
{
    /// <summary>
    /// A function used to calculate sparse vector similarity.
    /// </summary>
    public required ApiEnum<string, SparseDistanceMetric> DistanceMetric
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<ApiEnum<string, SparseDistanceMetric>>(
                "distance_metric"
            );
        }
        init { this._rawData.Set("distance_metric", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        this.DistanceMetric.Validate();
    }

    public SparseKnn() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public SparseKnn(SparseKnn sparseKnn)
        : base(sparseKnn) { }
#pragma warning restore CS8618

    public SparseKnn(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    SparseKnn(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="SparseKnnFromRaw.FromRawUnchecked"/>
    public static SparseKnn FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }

    [SetsRequiredMembers]
    public SparseKnn(ApiEnum<string, SparseDistanceMetric> distanceMetric)
        : this()
    {
        this.DistanceMetric = distanceMetric;
    }
}

class SparseKnnFromRaw : IFromRawJson<SparseKnn>
{
    /// <inheritdoc/>
    public SparseKnn FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        SparseKnn.FromRawUnchecked(rawData);
}
