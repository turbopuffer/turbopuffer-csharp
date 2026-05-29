using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class AttributeEmbedTest : TestBase
{
    [Fact]
    public void StringValidationWorks()
    {
        AttributeEmbed value = "string";
        value.Validate();
    }

    [Fact]
    public void ConfigValidationWorks()
    {
        AttributeEmbed value = new AttributeEmbedConfig()
        {
            Model = "model",
            Attribute = "attribute",
            Dims = 0,
        };
        value.Validate();
    }

    [Fact]
    public void StringSerializationRoundtripWorks()
    {
        AttributeEmbed value = "string";
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<AttributeEmbed>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void ConfigSerializationRoundtripWorks()
    {
        AttributeEmbed value = new AttributeEmbedConfig()
        {
            Model = "model",
            Attribute = "attribute",
            Dims = 0,
        };
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<AttributeEmbed>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}
