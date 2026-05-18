using System;
using System.Collections;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Core;

namespace Turbopuffer.Models.Namespaces;

/// <summary>
/// A list of documents in columnar format. Each key is a column name, mapped to an
/// array of values for that column.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<Columns, ColumnsFromRaw>))]
public sealed record class Columns : JsonModel
{
    /// <summary>
    /// Gets the raw values for an arbitrary column (including <c>id</c> and
    /// <c>vector</c>), or
    /// <c>null</c> if the column is absent.
    /// </summary>
    public IReadOnlyList<JsonElement>? this[string column]
    {
        get
        {
            this._rawData.Freeze();
            if (!this.RawData.TryGetValue(column, out var element))
            {
                return null;
            }
            if (element.ValueKind != JsonValueKind.Array)
            {
                return null;
            }
            return [.. element.EnumerateArray()];
        }
    }

    /// <summary>
    /// Sets the raw values for an arbitrary column.
    /// </summary>
    public Columns SetColumn(string column, IReadOnlyList<JsonElement> values)
    {
        this._rawData.Set<ImmutableArray<JsonElement>>(
            column,
            ImmutableArray.ToImmutableArray(values)
        );
        return this;
    }

    /// <summary>
    /// A raw map of JSON values with no enforced shape, so validation is always
    /// successful.
    /// </summary>
    public override void Validate() { }

    public Columns() { }

    public Columns(Columns columns)
        : base(columns) { }

    public Columns(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

    Columns(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

    /// <inheritdoc cref="ColumnsFromRaw.FromRawUnchecked"/>
    public static Columns FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class ColumnsFromRaw : IFromRawJson<Columns>
{
    /// <inheritdoc/>
    public Columns FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        Columns.FromRawUnchecked(rawData);
}

/// <summary>
/// An identifier for a document: a raw, unvalidated JSON value (mirrors
/// turbopuffer-java's <c>typealias Id = JsonValue</c>).
/// </summary>
[JsonConverter(typeof(IDConverter))]
public sealed record class ID : ModelBase
{
    readonly JsonElement _element;

    /// <summary>
    /// The raw JSON value backing this identifier.
    /// </summary>
    public JsonElement Json => this._element;

    public ID(JsonElement element)
    {
        this._element = element.Clone();
    }

    public ID(string value)
        : this(JsonSerializer.SerializeToElement(value, ModelBase.SerializerOptions)) { }

    public ID(long value)
        : this(JsonSerializer.SerializeToElement(value, ModelBase.SerializerOptions)) { }

    public ID(Guid value)
        : this(JsonSerializer.SerializeToElement(value, ModelBase.SerializerOptions)) { }

    public static implicit operator ID(string value) => new(value);

    public static implicit operator ID(long value) => new(value);

    public static implicit operator ID(Guid value) => new(value);

    /// <summary>
    /// A raw JSON value has no enforced shape, so validation is always successful.
    /// </summary>
    public override void Validate() { }

    public bool Equals(ID? other) =>
        other != null && JsonElement.DeepEquals(this._element, other._element);

    public override int GetHashCode() => 0;

    public override string ToString() =>
        JsonSerializer.Serialize(
            FriendlyJsonPrinter.PrintValue(this._element),
            ModelBase.ToStringSerializerOptions
        );
}

sealed class IDConverter : JsonConverter<ID>
{
    public override ID? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return new(JsonSerializer.Deserialize<JsonElement>(ref reader, options));
    }

    public override void Write(Utf8JsonWriter writer, ID value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.Json, options);
    }
}

/// <summary>
/// A single document, in a row-based format. A raw string-keyed map of JSON
/// values (mirrors turbopuffer-java's <c>Row : LinkedHashMap</c>).
/// </summary>
[JsonConverter(typeof(JsonModelConverter<Row, RowFromRaw>))]
public sealed record class Row : JsonModel, IEnumerable<KeyValuePair<string, JsonElement>>
{
    /// <summary>
    /// Gets or sets the raw JSON value for an arbitrary key, or <c>null</c> if the
    /// key is absent.
    /// </summary>
    public JsonElement? this[string key]
    {
        get
        {
            this._rawData.Freeze();
            if (!this.RawData.TryGetValue(key, out var element))
            {
                return null;
            }
            return element;
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set(key, value.Value);
        }
    }

    /// <summary>
    /// Adds or replaces the entry for the given key, wrapping the value as a JSON
    /// value.
    /// </summary>
    public Row Set<T>(string key, T value)
    {
        this._rawData.Set(key, value);
        return this;
    }

    /// <summary>
    /// Returns whether the row contains an entry for the given key.
    /// </summary>
    public bool ContainsKey(string key) => this.RawData.ContainsKey(key);

    /// <summary>
    /// The keys present in the row.
    /// </summary>
    public IEnumerable<string> Keys => this.RawData.Keys;

    /// <inheritdoc/>
    public IEnumerator<KeyValuePair<string, JsonElement>> GetEnumerator() =>
        this.RawData.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    /// <summary>
    /// A row is a raw map of JSON values with no enforced shape, so validation is
    /// always successful.
    /// </summary>
    public override void Validate() { }

    public Row() { }

    public Row(Row row)
        : base(row) { }

    public Row(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

    Row(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

    /// <inheritdoc cref="RowFromRaw.FromRawUnchecked"/>
    public static Row FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class RowFromRaw : IFromRawJson<Row>
{
    /// <inheritdoc/>
    public Row FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        Row.FromRawUnchecked(rawData);
}
