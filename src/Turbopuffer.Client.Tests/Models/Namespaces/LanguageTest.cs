using System.Text.Json;
using Turbopuffer.Client.Core;
using Turbopuffer.Client.Exceptions;
using Turbopuffer.Client.Models.Namespaces;

namespace Turbopuffer.Client.Tests.Models.Namespaces;

public class LanguageTest : TestBase
{
    [Theory]
    [InlineData(Language.Arabic)]
    [InlineData(Language.Danish)]
    [InlineData(Language.Dutch)]
    [InlineData(Language.English)]
    [InlineData(Language.Finnish)]
    [InlineData(Language.French)]
    [InlineData(Language.German)]
    [InlineData(Language.Greek)]
    [InlineData(Language.Hungarian)]
    [InlineData(Language.Italian)]
    [InlineData(Language.Norwegian)]
    [InlineData(Language.Portuguese)]
    [InlineData(Language.Romanian)]
    [InlineData(Language.Russian)]
    [InlineData(Language.Spanish)]
    [InlineData(Language.Swedish)]
    [InlineData(Language.Tamil)]
    [InlineData(Language.Turkish)]
    public void Validation_Works(Language rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<string, Language> value = rawValue;
        value.Validate();
    }

    [Fact]
    public void InvalidEnumValidationThrows_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<string, Language>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );

        Assert.NotNull(value);
        Assert.Throws<TurbopufferInvalidDataException>(() => value.Validate());
    }

    [Theory]
    [InlineData(Language.Arabic)]
    [InlineData(Language.Danish)]
    [InlineData(Language.Dutch)]
    [InlineData(Language.English)]
    [InlineData(Language.Finnish)]
    [InlineData(Language.French)]
    [InlineData(Language.German)]
    [InlineData(Language.Greek)]
    [InlineData(Language.Hungarian)]
    [InlineData(Language.Italian)]
    [InlineData(Language.Norwegian)]
    [InlineData(Language.Portuguese)]
    [InlineData(Language.Romanian)]
    [InlineData(Language.Russian)]
    [InlineData(Language.Spanish)]
    [InlineData(Language.Swedish)]
    [InlineData(Language.Tamil)]
    [InlineData(Language.Turkish)]
    public void SerializationRoundtrip_Works(Language rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<string, Language> value = rawValue;

        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ApiEnum<string, Language>>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void InvalidEnumSerializationRoundtrip_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<string, Language>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );
        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ApiEnum<string, Language>>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}
