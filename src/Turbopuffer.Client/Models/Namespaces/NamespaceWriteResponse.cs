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
/// The response to a successful write request.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<NamespaceWriteResponse, NamespaceWriteResponseFromRaw>))]
public sealed record class NamespaceWriteResponse : JsonModel
{
    /// <summary>
    /// The billing information for a write request.
    /// </summary>
    public required WriteBilling Billing
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<WriteBilling>("billing");
        }
        init { this._rawData.Set("billing", value); }
    }

    /// <summary>
    /// A message describing the result of the write request.
    /// </summary>
    public required string Message
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<string>("message");
        }
        init { this._rawData.Set("message", value); }
    }

    /// <summary>
    /// The number of rows affected by the write request.
    /// </summary>
    public required long RowsAffected
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<long>("rows_affected");
        }
        init { this._rawData.Set("rows_affected", value); }
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

    /// <summary>
    /// The IDs of documents that were deleted. Only included when `return_affected_ids`
    /// is true and at least one document was deleted.
    /// </summary>
    public IReadOnlyList<ID>? DeletedIds
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<ImmutableArray<ID>>("deleted_ids");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set<ImmutableArray<ID>?>(
                "deleted_ids",
                value == null ? null : ImmutableArray.ToImmutableArray(value)
            );
        }
    }

    /// <summary>
    /// The IDs of documents that were patched. Only included when `return_affected_ids`
    /// is true and at least one document was patched.
    /// </summary>
    public IReadOnlyList<ID>? PatchedIds
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<ImmutableArray<ID>>("patched_ids");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set<ImmutableArray<ID>?>(
                "patched_ids",
                value == null ? null : ImmutableArray.ToImmutableArray(value)
            );
        }
    }

    /// <summary>
    /// The performance information for a write request.
    /// </summary>
    public WritePerformance? Performance
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<WritePerformance>("performance");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("performance", value);
        }
    }

    /// <summary>
    /// The number of rows deleted by the write request.
    /// </summary>
    public long? RowsDeleted
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<long>("rows_deleted");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("rows_deleted", value);
        }
    }

    /// <summary>
    /// The number of rows patched by the write request.
    /// </summary>
    public long? RowsPatched
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<long>("rows_patched");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("rows_patched", value);
        }
    }

    /// <summary>
    /// Whether more documents match the filter for partial operations.
    /// </summary>
    public bool? RowsRemaining
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<bool>("rows_remaining");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("rows_remaining", value);
        }
    }

    /// <summary>
    /// The number of rows upserted by the write request.
    /// </summary>
    public long? RowsUpserted
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<long>("rows_upserted");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("rows_upserted", value);
        }
    }

    /// <summary>
    /// The IDs of documents that were upserted. Only included when `return_affected_ids`
    /// is true and at least one document was upserted.
    /// </summary>
    public IReadOnlyList<ID>? UpsertedIds
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<ImmutableArray<ID>>("upserted_ids");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set<ImmutableArray<ID>?>(
                "upserted_ids",
                value == null ? null : ImmutableArray.ToImmutableArray(value)
            );
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        this.Billing.Validate();
        _ = this.Message;
        _ = this.RowsAffected;
        if (!JsonElement.DeepEquals(this.Status, JsonSerializer.SerializeToElement("OK")))
        {
            throw new TurbopufferInvalidDataException("Invalid value given for constant");
        }
        foreach (var item in this.DeletedIds ?? [])
        {
            item.Validate();
        }
        foreach (var item in this.PatchedIds ?? [])
        {
            item.Validate();
        }
        this.Performance?.Validate();
        _ = this.RowsDeleted;
        _ = this.RowsPatched;
        _ = this.RowsRemaining;
        _ = this.RowsUpserted;
        foreach (var item in this.UpsertedIds ?? [])
        {
            item.Validate();
        }
    }

    public NamespaceWriteResponse()
    {
        this.Status = JsonSerializer.SerializeToElement("OK");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public NamespaceWriteResponse(NamespaceWriteResponse namespaceWriteResponse)
        : base(namespaceWriteResponse) { }
#pragma warning restore CS8618

    public NamespaceWriteResponse(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);

        this.Status = JsonSerializer.SerializeToElement("OK");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    NamespaceWriteResponse(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="NamespaceWriteResponseFromRaw.FromRawUnchecked"/>
    public static NamespaceWriteResponse FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class NamespaceWriteResponseFromRaw : IFromRawJson<NamespaceWriteResponse>
{
    /// <inheritdoc/>
    public NamespaceWriteResponse FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => NamespaceWriteResponse.FromRawUnchecked(rawData);
}
