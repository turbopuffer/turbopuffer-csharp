using System.Text.Json;
using Turbopuffer.Client.Core;
using Turbopuffer.Client.Models.Namespaces;

namespace Turbopuffer.Client.Tests.Models.Namespaces;

public class RowTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new Row { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) };

        ID expectedID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e";
        NamespaceVector expectedVector = new([0]);

        Assert.Equal(expectedID, model.ID);
        Assert.Equal(expectedVector, model.Vector);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new Row { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Row>(json, ModelBase.SerializerOptions);

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new Row { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Row>(element, ModelBase.SerializerOptions);
        Assert.NotNull(deserialized);

        ID expectedID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e";
        NamespaceVector expectedVector = new([0]);

        Assert.Equal(expectedID, deserialized.ID);
        Assert.Equal(expectedVector, deserialized.Vector);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new Row { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new Row { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e" };

        Assert.Null(model.Vector);
        Assert.False(model.RawData.ContainsKey("vector"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new Row { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e" };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new Row
        {
            ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e",

            // Null should be interpreted as omitted for these properties
            Vector = null,
        };

        Assert.Null(model.Vector);
        Assert.False(model.RawData.ContainsKey("vector"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new Row
        {
            ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e",

            // Null should be interpreted as omitted for these properties
            Vector = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new Row { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) };

        Row copied = new(model);

        Assert.Equal(model, copied);
    }
}
