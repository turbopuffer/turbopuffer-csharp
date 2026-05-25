using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Core;

namespace Turbopuffer.Models.Namespaces;

/// <summary>
/// The result of a query.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<NamespaceQueryResponse, NamespaceQueryResponseFromRaw>))]
public sealed record class NamespaceQueryResponse : JsonModel
{
    /// <summary>
    /// The billing information for a query.
    /// </summary>
    public required QueryBilling Billing
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<QueryBilling>("billing");
        }
        init { this._rawData.Set("billing", value); }
    }

    /// <summary>
    /// The performance information for a query.
    /// </summary>
    public required QueryPerformance Performance
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<QueryPerformance>("performance");
        }
        init { this._rawData.Set("performance", value); }
    }

    public IReadOnlyList<Row>? AggregationGroups
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<ImmutableArray<Row>>("aggregation_groups");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set<ImmutableArray<Row>?>(
                "aggregation_groups",
                value == null ? null : ImmutableArray.ToImmutableArray(value)
            );
        }
    }

    public IReadOnlyDictionary<string, JsonElement>? Aggregations
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<FrozenDictionary<string, JsonElement>>(
                "aggregations"
            );
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set<FrozenDictionary<string, JsonElement>?>(
                "aggregations",
                value == null ? null : FrozenDictionary.ToFrozenDictionary(value)
            );
        }
    }

    public IReadOnlyList<Row>? Rows
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<ImmutableArray<Row>>("rows");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set<ImmutableArray<Row>?>(
                "rows",
                value == null ? null : ImmutableArray.ToImmutableArray(value)
            );
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        this.Billing.Validate();
        this.Performance.Validate();
        foreach (var item in this.AggregationGroups ?? [])
        {
            item.Validate();
        }
        _ = this.Aggregations;
        foreach (var item in this.Rows ?? [])
        {
            item.Validate();
        }
    }

    public NamespaceQueryResponse() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public NamespaceQueryResponse(NamespaceQueryResponse namespaceQueryResponse)
        : base(namespaceQueryResponse) { }
#pragma warning restore CS8618

    public NamespaceQueryResponse(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    NamespaceQueryResponse(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="NamespaceQueryResponseFromRaw.FromRawUnchecked"/>
    public static NamespaceQueryResponse FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class NamespaceQueryResponseFromRaw : IFromRawJson<NamespaceQueryResponse>
{
    /// <inheritdoc/>
    public NamespaceQueryResponse FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => NamespaceQueryResponse.FromRawUnchecked(rawData);
}
