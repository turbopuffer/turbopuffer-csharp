using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Core;

namespace Turbopuffer.Models.Namespaces;

/// <summary>
/// Additional (optional) parameters for the Embed expression.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<EmbedParams, EmbedParamsFromRaw>))]
public sealed record class EmbedParams : JsonModel
{
    /// <summary>
    /// The model to use for embedding, overriding the model configured for the attribute.
    /// </summary>
    public string? Model
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("model");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("model", value);
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.Model;
    }

    public EmbedParams() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public EmbedParams(EmbedParams embedParams)
        : base(embedParams) { }
#pragma warning restore CS8618

    public EmbedParams(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    EmbedParams(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="EmbedParamsFromRaw.FromRawUnchecked"/>
    public static EmbedParams FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class EmbedParamsFromRaw : IFromRawJson<EmbedParams>
{
    /// <inheritdoc/>
    public EmbedParams FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        EmbedParams.FromRawUnchecked(rawData);
}
