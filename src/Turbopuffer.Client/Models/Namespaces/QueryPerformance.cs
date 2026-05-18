using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Client.Core;

namespace Turbopuffer.Client.Models.Namespaces;

/// <summary>
/// The performance information for a query.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<QueryPerformance, QueryPerformanceFromRaw>))]
public sealed record class QueryPerformance : JsonModel
{
    /// <summary>
    /// the approximate number of documents in the namespace.
    /// </summary>
    public required long ApproxNamespaceSize
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<long>("approx_namespace_size");
        }
        init { this._rawData.Set("approx_namespace_size", value); }
    }

    /// <summary>
    /// The ratio of cache hits to total cache lookups.
    /// </summary>
    public required double CacheHitRatio
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<double>("cache_hit_ratio");
        }
        init { this._rawData.Set("cache_hit_ratio", value); }
    }

    /// <summary>
    /// A qualitative description of the cache hit ratio (`hot`, `warm`, or `cold`).
    /// </summary>
    public required string CacheTemperature
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<string>("cache_temperature");
        }
        init { this._rawData.Set("cache_temperature", value); }
    }

    /// <summary>
    /// The number of unindexed documents processed by the query.
    /// </summary>
    public required long ExhaustiveSearchCount
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<long>("exhaustive_search_count");
        }
        init { this._rawData.Set("exhaustive_search_count", value); }
    }

    /// <summary>
    /// Request time measured on the server, excluding time spent waiting due to
    /// the namespace concurrency limit.
    /// </summary>
    public required long QueryExecutionMs
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<long>("query_execution_ms");
        }
        init { this._rawData.Set("query_execution_ms", value); }
    }

    /// <summary>
    /// Request time measured on the server, including time spent waiting for other
    /// queries to complete if the namespace was at its concurrency limit.
    /// </summary>
    public required long ServerTotalMs
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<long>("server_total_ms");
        }
        init { this._rawData.Set("server_total_ms", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.ApproxNamespaceSize;
        _ = this.CacheHitRatio;
        _ = this.CacheTemperature;
        _ = this.ExhaustiveSearchCount;
        _ = this.QueryExecutionMs;
        _ = this.ServerTotalMs;
    }

    public QueryPerformance() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public QueryPerformance(QueryPerformance queryPerformance)
        : base(queryPerformance) { }
#pragma warning restore CS8618

    public QueryPerformance(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    QueryPerformance(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="QueryPerformanceFromRaw.FromRawUnchecked"/>
    public static QueryPerformance FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class QueryPerformanceFromRaw : IFromRawJson<QueryPerformance>
{
    /// <inheritdoc/>
    public QueryPerformance FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        QueryPerformance.FromRawUnchecked(rawData);
}
