using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Core;

namespace Turbopuffer.Models.Namespaces;

/// <summary>
/// Additional (optional) parameters for a single BM25 query clause.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<Bm25ClauseParams, Bm25ClauseParamsFromRaw>))]
public sealed record class Bm25ClauseParams : JsonModel
{
    /// <summary>
    /// Whether to treat the last token in the query input as a literal prefix.
    /// </summary>
    public bool? LastAsPrefix
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<bool>("last_as_prefix");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("last_as_prefix", value);
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.LastAsPrefix;
    }

    public Bm25ClauseParams() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public Bm25ClauseParams(Bm25ClauseParams bm25ClauseParams)
        : base(bm25ClauseParams) { }
#pragma warning restore CS8618

    public Bm25ClauseParams(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    Bm25ClauseParams(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="Bm25ClauseParamsFromRaw.FromRawUnchecked"/>
    public static Bm25ClauseParams FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class Bm25ClauseParamsFromRaw : IFromRawJson<Bm25ClauseParams>
{
    /// <inheritdoc/>
    public Bm25ClauseParams FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        Bm25ClauseParams.FromRawUnchecked(rawData);
}
