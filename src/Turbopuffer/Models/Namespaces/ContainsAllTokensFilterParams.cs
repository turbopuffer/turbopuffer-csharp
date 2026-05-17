using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Core;

namespace Turbopuffer.Models.Namespaces;

/// <summary>
/// Additional (optional) parameters for the ContainsAllTokens filter.
/// </summary>
[JsonConverter(
    typeof(JsonModelConverter<ContainsAllTokensFilterParams, ContainsAllTokensFilterParamsFromRaw>)
)]
public sealed record class ContainsAllTokensFilterParams : JsonModel
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

    public ContainsAllTokensFilterParams() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public ContainsAllTokensFilterParams(
        ContainsAllTokensFilterParams containsAllTokensFilterParams
    )
        : base(containsAllTokensFilterParams) { }
#pragma warning restore CS8618

    public ContainsAllTokensFilterParams(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    ContainsAllTokensFilterParams(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="ContainsAllTokensFilterParamsFromRaw.FromRawUnchecked"/>
    public static ContainsAllTokensFilterParams FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class ContainsAllTokensFilterParamsFromRaw : IFromRawJson<ContainsAllTokensFilterParams>
{
    /// <inheritdoc/>
    public ContainsAllTokensFilterParams FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => ContainsAllTokensFilterParams.FromRawUnchecked(rawData);
}
