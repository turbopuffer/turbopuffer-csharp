using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Core;

namespace Turbopuffer.Models.Namespaces;

/// <summary>
/// Additional (optional) parameters for the Highlight compute expression.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<HighlightConfig, HighlightConfigFromRaw>))]
public sealed record class HighlightConfig : JsonModel
{
    /// <summary>
    /// How to split a text attribute into fragments for highlighting.
    /// </summary>
    public ApiEnum<string, HighlightFragmentBy>? FragmentBy
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<ApiEnum<string, HighlightFragmentBy>>(
                "fragment_by"
            );
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("fragment_by", value);
        }
    }

    /// <summary>
    /// The maximum number of fragments to return. Defaults to `3`.
    /// </summary>
    public long? FragmentLimit
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<long>("fragment_limit");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("fragment_limit", value);
        }
    }

    /// <summary>
    /// The units to report highlighted fragment offsets in.
    /// </summary>
    public ApiEnum<string, HighlightOffsetUnits>? IncludeOffsets
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<ApiEnum<string, HighlightOffsetUnits>>(
                "include_offsets"
            );
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("include_offsets", value);
        }
    }

    /// <summary>
    /// How to rank candidate fragments within the attribute before selecting the
    /// top `fragment_limit`. Defaults to the query's `rank_by`.
    /// </summary>
    public JsonElement? RankFragmentsBy
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<JsonElement>("rank_fragments_by");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("rank_fragments_by", value);
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        this.FragmentBy?.Validate();
        _ = this.FragmentLimit;
        this.IncludeOffsets?.Validate();
        _ = this.RankFragmentsBy;
    }

    public HighlightConfig() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public HighlightConfig(HighlightConfig highlightConfig)
        : base(highlightConfig) { }
#pragma warning restore CS8618

    public HighlightConfig(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    HighlightConfig(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="HighlightConfigFromRaw.FromRawUnchecked"/>
    public static HighlightConfig FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class HighlightConfigFromRaw : IFromRawJson<HighlightConfig>
{
    /// <inheritdoc/>
    public HighlightConfig FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        HighlightConfig.FromRawUnchecked(rawData);
}
