using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Client.Core;

namespace Turbopuffer.Client.Models.Namespaces;

/// <summary>
/// Additional (optional) parameters for the ContainsAnyToken filter.
/// </summary>
[JsonConverter(
    typeof(JsonModelConverter<ContainsAnyTokenFilterParams, ContainsAnyTokenFilterParamsFromRaw>)
)]
public sealed record class ContainsAnyTokenFilterParams : JsonModel
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

    public ContainsAnyTokenFilterParams() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public ContainsAnyTokenFilterParams(ContainsAnyTokenFilterParams containsAnyTokenFilterParams)
        : base(containsAnyTokenFilterParams) { }
#pragma warning restore CS8618

    public ContainsAnyTokenFilterParams(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    ContainsAnyTokenFilterParams(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="ContainsAnyTokenFilterParamsFromRaw.FromRawUnchecked"/>
    public static ContainsAnyTokenFilterParams FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class ContainsAnyTokenFilterParamsFromRaw : IFromRawJson<ContainsAnyTokenFilterParams>
{
    /// <inheritdoc/>
    public ContainsAnyTokenFilterParams FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => ContainsAnyTokenFilterParams.FromRawUnchecked(rawData);
}
