using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class WritePerformanceTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new WritePerformance
        {
            ServerTotalMs = 0,
            EmbeddingMs = 0,
            EmbeddingTokens = 0,
        };

        long expectedServerTotalMs = 0;
        long expectedEmbeddingMs = 0;
        long expectedEmbeddingTokens = 0;

        Assert.Equal(expectedServerTotalMs, model.ServerTotalMs);
        Assert.Equal(expectedEmbeddingMs, model.EmbeddingMs);
        Assert.Equal(expectedEmbeddingTokens, model.EmbeddingTokens);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new WritePerformance
        {
            ServerTotalMs = 0,
            EmbeddingMs = 0,
            EmbeddingTokens = 0,
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<WritePerformance>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new WritePerformance
        {
            ServerTotalMs = 0,
            EmbeddingMs = 0,
            EmbeddingTokens = 0,
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<WritePerformance>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        long expectedServerTotalMs = 0;
        long expectedEmbeddingMs = 0;
        long expectedEmbeddingTokens = 0;

        Assert.Equal(expectedServerTotalMs, deserialized.ServerTotalMs);
        Assert.Equal(expectedEmbeddingMs, deserialized.EmbeddingMs);
        Assert.Equal(expectedEmbeddingTokens, deserialized.EmbeddingTokens);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new WritePerformance
        {
            ServerTotalMs = 0,
            EmbeddingMs = 0,
            EmbeddingTokens = 0,
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new WritePerformance { ServerTotalMs = 0 };

        Assert.Null(model.EmbeddingMs);
        Assert.False(model.RawData.ContainsKey("embedding_ms"));
        Assert.Null(model.EmbeddingTokens);
        Assert.False(model.RawData.ContainsKey("embedding_tokens"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new WritePerformance { ServerTotalMs = 0 };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new WritePerformance
        {
            ServerTotalMs = 0,

            // Null should be interpreted as omitted for these properties
            EmbeddingMs = null,
            EmbeddingTokens = null,
        };

        Assert.Null(model.EmbeddingMs);
        Assert.False(model.RawData.ContainsKey("embedding_ms"));
        Assert.Null(model.EmbeddingTokens);
        Assert.False(model.RawData.ContainsKey("embedding_tokens"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new WritePerformance
        {
            ServerTotalMs = 0,

            // Null should be interpreted as omitted for these properties
            EmbeddingMs = null,
            EmbeddingTokens = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new WritePerformance
        {
            ServerTotalMs = 0,
            EmbeddingMs = 0,
            EmbeddingTokens = 0,
        };

        WritePerformance copied = new(model);

        Assert.Equal(model, copied);
    }
}
