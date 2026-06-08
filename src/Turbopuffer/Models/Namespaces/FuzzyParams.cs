using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Core;

namespace Turbopuffer.Models.Namespaces;

/// <summary>
/// Additional parameters for the Fuzzy filter.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<FuzzyParams, FuzzyParamsFromRaw>))]
public sealed record class FuzzyParams : JsonModel
{
    /// <summary>
    /// Maximum edit distance allowed at each query length. Queries shorter than the
    /// first threshold return no matches.
    /// </summary>
    public required IReadOnlyList<FuzzyMaxEditDistance> MaxEditDistance
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<ImmutableArray<FuzzyMaxEditDistance>>(
                "max_edit_distance"
            );
        }
        init
        {
            this._rawData.Set<ImmutableArray<FuzzyMaxEditDistance>>(
                "max_edit_distance",
                ImmutableArray.ToImmutableArray(value)
            );
        }
    }

    /// <summary>
    /// Whether searching with Fuzzy filter is case-sensitive. Defaults to `true`
    /// (i.e. case-sensitive).
    /// </summary>
    public bool? CaseSensitive
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<bool>("case_sensitive");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("case_sensitive", value);
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        foreach (var item in this.MaxEditDistance)
        {
            item.Validate();
        }
        _ = this.CaseSensitive;
    }

    public FuzzyParams() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public FuzzyParams(FuzzyParams fuzzyParams)
        : base(fuzzyParams) { }
#pragma warning restore CS8618

    public FuzzyParams(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    FuzzyParams(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="FuzzyParamsFromRaw.FromRawUnchecked"/>
    public static FuzzyParams FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }

    [SetsRequiredMembers]
    public FuzzyParams(IReadOnlyList<FuzzyMaxEditDistance> maxEditDistance)
        : this()
    {
        this.MaxEditDistance = maxEditDistance;
    }
}

class FuzzyParamsFromRaw : IFromRawJson<FuzzyParams>
{
    /// <inheritdoc/>
    public FuzzyParams FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        FuzzyParams.FromRawUnchecked(rawData);
}
