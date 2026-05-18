using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Client.Exceptions;

namespace Turbopuffer.Client.Models.Namespaces;

/// <summary>
/// A function used to calculate vector similarity.
/// </summary>
[JsonConverter(typeof(DistanceMetricConverter))]
public enum DistanceMetric
{
    /// <summary>
    /// Defined as `1 - cosine_similarity` and ranges from 0 to 2. Lower is better.
    /// </summary>
    CosineDistance,

    /// <summary>
    /// Defined as `sum((x - y)^2)`. Lower is better.
    /// </summary>
    EuclideanSquared,
}

sealed class DistanceMetricConverter : JsonConverter<DistanceMetric>
{
    public override DistanceMetric Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return JsonSerializer.Deserialize<string>(ref reader, options) switch
        {
            "cosine_distance" => DistanceMetric.CosineDistance,
            "euclidean_squared" => DistanceMetric.EuclideanSquared,
            _ => (DistanceMetric)(-1),
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        DistanceMetric value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(
            writer,
            value switch
            {
                DistanceMetric.CosineDistance => "cosine_distance",
                DistanceMetric.EuclideanSquared => "euclidean_squared",
                _ => throw new TurbopufferInvalidDataException(
                    string.Format("Invalid value '{0}' in {1}", value, nameof(value))
                ),
            },
            options
        );
    }
}
