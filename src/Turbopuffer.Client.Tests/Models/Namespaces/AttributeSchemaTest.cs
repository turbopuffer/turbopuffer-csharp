using System.Text.Json;
using Turbopuffer.Client.Core;
using Turbopuffer.Client.Models.Namespaces;

namespace Turbopuffer.Client.Tests.Models.Namespaces;

public class AttributeSchemaTest : TestBase
{
    [Fact]
    public void TypeValidationWorks()
    {
        AttributeSchema value = "string";
        value.Validate();
    }

    [Fact]
    public void ConfigValidationWorks()
    {
        AttributeSchema value = new AttributeSchemaConfig()
        {
            Type = "string",
            Ann = true,
            Filterable = true,
            FullTextSearch = true,
            Fuzzy = true,
            Glob = true,
            Regex = true,
            SparseKnn = new(SparseDistanceMetric.DotProduct),
        };
        value.Validate();
    }

    [Fact]
    public void TypeSerializationRoundtripWorks()
    {
        AttributeSchema value = "string";
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<AttributeSchema>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void ConfigSerializationRoundtripWorks()
    {
        AttributeSchema value = new AttributeSchemaConfig()
        {
            Type = "string",
            Ann = true,
            Filterable = true,
            FullTextSearch = true,
            Fuzzy = true,
            Glob = true,
            Regex = true,
            SparseKnn = new(SparseDistanceMetric.DotProduct),
        };
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<AttributeSchema>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}
