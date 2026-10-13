using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class RerankModelParamsTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new RerankModelParams
        {
            Attribute = "attribute",
            Model = "model",
            Query = "query",
        };

        string expectedAttribute = "attribute";
        string expectedModel = "model";
        string expectedQuery = "query";

        Assert.Equal(expectedAttribute, model.Attribute);
        Assert.Equal(expectedModel, model.Model);
        Assert.Equal(expectedQuery, model.Query);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new RerankModelParams
        {
            Attribute = "attribute",
            Model = "model",
            Query = "query",
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<RerankModelParams>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new RerankModelParams
        {
            Attribute = "attribute",
            Model = "model",
            Query = "query",
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<RerankModelParams>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        string expectedAttribute = "attribute";
        string expectedModel = "model";
        string expectedQuery = "query";

        Assert.Equal(expectedAttribute, deserialized.Attribute);
        Assert.Equal(expectedModel, deserialized.Model);
        Assert.Equal(expectedQuery, deserialized.Query);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new RerankModelParams
        {
            Attribute = "attribute",
            Model = "model",
            Query = "query",
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new RerankModelParams
        {
            Attribute = "attribute",
            Model = "model",
            Query = "query",
        };

        RerankModelParams copied = new(model);

        Assert.Equal(model, copied);
    }
}
