using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Core;

namespace Turbopuffer.Models.Namespaces;

/// <summary>
/// Configuration options for RRF.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<RrfParams, RrfParamsFromRaw>))]
public sealed record class RrfParams : JsonModel
{
    /// <summary>
    /// RRF rank constant (`k`). Must be greater than zero. Defaults to `60`.
    /// </summary>
    public long? RankConstant
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<long>("rank_constant");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("rank_constant", value);
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.RankConstant;
    }

    public RrfParams() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public RrfParams(RrfParams rrfParams)
        : base(rrfParams) { }
#pragma warning restore CS8618

    public RrfParams(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    RrfParams(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="RrfParamsFromRaw.FromRawUnchecked"/>
    public static RrfParams FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class RrfParamsFromRaw : IFromRawJson<RrfParams>
{
    /// <inheritdoc/>
    public RrfParams FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        RrfParams.FromRawUnchecked(rawData);
}
