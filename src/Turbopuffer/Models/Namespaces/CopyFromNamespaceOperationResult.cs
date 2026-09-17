using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Core;
using Turbopuffer.Exceptions;

namespace Turbopuffer.Models.Namespaces;

[JsonConverter(typeof(CopyFromNamespaceOperationResultConverter))]
public record class CopyFromNamespaceOperationResult : ModelBase
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

    public CopyFromNamespaceOperationResult(Success value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public CopyFromNamespaceOperationResult(Error value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public CopyFromNamespaceOperationResult(JsonElement element)
    {
        this._element = element;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="Success"/>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickSuccess(out var value)) {
    ///     // `value` is of type `Success`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickSuccess([NotNullWhen(true)] out Success? value)
    {
        value = this.Value as Success;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="Error"/>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickError(out var value)) {
    ///     // `value` is of type `Error`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickError([NotNullWhen(true)] out Error? value)
    {
        value = this.Value as Error;
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
    ///     (Success value) =&gt; {...},
    ///     (Error value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(Action<Success> success, Action<Error> error)
    {
        switch (this.Value)
        {
            case Success value:
                success(value);
                break;
            case Error value:
                error(value);
                break;
            default:
                throw new TurbopufferInvalidDataException(
                    "Data did not match any variant of CopyFromNamespaceOperationResult"
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
    ///     (Success value) =&gt; {...},
    ///     (Error value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(Func<Success, T> success, Func<Error, T> error)
    {
        return this.Value switch
        {
            Success value => success(value),
            Error value => error(value),
            _ => throw new TurbopufferInvalidDataException(
                "Data did not match any variant of CopyFromNamespaceOperationResult"
            ),
        };
    }

    public static implicit operator CopyFromNamespaceOperationResult(Success value) => new(value);

    public static implicit operator CopyFromNamespaceOperationResult(Error value) => new(value);

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
                "Data did not match any variant of CopyFromNamespaceOperationResult"
            );
        }
        this.Switch((success) => success.Validate(), (error) => error.Validate());
    }

    public virtual bool Equals(CopyFromNamespaceOperationResult? other) =>
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
            Success _ => 0,
            Error _ => 1,
            _ => -1,
        };
    }
}

sealed class CopyFromNamespaceOperationResultConverter
    : JsonConverter<CopyFromNamespaceOperationResult>
{
    public override CopyFromNamespaceOperationResult? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var element = JsonSerializer.Deserialize<JsonElement>(ref reader, options);
        try
        {
            var deserialized = JsonSerializer.Deserialize<Success>(element, options);
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
            var deserialized = JsonSerializer.Deserialize<Error>(element, options);
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
        CopyFromNamespaceOperationResult value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value.Json, options);
    }
}

[JsonConverter(typeof(JsonModelConverter<Success, SuccessFromRaw>))]
public sealed record class Success : JsonModel
{
    /// <summary>
    /// The response to a successful write request.
    /// </summary>
    public required WriteResult SuccessValue
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<WriteResult>("success");
        }
        init { this._rawData.Set("success", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        this.SuccessValue.Validate();
    }

    public Success() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public Success(Success success)
        : base(success) { }
#pragma warning restore CS8618

    public Success(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    Success(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="SuccessFromRaw.FromRawUnchecked"/>
    public static Success FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }

    [SetsRequiredMembers]
    public Success(WriteResult successValue)
        : this()
    {
        this.SuccessValue = successValue;
    }
}

class SuccessFromRaw : IFromRawJson<Success>
{
    /// <inheritdoc/>
    public Success FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        Success.FromRawUnchecked(rawData);
}

[JsonConverter(typeof(JsonModelConverter<Error, ErrorFromRaw>))]
public sealed record class Error : JsonModel
{
    public required OperationError ErrorValue
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<OperationError>("error");
        }
        init { this._rawData.Set("error", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        this.ErrorValue.Validate();
    }

    public Error() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public Error(Error error)
        : base(error) { }
#pragma warning restore CS8618

    public Error(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    Error(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="ErrorFromRaw.FromRawUnchecked"/>
    public static Error FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }

    [SetsRequiredMembers]
    public Error(OperationError errorValue)
        : this()
    {
        this.ErrorValue = errorValue;
    }
}

class ErrorFromRaw : IFromRawJson<Error>
{
    /// <inheritdoc/>
    public Error FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        Error.FromRawUnchecked(rawData);
}
