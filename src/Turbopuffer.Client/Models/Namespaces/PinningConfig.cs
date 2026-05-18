using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Client.Core;

namespace Turbopuffer.Client.Models.Namespaces;

/// <summary>
/// Configuration for namespace pinning.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<PinningConfig, PinningConfigFromRaw>))]
public sealed record class PinningConfig : JsonModel
{
    /// <summary>
    /// The number of read replicas to provision. Defaults to 1 if not specified.
    /// </summary>
    public long? Replicas
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<long>("replicas");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("replicas", value);
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.Replicas;
    }

    public PinningConfig() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public PinningConfig(PinningConfig pinningConfig)
        : base(pinningConfig) { }
#pragma warning restore CS8618

    public PinningConfig(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    PinningConfig(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="PinningConfigFromRaw.FromRawUnchecked"/>
    public static PinningConfig FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class PinningConfigFromRaw : IFromRawJson<PinningConfig>
{
    /// <inheritdoc/>
    public PinningConfig FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        PinningConfig.FromRawUnchecked(rawData);
}
