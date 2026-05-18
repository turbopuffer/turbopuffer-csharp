using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Client.Exceptions;

namespace Turbopuffer.Client.Models.Namespaces;

/// <summary>
/// A function used to calculate sparse vector similarity.
/// </summary>
[JsonConverter(typeof(SparseDistanceMetricConverter))]
public enum SparseDistanceMetric
{
    DotProduct,
}

sealed class SparseDistanceMetricConverter : JsonConverter<SparseDistanceMetric>
{
    public override SparseDistanceMetric Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<string>(ref reader, options) switch
        {
            "dot_product" => SparseDistanceMetric.DotProduct,
            _ => (SparseDistanceMetric)(-1),
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        SparseDistanceMetric value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            value switch
            {
                SparseDistanceMetric.DotProduct => "dot_product",
                _ => throw new TurbopufferInvalidDataException(
                    string.Format("Invalid value '{0}' in {1}", value, nameof(value))
                ),
            },
            options
        );
    }
}
