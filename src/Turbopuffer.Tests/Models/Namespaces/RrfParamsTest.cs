using System.Collections.Generic;
using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class RrfParamsTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new RrfParams { RankConstant = 0, Weights = [1] };

        long expectedRankConstant = 0;
        List<float> expectedWeights = [1];

        Assert.Equal(expectedRankConstant, model.RankConstant);
        Assert.NotNull(model.Weights);
        Assert.Equal(expectedWeights.Count, model.Weights.Count);
        for (int i = 0; i < expectedWeights.Count; i++)
        {
            Assert.Equal(expectedWeights[i], model.Weights[i]);
        }
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new RrfParams { RankConstant = 0, Weights = [1] };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<RrfParams>(json, ModelBase.SerializerOptions);

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new RrfParams { RankConstant = 0, Weights = [1] };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<RrfParams>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        long expectedRankConstant = 0;
        List<float> expectedWeights = [1];

        Assert.Equal(expectedRankConstant, deserialized.RankConstant);
        Assert.NotNull(deserialized.Weights);
        Assert.Equal(expectedWeights.Count, deserialized.Weights.Count);
        for (int i = 0; i < expectedWeights.Count; i++)
        {
            Assert.Equal(expectedWeights[i], deserialized.Weights[i]);
        }
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new RrfParams { RankConstant = 0, Weights = [1] };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new RrfParams { };

        Assert.Null(model.RankConstant);
        Assert.False(model.RawData.ContainsKey("rank_constant"));
        Assert.Null(model.Weights);
        Assert.False(model.RawData.ContainsKey("weights"));
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
            Weights = null,
        };

        Assert.Null(model.RankConstant);
        Assert.False(model.RawData.ContainsKey("rank_constant"));
        Assert.Null(model.Weights);
        Assert.False(model.RawData.ContainsKey("weights"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new RrfParams
        {
            // Null should be interpreted as omitted for these properties
            RankConstant = null,
            Weights = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new RrfParams { RankConstant = 0, Weights = [1] };

        RrfParams copied = new(model);

        Assert.Equal(model, copied);
    }
}
