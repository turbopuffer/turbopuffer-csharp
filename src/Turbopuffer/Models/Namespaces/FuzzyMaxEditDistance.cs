using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Core;

namespace Turbopuffer.Models.Namespaces;

/// <summary>
/// An edit distance threshold for the Fuzzy filter.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<FuzzyMaxEditDistance, FuzzyMaxEditDistanceFromRaw>))]
public sealed record class FuzzyMaxEditDistance : JsonModel
{
    /// <summary>
    /// The maximum edit distance to allow.
    /// </summary>
    public required long Distance
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<long>("distance");
        }
        init { this._rawData.Set("distance", value); }
    }

    /// <summary>
    /// Minimum number of characters in a query where this distance applies. Must
    /// be at least 3 · (distance + 1).
    /// </summary>
    public required long MinQueryChars
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<long>("min_query_chars");
        }
        init { this._rawData.Set("min_query_chars", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.Distance;
        _ = this.MinQueryChars;
    }

    public FuzzyMaxEditDistance() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public FuzzyMaxEditDistance(FuzzyMaxEditDistance fuzzyMaxEditDistance)
        : base(fuzzyMaxEditDistance) { }
#pragma warning restore CS8618

    public FuzzyMaxEditDistance(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    FuzzyMaxEditDistance(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="FuzzyMaxEditDistanceFromRaw.FromRawUnchecked"/>
    public static FuzzyMaxEditDistance FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class FuzzyMaxEditDistanceFromRaw : IFromRawJson<FuzzyMaxEditDistance>
{
    /// <inheritdoc/>
    public FuzzyMaxEditDistance FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => FuzzyMaxEditDistance.FromRawUnchecked(rawData);
}
