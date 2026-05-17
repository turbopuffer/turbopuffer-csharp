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
/// The result of a multi-query.
/// </summary>
[JsonConverter(
    typeof(JsonModelConverter<NamespaceMultiQueryResponse, NamespaceMultiQueryResponseFromRaw>)
)]
public sealed record class NamespaceMultiQueryResponse : JsonModel
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

    public required IReadOnlyList<Result> Results
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<ImmutableArray<Result>>("results");
        }
        init
        {
            this._rawData.Set<ImmutableArray<Result>>(
                "results",
                ImmutableArray.ToImmutableArray(value)
            );
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        this.Billing.Validate();
        this.Performance.Validate();
        foreach (var item in this.Results)
        {
            item.Validate();
        }
    }

    public NamespaceMultiQueryResponse() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public NamespaceMultiQueryResponse(NamespaceMultiQueryResponse namespaceMultiQueryResponse)
        : base(namespaceMultiQueryResponse) { }
#pragma warning restore CS8618

    public NamespaceMultiQueryResponse(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    NamespaceMultiQueryResponse(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="NamespaceMultiQueryResponseFromRaw.FromRawUnchecked"/>
    public static NamespaceMultiQueryResponse FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class NamespaceMultiQueryResponseFromRaw : IFromRawJson<NamespaceMultiQueryResponse>
{
    /// <inheritdoc/>
    public NamespaceMultiQueryResponse FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => NamespaceMultiQueryResponse.FromRawUnchecked(rawData);
}

[JsonConverter(typeof(JsonModelConverter<Result, ResultFromRaw>))]
public sealed record class Result : JsonModel
{
    public IReadOnlyList<IReadOnlyDictionary<string, JsonElement>>? AggregationGroups
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<
                ImmutableArray<FrozenDictionary<string, JsonElement>>
            >("aggregation_groups");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set<ImmutableArray<FrozenDictionary<string, JsonElement>>?>(
                "aggregation_groups",
                value == null
                    ? null
                    : ImmutableArray.ToImmutableArray(
                        Enumerable.Select(
                            value,
                            (item) => FrozenDictionary.ToFrozenDictionary(item)
                        )
                    )
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
        _ = this.AggregationGroups;
        _ = this.Aggregations;
        foreach (var item in this.Rows ?? [])
        {
            item.Validate();
        }
    }

    public Result() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public Result(Result result)
        : base(result) { }
#pragma warning restore CS8618

    public Result(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    Result(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="ResultFromRaw.FromRawUnchecked"/>
    public static Result FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class ResultFromRaw : IFromRawJson<Result>
{
    /// <inheritdoc/>
    public Result FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        Result.FromRawUnchecked(rawData);
}
