using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Exceptions;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class DistanceMetricTest : TestBase
{
    [Theory]
    [InlineData(DistanceMetric.CosineDistance)]
    [InlineData(DistanceMetric.EuclideanSquared)]
    public void Validation_Works(DistanceMetric rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<string, DistanceMetric> value = rawValue;
        value.Validate();
    }

    [Fact]
    public void InvalidEnumValidationThrows_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<string, DistanceMetric>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );

        Assert.NotNull(value);
        Assert.Throws<TurbopufferInvalidDataException>(() => value.Validate());
    }

    [Theory]
    [InlineData(DistanceMetric.CosineDistance)]
    [InlineData(DistanceMetric.EuclideanSquared)]
    public void SerializationRoundtrip_Works(DistanceMetric rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<string, DistanceMetric> value = rawValue;

        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ApiEnum<string, DistanceMetric>>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void InvalidEnumSerializationRoundtrip_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<string, DistanceMetric>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );
        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ApiEnum<string, DistanceMetric>>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}
