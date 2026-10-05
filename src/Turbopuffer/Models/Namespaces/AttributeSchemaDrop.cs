using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Core;

namespace Turbopuffer.Models.Namespaces;

/// <summary>
/// Drops the attribute from the namespace. Cannot be combined with other schema settings.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<AttributeSchemaDrop, AttributeSchemaDropFromRaw>))]
public sealed record class AttributeSchemaDrop : JsonModel
{
    /// <summary>
    /// Must be `true`.
    /// </summary>
    public required bool Drop
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<bool>("drop");
        }
        init { this._rawData.Set("drop", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.Drop;
    }

    public AttributeSchemaDrop() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public AttributeSchemaDrop(AttributeSchemaDrop attributeSchemaDrop)
        : base(attributeSchemaDrop) { }
#pragma warning restore CS8618

    public AttributeSchemaDrop(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    AttributeSchemaDrop(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="AttributeSchemaDropFromRaw.FromRawUnchecked"/>
    public static AttributeSchemaDrop FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }

    [SetsRequiredMembers]
    public AttributeSchemaDrop(bool drop)
        : this()
    {
        this.Drop = drop;
    }
}

class AttributeSchemaDropFromRaw : IFromRawJson<AttributeSchemaDrop>
{
    /// <inheritdoc/>
    public AttributeSchemaDrop FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        AttributeSchemaDrop.FromRawUnchecked(rawData);
}
