using System.Collections.Generic;
using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class FuzzyParamsTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new FuzzyParams
        {
            MaxEditDistance = [new() { Distance = 0, MinQueryChars = 0 }],
            CaseSensitive = true,
        };

        List<FuzzyMaxEditDistance> expectedMaxEditDistance =
        [
            new() { Distance = 0, MinQueryChars = 0 },
        ];
        bool expectedCaseSensitive = true;

        Assert.Equal(expectedMaxEditDistance.Count, model.MaxEditDistance.Count);
        for (int i = 0; i < expectedMaxEditDistance.Count; i++)
        {
            Assert.Equal(expectedMaxEditDistance[i], model.MaxEditDistance[i]);
        }
        Assert.Equal(expectedCaseSensitive, model.CaseSensitive);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new FuzzyParams
        {
            MaxEditDistance = [new() { Distance = 0, MinQueryChars = 0 }],
            CaseSensitive = true,
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<FuzzyParams>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new FuzzyParams
        {
            MaxEditDistance = [new() { Distance = 0, MinQueryChars = 0 }],
            CaseSensitive = true,
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<FuzzyParams>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        List<FuzzyMaxEditDistance> expectedMaxEditDistance =
        [
            new() { Distance = 0, MinQueryChars = 0 },
        ];
        bool expectedCaseSensitive = true;

        Assert.Equal(expectedMaxEditDistance.Count, deserialized.MaxEditDistance.Count);
        for (int i = 0; i < expectedMaxEditDistance.Count; i++)
        {
            Assert.Equal(expectedMaxEditDistance[i], deserialized.MaxEditDistance[i]);
        }
        Assert.Equal(expectedCaseSensitive, deserialized.CaseSensitive);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new FuzzyParams
        {
            MaxEditDistance = [new() { Distance = 0, MinQueryChars = 0 }],
            CaseSensitive = true,
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new FuzzyParams
        {
            MaxEditDistance = [new() { Distance = 0, MinQueryChars = 0 }],
        };

        Assert.Null(model.CaseSensitive);
        Assert.False(model.RawData.ContainsKey("case_sensitive"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new FuzzyParams
        {
            MaxEditDistance = [new() { Distance = 0, MinQueryChars = 0 }],
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new FuzzyParams
        {
            MaxEditDistance = [new() { Distance = 0, MinQueryChars = 0 }],

            // Null should be interpreted as omitted for these properties
            CaseSensitive = null,
        };

        Assert.Null(model.CaseSensitive);
        Assert.False(model.RawData.ContainsKey("case_sensitive"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new FuzzyParams
        {
            MaxEditDistance = [new() { Distance = 0, MinQueryChars = 0 }],

            // Null should be interpreted as omitted for these properties
            CaseSensitive = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new FuzzyParams
        {
            MaxEditDistance = [new() { Distance = 0, MinQueryChars = 0 }],
            CaseSensitive = true,
        };

        FuzzyParams copied = new(model);

        Assert.Equal(model, copied);
    }
}
