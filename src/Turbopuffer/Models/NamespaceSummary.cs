using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Core;

namespace Turbopuffer.Models;

/// <summary>
/// A summary of a namespace.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<NamespaceSummary, NamespaceSummaryFromRaw>))]
public sealed record class NamespaceSummary : JsonModel
{
    /// <summary>
    /// The namespace ID.
    /// </summary>
    public required string ID
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<string>("id");
        }
        init { this._rawData.Set("id", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.ID;
    }

    public NamespaceSummary() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public NamespaceSummary(NamespaceSummary namespaceSummary)
        : base(namespaceSummary) { }
#pragma warning restore CS8618

    public NamespaceSummary(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    NamespaceSummary(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="NamespaceSummaryFromRaw.FromRawUnchecked"/>
    public static NamespaceSummary FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }

    [SetsRequiredMembers]
    public NamespaceSummary(string id)
        : this()
    {
        this.ID = id;
    }
}

class NamespaceSummaryFromRaw : IFromRawJson<NamespaceSummary>
{
    /// <inheritdoc/>
    public NamespaceSummary FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        NamespaceSummary.FromRawUnchecked(rawData);
}
