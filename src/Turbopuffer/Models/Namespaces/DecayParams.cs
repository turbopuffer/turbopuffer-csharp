using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Core;

namespace Turbopuffer.Models.Namespaces;

/// <summary>
/// Additional parameters for the Decay operator.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<DecayParams, DecayParamsFromRaw>))]
public sealed record class DecayParams : JsonModel
{
    /// <summary>
    /// An exponent that helps further control the shape of the Decay function.
    /// </summary>
    public double? Exponent
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<double>("exponent");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("exponent", value);
        }
    }

    /// <summary>
    /// The midpoint of the Decay operator.
    /// </summary>
    public JsonElement? Midpoint
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<JsonElement>("midpoint");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("midpoint", value);
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.Exponent;
        _ = this.Midpoint;
    }

    public DecayParams() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public DecayParams(DecayParams decayParams)
        : base(decayParams) { }
#pragma warning restore CS8618

    public DecayParams(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    DecayParams(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="DecayParamsFromRaw.FromRawUnchecked"/>
    public static DecayParams FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class DecayParamsFromRaw : IFromRawJson<DecayParams>
{
    /// <inheritdoc/>
    public DecayParams FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        DecayParams.FromRawUnchecked(rawData);
}
