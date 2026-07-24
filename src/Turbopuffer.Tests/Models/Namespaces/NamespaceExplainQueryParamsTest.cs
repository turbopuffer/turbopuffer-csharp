using System;
using System.Collections.Generic;
using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Exceptions;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class NamespaceExplainQueryParamsTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var parameters = new NamespaceExplainQueryParams
        {
            Namespace = "namespace",
            AggregateBy = new Dictionary<string, JsonElement>()
            {
                { "foo", JsonSerializer.SerializeToElement("bar") },
            },
            ComputeAttributes = new Dictionary<string, JsonElement>()
            {
                { "foo", JsonSerializer.SerializeToElement("bar") },
            },
            Consistency = new() { Level = Level.Strong },
            DistanceMetric = DistanceMetric.CosineDistance,
            ExcludeAttributes = ["string"],
            Filters = JsonSerializer.Deserialize<JsonElement>("{}"),
            GroupBy = [JsonSerializer.Deserialize<JsonElement>("{}")],
            IncludeAttributes = true,
            Limit = 0,
            RankBy = JsonSerializer.Deserialize<JsonElement>("{}"),
            TopK = 0,
            VectorEncoding = VectorEncoding.Float,
        };

        string expectedNamespace = "namespace";
        Dictionary<string, JsonElement> expectedAggregateBy = new()
        {
            { "foo", JsonSerializer.SerializeToElement("bar") },
        };
        Dictionary<string, JsonElement> expectedComputeAttributes = new()
        {
            { "foo", JsonSerializer.SerializeToElement("bar") },
        };
        Consistency expectedConsistency = new() { Level = Level.Strong };
        ApiEnum<string, DistanceMetric> expectedDistanceMetric = DistanceMetric.CosineDistance;
        List<string> expectedExcludeAttributes = ["string"];
        JsonElement expectedFilters = JsonSerializer.Deserialize<JsonElement>("{}");
        List<JsonElement> expectedGroupBy = [JsonSerializer.Deserialize<JsonElement>("{}")];
        IncludeAttributes expectedIncludeAttributes = true;
        Limit expectedLimit = 0;
        JsonElement expectedRankBy = JsonSerializer.Deserialize<JsonElement>("{}");
        long expectedTopK = 0;
        ApiEnum<string, VectorEncoding> expectedVectorEncoding = VectorEncoding.Float;

        Assert.Equal(expectedNamespace, parameters.Namespace);
        Assert.NotNull(parameters.AggregateBy);
        Assert.Equal(expectedAggregateBy.Count, parameters.AggregateBy.Count);
        foreach (var item in expectedAggregateBy)
        {
            Assert.True(parameters.AggregateBy.TryGetValue(item.Key, out var value));

            Assert.True(JsonElement.DeepEquals(value, parameters.AggregateBy[item.Key]));
        }
        Assert.NotNull(parameters.ComputeAttributes);
        Assert.Equal(expectedComputeAttributes.Count, parameters.ComputeAttributes.Count);
        foreach (var item in expectedComputeAttributes)
        {
            Assert.True(parameters.ComputeAttributes.TryGetValue(item.Key, out var value));

            Assert.True(JsonElement.DeepEquals(value, parameters.ComputeAttributes[item.Key]));
        }
        Assert.Equal(expectedConsistency, parameters.Consistency);
        Assert.Equal(expectedDistanceMetric, parameters.DistanceMetric);
        Assert.NotNull(parameters.ExcludeAttributes);
        Assert.Equal(expectedExcludeAttributes.Count, parameters.ExcludeAttributes.Count);
        for (int i = 0; i < expectedExcludeAttributes.Count; i++)
        {
            Assert.Equal(expectedExcludeAttributes[i], parameters.ExcludeAttributes[i]);
        }
        Assert.NotNull(parameters.Filters);
        Assert.True(JsonElement.DeepEquals(expectedFilters, parameters.Filters.Value));
        Assert.NotNull(parameters.GroupBy);
        Assert.Equal(expectedGroupBy.Count, parameters.GroupBy.Count);
        for (int i = 0; i < expectedGroupBy.Count; i++)
        {
            Assert.True(JsonElement.DeepEquals(expectedGroupBy[i], parameters.GroupBy[i]));
        }
        Assert.Equal(expectedIncludeAttributes, parameters.IncludeAttributes);
        Assert.Equal(expectedLimit, parameters.Limit);
        Assert.NotNull(parameters.RankBy);
        Assert.True(JsonElement.DeepEquals(expectedRankBy, parameters.RankBy.Value));
        Assert.Equal(expectedTopK, parameters.TopK);
        Assert.Equal(expectedVectorEncoding, parameters.VectorEncoding);
    }

    [Fact]
    public void OptionalNonNullableParamsUnsetAreNotSet_Works()
    {
        var parameters = new NamespaceExplainQueryParams { Namespace = "namespace" };

        Assert.Null(parameters.AggregateBy);
        Assert.False(parameters.RawBodyData.ContainsKey("aggregate_by"));
        Assert.Null(parameters.ComputeAttributes);
        Assert.False(parameters.RawBodyData.ContainsKey("compute_attributes"));
        Assert.Null(parameters.Consistency);
        Assert.False(parameters.RawBodyData.ContainsKey("consistency"));
        Assert.Null(parameters.DistanceMetric);
        Assert.False(parameters.RawBodyData.ContainsKey("distance_metric"));
        Assert.Null(parameters.ExcludeAttributes);
        Assert.False(parameters.RawBodyData.ContainsKey("exclude_attributes"));
        Assert.Null(parameters.Filters);
        Assert.False(parameters.RawBodyData.ContainsKey("filters"));
        Assert.Null(parameters.GroupBy);
        Assert.False(parameters.RawBodyData.ContainsKey("group_by"));
        Assert.Null(parameters.IncludeAttributes);
        Assert.False(parameters.RawBodyData.ContainsKey("include_attributes"));
        Assert.Null(parameters.Limit);
        Assert.False(parameters.RawBodyData.ContainsKey("limit"));
        Assert.Null(parameters.RankBy);
        Assert.False(parameters.RawBodyData.ContainsKey("rank_by"));
        Assert.Null(parameters.TopK);
        Assert.False(parameters.RawBodyData.ContainsKey("top_k"));
        Assert.Null(parameters.VectorEncoding);
        Assert.False(parameters.RawBodyData.ContainsKey("vector_encoding"));
    }

    [Fact]
    public void OptionalNonNullableParamsSetToNullAreNotSet_Works()
    {
        var parameters = new NamespaceExplainQueryParams
        {
            Namespace = "namespace",

            // Null should be interpreted as omitted for these properties
            AggregateBy = null,
            ComputeAttributes = null,
            Consistency = null,
            DistanceMetric = null,
            ExcludeAttributes = null,
            Filters = null,
            GroupBy = null,
            IncludeAttributes = null,
            Limit = null,
            RankBy = null,
            TopK = null,
            VectorEncoding = null,
        };

        Assert.Null(parameters.AggregateBy);
        Assert.False(parameters.RawBodyData.ContainsKey("aggregate_by"));
        Assert.Null(parameters.ComputeAttributes);
        Assert.False(parameters.RawBodyData.ContainsKey("compute_attributes"));
        Assert.Null(parameters.Consistency);
        Assert.False(parameters.RawBodyData.ContainsKey("consistency"));
        Assert.Null(parameters.DistanceMetric);
        Assert.False(parameters.RawBodyData.ContainsKey("distance_metric"));
        Assert.Null(parameters.ExcludeAttributes);
        Assert.False(parameters.RawBodyData.ContainsKey("exclude_attributes"));
        Assert.Null(parameters.Filters);
        Assert.False(parameters.RawBodyData.ContainsKey("filters"));
        Assert.Null(parameters.GroupBy);
        Assert.False(parameters.RawBodyData.ContainsKey("group_by"));
        Assert.Null(parameters.IncludeAttributes);
        Assert.False(parameters.RawBodyData.ContainsKey("include_attributes"));
        Assert.Null(parameters.Limit);
        Assert.False(parameters.RawBodyData.ContainsKey("limit"));
        Assert.Null(parameters.RankBy);
        Assert.False(parameters.RawBodyData.ContainsKey("rank_by"));
        Assert.Null(parameters.TopK);
        Assert.False(parameters.RawBodyData.ContainsKey("top_k"));
        Assert.Null(parameters.VectorEncoding);
        Assert.False(parameters.RawBodyData.ContainsKey("vector_encoding"));
    }

    [Fact]
    public void Url_Works()
    {
        NamespaceExplainQueryParams parameters = new() { Namespace = "namespace" };

        var url = parameters.Url(new() { Region = "gcp-us-central1", ApiKey = "tpuf_A1..." });

        Assert.True(
            TestBase.UrisEqual(
                new Uri(
                    "https://gcp-us-central1.turbopuffer.com/v2/namespaces/namespace/explain_query"
                ),
                url
            )
        );
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var parameters = new NamespaceExplainQueryParams
        {
            Namespace = "namespace",
            AggregateBy = new Dictionary<string, JsonElement>()
            {
                { "foo", JsonSerializer.SerializeToElement("bar") },
            },
            ComputeAttributes = new Dictionary<string, JsonElement>()
            {
                { "foo", JsonSerializer.SerializeToElement("bar") },
            },
            Consistency = new() { Level = Level.Strong },
            DistanceMetric = DistanceMetric.CosineDistance,
            ExcludeAttributes = ["string"],
            Filters = JsonSerializer.Deserialize<JsonElement>("{}"),
            GroupBy = [JsonSerializer.Deserialize<JsonElement>("{}")],
            IncludeAttributes = true,
            Limit = 0,
            RankBy = JsonSerializer.Deserialize<JsonElement>("{}"),
            TopK = 0,
            VectorEncoding = VectorEncoding.Float,
        };

        NamespaceExplainQueryParams copied = new(parameters);

        Assert.Equal(parameters, copied);
    }
}

public class ConsistencyTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new Consistency { Level = Level.Strong };

        ApiEnum<string, Level> expectedLevel = Level.Strong;

        Assert.Equal(expectedLevel, model.Level);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new Consistency { Level = Level.Strong };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Consistency>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new Consistency { Level = Level.Strong };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Consistency>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        ApiEnum<string, Level> expectedLevel = Level.Strong;

        Assert.Equal(expectedLevel, deserialized.Level);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new Consistency { Level = Level.Strong };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new Consistency { };

        Assert.Null(model.Level);
        Assert.False(model.RawData.ContainsKey("level"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new Consistency { };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new Consistency
        {
            // Null should be interpreted as omitted for these properties
            Level = null,
        };

        Assert.Null(model.Level);
        Assert.False(model.RawData.ContainsKey("level"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new Consistency
        {
            // Null should be interpreted as omitted for these properties
            Level = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new Consistency { Level = Level.Strong };

        Consistency copied = new(model);

        Assert.Equal(model, copied);
    }
}

public class LevelTest : TestBase
{
    [Theory]
    [InlineData(Level.Strong)]
    [InlineData(Level.Eventual)]
    public void Validation_Works(Level rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<string, Level> value = rawValue;
        value.Validate();
    }

    [Fact]
    public void InvalidEnumValidationThrows_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<string, Level>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );

        Assert.NotNull(value);
        Assert.Throws<TurbopufferInvalidDataException>(() => value.Validate());
    }

    [Theory]
    [InlineData(Level.Strong)]
    [InlineData(Level.Eventual)]
    public void SerializationRoundtrip_Works(Level rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<string, Level> value = rawValue;

        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ApiEnum<string, Level>>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void InvalidEnumSerializationRoundtrip_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<string, Level>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );
        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ApiEnum<string, Level>>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}

public class LimitTest : TestBase
{
    [Fact]
    public void LongValidationWorks()
    {
        Limit value = 0;
        value.Validate();
    }

    [Fact]
    public void LimitValidationWorks()
    {
        Limit value = new NamespaceLimit()
        {
            Total = 0,
            Per = new() { Attributes = ["string"], Limit = 0 },
        };
        value.Validate();
    }

    [Fact]
    public void LongSerializationRoundtripWorks()
    {
        Limit value = 0;
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Limit>(element, ModelBase.SerializerOptions);

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void LimitSerializationRoundtripWorks()
    {
        Limit value = new NamespaceLimit()
        {
            Total = 0,
            Per = new() { Attributes = ["string"], Limit = 0 },
        };
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Limit>(element, ModelBase.SerializerOptions);

        Assert.Equal(value, deserialized);
    }
}
