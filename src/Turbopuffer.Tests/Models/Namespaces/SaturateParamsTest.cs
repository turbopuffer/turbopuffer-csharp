using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class SaturateParamsTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new SaturateParams
        {
            Exponent = 0,
            Midpoint = JsonSerializer.Deserialize<JsonElement>("{}"),
        };

        double expectedExponent = 0;
        JsonElement expectedMidpoint = JsonSerializer.Deserialize<JsonElement>("{}");

        Assert.Equal(expectedExponent, model.Exponent);
        Assert.NotNull(model.Midpoint);
        Assert.True(JsonElement.DeepEquals(expectedMidpoint, model.Midpoint.Value));
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new SaturateParams
        {
            Exponent = 0,
            Midpoint = JsonSerializer.Deserialize<JsonElement>("{}"),
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<SaturateParams>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new SaturateParams
        {
            Exponent = 0,
            Midpoint = JsonSerializer.Deserialize<JsonElement>("{}"),
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<SaturateParams>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        double expectedExponent = 0;
        JsonElement expectedMidpoint = JsonSerializer.Deserialize<JsonElement>("{}");

        Assert.Equal(expectedExponent, deserialized.Exponent);
        Assert.NotNull(deserialized.Midpoint);
        Assert.True(JsonElement.DeepEquals(expectedMidpoint, deserialized.Midpoint.Value));
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new SaturateParams
        {
            Exponent = 0,
            Midpoint = JsonSerializer.Deserialize<JsonElement>("{}"),
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new SaturateParams { };

        Assert.Null(model.Exponent);
        Assert.False(model.RawData.ContainsKey("exponent"));
        Assert.Null(model.Midpoint);
        Assert.False(model.RawData.ContainsKey("midpoint"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new SaturateParams { };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new SaturateParams
        {
            // Null should be interpreted as omitted for these properties
            Exponent = null,
            Midpoint = null,
        };

        Assert.Null(model.Exponent);
        Assert.False(model.RawData.ContainsKey("exponent"));
        Assert.Null(model.Midpoint);
        Assert.False(model.RawData.ContainsKey("midpoint"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new SaturateParams
        {
            // Null should be interpreted as omitted for these properties
            Exponent = null,
            Midpoint = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new SaturateParams
        {
            Exponent = 0,
            Midpoint = JsonSerializer.Deserialize<JsonElement>("{}"),
        };

        SaturateParams copied = new(model);

        Assert.Equal(model, copied);
    }
}
