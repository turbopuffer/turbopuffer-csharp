using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class FullTextSearchTest : TestBase
{
    [Fact]
    public void BoolValidationWorks()
    {
        FullTextSearch value = true;
        value.Validate();
    }

    [Fact]
    public void ConfigValidationWorks()
    {
        FullTextSearch value = new FullTextSearchConfig()
        {
            AsciiFolding = true,
            B = 0,
            CaseSensitive = true,
            K1 = 0,
            Language = Language.Arabic,
            MaxTokenLength = 0,
            RemoveStopwords = true,
            Stemming = true,
            Tokenizer = Tokenizer.PreTokenizedArray,
        };
        value.Validate();
    }

    [Fact]
    public void BoolSerializationRoundtripWorks()
    {
        FullTextSearch value = true;
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<FullTextSearch>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void ConfigSerializationRoundtripWorks()
    {
        FullTextSearch value = new FullTextSearchConfig()
        {
            AsciiFolding = true,
            B = 0,
            CaseSensitive = true,
            K1 = 0,
            Language = Language.Arabic,
            MaxTokenLength = 0,
            RemoveStopwords = true,
            Stemming = true,
            Tokenizer = Tokenizer.PreTokenizedArray,
        };
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<FullTextSearch>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}
