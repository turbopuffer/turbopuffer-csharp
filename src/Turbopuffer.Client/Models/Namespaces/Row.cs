using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Client.Core;

namespace Turbopuffer.Client.Models.Namespaces;

/// <summary>
/// A single document, in a row-based format.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<Row, RowFromRaw>))]
public sealed record class Row : JsonModel
{
    /// <summary>
    /// An identifier for a document.
    /// </summary>
    public required ID ID
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<ID>("id");
        }
        init { this._rawData.Set("id", value); }
    }

    /// <summary>
    /// A vector embedding associated with a document.
    /// </summary>
    public NamespaceVector? Vector
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<NamespaceVector>("vector");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("vector", value);
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        this.ID.Validate();
        this.Vector?.Validate();
    }

    public Row() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public Row(Row row)
        : base(row) { }
#pragma warning restore CS8618

    public Row(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    Row(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="RowFromRaw.FromRawUnchecked"/>
    public static Row FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }

    [SetsRequiredMembers]
    public Row(ID id)
        : this()
    {
        this.ID = id;
    }
}

class RowFromRaw : IFromRawJson<Row>
{
    /// <inheritdoc/>
    public Row FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        Row.FromRawUnchecked(rawData);
}
