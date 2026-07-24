using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Exceptions;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class HighlightFragmentByTest : TestBase
{
    [Theory]
    [InlineData(HighlightFragmentBy.None)]
    [InlineData(HighlightFragmentBy.Sentence)]
    [InlineData(HighlightFragmentBy.Paragraph)]
    [InlineData(HighlightFragmentBy.Word)]
    public void Validation_Works(HighlightFragmentBy rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<string, HighlightFragmentBy> value = rawValue;
        value.Validate();
    }

    [Fact]
    public void InvalidEnumValidationThrows_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<string, HighlightFragmentBy>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );

        Assert.NotNull(value);
        Assert.Throws<TurbopufferInvalidDataException>(() => value.Validate());
    }

    [Theory]
    [InlineData(HighlightFragmentBy.None)]
    [InlineData(HighlightFragmentBy.Sentence)]
    [InlineData(HighlightFragmentBy.Paragraph)]
    [InlineData(HighlightFragmentBy.Word)]
    public void SerializationRoundtrip_Works(HighlightFragmentBy rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<string, HighlightFragmentBy> value = rawValue;

        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ApiEnum<string, HighlightFragmentBy>>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void InvalidEnumSerializationRoundtrip_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<string, HighlightFragmentBy>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );
        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ApiEnum<string, HighlightFragmentBy>>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}
