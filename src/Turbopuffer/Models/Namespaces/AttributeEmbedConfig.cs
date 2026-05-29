using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Core;

namespace Turbopuffer.Models.Namespaces;

/// <summary>
/// Configuration options for automatic embedding.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<AttributeEmbedConfig, AttributeEmbedConfigFromRaw>))]
public sealed record class AttributeEmbedConfig : JsonModel
{
    /// <summary>
    /// The model to use for embedding. See our documentation for a list of models
    /// supported in each region.
    /// </summary>
    public required string Model
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<string>("model");
        }
        init { this._rawData.Set("model", value); }
    }

    /// <summary>
    /// The name of an existing vector attribute to store embeddings in. If omitted,
    /// turbopuffer will generate a computed vector attribute named `$embed_&lt;attribute&gt;`.
    /// </summary>
    public string? Attribute
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("attribute");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("attribute", value);
        }
    }

    /// <summary>
    /// The dimensionality to embed at. If not set, will pick the default for this
    /// model. If you're storing embeddings in an existing attribute, this can be
    /// omitted, and may not be set to a value other than the dimensions of that attribute.
    /// </summary>
    public long? Dims
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<long>("dims");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("dims", value);
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.Model;
        _ = this.Attribute;
        _ = this.Dims;
    }

    public AttributeEmbedConfig() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public AttributeEmbedConfig(AttributeEmbedConfig attributeEmbedConfig)
        : base(attributeEmbedConfig) { }
#pragma warning restore CS8618

    public AttributeEmbedConfig(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    AttributeEmbedConfig(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="AttributeEmbedConfigFromRaw.FromRawUnchecked"/>
    public static AttributeEmbedConfig FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }

    [SetsRequiredMembers]
    public AttributeEmbedConfig(string model)
        : this()
    {
        this.Model = model;
    }
}

class AttributeEmbedConfigFromRaw : IFromRawJson<AttributeEmbedConfig>
{
    /// <inheritdoc/>
    public AttributeEmbedConfig FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => AttributeEmbedConfig.FromRawUnchecked(rawData);
}
