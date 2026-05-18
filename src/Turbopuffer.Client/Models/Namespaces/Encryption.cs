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
/// The encryption configuration for a namespace.
/// </summary>
[JsonConverter(typeof(EncryptionConverter))]
public record class Encryption : ModelBase
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

    public Encryption(CustomerManaged value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public Encryption(Default value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public Encryption(JsonElement element)
    {
        this._element = element;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="CustomerManaged"/>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickCustomerManaged(out var value)) {
    ///     // `value` is of type `CustomerManaged`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickCustomerManaged([NotNullWhen(true)] out CustomerManaged? value)
    {
        value = this.Value as CustomerManaged;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="Default"/>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickDefault(out var value)) {
    ///     // `value` is of type `Default`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickDefault([NotNullWhen(true)] out Default? value)
    {
        value = this.Value as Default;
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
    ///     (CustomerManaged value) =&gt; {...},
    ///     (Default value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(Action<CustomerManaged> customerManaged, Action<Default> default_)
    {
        switch (this.Value)
        {
            case CustomerManaged value:
                customerManaged(value);
                break;
            case Default value:
                default_(value);
                break;
            default:
                throw new TurbopufferInvalidDataException(
                    "Data did not match any variant of Encryption"
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
    ///     (CustomerManaged value) =&gt; {...},
    ///     (Default value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(Func<CustomerManaged, T> customerManaged, Func<Default, T> default_)
    {
        return this.Value switch
        {
            CustomerManaged value => customerManaged(value),
            Default value => default_(value),
            _ => throw new TurbopufferInvalidDataException(
                "Data did not match any variant of Encryption"
            ),
        };
    }

    public static implicit operator Encryption(CustomerManaged value) => new(value);

    public static implicit operator Encryption(Default value) => new(value);

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
                "Data did not match any variant of Encryption"
            );
        }
        this.Switch(
            (customerManaged) => customerManaged.Validate(),
            (default_) => default_.Validate()
        );
    }

    public virtual bool Equals(Encryption? other) =>
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
            CustomerManaged _ => 0,
            Default _ => 1,
            _ => -1,
        };
    }
}

sealed class EncryptionConverter : JsonConverter<Encryption>
{
    public override Encryption? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var element = JsonSerializer.Deserialize<JsonElement>(ref reader, options);
        try
        {
            var deserialized = JsonSerializer.Deserialize<Default>(element, options);
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
            var deserialized = JsonSerializer.Deserialize<CustomerManaged>(element, options);
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

        return new(element);
    }

    public override void Write(
        Utf8JsonWriter writer,
        Encryption value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value.Json, options);
    }
}

/// <summary>
/// Encrypt the namespace with a customer-managed encryption key (CMEK).
/// </summary>
[JsonConverter(typeof(JsonModelConverter<CustomerManaged, CustomerManagedFromRaw>))]
public sealed record class CustomerManaged : JsonModel
{
    /// <summary>
    /// The identifier of the CMEK key to use for encryption. For GCP, the fully-qualified
    /// resource name of the key. For AWS, the ARN of the key.
    /// </summary>
    public required string KeyName
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<string>("key_name");
        }
        init { this._rawData.Set("key_name", value); }
    }

    public JsonElement Mode
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<JsonElement>("mode");
        }
        init { this._rawData.Set("mode", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.KeyName;
        if (
            !JsonElement.DeepEquals(
                this.Mode,
                JsonSerializer.SerializeToElement("customer-managed")
            )
        )
        {
            throw new TurbopufferInvalidDataException("Invalid value given for constant");
        }
    }

    public CustomerManaged()
    {
        this.Mode = JsonSerializer.SerializeToElement("customer-managed");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public CustomerManaged(CustomerManaged customerManaged)
        : base(customerManaged) { }
#pragma warning restore CS8618

    public CustomerManaged(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);

        this.Mode = JsonSerializer.SerializeToElement("customer-managed");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    CustomerManaged(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="CustomerManagedFromRaw.FromRawUnchecked"/>
    public static CustomerManaged FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }

    [SetsRequiredMembers]
    public CustomerManaged(string keyName)
        : this()
    {
        this.KeyName = keyName;
    }
}

class CustomerManagedFromRaw : IFromRawJson<CustomerManaged>
{
    /// <inheritdoc/>
    public CustomerManaged FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        CustomerManaged.FromRawUnchecked(rawData);
}

/// <summary>
/// Use the default server-side encryption (SSE).
/// </summary>
[JsonConverter(typeof(DefaultConverter))]
public record class Default
{
    public JsonElement Element { get; private init; }

    public Default()
    {
        Element = JsonSerializer.Deserialize<JsonElement>(
            """
            {
              "mode": "default"
            }
            """
        );
    }

    internal Default(JsonElement element)
    {
        Element = element;
    }

    /// <summary>
    /// Validates that the instance's underlying value is the expected constant.
    ///
    /// <para>This is useful for instances constructed from raw JSON data (e.g. deserialized from an API response).</para>
    ///
    /// <exception cref="TurbopufferInvalidDataException">
    /// Thrown when the instance does not pass validation.
    /// </exception>
    /// </summary>
    public void Validate()
    {
        if (this != new Default())
        {
            throw new TurbopufferInvalidDataException("Invalid value given for 'Default'");
        }
    }

    public override int GetHashCode()
    {
        return 0;
    }

    public virtual bool Equals(Default? other)
    {
        if (other == null)
        {
            return false;
        }

        return JsonElement.DeepEquals(this.Element, other.Element);
    }
}

class DefaultConverter : JsonConverter<Default>
{
    public override Default? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return new(JsonSerializer.Deserialize<JsonElement>(ref reader, options));
    }

    public override void Write(Utf8JsonWriter writer, Default value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value.Element, options);
    }
}
