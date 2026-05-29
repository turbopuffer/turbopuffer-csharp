using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class AttributeEmbedConfigTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new AttributeEmbedConfig
        {
            Model = "model",
            Attribute = "attribute",
            Dims = 0,
        };

        string expectedModel = "model";
        string expectedAttribute = "attribute";
        long expectedDims = 0;

        Assert.Equal(expectedModel, model.Model);
        Assert.Equal(expectedAttribute, model.Attribute);
        Assert.Equal(expectedDims, model.Dims);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new AttributeEmbedConfig
        {
            Model = "model",
            Attribute = "attribute",
            Dims = 0,
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<AttributeEmbedConfig>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new AttributeEmbedConfig
        {
            Model = "model",
            Attribute = "attribute",
            Dims = 0,
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<AttributeEmbedConfig>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        string expectedModel = "model";
        string expectedAttribute = "attribute";
        long expectedDims = 0;

        Assert.Equal(expectedModel, deserialized.Model);
        Assert.Equal(expectedAttribute, deserialized.Attribute);
        Assert.Equal(expectedDims, deserialized.Dims);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new AttributeEmbedConfig
        {
            Model = "model",
            Attribute = "attribute",
            Dims = 0,
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new AttributeEmbedConfig { Model = "model" };

        Assert.Null(model.Attribute);
        Assert.False(model.RawData.ContainsKey("attribute"));
        Assert.Null(model.Dims);
        Assert.False(model.RawData.ContainsKey("dims"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new AttributeEmbedConfig { Model = "model" };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new AttributeEmbedConfig
        {
            Model = "model",

            // Null should be interpreted as omitted for these properties
            Attribute = null,
            Dims = null,
        };

        Assert.Null(model.Attribute);
        Assert.False(model.RawData.ContainsKey("attribute"));
        Assert.Null(model.Dims);
        Assert.False(model.RawData.ContainsKey("dims"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new AttributeEmbedConfig
        {
            Model = "model",

            // Null should be interpreted as omitted for these properties
            Attribute = null,
            Dims = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new AttributeEmbedConfig
        {
            Model = "model",
            Attribute = "attribute",
            Dims = 0,
        };

        AttributeEmbedConfig copied = new(model);

        Assert.Equal(model, copied);
    }
}
