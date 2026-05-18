using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Client.Core;

namespace Turbopuffer.Client.Models.Namespaces;

/// <summary>
/// The response to a successful cache warm request.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<NamespaceRecallResponse, NamespaceRecallResponseFromRaw>))]
public sealed record class NamespaceRecallResponse : JsonModel
{
    /// <summary>
    /// The average number of documents retrieved by the approximate nearest neighbor searches.
    /// </summary>
    public required double AvgAnnCount
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<double>("avg_ann_count");
        }
        init { this._rawData.Set("avg_ann_count", value); }
    }

    /// <summary>
    /// The average number of documents retrieved by the exhaustive searches.
    /// </summary>
    public required double AvgExhaustiveCount
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<double>("avg_exhaustive_count");
        }
        init { this._rawData.Set("avg_exhaustive_count", value); }
    }

    /// <summary>
    /// The average recall of the queries.
    /// </summary>
    public required double AvgRecall
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<double>("avg_recall");
        }
        init { this._rawData.Set("avg_recall", value); }
    }

    /// <summary>
    /// Ground truth data including query vectors and true nearest neighbors. Only
    /// included when include_ground_truth is true.
    /// </summary>
    public IReadOnlyList<GroundTruth>? GroundTruth
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<ImmutableArray<GroundTruth>>("ground_truth");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set<ImmutableArray<GroundTruth>?>(
                "ground_truth",
                value == null ? null : ImmutableArray.ToImmutableArray(value)
            );
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.AvgAnnCount;
        _ = this.AvgExhaustiveCount;
        _ = this.AvgRecall;
        foreach (var item in this.GroundTruth ?? [])
        {
            item.Validate();
        }
    }

    public NamespaceRecallResponse() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public NamespaceRecallResponse(NamespaceRecallResponse namespaceRecallResponse)
        : base(namespaceRecallResponse) { }
#pragma warning restore CS8618

    public NamespaceRecallResponse(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    NamespaceRecallResponse(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="NamespaceRecallResponseFromRaw.FromRawUnchecked"/>
    public static NamespaceRecallResponse FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class NamespaceRecallResponseFromRaw : IFromRawJson<NamespaceRecallResponse>
{
    /// <inheritdoc/>
    public NamespaceRecallResponse FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => NamespaceRecallResponse.FromRawUnchecked(rawData);
}

[JsonConverter(typeof(JsonModelConverter<GroundTruth, GroundTruthFromRaw>))]
public sealed record class GroundTruth : JsonModel
{
    /// <summary>
    /// The true nearest neighbors with their distances and vectors.
    /// </summary>
    public required IReadOnlyList<Row> NearestNeighbors
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<ImmutableArray<Row>>("nearest_neighbors");
        }
        init
        {
            this._rawData.Set<ImmutableArray<Row>>(
                "nearest_neighbors",
                ImmutableArray.ToImmutableArray(value)
            );
        }
    }

    /// <summary>
    /// The query vector used for this search.
    /// </summary>
    public required IReadOnlyList<double> QueryVector
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<ImmutableArray<double>>("query_vector");
        }
        init
        {
            this._rawData.Set<ImmutableArray<double>>(
                "query_vector",
                ImmutableArray.ToImmutableArray(value)
            );
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        foreach (var item in this.NearestNeighbors)
        {
            item.Validate();
        }
        _ = this.QueryVector;
    }

    public GroundTruth() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public GroundTruth(GroundTruth groundTruth)
        : base(groundTruth) { }
#pragma warning restore CS8618

    public GroundTruth(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    GroundTruth(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="GroundTruthFromRaw.FromRawUnchecked"/>
    public static GroundTruth FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class GroundTruthFromRaw : IFromRawJson<GroundTruth>
{
    /// <inheritdoc/>
    public GroundTruth FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        GroundTruth.FromRawUnchecked(rawData);
}
