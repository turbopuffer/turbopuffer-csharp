using System.Collections.Generic;
using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class NamespaceLimitTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new NamespaceLimit
        {
            Total = 0,
            Per = new() { Attributes = ["string"], Limit = 0 },
        };

        long expectedTotal = 0;
        Per expectedPer = new() { Attributes = ["string"], Limit = 0 };

        Assert.Equal(expectedTotal, model.Total);
        Assert.Equal(expectedPer, model.Per);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new NamespaceLimit
        {
            Total = 0,
            Per = new() { Attributes = ["string"], Limit = 0 },
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<NamespaceLimit>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new NamespaceLimit
        {
            Total = 0,
            Per = new() { Attributes = ["string"], Limit = 0 },
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<NamespaceLimit>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        long expectedTotal = 0;
        Per expectedPer = new() { Attributes = ["string"], Limit = 0 };

        Assert.Equal(expectedTotal, deserialized.Total);
        Assert.Equal(expectedPer, deserialized.Per);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new NamespaceLimit
        {
            Total = 0,
            Per = new() { Attributes = ["string"], Limit = 0 },
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new NamespaceLimit { Total = 0 };

        Assert.Null(model.Per);
        Assert.False(model.RawData.ContainsKey("per"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new NamespaceLimit { Total = 0 };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new NamespaceLimit
        {
            Total = 0,

            // Null should be interpreted as omitted for these properties
            Per = null,
        };

        Assert.Null(model.Per);
        Assert.False(model.RawData.ContainsKey("per"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new NamespaceLimit
        {
            Total = 0,

            // Null should be interpreted as omitted for these properties
            Per = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new NamespaceLimit
        {
            Total = 0,
            Per = new() { Attributes = ["string"], Limit = 0 },
        };

        NamespaceLimit copied = new(model);

        Assert.Equal(model, copied);
    }
}

public class PerTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new Per { Attributes = ["string"], Limit = 0 };

        List<string> expectedAttributes = ["string"];
        long expectedLimit = 0;

        Assert.Equal(expectedAttributes.Count, model.Attributes.Count);
        for (int i = 0; i < expectedAttributes.Count; i++)
        {
            Assert.Equal(expectedAttributes[i], model.Attributes[i]);
        }
        Assert.Equal(expectedLimit, model.Limit);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new Per { Attributes = ["string"], Limit = 0 };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Per>(json, ModelBase.SerializerOptions);

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new Per { Attributes = ["string"], Limit = 0 };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Per>(element, ModelBase.SerializerOptions);
        Assert.NotNull(deserialized);

        List<string> expectedAttributes = ["string"];
        long expectedLimit = 0;

        Assert.Equal(expectedAttributes.Count, deserialized.Attributes.Count);
        for (int i = 0; i < expectedAttributes.Count; i++)
        {
            Assert.Equal(expectedAttributes[i], deserialized.Attributes[i]);
        }
        Assert.Equal(expectedLimit, deserialized.Limit);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new Per { Attributes = ["string"], Limit = 0 };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new Per { Attributes = ["string"], Limit = 0 };

        Per copied = new(model);

        Assert.Equal(model, copied);
    }
}
