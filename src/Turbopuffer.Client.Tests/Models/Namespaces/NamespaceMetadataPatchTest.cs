using System.Text.Json;
using Turbopuffer.Client.Core;
using Turbopuffer.Client.Models.Namespaces;

namespace Turbopuffer.Client.Tests.Models.Namespaces;

public class NamespaceMetadataPatchTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new NamespaceMetadataPatch { Pinning = true };

        NamespaceMetadataPatchPinning expectedPinning = true;

        Assert.Equal(expectedPinning, model.Pinning);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new NamespaceMetadataPatch { Pinning = true };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<NamespaceMetadataPatch>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new NamespaceMetadataPatch { Pinning = true };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<NamespaceMetadataPatch>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        NamespaceMetadataPatchPinning expectedPinning = true;

        Assert.Equal(expectedPinning, deserialized.Pinning);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new NamespaceMetadataPatch { Pinning = true };

        model.Validate();
    }

    [Fact]
    public void OptionalNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new NamespaceMetadataPatch { };

        Assert.Null(model.Pinning);
        Assert.False(model.RawData.ContainsKey("pinning"));
    }

    [Fact]
    public void OptionalNullablePropertiesUnsetValidation_Works()
    {
        var model = new NamespaceMetadataPatch { };

        model.Validate();
    }

    [Fact]
    public void OptionalNullablePropertiesSetToNullAreSetToNull_Works()
    {
        var model = new NamespaceMetadataPatch { Pinning = null };

        Assert.Null(model.Pinning);
        Assert.True(model.RawData.ContainsKey("pinning"));
    }

    [Fact]
    public void OptionalNullablePropertiesSetToNullValidation_Works()
    {
        var model = new NamespaceMetadataPatch { Pinning = null };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new NamespaceMetadataPatch { Pinning = true };

        NamespaceMetadataPatch copied = new(model);

        Assert.Equal(model, copied);
    }
}

public class NamespaceMetadataPatchPinningTest : TestBase
{
    [Fact]
    public void BoolValidationWorks()
    {
        NamespaceMetadataPatchPinning value = true;
        value.Validate();
    }

    [Fact]
    public void ConfigValidationWorks()
    {
        NamespaceMetadataPatchPinning value = new PinningConfig() { Replicas = 1 };
        value.Validate();
    }

    [Fact]
    public void BoolSerializationRoundtripWorks()
    {
        NamespaceMetadataPatchPinning value = true;
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<NamespaceMetadataPatchPinning>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void ConfigSerializationRoundtripWorks()
    {
        NamespaceMetadataPatchPinning value = new PinningConfig() { Replicas = 1 };
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<NamespaceMetadataPatchPinning>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}
