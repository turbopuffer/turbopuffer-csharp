using System.Text.Json;
using Turbopuffer.Client.Exceptions;
using Turbopuffer.Client.Models.Namespaces;

namespace Turbopuffer.Client.Core;

/// <summary>
/// The base class for all API objects with properties.
///
/// <para>API objects such as enums do not inherit from this class.</para>
/// </summary>
public abstract record class ModelBase
{
    protected ModelBase(ModelBase modelBase)
    {
        // Nothing to copy. Just so that subclasses can define copy constructors.
    }

    internal static readonly JsonSerializerOptions SerializerOptions = new()
    {
        Converters =
        {
            new FrozenDictionaryConverterFactory(),
            new ApiEnumConverter<string, DistanceMetric>(),
            new ApiEnumConverter<string, Language>(),
            new ApiEnumConverter<string, SparseDistanceMetric>(),
            new ApiEnumConverter<string, Tokenizer>(),
            new ApiEnumConverter<string, VectorEncoding>(),
            new ApiEnumConverter<string, Level>(),
            new ApiEnumConverter<string, NamespaceMultiQueryParamsConsistencyLevel>(),
            new ApiEnumConverter<string, NamespaceQueryParamsConsistencyLevel>(),
        },
    };

    internal static readonly JsonSerializerOptions ToStringSerializerOptions = new(
        SerializerOptions
    )
    {
        WriteIndented = true,
    };

    /// <summary>
    /// Validates that all required fields are set and that each field's value is of the expected type.
    ///
    /// <para>This is useful for instances constructed from raw JSON data (e.g. deserialized from an API response).</para>
    ///
    /// <exception cref="TurbopufferInvalidDataException">
    /// Thrown when the instance does not pass validation.
    /// </exception>
    /// </summary>
    public abstract void Validate();
}
