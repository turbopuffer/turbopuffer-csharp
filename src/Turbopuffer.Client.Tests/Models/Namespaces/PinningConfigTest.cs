using System.Text.Json;
using Turbopuffer.Client.Core;
using Turbopuffer.Client.Models.Namespaces;

namespace Turbopuffer.Client.Tests.Models.Namespaces;

public class PinningConfigTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new PinningConfig { Replicas = 1 };

        long expectedReplicas = 1;

        Assert.Equal(expectedReplicas, model.Replicas);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new PinningConfig { Replicas = 1 };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<PinningConfig>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new PinningConfig { Replicas = 1 };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<PinningConfig>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        long expectedReplicas = 1;

        Assert.Equal(expectedReplicas, deserialized.Replicas);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new PinningConfig { Replicas = 1 };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new PinningConfig { };

        Assert.Null(model.Replicas);
        Assert.False(model.RawData.ContainsKey("replicas"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new PinningConfig { };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new PinningConfig
        {
            // Null should be interpreted as omitted for these properties
            Replicas = null,
        };

        Assert.Null(model.Replicas);
        Assert.False(model.RawData.ContainsKey("replicas"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new PinningConfig
        {
            // Null should be interpreted as omitted for these properties
            Replicas = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new PinningConfig { Replicas = 1 };

        PinningConfig copied = new(model);

        Assert.Equal(model, copied);
    }
}
