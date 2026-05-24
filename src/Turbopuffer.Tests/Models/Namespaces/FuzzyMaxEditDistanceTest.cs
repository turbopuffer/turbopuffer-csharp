using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class FuzzyMaxEditDistanceTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new FuzzyMaxEditDistance { Distance = 0, MinQueryChars = 0 };

        long expectedDistance = 0;
        long expectedMinQueryChars = 0;

        Assert.Equal(expectedDistance, model.Distance);
        Assert.Equal(expectedMinQueryChars, model.MinQueryChars);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new FuzzyMaxEditDistance { Distance = 0, MinQueryChars = 0 };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<FuzzyMaxEditDistance>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new FuzzyMaxEditDistance { Distance = 0, MinQueryChars = 0 };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<FuzzyMaxEditDistance>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        long expectedDistance = 0;
        long expectedMinQueryChars = 0;

        Assert.Equal(expectedDistance, deserialized.Distance);
        Assert.Equal(expectedMinQueryChars, deserialized.MinQueryChars);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new FuzzyMaxEditDistance { Distance = 0, MinQueryChars = 0 };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new FuzzyMaxEditDistance { Distance = 0, MinQueryChars = 0 };

        FuzzyMaxEditDistance copied = new(model);

        Assert.Equal(model, copied);
    }
}
