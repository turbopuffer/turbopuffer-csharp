using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Core;
using Turbopuffer.Exceptions;

namespace Turbopuffer.Models.Namespaces;

/// <summary>
/// The current status of a copy operation.
/// </summary>
[JsonConverter(typeof(CopyFromNamespaceOperationConverter))]
public record class CopyFromNamespaceOperation : ModelBase
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

    public DateTimeOffset StartTime
    {
        get { return Match(running: (x) => x.StartTime, finished: (x) => x.StartTime); }
    }

    public JsonElement Status
    {
        get { return Match(running: (x) => x.Status, finished: (x) => x.Status); }
    }

    public CopyFromNamespaceOperation(Running value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public CopyFromNamespaceOperation(Finished value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public CopyFromNamespaceOperation(JsonElement element)
    {
        this._element = element;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="Running"/>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickRunning(out var value)) {
    ///     // `value` is of type `Running`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickRunning([NotNullWhen(true)] out Running? value)
    {
        value = this.Value as Running;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="Finished"/>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickFinished(out var value)) {
    ///     // `value` is of type `Finished`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickFinished([NotNullWhen(true)] out Finished? value)
    {
        value = this.Value as Finished;
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
    ///     (Running value) =&gt; {...},
    ///     (Finished value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(Action<Running> running, Action<Finished> finished)
    {
        switch (this.Value)
        {
            case Running value:
                running(value);
                break;
            case Finished value:
                finished(value);
                break;
            default:
                throw new TurbopufferInvalidDataException(
                    "Data did not match any variant of CopyFromNamespaceOperation"
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
    ///     (Running value) =&gt; {...},
    ///     (Finished value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(Func<Running, T> running, Func<Finished, T> finished)
    {
        return this.Value switch
        {
            Running value => running(value),
            Finished value => finished(value),
            _ => throw new TurbopufferInvalidDataException(
                "Data did not match any variant of CopyFromNamespaceOperation"
            ),
        };
    }

    public static implicit operator CopyFromNamespaceOperation(Running value) => new(value);

    public static implicit operator CopyFromNamespaceOperation(Finished value) => new(value);

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
                "Data did not match any variant of CopyFromNamespaceOperation"
            );
        }
        this.Switch((running) => running.Validate(), (finished) => finished.Validate());
    }

    public virtual bool Equals(CopyFromNamespaceOperation? other) =>
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
            Running _ => 0,
            Finished _ => 1,
            _ => -1,
        };
    }
}

sealed class CopyFromNamespaceOperationConverter : JsonConverter<CopyFromNamespaceOperation>
{
    public override CopyFromNamespaceOperation? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var element = JsonSerializer.Deserialize<JsonElement>(ref reader, options);
        string? status;
        try
        {
            status = element.GetProperty("status").GetString();
        }
        catch
        {
            status = null;
        }

        switch (status)
        {
            case "running":
            {
                try
                {
                    var deserialized = JsonSerializer.Deserialize<Running>(element, options);
                    if (deserialized != null)
                    {
                        return new(deserialized, element);
                    }
                }
                catch (JsonException)
                {
                    // ignore
                }

                return new(element);
            }
            case "finished":
            {
                try
                {
                    var deserialized = JsonSerializer.Deserialize<Finished>(element, options);
                    if (deserialized != null)
                    {
                        return new(deserialized, element);
                    }
                }
                catch (JsonException)
                {
                    // ignore
                }

                return new(element);
            }
            default:
            {
                return new CopyFromNamespaceOperation(element);
            }
        }
    }

    public override void Write(
        Utf8JsonWriter writer,
        CopyFromNamespaceOperation value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value.Json, options);
    }
}

[JsonConverter(typeof(JsonModelConverter<Running, RunningFromRaw>))]
public sealed record class Running : JsonModel
{
    /// <summary>
    /// The time at which the operation started.
    /// </summary>
    public required DateTimeOffset StartTime
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<DateTimeOffset>("start_time");
        }
        init { this._rawData.Set("start_time", value); }
    }

    public JsonElement Status
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<JsonElement>("status");
        }
        init { this._rawData.Set("status", value); }
    }

    /// <summary>
    /// A freeform description of the operation's progress. May be absent, and its
    /// format may change.
    /// </summary>
    public string? Progress
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("progress");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("progress", value);
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.StartTime;
        if (!JsonElement.DeepEquals(this.Status, JsonSerializer.SerializeToElement("running")))
        {
            throw new TurbopufferInvalidDataException("Invalid value given for constant");
        }
        _ = this.Progress;
    }

    public Running()
    {
        this.Status = JsonSerializer.SerializeToElement("running");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public Running(Running running)
        : base(running) { }
#pragma warning restore CS8618

    public Running(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);

        this.Status = JsonSerializer.SerializeToElement("running");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    Running(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="RunningFromRaw.FromRawUnchecked"/>
    public static Running FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }

    [SetsRequiredMembers]
    public Running(DateTimeOffset startTime)
        : this()
    {
        this.StartTime = startTime;
    }
}

class RunningFromRaw : IFromRawJson<Running>
{
    /// <inheritdoc/>
    public Running FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        Running.FromRawUnchecked(rawData);
}

[JsonConverter(typeof(JsonModelConverter<Finished, FinishedFromRaw>))]
public sealed record class Finished : JsonModel
{
    /// <summary>
    /// The time at which the operation finished.
    /// </summary>
    public required DateTimeOffset FinishTime
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<DateTimeOffset>("finish_time");
        }
        init { this._rawData.Set("finish_time", value); }
    }

    public required CopyFromNamespaceOperationResult Result
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<CopyFromNamespaceOperationResult>("result");
        }
        init { this._rawData.Set("result", value); }
    }

    /// <summary>
    /// The time at which the operation started.
    /// </summary>
    public required DateTimeOffset StartTime
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<DateTimeOffset>("start_time");
        }
        init { this._rawData.Set("start_time", value); }
    }

    public JsonElement Status
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<JsonElement>("status");
        }
        init { this._rawData.Set("status", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.FinishTime;
        this.Result.Validate();
        _ = this.StartTime;
        if (!JsonElement.DeepEquals(this.Status, JsonSerializer.SerializeToElement("finished")))
        {
            throw new TurbopufferInvalidDataException("Invalid value given for constant");
        }
    }

    public Finished()
    {
        this.Status = JsonSerializer.SerializeToElement("finished");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public Finished(Finished finished)
        : base(finished) { }
#pragma warning restore CS8618

    public Finished(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);

        this.Status = JsonSerializer.SerializeToElement("finished");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    Finished(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="FinishedFromRaw.FromRawUnchecked"/>
    public static Finished FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class FinishedFromRaw : IFromRawJson<Finished>
{
    /// <inheritdoc/>
    public Finished FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        Finished.FromRawUnchecked(rawData);
}
