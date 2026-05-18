using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Client.Core;

namespace Turbopuffer.Client.Models.Namespaces;

/// <summary>
/// The billing information for a query.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<QueryBilling, QueryBillingFromRaw>))]
public sealed record class QueryBilling : JsonModel
{
    /// <summary>
    /// The number of billable logical bytes queried from the namespace.
    /// </summary>
    public required long BillableLogicalBytesQueried
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<long>("billable_logical_bytes_queried");
        }
        init { this._rawData.Set("billable_logical_bytes_queried", value); }
    }

    /// <summary>
    /// The number of billable logical bytes returned from the query.
    /// </summary>
    public required long BillableLogicalBytesReturned
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<long>("billable_logical_bytes_returned");
        }
        init { this._rawData.Set("billable_logical_bytes_returned", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.BillableLogicalBytesQueried;
        _ = this.BillableLogicalBytesReturned;
    }

    public QueryBilling() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public QueryBilling(QueryBilling queryBilling)
        : base(queryBilling) { }
#pragma warning restore CS8618

    public QueryBilling(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    QueryBilling(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="QueryBillingFromRaw.FromRawUnchecked"/>
    public static QueryBilling FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class QueryBillingFromRaw : IFromRawJson<QueryBilling>
{
    /// <inheritdoc/>
    public QueryBilling FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        QueryBilling.FromRawUnchecked(rawData);
}
