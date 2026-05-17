using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class WritePerformanceTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new WritePerformance { ServerTotalMs = 0 };

        long expectedServerTotalMs = 0;

        Assert.Equal(expectedServerTotalMs, model.ServerTotalMs);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new WritePerformance { ServerTotalMs = 0 };

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
        var model = new WritePerformance { ServerTotalMs = 0 };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<WritePerformance>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        long expectedServerTotalMs = 0;

        Assert.Equal(expectedServerTotalMs, deserialized.ServerTotalMs);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new WritePerformance { ServerTotalMs = 0 };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new WritePerformance { ServerTotalMs = 0 };

        WritePerformance copied = new(model);

        Assert.Equal(model, copied);
    }
}
