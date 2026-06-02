using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class RrfParamsTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new RrfParams { RankConstant = 0 };

        long expectedRankConstant = 0;

        Assert.Equal(expectedRankConstant, model.RankConstant);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new RrfParams { RankConstant = 0 };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<RrfParams>(json, ModelBase.SerializerOptions);

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new RrfParams { RankConstant = 0 };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<RrfParams>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        long expectedRankConstant = 0;

        Assert.Equal(expectedRankConstant, deserialized.RankConstant);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new RrfParams { RankConstant = 0 };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new RrfParams { };

        Assert.Null(model.RankConstant);
        Assert.False(model.RawData.ContainsKey("rank_constant"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new RrfParams { };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new RrfParams
        {
            // Null should be interpreted as omitted for these properties
            RankConstant = null,
        };

        Assert.Null(model.RankConstant);
        Assert.False(model.RawData.ContainsKey("rank_constant"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new RrfParams
        {
            // Null should be interpreted as omitted for these properties
            RankConstant = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new RrfParams { RankConstant = 0 };

        RrfParams copied = new(model);

        Assert.Equal(model, copied);
    }
}
