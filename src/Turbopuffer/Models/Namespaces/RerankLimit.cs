using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Core;

namespace Turbopuffer.Models.Namespaces;

/// <summary>
/// Limits the total number of reranked documents returned.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<RerankLimit, RerankLimitFromRaw>))]
public sealed record class RerankLimit : JsonModel
{
    /// <summary>
    /// Limits the total number of documents returned after reranking.
    /// </summary>
    public required long Total
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<long>("total");
        }
        init { this._rawData.Set("total", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.Total;
    }

    public RerankLimit() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public RerankLimit(RerankLimit rerankLimit)
        : base(rerankLimit) { }
#pragma warning restore CS8618

    public RerankLimit(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    RerankLimit(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="RerankLimitFromRaw.FromRawUnchecked"/>
    public static RerankLimit FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }

    [SetsRequiredMembers]
    public RerankLimit(long total)
        : this()
    {
        this.Total = total;
    }
}

class RerankLimitFromRaw : IFromRawJson<RerankLimit>
{
    /// <inheritdoc/>
    public RerankLimit FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        RerankLimit.FromRawUnchecked(rawData);
}
