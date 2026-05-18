using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Client.Exceptions;

namespace Turbopuffer.Client.Models.Namespaces;

/// <summary>
/// The encoding to use for vectors in the response.
/// </summary>
[JsonConverter(typeof(VectorEncodingConverter))]
public enum VectorEncoding
{
    Float,
    Base64,
}

sealed class VectorEncodingConverter : JsonConverter<VectorEncoding>
{
    public override VectorEncoding Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<string>(ref reader, options) switch
        {
            "float" => VectorEncoding.Float,
            "base64" => VectorEncoding.Base64,
            _ => (VectorEncoding)(-1),
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        VectorEncoding value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            value switch
            {
                VectorEncoding.Float => "float",
                VectorEncoding.Base64 => "base64",
                _ => throw new TurbopufferInvalidDataException(
                    string.Format("Invalid value '{0}' in {1}", value, nameof(value))
                ),
            },
            options
        );
    }
}
