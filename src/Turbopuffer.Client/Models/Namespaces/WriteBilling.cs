using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Client.Core;

namespace Turbopuffer.Client.Models.Namespaces;

/// <summary>
/// The billing information for a write request.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<WriteBilling, WriteBillingFromRaw>))]
public sealed record class WriteBilling : JsonModel
{
    /// <summary>
    /// The number of billable logical bytes written to the namespace.
    /// </summary>
    public required long BillableLogicalBytesWritten
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<long>("billable_logical_bytes_written");
        }
        init { this._rawData.Set("billable_logical_bytes_written", value); }
    }

    /// <summary>
    /// The billing information for a query.
    /// </summary>
    public QueryBilling? Query
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<QueryBilling>("query");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("query", value);
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.BillableLogicalBytesWritten;
        this.Query?.Validate();
    }

    public WriteBilling() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public WriteBilling(WriteBilling writeBilling)
        : base(writeBilling) { }
#pragma warning restore CS8618

    public WriteBilling(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    WriteBilling(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="WriteBillingFromRaw.FromRawUnchecked"/>
    public static WriteBilling FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }

    [SetsRequiredMembers]
    public WriteBilling(long billableLogicalBytesWritten)
        : this()
    {
        this.BillableLogicalBytesWritten = billableLogicalBytesWritten;
    }
}

class WriteBillingFromRaw : IFromRawJson<WriteBilling>
{
    /// <inheritdoc/>
    public WriteBilling FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        WriteBilling.FromRawUnchecked(rawData);
}
