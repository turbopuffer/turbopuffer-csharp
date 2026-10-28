using System.Collections.Generic;
using System.Text.Json;

namespace Turbopuffer.Models.Namespaces;

public sealed partial record class NamespaceQueryResponse
{
    /// <summary>
    /// Returns <see cref="Rows"/>, or throws if it is null.
    /// </summary>
    /// <exception cref="System.InvalidOperationException">Thrown if <see cref="Rows"/> is null.</exception>
    public IReadOnlyList<Row> GetRows() =>
        this.Rows ?? throw new System.InvalidOperationException("Query did not return Rows");

    /// <summary>
    /// Returns <see cref="Aggregations"/>, or throws if it is null.
    /// </summary>
    /// <exception cref="System.InvalidOperationException">Thrown if <see cref="Aggregations"/> is null.</exception>
    public IReadOnlyDictionary<string, JsonElement> GetAggregations() =>
        this.Aggregations
        ?? throw new System.InvalidOperationException("Query did not return Aggregations");

    /// <summary>
    /// Returns <see cref="AggregationGroups"/>, or throws if it is null.
    /// </summary>
    /// <exception cref="System.InvalidOperationException">Thrown if <see cref="AggregationGroups"/> is null.</exception>
    public IReadOnlyList<Row> GetAggregationGroups() =>
        this.AggregationGroups
        ?? throw new System.InvalidOperationException("Query did not return AggregationGroups");
}

public sealed partial record class Result
{
    /// <summary>
    /// Returns <see cref="Rows"/>, or throws if it is null.
    /// </summary>
    /// <exception cref="System.InvalidOperationException">Thrown if <see cref="Rows"/> is null.</exception>
    public IReadOnlyList<Row> GetRows() =>
        this.Rows ?? throw new System.InvalidOperationException("Query did not return Rows");

    /// <summary>
    /// Returns <see cref="Aggregations"/>, or throws if it is null.
    /// </summary>
    /// <exception cref="System.InvalidOperationException">Thrown if <see cref="Aggregations"/> is null.</exception>
    public IReadOnlyDictionary<string, JsonElement> GetAggregations() =>
        this.Aggregations
        ?? throw new System.InvalidOperationException("Query did not return Aggregations");

    /// <summary>
    /// Returns <see cref="AggregationGroups"/>, or throws if it is null.
    /// </summary>
    /// <exception cref="System.InvalidOperationException">Thrown if <see cref="AggregationGroups"/> is null.</exception>
    public IReadOnlyList<Row> GetAggregationGroups() =>
        this.AggregationGroups
        ?? throw new System.InvalidOperationException("Query did not return AggregationGroups");
}
