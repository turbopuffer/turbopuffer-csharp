using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class ShardingConfigTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new ShardingConfig { NumShards = 1 };

        int expectedNumShards = 1;

        Assert.Equal(expectedNumShards, model.NumShards);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new ShardingConfig { NumShards = 1 };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ShardingConfig>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new ShardingConfig { NumShards = 1 };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ShardingConfig>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        int expectedNumShards = 1;

        Assert.Equal(expectedNumShards, deserialized.NumShards);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new ShardingConfig { NumShards = 1 };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new ShardingConfig { NumShards = 1 };

        ShardingConfig copied = new(model);

        Assert.Equal(model, copied);
    }
}
