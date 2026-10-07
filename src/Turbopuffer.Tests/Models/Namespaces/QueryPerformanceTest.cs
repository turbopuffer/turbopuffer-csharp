using System;
using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class QueryPerformanceTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new QueryPerformance
        {
            ApproxNamespaceSize = 0,
            CacheHitRatio = 0,
            CacheTemperature = "cache_temperature",
            ExhaustiveSearchCount = 0,
            QueryExecutionMs = 0,
            ServerTotalMs = 0,
            EmbeddingMs = 0,
            EmbeddingTokens = 0,
            LastIncludedWriteAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
        };

        long expectedApproxNamespaceSize = 0;
        double expectedCacheHitRatio = 0;
        string expectedCacheTemperature = "cache_temperature";
        long expectedExhaustiveSearchCount = 0;
        long expectedQueryExecutionMs = 0;
        long expectedServerTotalMs = 0;
        long expectedEmbeddingMs = 0;
        long expectedEmbeddingTokens = 0;
        DateTimeOffset expectedLastIncludedWriteAt = DateTimeOffset.Parse(
            "2019-12-27T18:11:19.117Z"
        );

        Assert.Equal(expectedApproxNamespaceSize, model.ApproxNamespaceSize);
        Assert.Equal(expectedCacheHitRatio, model.CacheHitRatio);
        Assert.Equal(expectedCacheTemperature, model.CacheTemperature);
        Assert.Equal(expectedExhaustiveSearchCount, model.ExhaustiveSearchCount);
        Assert.Equal(expectedQueryExecutionMs, model.QueryExecutionMs);
        Assert.Equal(expectedServerTotalMs, model.ServerTotalMs);
        Assert.Equal(expectedEmbeddingMs, model.EmbeddingMs);
        Assert.Equal(expectedEmbeddingTokens, model.EmbeddingTokens);
        Assert.Equal(expectedLastIncludedWriteAt, model.LastIncludedWriteAt);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new QueryPerformance
        {
            ApproxNamespaceSize = 0,
            CacheHitRatio = 0,
            CacheTemperature = "cache_temperature",
            ExhaustiveSearchCount = 0,
            QueryExecutionMs = 0,
            ServerTotalMs = 0,
            EmbeddingMs = 0,
            EmbeddingTokens = 0,
            LastIncludedWriteAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<QueryPerformance>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new QueryPerformance
        {
            ApproxNamespaceSize = 0,
            CacheHitRatio = 0,
            CacheTemperature = "cache_temperature",
            ExhaustiveSearchCount = 0,
            QueryExecutionMs = 0,
            ServerTotalMs = 0,
            EmbeddingMs = 0,
            EmbeddingTokens = 0,
            LastIncludedWriteAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<QueryPerformance>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        long expectedApproxNamespaceSize = 0;
        double expectedCacheHitRatio = 0;
        string expectedCacheTemperature = "cache_temperature";
        long expectedExhaustiveSearchCount = 0;
        long expectedQueryExecutionMs = 0;
        long expectedServerTotalMs = 0;
        long expectedEmbeddingMs = 0;
        long expectedEmbeddingTokens = 0;
        DateTimeOffset expectedLastIncludedWriteAt = DateTimeOffset.Parse(
            "2019-12-27T18:11:19.117Z"
        );

        Assert.Equal(expectedApproxNamespaceSize, deserialized.ApproxNamespaceSize);
        Assert.Equal(expectedCacheHitRatio, deserialized.CacheHitRatio);
        Assert.Equal(expectedCacheTemperature, deserialized.CacheTemperature);
        Assert.Equal(expectedExhaustiveSearchCount, deserialized.ExhaustiveSearchCount);
        Assert.Equal(expectedQueryExecutionMs, deserialized.QueryExecutionMs);
        Assert.Equal(expectedServerTotalMs, deserialized.ServerTotalMs);
        Assert.Equal(expectedEmbeddingMs, deserialized.EmbeddingMs);
        Assert.Equal(expectedEmbeddingTokens, deserialized.EmbeddingTokens);
        Assert.Equal(expectedLastIncludedWriteAt, deserialized.LastIncludedWriteAt);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new QueryPerformance
        {
            ApproxNamespaceSize = 0,
            CacheHitRatio = 0,
            CacheTemperature = "cache_temperature",
            ExhaustiveSearchCount = 0,
            QueryExecutionMs = 0,
            ServerTotalMs = 0,
            EmbeddingMs = 0,
            EmbeddingTokens = 0,
            LastIncludedWriteAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new QueryPerformance
        {
            ApproxNamespaceSize = 0,
            CacheHitRatio = 0,
            CacheTemperature = "cache_temperature",
            ExhaustiveSearchCount = 0,
            QueryExecutionMs = 0,
            ServerTotalMs = 0,
        };

        Assert.Null(model.EmbeddingMs);
        Assert.False(model.RawData.ContainsKey("embedding_ms"));
        Assert.Null(model.EmbeddingTokens);
        Assert.False(model.RawData.ContainsKey("embedding_tokens"));
        Assert.Null(model.LastIncludedWriteAt);
        Assert.False(model.RawData.ContainsKey("last_included_write_at"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new QueryPerformance
        {
            ApproxNamespaceSize = 0,
            CacheHitRatio = 0,
            CacheTemperature = "cache_temperature",
            ExhaustiveSearchCount = 0,
            QueryExecutionMs = 0,
            ServerTotalMs = 0,
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new QueryPerformance
        {
            ApproxNamespaceSize = 0,
            CacheHitRatio = 0,
            CacheTemperature = "cache_temperature",
            ExhaustiveSearchCount = 0,
            QueryExecutionMs = 0,
            ServerTotalMs = 0,

            // Null should be interpreted as omitted for these properties
            EmbeddingMs = null,
            EmbeddingTokens = null,
            LastIncludedWriteAt = null,
        };

        Assert.Null(model.EmbeddingMs);
        Assert.False(model.RawData.ContainsKey("embedding_ms"));
        Assert.Null(model.EmbeddingTokens);
        Assert.False(model.RawData.ContainsKey("embedding_tokens"));
        Assert.Null(model.LastIncludedWriteAt);
        Assert.False(model.RawData.ContainsKey("last_included_write_at"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new QueryPerformance
        {
            ApproxNamespaceSize = 0,
            CacheHitRatio = 0,
            CacheTemperature = "cache_temperature",
            ExhaustiveSearchCount = 0,
            QueryExecutionMs = 0,
            ServerTotalMs = 0,

            // Null should be interpreted as omitted for these properties
            EmbeddingMs = null,
            EmbeddingTokens = null,
            LastIncludedWriteAt = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new QueryPerformance
        {
            ApproxNamespaceSize = 0,
            CacheHitRatio = 0,
            CacheTemperature = "cache_temperature",
            ExhaustiveSearchCount = 0,
            QueryExecutionMs = 0,
            ServerTotalMs = 0,
            EmbeddingMs = 0,
            EmbeddingTokens = 0,
            LastIncludedWriteAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
        };

        QueryPerformance copied = new(model);

        Assert.Equal(model, copied);
    }
}
