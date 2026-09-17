using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Core;
using Turbopuffer.Exceptions;

namespace Turbopuffer.Models.Namespaces;

[JsonConverter(typeof(JsonModelConverter<OperationError, OperationErrorFromRaw>))]
public sealed record class OperationError : JsonModel
{
    /// <summary>
    /// The response to an unsuccessful request.
    /// </summary>
    public required Detail Detail
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<Detail>("detail");
        }
        init { this._rawData.Set("detail", value); }
    }

    /// <summary>
    /// The HTTP status code of the operation's error.
    /// </summary>
    public required long StatusCode
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<long>("status_code");
        }
        init { this._rawData.Set("status_code", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        this.Detail.Validate();
        _ = this.StatusCode;
    }

    public OperationError() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public OperationError(OperationError operationError)
        : base(operationError) { }
#pragma warning restore CS8618

    public OperationError(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    OperationError(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="OperationErrorFromRaw.FromRawUnchecked"/>
    public static OperationError FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class OperationErrorFromRaw : IFromRawJson<OperationError>
{
    /// <inheritdoc/>
    public OperationError FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        OperationError.FromRawUnchecked(rawData);
}

/// <summary>
/// The response to an unsuccessful request.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<Detail, DetailFromRaw>))]
public sealed record class Detail : JsonModel
{
    /// <summary>
    /// The error message.
    /// </summary>
    public required string Error
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<string>("error");
        }
        init { this._rawData.Set("error", value); }
    }

    /// <summary>
    /// The status of the request.
    /// </summary>
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
        _ = this.Error;
        if (!JsonElement.DeepEquals(this.Status, JsonSerializer.SerializeToElement("error")))
        {
            throw new TurbopufferInvalidDataException("Invalid value given for constant");
        }
    }

    public Detail()
    {
        this.Status = JsonSerializer.SerializeToElement("error");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public Detail(Detail detail)
        : base(detail) { }
#pragma warning restore CS8618

    public Detail(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);

        this.Status = JsonSerializer.SerializeToElement("error");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    Detail(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="DetailFromRaw.FromRawUnchecked"/>
    public static Detail FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }

    [SetsRequiredMembers]
    public Detail(string error)
        : this()
    {
        this.Error = error;
    }
}

class DetailFromRaw : IFromRawJson<Detail>
{
    /// <inheritdoc/>
    public Detail FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        Detail.FromRawUnchecked(rawData);
}
