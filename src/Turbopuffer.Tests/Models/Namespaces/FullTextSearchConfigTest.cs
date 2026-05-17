using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class FullTextSearchConfigTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new FullTextSearchConfig
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

        bool expectedAsciiFolding = true;
        double expectedB = 0;
        bool expectedCaseSensitive = true;
        double expectedK1 = 0;
        ApiEnum<string, Language> expectedLanguage = Language.Arabic;
        long expectedMaxTokenLength = 0;
        bool expectedRemoveStopwords = true;
        bool expectedStemming = true;
        ApiEnum<string, Tokenizer> expectedTokenizer = Tokenizer.PreTokenizedArray;

        Assert.Equal(expectedAsciiFolding, model.AsciiFolding);
        Assert.Equal(expectedB, model.B);
        Assert.Equal(expectedCaseSensitive, model.CaseSensitive);
        Assert.Equal(expectedK1, model.K1);
        Assert.Equal(expectedLanguage, model.Language);
        Assert.Equal(expectedMaxTokenLength, model.MaxTokenLength);
        Assert.Equal(expectedRemoveStopwords, model.RemoveStopwords);
        Assert.Equal(expectedStemming, model.Stemming);
        Assert.Equal(expectedTokenizer, model.Tokenizer);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new FullTextSearchConfig
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

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<FullTextSearchConfig>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new FullTextSearchConfig
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

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<FullTextSearchConfig>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        bool expectedAsciiFolding = true;
        double expectedB = 0;
        bool expectedCaseSensitive = true;
        double expectedK1 = 0;
        ApiEnum<string, Language> expectedLanguage = Language.Arabic;
        long expectedMaxTokenLength = 0;
        bool expectedRemoveStopwords = true;
        bool expectedStemming = true;
        ApiEnum<string, Tokenizer> expectedTokenizer = Tokenizer.PreTokenizedArray;

        Assert.Equal(expectedAsciiFolding, deserialized.AsciiFolding);
        Assert.Equal(expectedB, deserialized.B);
        Assert.Equal(expectedCaseSensitive, deserialized.CaseSensitive);
        Assert.Equal(expectedK1, deserialized.K1);
        Assert.Equal(expectedLanguage, deserialized.Language);
        Assert.Equal(expectedMaxTokenLength, deserialized.MaxTokenLength);
        Assert.Equal(expectedRemoveStopwords, deserialized.RemoveStopwords);
        Assert.Equal(expectedStemming, deserialized.Stemming);
        Assert.Equal(expectedTokenizer, deserialized.Tokenizer);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new FullTextSearchConfig
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

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new FullTextSearchConfig { };

        Assert.Null(model.AsciiFolding);
        Assert.False(model.RawData.ContainsKey("ascii_folding"));
        Assert.Null(model.B);
        Assert.False(model.RawData.ContainsKey("b"));
        Assert.Null(model.CaseSensitive);
        Assert.False(model.RawData.ContainsKey("case_sensitive"));
        Assert.Null(model.K1);
        Assert.False(model.RawData.ContainsKey("k1"));
        Assert.Null(model.Language);
        Assert.False(model.RawData.ContainsKey("language"));
        Assert.Null(model.MaxTokenLength);
        Assert.False(model.RawData.ContainsKey("max_token_length"));
        Assert.Null(model.RemoveStopwords);
        Assert.False(model.RawData.ContainsKey("remove_stopwords"));
        Assert.Null(model.Stemming);
        Assert.False(model.RawData.ContainsKey("stemming"));
        Assert.Null(model.Tokenizer);
        Assert.False(model.RawData.ContainsKey("tokenizer"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new FullTextSearchConfig { };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new FullTextSearchConfig
        {
            // Null should be interpreted as omitted for these properties
            AsciiFolding = null,
            B = null,
            CaseSensitive = null,
            K1 = null,
            Language = null,
            MaxTokenLength = null,
            RemoveStopwords = null,
            Stemming = null,
            Tokenizer = null,
        };

        Assert.Null(model.AsciiFolding);
        Assert.False(model.RawData.ContainsKey("ascii_folding"));
        Assert.Null(model.B);
        Assert.False(model.RawData.ContainsKey("b"));
        Assert.Null(model.CaseSensitive);
        Assert.False(model.RawData.ContainsKey("case_sensitive"));
        Assert.Null(model.K1);
        Assert.False(model.RawData.ContainsKey("k1"));
        Assert.Null(model.Language);
        Assert.False(model.RawData.ContainsKey("language"));
        Assert.Null(model.MaxTokenLength);
        Assert.False(model.RawData.ContainsKey("max_token_length"));
        Assert.Null(model.RemoveStopwords);
        Assert.False(model.RawData.ContainsKey("remove_stopwords"));
        Assert.Null(model.Stemming);
        Assert.False(model.RawData.ContainsKey("stemming"));
        Assert.Null(model.Tokenizer);
        Assert.False(model.RawData.ContainsKey("tokenizer"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new FullTextSearchConfig
        {
            // Null should be interpreted as omitted for these properties
            AsciiFolding = null,
            B = null,
            CaseSensitive = null,
            K1 = null,
            Language = null,
            MaxTokenLength = null,
            RemoveStopwords = null,
            Stemming = null,
            Tokenizer = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new FullTextSearchConfig
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

        FullTextSearchConfig copied = new(model);

        Assert.Equal(model, copied);
    }
}
