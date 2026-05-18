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
        };

        long expectedApproxNamespaceSize = 0;
        double expectedCacheHitRatio = 0;
        string expectedCacheTemperature = "cache_temperature";
        long expectedExhaustiveSearchCount = 0;
        long expectedQueryExecutionMs = 0;
        long expectedServerTotalMs = 0;

        Assert.Equal(expectedApproxNamespaceSize, model.ApproxNamespaceSize);
        Assert.Equal(expectedCacheHitRatio, model.CacheHitRatio);
        Assert.Equal(expectedCacheTemperature, model.CacheTemperature);
        Assert.Equal(expectedExhaustiveSearchCount, model.ExhaustiveSearchCount);
        Assert.Equal(expectedQueryExecutionMs, model.QueryExecutionMs);
        Assert.Equal(expectedServerTotalMs, model.ServerTotalMs);
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

        Assert.Equal(expectedApproxNamespaceSize, deserialized.ApproxNamespaceSize);
        Assert.Equal(expectedCacheHitRatio, deserialized.CacheHitRatio);
        Assert.Equal(expectedCacheTemperature, deserialized.CacheTemperature);
        Assert.Equal(expectedExhaustiveSearchCount, deserialized.ExhaustiveSearchCount);
        Assert.Equal(expectedQueryExecutionMs, deserialized.QueryExecutionMs);
        Assert.Equal(expectedServerTotalMs, deserialized.ServerTotalMs);
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
        };

        QueryPerformance copied = new(model);

        Assert.Equal(model, copied);
    }
}
