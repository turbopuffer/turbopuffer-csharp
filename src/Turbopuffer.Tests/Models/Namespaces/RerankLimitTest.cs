using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class RerankLimitTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new RerankLimit { Total = 0 };

        long expectedTotal = 0;

        Assert.Equal(expectedTotal, model.Total);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new RerankLimit { Total = 0 };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<RerankLimit>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new RerankLimit { Total = 0 };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<RerankLimit>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        long expectedTotal = 0;

        Assert.Equal(expectedTotal, deserialized.Total);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new RerankLimit { Total = 0 };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new RerankLimit { Total = 0 };

        RerankLimit copied = new(model);

        Assert.Equal(model, copied);
    }
}
