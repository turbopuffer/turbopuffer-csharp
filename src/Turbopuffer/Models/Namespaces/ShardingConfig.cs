using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Core;

namespace Turbopuffer.Models.Namespaces;

/// <summary>
/// Configuration for namespace sharding, which partitions a namespace's documents
/// across multiple internal shards to scale indexing and query throughput beyond
/// a single machine. Sharding can only be configured on a namespace's inaugural
/// write, and cannot be added to or changed on an existing namespace.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<ShardingConfig, ShardingConfigFromRaw>))]
public sealed record class ShardingConfig : JsonModel
{
    /// <summary>
    /// The number of shards to partition the namespace into.
    /// </summary>
    public required int NumShards
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<int>("num_shards");
        }
        init { this._rawData.Set("num_shards", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.NumShards;
    }

    public ShardingConfig() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public ShardingConfig(ShardingConfig shardingConfig)
        : base(shardingConfig) { }
#pragma warning restore CS8618

    public ShardingConfig(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    ShardingConfig(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="ShardingConfigFromRaw.FromRawUnchecked"/>
    public static ShardingConfig FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }

    [SetsRequiredMembers]
    public ShardingConfig(int numShards)
        : this()
    {
        this.NumShards = numShards;
    }
}

class ShardingConfigFromRaw : IFromRawJson<ShardingConfig>
{
    /// <inheritdoc/>
    public ShardingConfig FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        ShardingConfig.FromRawUnchecked(rawData);
}
