using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Exceptions;

namespace Turbopuffer.Models.Namespaces;

/// <summary>
/// How to split a text attribute into fragments for highlighting.
/// </summary>
[JsonConverter(typeof(HighlightFragmentByConverter))]
public enum HighlightFragmentBy
{
    /// <summary>
    /// Treat the whole attribute as a single fragment.
    /// </summary>
    None,

    /// <summary>
    /// Split the attribute into sentences. This is the default.
    /// </summary>
    Sentence,

    /// <summary>
    /// Split the attribute into paragraphs.
    /// </summary>
    Paragraph,

    /// <summary>
    /// Split the attribute into individual words.
    /// </summary>
    Word,
}

sealed class HighlightFragmentByConverter : JsonConverter<HighlightFragmentBy>
{
    public override HighlightFragmentBy Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<string>(ref reader, options) switch
        {
            "none" => HighlightFragmentBy.None,
            "sentence" => HighlightFragmentBy.Sentence,
            "paragraph" => HighlightFragmentBy.Paragraph,
            "word" => HighlightFragmentBy.Word,
            _ => (HighlightFragmentBy)(-1),
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        HighlightFragmentBy value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            value switch
            {
                HighlightFragmentBy.None => "none",
                HighlightFragmentBy.Sentence => "sentence",
                HighlightFragmentBy.Paragraph => "paragraph",
                HighlightFragmentBy.Word => "word",
                _ => throw new TurbopufferInvalidDataException(
                    string.Format("Invalid value '{0}' in {1}", value, nameof(value))
                ),
            },
            options
        );
    }
}
