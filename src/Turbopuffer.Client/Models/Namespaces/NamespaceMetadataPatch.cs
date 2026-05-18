using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Client.Core;
using Turbopuffer.Client.Exceptions;

namespace Turbopuffer.Client.Models.Namespaces;

/// <summary>
/// Request to update namespace metadata configuration.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<NamespaceMetadataPatch, NamespaceMetadataPatchFromRaw>))]
public sealed record class NamespaceMetadataPatch : JsonModel
{
    /// <summary>
    /// Configuration for namespace pinning. - Missing field: no change to pinning
    /// configuration - `null` or `false`: explicitly remove pinning - `true`: enable
    /// pinning with default configuration - Object: set pinning configuration
    /// </summary>
    public NamespaceMetadataPatchPinning? Pinning
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<NamespaceMetadataPatchPinning>("pinning");
        }
        init { this._rawData.Set("pinning", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        this.Pinning?.Validate();
    }

    public NamespaceMetadataPatch() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public NamespaceMetadataPatch(NamespaceMetadataPatch namespaceMetadataPatch)
        : base(namespaceMetadataPatch) { }
#pragma warning restore CS8618

    public NamespaceMetadataPatch(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    NamespaceMetadataPatch(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="NamespaceMetadataPatchFromRaw.FromRawUnchecked"/>
    public static NamespaceMetadataPatch FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class NamespaceMetadataPatchFromRaw : IFromRawJson<NamespaceMetadataPatch>
{
    /// <inheritdoc/>
    public NamespaceMetadataPatch FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => NamespaceMetadataPatch.FromRawUnchecked(rawData);
}

/// <summary>
/// Configuration for namespace pinning. - Missing field: no change to pinning configuration
/// - `null` or `false`: explicitly remove pinning - `true`: enable pinning with
/// default configuration - Object: set pinning configuration
/// </summary>
[JsonConverter(typeof(NamespaceMetadataPatchPinningConverter))]
public record class NamespaceMetadataPatchPinning : ModelBase
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

    public NamespaceMetadataPatchPinning(bool value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public NamespaceMetadataPatchPinning(PinningConfig value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public NamespaceMetadataPatchPinning(JsonElement element)
    {
        this._element = element;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="bool"/>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickBool(out var value)) {
    ///     // `value` is of type `bool`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickBool([NotNullWhen(true)] out bool? value)
    {
        value = this.Value as bool?;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="PinningConfig"/>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickConfig(out var value)) {
    ///     // `value` is of type `PinningConfig`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickConfig([NotNullWhen(true)] out PinningConfig? value)
    {
        value = this.Value as PinningConfig;
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
    ///     (bool value) =&gt; {...},
    ///     (PinningConfig value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(Action<bool> @bool, Action<PinningConfig> config)
    {
        switch (this.Value)
        {
            case bool value:
                @bool(value);
                break;
            case PinningConfig value:
                config(value);
                break;
            default:
                throw new TurbopufferInvalidDataException(
                    "Data did not match any variant of NamespaceMetadataPatchPinning"
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
    ///     (bool value) =&gt; {...},
    ///     (PinningConfig value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(Func<bool, T> @bool, Func<PinningConfig, T> config)
    {
        return this.Value switch
        {
            bool value => @bool(value),
            PinningConfig value => config(value),
            _ => throw new TurbopufferInvalidDataException(
                "Data did not match any variant of NamespaceMetadataPatchPinning"
            ),
        };
    }

    public static implicit operator NamespaceMetadataPatchPinning(bool value) => new(value);

    public static implicit operator NamespaceMetadataPatchPinning(PinningConfig value) =>
        new(value);

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
            throw new TurbopufferInvalidDataException(
                "Data did not match any variant of NamespaceMetadataPatchPinning"
            );
        }
        this.Switch((_) => { }, (config) => config.Validate());
    }

    public virtual bool Equals(NamespaceMetadataPatchPinning? other) =>
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
            bool _ => 0,
            PinningConfig _ => 1,
            _ => -1,
        };
    }
}

sealed class NamespaceMetadataPatchPinningConverter : JsonConverter<NamespaceMetadataPatchPinning?>
{
    public override NamespaceMetadataPatchPinning? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var element = JsonSerializer.Deserialize<JsonElement>(ref reader, options);
        try
        {
            var deserialized = JsonSerializer.Deserialize<PinningConfig>(element, options);
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
            return new(JsonSerializer.Deserialize<bool>(element, options), element);
        }
        catch (Exception e) when (e is JsonException || e is TurbopufferInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(
        Utf8JsonWriter writer,
        NamespaceMetadataPatchPinning? value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value?.Json, options);
    }
}
