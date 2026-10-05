using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class AttributeSchemaDropTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new AttributeSchemaDrop { Drop = true };

        bool expectedDrop = true;

        Assert.Equal(expectedDrop, model.Drop);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new AttributeSchemaDrop { Drop = true };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<AttributeSchemaDrop>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new AttributeSchemaDrop { Drop = true };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<AttributeSchemaDrop>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        bool expectedDrop = true;

        Assert.Equal(expectedDrop, deserialized.Drop);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new AttributeSchemaDrop { Drop = true };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new AttributeSchemaDrop { Drop = true };

        AttributeSchemaDrop copied = new(model);

        Assert.Equal(model, copied);
    }
}
