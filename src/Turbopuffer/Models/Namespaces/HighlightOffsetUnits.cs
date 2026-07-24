using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Exceptions;

namespace Turbopuffer.Models.Namespaces;

/// <summary>
/// The units to report highlighted fragment offsets in.
/// </summary>
[JsonConverter(typeof(HighlightOffsetUnitsConverter))]
public enum HighlightOffsetUnits
{
    Utf8,
    Utf16,
    Codepoints,
}

sealed class HighlightOffsetUnitsConverter : JsonConverter<HighlightOffsetUnits>
{
    public override HighlightOffsetUnits Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<string>(ref reader, options) switch
        {
            "utf-8" => HighlightOffsetUnits.Utf8,
            "utf-16" => HighlightOffsetUnits.Utf16,
            "codepoints" => HighlightOffsetUnits.Codepoints,
            _ => (HighlightOffsetUnits)(-1),
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        HighlightOffsetUnits value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            value switch
            {
                HighlightOffsetUnits.Utf8 => "utf-8",
                HighlightOffsetUnits.Utf16 => "utf-16",
                HighlightOffsetUnits.Codepoints => "codepoints",
                _ => throw new TurbopufferInvalidDataException(
                    string.Format("Invalid value '{0}' in {1}", value, nameof(value))
                ),
            },
            options
        );
    }
}
