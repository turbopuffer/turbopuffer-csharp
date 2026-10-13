using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Core;

namespace Turbopuffer.Models.Namespaces;

/// <summary>
/// Rerank retrieved candidates with a model.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<RerankModelParams, RerankModelParamsFromRaw>))]
public sealed record class RerankModelParams : JsonModel
{
    /// <summary>
    /// The string attribute to score. Must be included in `include_attributes` in
    /// every query.
    /// </summary>
    public required string Attribute
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<string>("attribute");
        }
        init { this._rawData.Set("attribute", value); }
    }

    /// <summary>
    /// The reranking model to use, e.g. `voyage/rerank-3`. See https://turbopuffer.com/docs/reranking#models
    /// for the available models.
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
    /// The query text to score each document against.
    /// </summary>
    public required string Query
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<string>("query");
        }
        init { this._rawData.Set("query", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.Attribute;
        _ = this.Model;
        _ = this.Query;
    }

    public RerankModelParams() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public RerankModelParams(RerankModelParams rerankModelParams)
        : base(rerankModelParams) { }
#pragma warning restore CS8618

    public RerankModelParams(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    RerankModelParams(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="RerankModelParamsFromRaw.FromRawUnchecked"/>
    public static RerankModelParams FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class RerankModelParamsFromRaw : IFromRawJson<RerankModelParams>
{
    /// <inheritdoc/>
    public RerankModelParams FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        RerankModelParams.FromRawUnchecked(rawData);
}
