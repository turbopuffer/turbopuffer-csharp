using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class AttributeSchemaConfigTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new AttributeSchemaConfig
        {
            Type = "string",
            Ann = true,
            Embed = "string",
            Filterable = true,
            FullTextSearch = true,
            Fuzzy = true,
            Glob = true,
            Regex = true,
            SparseKnn = new(SparseDistanceMetric.DotProduct),
        };

        string expectedType = "string";
        Ann expectedAnn = true;
        AttributeEmbed expectedEmbed = "string";
        bool expectedFilterable = true;
        FullTextSearch expectedFullTextSearch = true;
        bool expectedFuzzy = true;
        bool expectedGlob = true;
        bool expectedRegex = true;
        SparseKnn expectedSparseKnn = new(SparseDistanceMetric.DotProduct);

        Assert.Equal(expectedType, model.Type);
        Assert.Equal(expectedAnn, model.Ann);
        Assert.Equal(expectedEmbed, model.Embed);
        Assert.Equal(expectedFilterable, model.Filterable);
        Assert.Equal(expectedFullTextSearch, model.FullTextSearch);
        Assert.Equal(expectedFuzzy, model.Fuzzy);
        Assert.Equal(expectedGlob, model.Glob);
        Assert.Equal(expectedRegex, model.Regex);
        Assert.Equal(expectedSparseKnn, model.SparseKnn);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new AttributeSchemaConfig
        {
            Type = "string",
            Ann = true,
            Embed = "string",
            Filterable = true,
            FullTextSearch = true,
            Fuzzy = true,
            Glob = true,
            Regex = true,
            SparseKnn = new(SparseDistanceMetric.DotProduct),
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<AttributeSchemaConfig>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new AttributeSchemaConfig
        {
            Type = "string",
            Ann = true,
            Embed = "string",
            Filterable = true,
            FullTextSearch = true,
            Fuzzy = true,
            Glob = true,
            Regex = true,
            SparseKnn = new(SparseDistanceMetric.DotProduct),
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<AttributeSchemaConfig>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        string expectedType = "string";
        Ann expectedAnn = true;
        AttributeEmbed expectedEmbed = "string";
        bool expectedFilterable = true;
        FullTextSearch expectedFullTextSearch = true;
        bool expectedFuzzy = true;
        bool expectedGlob = true;
        bool expectedRegex = true;
        SparseKnn expectedSparseKnn = new(SparseDistanceMetric.DotProduct);

        Assert.Equal(expectedType, deserialized.Type);
        Assert.Equal(expectedAnn, deserialized.Ann);
        Assert.Equal(expectedEmbed, deserialized.Embed);
        Assert.Equal(expectedFilterable, deserialized.Filterable);
        Assert.Equal(expectedFullTextSearch, deserialized.FullTextSearch);
        Assert.Equal(expectedFuzzy, deserialized.Fuzzy);
        Assert.Equal(expectedGlob, deserialized.Glob);
        Assert.Equal(expectedRegex, deserialized.Regex);
        Assert.Equal(expectedSparseKnn, deserialized.SparseKnn);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new AttributeSchemaConfig
        {
            Type = "string",
            Ann = true,
            Embed = "string",
            Filterable = true,
            FullTextSearch = true,
            Fuzzy = true,
            Glob = true,
            Regex = true,
            SparseKnn = new(SparseDistanceMetric.DotProduct),
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new AttributeSchemaConfig { Type = "string", Embed = "string" };

        Assert.Null(model.Ann);
        Assert.False(model.RawData.ContainsKey("ann"));
        Assert.Null(model.Filterable);
        Assert.False(model.RawData.ContainsKey("filterable"));
        Assert.Null(model.FullTextSearch);
        Assert.False(model.RawData.ContainsKey("full_text_search"));
        Assert.Null(model.Fuzzy);
        Assert.False(model.RawData.ContainsKey("fuzzy"));
        Assert.Null(model.Glob);
        Assert.False(model.RawData.ContainsKey("glob"));
        Assert.Null(model.Regex);
        Assert.False(model.RawData.ContainsKey("regex"));
        Assert.Null(model.SparseKnn);
        Assert.False(model.RawData.ContainsKey("sparse_knn"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new AttributeSchemaConfig { Type = "string", Embed = "string" };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new AttributeSchemaConfig
        {
            Type = "string",
            Embed = "string",

            // Null should be interpreted as omitted for these properties
            Ann = null,
            Filterable = null,
            FullTextSearch = null,
            Fuzzy = null,
            Glob = null,
            Regex = null,
            SparseKnn = null,
        };

        Assert.Null(model.Ann);
        Assert.False(model.RawData.ContainsKey("ann"));
        Assert.Null(model.Filterable);
        Assert.False(model.RawData.ContainsKey("filterable"));
        Assert.Null(model.FullTextSearch);
        Assert.False(model.RawData.ContainsKey("full_text_search"));
        Assert.Null(model.Fuzzy);
        Assert.False(model.RawData.ContainsKey("fuzzy"));
        Assert.Null(model.Glob);
        Assert.False(model.RawData.ContainsKey("glob"));
        Assert.Null(model.Regex);
        Assert.False(model.RawData.ContainsKey("regex"));
        Assert.Null(model.SparseKnn);
        Assert.False(model.RawData.ContainsKey("sparse_knn"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new AttributeSchemaConfig
        {
            Type = "string",
            Embed = "string",

            // Null should be interpreted as omitted for these properties
            Ann = null,
            Filterable = null,
            FullTextSearch = null,
            Fuzzy = null,
            Glob = null,
            Regex = null,
            SparseKnn = null,
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new AttributeSchemaConfig
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

        Assert.Null(model.Embed);
        Assert.False(model.RawData.ContainsKey("embed"));
    }

    [Fact]
    public void OptionalNullablePropertiesUnsetValidation_Works()
    {
        var model = new AttributeSchemaConfig
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

        model.Validate();
    }

    [Fact]
    public void OptionalNullablePropertiesSetToNullAreSetToNull_Works()
    {
        var model = new AttributeSchemaConfig
        {
            Type = "string",
            Ann = true,
            Filterable = true,
            FullTextSearch = true,
            Fuzzy = true,
            Glob = true,
            Regex = true,
            SparseKnn = new(SparseDistanceMetric.DotProduct),

            Embed = null,
        };

        Assert.Null(model.Embed);
        Assert.True(model.RawData.ContainsKey("embed"));
    }

    [Fact]
    public void OptionalNullablePropertiesSetToNullValidation_Works()
    {
        var model = new AttributeSchemaConfig
        {
            Type = "string",
            Ann = true,
            Filterable = true,
            FullTextSearch = true,
            Fuzzy = true,
            Glob = true,
            Regex = true,
            SparseKnn = new(SparseDistanceMetric.DotProduct),

            Embed = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new AttributeSchemaConfig
        {
            Type = "string",
            Ann = true,
            Embed = "string",
            Filterable = true,
            FullTextSearch = true,
            Fuzzy = true,
            Glob = true,
            Regex = true,
            SparseKnn = new(SparseDistanceMetric.DotProduct),
        };

        AttributeSchemaConfig copied = new(model);

        Assert.Equal(model, copied);
    }
}

public class AnnTest : TestBase
{
    [Fact]
    public void BoolValidationWorks()
    {
        Ann value = true;
        value.Validate();
    }

    [Fact]
    public void ConfigValidationWorks()
    {
        Ann value = new AnnConfig() { DistanceMetric = DistanceMetric.CosineDistance };
        value.Validate();
    }

    [Fact]
    public void BoolSerializationRoundtripWorks()
    {
        Ann value = true;
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Ann>(element, ModelBase.SerializerOptions);

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void ConfigSerializationRoundtripWorks()
    {
        Ann value = new AnnConfig() { DistanceMetric = DistanceMetric.CosineDistance };
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Ann>(element, ModelBase.SerializerOptions);

        Assert.Equal(value, deserialized);
    }
}

public class AnnConfigTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new AnnConfig { DistanceMetric = DistanceMetric.CosineDistance };

        ApiEnum<string, DistanceMetric> expectedDistanceMetric = DistanceMetric.CosineDistance;

        Assert.Equal(expectedDistanceMetric, model.DistanceMetric);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new AnnConfig { DistanceMetric = DistanceMetric.CosineDistance };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<AnnConfig>(json, ModelBase.SerializerOptions);

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new AnnConfig { DistanceMetric = DistanceMetric.CosineDistance };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<AnnConfig>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        ApiEnum<string, DistanceMetric> expectedDistanceMetric = DistanceMetric.CosineDistance;

        Assert.Equal(expectedDistanceMetric, deserialized.DistanceMetric);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new AnnConfig { DistanceMetric = DistanceMetric.CosineDistance };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new AnnConfig { };

        Assert.Null(model.DistanceMetric);
        Assert.False(model.RawData.ContainsKey("distance_metric"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new AnnConfig { };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new AnnConfig
        {
            // Null should be interpreted as omitted for these properties
            DistanceMetric = null,
        };

        Assert.Null(model.DistanceMetric);
        Assert.False(model.RawData.ContainsKey("distance_metric"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new AnnConfig
        {
            // Null should be interpreted as omitted for these properties
            DistanceMetric = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new AnnConfig { DistanceMetric = DistanceMetric.CosineDistance };

        AnnConfig copied = new(model);

        Assert.Equal(model, copied);
    }
}

public class SparseKnnTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new SparseKnn { DistanceMetric = SparseDistanceMetric.DotProduct };

        ApiEnum<string, SparseDistanceMetric> expectedDistanceMetric =
            SparseDistanceMetric.DotProduct;

        Assert.Equal(expectedDistanceMetric, model.DistanceMetric);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new SparseKnn { DistanceMetric = SparseDistanceMetric.DotProduct };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<SparseKnn>(json, ModelBase.SerializerOptions);

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new SparseKnn { DistanceMetric = SparseDistanceMetric.DotProduct };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<SparseKnn>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        ApiEnum<string, SparseDistanceMetric> expectedDistanceMetric =
            SparseDistanceMetric.DotProduct;

        Assert.Equal(expectedDistanceMetric, deserialized.DistanceMetric);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new SparseKnn { DistanceMetric = SparseDistanceMetric.DotProduct };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new SparseKnn { DistanceMetric = SparseDistanceMetric.DotProduct };

        SparseKnn copied = new(model);

        Assert.Equal(model, copied);
    }
}
