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
/// The namespace to copy documents from.
/// </summary>
[JsonConverter(typeof(CopyFromNamespaceParamsConverter))]
public record class CopyFromNamespaceParams : ModelBase
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

    public CopyFromNamespaceParams(string value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public CopyFromNamespaceParams(CopyFromNamespaceConfig value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public CopyFromNamespaceParams(JsonElement element)
    {
        this._element = element;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="string"/>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickString(out var value)) {
    ///     // `value` is of type `string`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickString([NotNullWhen(true)] out string? value)
    {
        value = this.Value as string;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="CopyFromNamespaceConfig"/>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickConfig(out var value)) {
    ///     // `value` is of type `CopyFromNamespaceConfig`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickConfig([NotNullWhen(true)] out CopyFromNamespaceConfig? value)
    {
        value = this.Value as CopyFromNamespaceConfig;
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
    ///     (string value) =&gt; {...},
    ///     (CopyFromNamespaceConfig value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(Action<string> @string, Action<CopyFromNamespaceConfig> config)
    {
        switch (this.Value)
        {
            case string value:
                @string(value);
                break;
            case CopyFromNamespaceConfig value:
                config(value);
                break;
            default:
                throw new TurbopufferInvalidDataException(
                    "Data did not match any variant of CopyFromNamespaceParams"
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
    ///     (string value) =&gt; {...},
    ///     (CopyFromNamespaceConfig value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(Func<string, T> @string, Func<CopyFromNamespaceConfig, T> config)
    {
        return this.Value switch
        {
            string value => @string(value),
            CopyFromNamespaceConfig value => config(value),
            _ => throw new TurbopufferInvalidDataException(
                "Data did not match any variant of CopyFromNamespaceParams"
            ),
        };
    }

    public static implicit operator CopyFromNamespaceParams(string value) => new(value);

    public static implicit operator CopyFromNamespaceParams(CopyFromNamespaceConfig value) =>
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
                "Data did not match any variant of CopyFromNamespaceParams"
            );
        }
        this.Switch((_) => { }, (config) => config.Validate());
    }

    public virtual bool Equals(CopyFromNamespaceParams? other) =>
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
            string _ => 0,
            CopyFromNamespaceConfig _ => 1,
            _ => -1,
        };
    }
}

sealed class CopyFromNamespaceParamsConverter : JsonConverter<CopyFromNamespaceParams>
{
    public override CopyFromNamespaceParams? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var element = JsonSerializer.Deserialize<JsonElement>(ref reader, options);
        try
        {
            var deserialized = JsonSerializer.Deserialize<CopyFromNamespaceConfig>(
                element,
                options
            );
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
            var deserialized = JsonSerializer.Deserialize<string>(element, options);
            if (deserialized != null)
            {
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is TurbopufferInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(
        Utf8JsonWriter writer,
        CopyFromNamespaceParams value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value.Json, options);
    }
}

[JsonConverter(typeof(JsonModelConverter<CopyFromNamespaceConfig, CopyFromNamespaceConfigFromRaw>))]
public sealed record class CopyFromNamespaceConfig : JsonModel
{
    /// <summary>
    /// The namespace to copy documents from.
    /// </summary>
    public required string SourceNamespace
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<string>("source_namespace");
        }
        init { this._rawData.Set("source_namespace", value); }
    }

    /// <summary>
    /// (Optional) An API key for the organization containing the source namespace
    /// </summary>
    public string? SourceApiKey
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("source_api_key");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("source_api_key", value);
        }
    }

    /// <summary>
    /// (Optional) The region of the source namespace.
    /// </summary>
    public string? SourceRegion
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("source_region");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("source_region", value);
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.SourceNamespace;
        _ = this.SourceApiKey;
        _ = this.SourceRegion;
    }

    public CopyFromNamespaceConfig() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public CopyFromNamespaceConfig(CopyFromNamespaceConfig copyFromNamespaceConfig)
        : base(copyFromNamespaceConfig) { }
#pragma warning restore CS8618

    public CopyFromNamespaceConfig(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    CopyFromNamespaceConfig(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="CopyFromNamespaceConfigFromRaw.FromRawUnchecked"/>
    public static CopyFromNamespaceConfig FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }

    [SetsRequiredMembers]
    public CopyFromNamespaceConfig(string sourceNamespace)
        : this()
    {
        this.SourceNamespace = sourceNamespace;
    }
}

class CopyFromNamespaceConfigFromRaw : IFromRawJson<CopyFromNamespaceConfig>
{
    /// <inheritdoc/>
    public CopyFromNamespaceConfig FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => CopyFromNamespaceConfig.FromRawUnchecked(rawData);
}
