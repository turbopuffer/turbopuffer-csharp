using System.Collections.Generic;
using System.Text.Json;
using Turbopuffer.Client.Core;
using Turbopuffer.Client.Models.Namespaces;

namespace Turbopuffer.Client.Tests.Models.Namespaces;

public class ColumnsTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new Columns
        {
            ID = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],
            Vector = new([new([0])]),
        };

        List<ID> expectedID = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"];
        Vector expectedVector = new([new([0])]);

        Assert.Equal(expectedID.Count, model.ID.Count);
        for (int i = 0; i < expectedID.Count; i++)
        {
            Assert.Equal(expectedID[i], model.ID[i]);
        }
        Assert.Equal(expectedVector, model.Vector);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new Columns
        {
            ID = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],
            Vector = new([new([0])]),
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Columns>(json, ModelBase.SerializerOptions);

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new Columns
        {
            ID = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],
            Vector = new([new([0])]),
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Columns>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        List<ID> expectedID = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"];
        Vector expectedVector = new([new([0])]);

        Assert.Equal(expectedID.Count, deserialized.ID.Count);
        for (int i = 0; i < expectedID.Count; i++)
        {
            Assert.Equal(expectedID[i], deserialized.ID[i]);
        }
        Assert.Equal(expectedVector, deserialized.Vector);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new Columns
        {
            ID = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],
            Vector = new([new([0])]),
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new Columns { ID = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"] };

        Assert.Null(model.Vector);
        Assert.False(model.RawData.ContainsKey("vector"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new Columns { ID = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"] };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new Columns
        {
            ID = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],

            // Null should be interpreted as omitted for these properties
            Vector = null,
        };

        Assert.Null(model.Vector);
        Assert.False(model.RawData.ContainsKey("vector"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new Columns
        {
            ID = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],

            // Null should be interpreted as omitted for these properties
            Vector = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new Columns
        {
            ID = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],
            Vector = new([new([0])]),
        };

        Columns copied = new(model);

        Assert.Equal(model, copied);
    }
}

public class VectorTest : TestBase
{
    [Fact]
    public void NamespaceVectorsValidationWorks()
    {
        Vector value = new([new([0])]);
        value.Validate();
    }

    [Fact]
    public void VectorValidationWorks()
    {
        Vector value = new([0]);
        value.Validate();
    }

    [Fact]
    public void NamespaceVectorsSerializationRoundtripWorks()
    {
        Vector value = new([new([0])]);
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Vector>(element, ModelBase.SerializerOptions);

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void VectorSerializationRoundtripWorks()
    {
        Vector value = new([0]);
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Vector>(element, ModelBase.SerializerOptions);

        Assert.Equal(value, deserialized);
    }
}
