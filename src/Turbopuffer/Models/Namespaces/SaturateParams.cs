using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Core;

namespace Turbopuffer.Models.Namespaces;

/// <summary>
/// Additional parameters for the Saturate operator.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<SaturateParams, SaturateParamsFromRaw>))]
public sealed record class SaturateParams : JsonModel
{
    /// <summary>
    /// An exponent that helps further control the shape of the Saturate function.
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
    /// The midpoint of the Saturate operator.
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

    public SaturateParams() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public SaturateParams(SaturateParams saturateParams)
        : base(saturateParams) { }
#pragma warning restore CS8618

    public SaturateParams(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    SaturateParams(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="SaturateParamsFromRaw.FromRawUnchecked"/>
    public static SaturateParams FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class SaturateParamsFromRaw : IFromRawJson<SaturateParams>
{
    /// <inheritdoc/>
    public SaturateParams FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        SaturateParams.FromRawUnchecked(rawData);
}
