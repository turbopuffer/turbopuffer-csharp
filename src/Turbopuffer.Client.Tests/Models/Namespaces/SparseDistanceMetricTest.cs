using System.Text.Json;
using Turbopuffer.Client.Core;
using Turbopuffer.Client.Exceptions;
using Turbopuffer.Client.Models.Namespaces;

namespace Turbopuffer.Client.Tests.Models.Namespaces;

public class SparseDistanceMetricTest : TestBase
{
    [Theory]
    [InlineData(SparseDistanceMetric.DotProduct)]
    public void Validation_Works(SparseDistanceMetric rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<string, SparseDistanceMetric> value = rawValue;
        value.Validate();
    }

    [Fact]
    public void InvalidEnumValidationThrows_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<string, SparseDistanceMetric>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );

        Assert.NotNull(value);
        Assert.Throws<TurbopufferInvalidDataException>(() => value.Validate());
    }

    [Theory]
    [InlineData(SparseDistanceMetric.DotProduct)]
    public void SerializationRoundtrip_Works(SparseDistanceMetric rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<string, SparseDistanceMetric> value = rawValue;

        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ApiEnum<string, SparseDistanceMetric>>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void InvalidEnumSerializationRoundtrip_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<string, SparseDistanceMetric>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );
        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ApiEnum<string, SparseDistanceMetric>>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}
