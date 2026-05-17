using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Exceptions;

namespace Turbopuffer.Models.Namespaces;

/// <summary>
/// The tokenizer to use for full-text search on an attribute. Defaults to `word_v3`.
/// </summary>
[JsonConverter(typeof(TokenizerConverter))]
public enum Tokenizer
{
    PreTokenizedArray,
    WordV0,
    WordV1,
    WordV2,
    WordV3,
}

sealed class TokenizerConverter : JsonConverter<Tokenizer>
{
    public override Tokenizer Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<string>(ref reader, options) switch
        {
            "pre_tokenized_array" => Tokenizer.PreTokenizedArray,
            "word_v0" => Tokenizer.WordV0,
            "word_v1" => Tokenizer.WordV1,
            "word_v2" => Tokenizer.WordV2,
            "word_v3" => Tokenizer.WordV3,
            _ => (Tokenizer)(-1),
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        Tokenizer value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            value switch
            {
                Tokenizer.PreTokenizedArray => "pre_tokenized_array",
                Tokenizer.WordV0 => "word_v0",
                Tokenizer.WordV1 => "word_v1",
                Tokenizer.WordV2 => "word_v2",
                Tokenizer.WordV3 => "word_v3",
                _ => throw new TurbopufferInvalidDataException(
                    string.Format("Invalid value '{0}' in {1}", value, nameof(value))
                ),
            },
            options
        );
    }
}
