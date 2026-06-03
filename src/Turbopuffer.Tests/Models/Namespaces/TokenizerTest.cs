using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Exceptions;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class TokenizerTest : TestBase
{
    [Theory]
    [InlineData(Tokenizer.PreTokenizedArray)]
    [InlineData(Tokenizer.WordV0)]
    [InlineData(Tokenizer.WordV1)]
    [InlineData(Tokenizer.WordV2)]
    [InlineData(Tokenizer.WordV3)]
    [InlineData(Tokenizer.WordV4)]
    public void Validation_Works(Tokenizer rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<string, Tokenizer> value = rawValue;
        value.Validate();
    }

    [Fact]
    public void InvalidEnumValidationThrows_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<string, Tokenizer>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );

        Assert.NotNull(value);
        Assert.Throws<TurbopufferInvalidDataException>(() => value.Validate());
    }

    [Theory]
    [InlineData(Tokenizer.PreTokenizedArray)]
    [InlineData(Tokenizer.WordV0)]
    [InlineData(Tokenizer.WordV1)]
    [InlineData(Tokenizer.WordV2)]
    [InlineData(Tokenizer.WordV3)]
    [InlineData(Tokenizer.WordV4)]
    public void SerializationRoundtrip_Works(Tokenizer rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<string, Tokenizer> value = rawValue;

        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ApiEnum<string, Tokenizer>>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void InvalidEnumSerializationRoundtrip_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<string, Tokenizer>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );
        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ApiEnum<string, Tokenizer>>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}
