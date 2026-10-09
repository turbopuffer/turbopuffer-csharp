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
/// The response to a successful read-only optimize request.
/// </summary>
[JsonConverter(
    typeof(JsonModelConverter<ReadOnlyOptimizeResponse, ReadOnlyOptimizeResponseFromRaw>)
)]
public sealed record class ReadOnlyOptimizeResponse : JsonModel
{
    /// <summary>
    /// `OK` if the namespace is optimized for a read-only workload, or `ACCEPTED`
    /// if the optimization is in progress.
    /// </summary>
    public required ApiEnum<string, ReadOnlyOptimizeResponseStatus> Status
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<ApiEnum<string, ReadOnlyOptimizeResponseStatus>>(
                "status"
            );
        }
        init { this._rawData.Set("status", value); }
    }

    /// <summary>
    /// The billing information for a write request.
    /// </summary>
    public WriteBilling? Billing
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<WriteBilling>("billing");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("billing", value);
        }
    }

    public string? Message
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("message");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("message", value);
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        this.Status.Validate();
        this.Billing?.Validate();
        _ = this.Message;
    }

    public ReadOnlyOptimizeResponse() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public ReadOnlyOptimizeResponse(ReadOnlyOptimizeResponse readOnlyOptimizeResponse)
        : base(readOnlyOptimizeResponse) { }
#pragma warning restore CS8618

    public ReadOnlyOptimizeResponse(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    ReadOnlyOptimizeResponse(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="ReadOnlyOptimizeResponseFromRaw.FromRawUnchecked"/>
    public static ReadOnlyOptimizeResponse FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }

    [SetsRequiredMembers]
    public ReadOnlyOptimizeResponse(ApiEnum<string, ReadOnlyOptimizeResponseStatus> status)
        : this()
    {
        this.Status = status;
    }
}

class ReadOnlyOptimizeResponseFromRaw : IFromRawJson<ReadOnlyOptimizeResponse>
{
    /// <inheritdoc/>
    public ReadOnlyOptimizeResponse FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => ReadOnlyOptimizeResponse.FromRawUnchecked(rawData);
}

/// <summary>
/// `OK` if the namespace is optimized for a read-only workload, or `ACCEPTED` if
/// the optimization is in progress.
/// </summary>
[JsonConverter(typeof(ReadOnlyOptimizeResponseStatusConverter))]
public enum ReadOnlyOptimizeResponseStatus
{
    Ok,
    Accepted,
}

sealed class ReadOnlyOptimizeResponseStatusConverter : JsonConverter<ReadOnlyOptimizeResponseStatus>
{
    public override ReadOnlyOptimizeResponseStatus Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<string>(ref reader, options) switch
        {
            "OK" => ReadOnlyOptimizeResponseStatus.Ok,
            "ACCEPTED" => ReadOnlyOptimizeResponseStatus.Accepted,
            _ => (ReadOnlyOptimizeResponseStatus)(-1),
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        ReadOnlyOptimizeResponseStatus value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            value switch
            {
                ReadOnlyOptimizeResponseStatus.Ok => "OK",
                ReadOnlyOptimizeResponseStatus.Accepted => "ACCEPTED",
                _ => throw new TurbopufferInvalidDataException(
                    string.Format("Invalid value '{0}' in {1}", value, nameof(value))
                ),
            },
            options
        );
    }
}
