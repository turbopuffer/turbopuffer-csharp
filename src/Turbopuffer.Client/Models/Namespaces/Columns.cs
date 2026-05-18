using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Client.Core;
using Turbopuffer.Client.Exceptions;

namespace Turbopuffer.Client.Models.Namespaces;

/// <summary>
/// A list of documents in columnar format. Each key is a column name, mapped to an
/// array of values for that column.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<Columns, ColumnsFromRaw>))]
public sealed record class Columns : JsonModel
{
    /// <summary>
    /// The IDs of the documents.
    /// </summary>
    public required IReadOnlyList<ID> ID
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<ImmutableArray<ID>>("id");
        }
        init
        {
            this._rawData.Set<ImmutableArray<ID>>("id", ImmutableArray.ToImmutableArray(value));
        }
    }

    /// <summary>
    /// The vector embeddings of the documents.
    /// </summary>
    public Vector? Vector
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<Vector>("vector");
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
        foreach (var item in this.ID)
        {
            item.Validate();
        }
        this.Vector?.Validate();
    }

    public Columns() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public Columns(Columns columns)
        : base(columns) { }
#pragma warning restore CS8618

    public Columns(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    Columns(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="ColumnsFromRaw.FromRawUnchecked"/>
    public static Columns FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }

    [SetsRequiredMembers]
    public Columns(IReadOnlyList<ID> id)
        : this()
    {
        this.ID = id;
    }
}

class ColumnsFromRaw : IFromRawJson<Columns>
{
    /// <inheritdoc/>
    public Columns FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        Columns.FromRawUnchecked(rawData);
}

/// <summary>
/// The vector embeddings of the documents.
/// </summary>
[JsonConverter(typeof(VectorConverter))]
public record class Vector : ModelBase
{
    public object? Value { get; } = null;

    JsonElement? _element = null;

    public JsonElement Json
    {
        get
        {
            return this._element ??= JsonSerializer.SerializeToElement(
                this.Value,
                ModelBase.SerializerOptions
            );
        }
    }

    public Vector(IReadOnlyList<NamespaceVector> value, JsonElement? element = null)
    {
        this.Value = ImmutableArray.ToImmutableArray(value);
        this._element = element;
    }

    public Vector(NamespaceVector value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public Vector(JsonElement element)
    {
        this._element = element;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="List{T}"/> where <c>T</c> is a <c>NamespaceVector</c>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickNamespaceVectors(out var value)) {
    ///     // `value` is of type `IReadOnlyList&lt;NamespaceVector&gt;`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickNamespaceVectors(
        [NotNullWhen(true)] out IReadOnlyList<NamespaceVector>? value
    )
    {
        value = this.Value as IReadOnlyList<NamespaceVector>;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="NamespaceVector"/>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickVector(out var value)) {
    ///     // `value` is of type `NamespaceVector`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickVector([NotNullWhen(true)] out NamespaceVector? value)
    {
        value = this.Value as NamespaceVector;
        return value != null;
    }

    /// <summary>
    /// Calls the function parameter corresponding to the variant the instance was constructed with.
    ///
    /// <para>Use the <c>TryPick</c> method(s) if you don't need to handle every variant, or <see cref="Match"/>
    /// if you need your function parameters to return something.</para>
    ///
    /// <exception cref="TurbopufferInvalidDataException">
    /// Thrown when the instance was constructed with an unknown variant (e.g. deserialized from raw data
    /// that doesn't match any variant's expected shape).
    /// </exception>
    ///
    /// <example>
    /// <code>
    /// instance.Switch(
    ///     (IReadOnlyList&lt;NamespaceVector&gt; value) =&gt; {...},
    ///     (NamespaceVector value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(
        Action<IReadOnlyList<NamespaceVector>> namespaceVectors,
        Action<NamespaceVector> vector
    )
    {
        switch (this.Value)
        {
            case IReadOnlyList<NamespaceVector> value:
                namespaceVectors(value);
                break;
            case NamespaceVector value:
                vector(value);
                break;
            default:
                throw new TurbopufferInvalidDataException(
                    "Data did not match any variant of Vector"
                );
        }
    }

    /// <summary>
    /// Calls the function parameter corresponding to the variant the instance was constructed with and
    /// returns its result.
    ///
    /// <para>Use the <c>TryPick</c> method(s) if you don't need to handle every variant, or <see cref="Switch"/>
    /// if you don't need your function parameters to return a value.</para>
    ///
    /// <exception cref="TurbopufferInvalidDataException">
    /// Thrown when the instance was constructed with an unknown variant (e.g. deserialized from raw data
    /// that doesn't match any variant's expected shape).
    /// </exception>
    ///
    /// <example>
    /// <code>
    /// var result = instance.Match(
    ///     (IReadOnlyList&lt;NamespaceVector&gt; value) =&gt; {...},
    ///     (NamespaceVector value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(
        Func<IReadOnlyList<NamespaceVector>, T> namespaceVectors,
        Func<NamespaceVector, T> vector
    )
    {
        return this.Value switch
        {
            IReadOnlyList<NamespaceVector> value => namespaceVectors(value),
            NamespaceVector value => vector(value),
            _ => throw new TurbopufferInvalidDataException(
                "Data did not match any variant of Vector"
            ),
        };
    }

    public static implicit operator Vector(List<NamespaceVector> value) =>
        new((IReadOnlyList<NamespaceVector>)value);

    public static implicit operator Vector(NamespaceVector value) => new(value);

    /// <summary>
    /// Validates that the instance was constructed with a known variant and that this variant is valid
    /// (based on its own <c>Validate</c> method).
    ///
    /// <para>This is useful for instances constructed from raw JSON data (e.g. deserialized from an API response).</para>
    ///
    /// <exception cref="TurbopufferInvalidDataException">
    /// Thrown when the instance does not pass validation.
    /// </exception>
    /// </summary>
    public override void Validate()
    {
        if (this.Value == null)
        {
            throw new TurbopufferInvalidDataException("Data did not match any variant of Vector");
        }
        this.Switch(
            (namespaceVectors) =>
            {
                foreach (var item in namespaceVectors)
                {
                    item.Validate();
                }
            },
            (vector) => vector.Validate()
        );
    }

    public virtual bool Equals(Vector? other) =>
        other != null
        && this.VariantIndex() == other.VariantIndex()
        && JsonElement.DeepEquals(this.Json, other.Json);

    public override int GetHashCode()
    {
        return 0;
    }

    public override string ToString() =>
        JsonSerializer.Serialize(
            FriendlyJsonPrinter.PrintValue(this.Json),
            ModelBase.ToStringSerializerOptions
        );

    int VariantIndex()
    {
        return this.Value switch
        {
            IReadOnlyList<NamespaceVector> _ => 0,
            NamespaceVector _ => 1,
            _ => -1,
        };
    }
}

sealed class VectorConverter : JsonConverter<Vector>
{
    public override Vector? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var element = JsonSerializer.Deserialize<JsonElement>(ref reader, options);
        try
        {
            var deserialized = JsonSerializer.Deserialize<NamespaceVector>(element, options);
            if (deserialized != null)
            {
                deserialized.Validate();
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is TurbopufferInvalidDataException)
        {
            // ignore
        }

        try
        {
            var deserialized = JsonSerializer.Deserialize<List<NamespaceVector>>(element, options);
            if (deserialized != null)
            {
                foreach (var item in deserialized)
                {
                    item.Validate();
                }
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is TurbopufferInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(Utf8JsonWriter writer, Vector value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.Json, options);
    }
}
