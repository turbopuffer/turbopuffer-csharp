using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Exceptions;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class HighlightOffsetUnitsTest : TestBase
{
    [Theory]
    [InlineData(HighlightOffsetUnits.Utf8)]
    [InlineData(HighlightOffsetUnits.Utf16)]
    [InlineData(HighlightOffsetUnits.Codepoints)]
    public void Validation_Works(HighlightOffsetUnits rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<string, HighlightOffsetUnits> value = rawValue;
        value.Validate();
    }

    [Fact]
    public void InvalidEnumValidationThrows_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<string, HighlightOffsetUnits>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );

        Assert.NotNull(value);
        Assert.Throws<TurbopufferInvalidDataException>(() => value.Validate());
    }

    [Theory]
    [InlineData(HighlightOffsetUnits.Utf8)]
    [InlineData(HighlightOffsetUnits.Utf16)]
    [InlineData(HighlightOffsetUnits.Codepoints)]
    public void SerializationRoundtrip_Works(HighlightOffsetUnits rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<string, HighlightOffsetUnits> value = rawValue;

        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ApiEnum<string, HighlightOffsetUnits>>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void InvalidEnumSerializationRoundtrip_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<string, HighlightOffsetUnits>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );
        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ApiEnum<string, HighlightOffsetUnits>>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}
