using System;
using System.Collections.Generic;
using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Exceptions;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class NamespaceMultiQueryParamsTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var parameters = new NamespaceMultiQueryParams
        {
            Namespace = "namespace",
            Queries =
            [
                new()
                {
                    AggregateBy = new Dictionary<string, JsonElement>()
                    {
                        { "foo", JsonSerializer.SerializeToElement("bar") },
                    },
                    ComputeAttributes = new Dictionary<string, JsonElement>()
                    {
                        { "foo", JsonSerializer.SerializeToElement("bar") },
                    },
                    DistanceMetric = DistanceMetric.CosineDistance,
                    ExcludeAttributes = ["string"],
                    Filters = JsonSerializer.Deserialize<JsonElement>("{}"),
                    GroupBy = [JsonSerializer.Deserialize<JsonElement>("{}")],
                    IncludeAttributes = true,
                    Limit = 0,
                    RankBy = JsonSerializer.Deserialize<JsonElement>("{}"),
                    TopK = 0,
                },
            ],
            Consistency = new() { Level = NamespaceMultiQueryParamsConsistencyLevel.Strong },
            Limit = 0,
            RerankBy = JsonSerializer.Deserialize<JsonElement>("{}"),
            VectorEncoding = VectorEncoding.Float,
        };

        string expectedNamespace = "namespace";
        List<Query> expectedQueries =
        [
            new()
            {
                AggregateBy = new Dictionary<string, JsonElement>()
                {
                    { "foo", JsonSerializer.SerializeToElement("bar") },
                },
                ComputeAttributes = new Dictionary<string, JsonElement>()
                {
                    { "foo", JsonSerializer.SerializeToElement("bar") },
                },
                DistanceMetric = DistanceMetric.CosineDistance,
                ExcludeAttributes = ["string"],
                Filters = JsonSerializer.Deserialize<JsonElement>("{}"),
                GroupBy = [JsonSerializer.Deserialize<JsonElement>("{}")],
                IncludeAttributes = true,
                Limit = 0,
                RankBy = JsonSerializer.Deserialize<JsonElement>("{}"),
                TopK = 0,
            },
        ];
        NamespaceMultiQueryParamsConsistency expectedConsistency = new()
        {
            Level = NamespaceMultiQueryParamsConsistencyLevel.Strong,
        };
        NamespaceMultiQueryParamsLimit expectedLimit = 0;
        JsonElement expectedRerankBy = JsonSerializer.Deserialize<JsonElement>("{}");
        ApiEnum<string, VectorEncoding> expectedVectorEncoding = VectorEncoding.Float;

        Assert.Equal(expectedNamespace, parameters.Namespace);
        Assert.Equal(expectedQueries.Count, parameters.Queries.Count);
        for (int i = 0; i < expectedQueries.Count; i++)
        {
            Assert.Equal(expectedQueries[i], parameters.Queries[i]);
        }
        Assert.Equal(expectedConsistency, parameters.Consistency);
        Assert.Equal(expectedLimit, parameters.Limit);
        Assert.NotNull(parameters.RerankBy);
        Assert.True(JsonElement.DeepEquals(expectedRerankBy, parameters.RerankBy.Value));
        Assert.Equal(expectedVectorEncoding, parameters.VectorEncoding);
    }

    [Fact]
    public void OptionalNonNullableParamsUnsetAreNotSet_Works()
    {
        var parameters = new NamespaceMultiQueryParams
        {
            Namespace = "namespace",
            Queries =
            [
                new()
                {
                    AggregateBy = new Dictionary<string, JsonElement>()
                    {
                        { "foo", JsonSerializer.SerializeToElement("bar") },
                    },
                    ComputeAttributes = new Dictionary<string, JsonElement>()
                    {
                        { "foo", JsonSerializer.SerializeToElement("bar") },
                    },
                    DistanceMetric = DistanceMetric.CosineDistance,
                    ExcludeAttributes = ["string"],
                    Filters = JsonSerializer.Deserialize<JsonElement>("{}"),
                    GroupBy = [JsonSerializer.Deserialize<JsonElement>("{}")],
                    IncludeAttributes = true,
                    Limit = 0,
                    RankBy = JsonSerializer.Deserialize<JsonElement>("{}"),
                    TopK = 0,
                },
            ],
        };

        Assert.Null(parameters.Consistency);
        Assert.False(parameters.RawBodyData.ContainsKey("consistency"));
        Assert.Null(parameters.Limit);
        Assert.False(parameters.RawBodyData.ContainsKey("limit"));
        Assert.Null(parameters.RerankBy);
        Assert.False(parameters.RawBodyData.ContainsKey("rerank_by"));
        Assert.Null(parameters.VectorEncoding);
        Assert.False(parameters.RawBodyData.ContainsKey("vector_encoding"));
    }

    [Fact]
    public void OptionalNonNullableParamsSetToNullAreNotSet_Works()
    {
        var parameters = new NamespaceMultiQueryParams
        {
            Namespace = "namespace",
            Queries =
            [
                new()
                {
                    AggregateBy = new Dictionary<string, JsonElement>()
                    {
                        { "foo", JsonSerializer.SerializeToElement("bar") },
                    },
                    ComputeAttributes = new Dictionary<string, JsonElement>()
                    {
                        { "foo", JsonSerializer.SerializeToElement("bar") },
                    },
                    DistanceMetric = DistanceMetric.CosineDistance,
                    ExcludeAttributes = ["string"],
                    Filters = JsonSerializer.Deserialize<JsonElement>("{}"),
                    GroupBy = [JsonSerializer.Deserialize<JsonElement>("{}")],
                    IncludeAttributes = true,
                    Limit = 0,
                    RankBy = JsonSerializer.Deserialize<JsonElement>("{}"),
                    TopK = 0,
                },
            ],

            // Null should be interpreted as omitted for these properties
            Consistency = null,
            Limit = null,
            RerankBy = null,
            VectorEncoding = null,
        };

        Assert.Null(parameters.Consistency);
        Assert.False(parameters.RawBodyData.ContainsKey("consistency"));
        Assert.Null(parameters.Limit);
        Assert.False(parameters.RawBodyData.ContainsKey("limit"));
        Assert.Null(parameters.RerankBy);
        Assert.False(parameters.RawBodyData.ContainsKey("rerank_by"));
        Assert.Null(parameters.VectorEncoding);
        Assert.False(parameters.RawBodyData.ContainsKey("vector_encoding"));
    }

    [Fact]
    public void Url_Works()
    {
        NamespaceMultiQueryParams parameters = new()
        {
            Namespace = "namespace",
            Queries =
            [
                new()
                {
                    AggregateBy = new Dictionary<string, JsonElement>()
                    {
                        { "foo", JsonSerializer.SerializeToElement("bar") },
                    },
                    ComputeAttributes = new Dictionary<string, JsonElement>()
                    {
                        { "foo", JsonSerializer.SerializeToElement("bar") },
                    },
                    DistanceMetric = DistanceMetric.CosineDistance,
                    ExcludeAttributes = ["string"],
                    Filters = JsonSerializer.Deserialize<JsonElement>("{}"),
                    GroupBy = [JsonSerializer.Deserialize<JsonElement>("{}")],
                    IncludeAttributes = true,
                    Limit = 0,
                    RankBy = JsonSerializer.Deserialize<JsonElement>("{}"),
                    TopK = 0,
                },
            ],
        };

        var url = parameters.Url(new() { Region = "gcp-us-central1", ApiKey = "tpuf_A1..." });

        Assert.True(
            TestBase.UrisEqual(
                new Uri(
                    "https://gcp-us-central1.turbopuffer.com/v2/namespaces/namespace/query?stainless_overload=multiQuery"
                ),
                url
            )
        );
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var parameters = new NamespaceMultiQueryParams
        {
            Namespace = "namespace",
            Queries =
            [
                new()
                {
                    AggregateBy = new Dictionary<string, JsonElement>()
                    {
                        { "foo", JsonSerializer.SerializeToElement("bar") },
                    },
                    ComputeAttributes = new Dictionary<string, JsonElement>()
                    {
                        { "foo", JsonSerializer.SerializeToElement("bar") },
                    },
                    DistanceMetric = DistanceMetric.CosineDistance,
                    ExcludeAttributes = ["string"],
                    Filters = JsonSerializer.Deserialize<JsonElement>("{}"),
                    GroupBy = [JsonSerializer.Deserialize<JsonElement>("{}")],
                    IncludeAttributes = true,
                    Limit = 0,
                    RankBy = JsonSerializer.Deserialize<JsonElement>("{}"),
                    TopK = 0,
                },
            ],
            Consistency = new() { Level = NamespaceMultiQueryParamsConsistencyLevel.Strong },
            Limit = 0,
            RerankBy = JsonSerializer.Deserialize<JsonElement>("{}"),
            VectorEncoding = VectorEncoding.Float,
        };

        NamespaceMultiQueryParams copied = new(parameters);

        Assert.Equal(parameters, copied);
    }
}

public class QueryTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new Query
        {
            AggregateBy = new Dictionary<string, JsonElement>()
            {
                { "foo", JsonSerializer.SerializeToElement("bar") },
            },
            ComputeAttributes = new Dictionary<string, JsonElement>()
            {
                { "foo", JsonSerializer.SerializeToElement("bar") },
            },
            DistanceMetric = DistanceMetric.CosineDistance,
            ExcludeAttributes = ["string"],
            Filters = JsonSerializer.Deserialize<JsonElement>("{}"),
            GroupBy = [JsonSerializer.Deserialize<JsonElement>("{}")],
            IncludeAttributes = true,
            Limit = 0,
            RankBy = JsonSerializer.Deserialize<JsonElement>("{}"),
            TopK = 0,
        };

        Dictionary<string, JsonElement> expectedAggregateBy = new()
        {
            { "foo", JsonSerializer.SerializeToElement("bar") },
        };
        Dictionary<string, JsonElement> expectedComputeAttributes = new()
        {
            { "foo", JsonSerializer.SerializeToElement("bar") },
        };
        ApiEnum<string, DistanceMetric> expectedDistanceMetric = DistanceMetric.CosineDistance;
        List<string> expectedExcludeAttributes = ["string"];
        JsonElement expectedFilters = JsonSerializer.Deserialize<JsonElement>("{}");
        List<JsonElement> expectedGroupBy = [JsonSerializer.Deserialize<JsonElement>("{}")];
        IncludeAttributes expectedIncludeAttributes = true;
        QueryLimit expectedLimit = 0;
        JsonElement expectedRankBy = JsonSerializer.Deserialize<JsonElement>("{}");
        long expectedTopK = 0;

        Assert.NotNull(model.AggregateBy);
        Assert.Equal(expectedAggregateBy.Count, model.AggregateBy.Count);
        foreach (var item in expectedAggregateBy)
        {
            Assert.True(model.AggregateBy.TryGetValue(item.Key, out var value));

            Assert.True(JsonElement.DeepEquals(value, model.AggregateBy[item.Key]));
        }
        Assert.NotNull(model.ComputeAttributes);
        Assert.Equal(expectedComputeAttributes.Count, model.ComputeAttributes.Count);
        foreach (var item in expectedComputeAttributes)
        {
            Assert.True(model.ComputeAttributes.TryGetValue(item.Key, out var value));

            Assert.True(JsonElement.DeepEquals(value, model.ComputeAttributes[item.Key]));
        }
        Assert.Equal(expectedDistanceMetric, model.DistanceMetric);
        Assert.NotNull(model.ExcludeAttributes);
        Assert.Equal(expectedExcludeAttributes.Count, model.ExcludeAttributes.Count);
        for (int i = 0; i < expectedExcludeAttributes.Count; i++)
        {
            Assert.Equal(expectedExcludeAttributes[i], model.ExcludeAttributes[i]);
        }
        Assert.NotNull(model.Filters);
        Assert.True(JsonElement.DeepEquals(expectedFilters, model.Filters.Value));
        Assert.NotNull(model.GroupBy);
        Assert.Equal(expectedGroupBy.Count, model.GroupBy.Count);
        for (int i = 0; i < expectedGroupBy.Count; i++)
        {
            Assert.True(JsonElement.DeepEquals(expectedGroupBy[i], model.GroupBy[i]));
        }
        Assert.Equal(expectedIncludeAttributes, model.IncludeAttributes);
        Assert.Equal(expectedLimit, model.Limit);
        Assert.NotNull(model.RankBy);
        Assert.True(JsonElement.DeepEquals(expectedRankBy, model.RankBy.Value));
        Assert.Equal(expectedTopK, model.TopK);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new Query
        {
            AggregateBy = new Dictionary<string, JsonElement>()
            {
                { "foo", JsonSerializer.SerializeToElement("bar") },
            },
            ComputeAttributes = new Dictionary<string, JsonElement>()
            {
                { "foo", JsonSerializer.SerializeToElement("bar") },
            },
            DistanceMetric = DistanceMetric.CosineDistance,
            ExcludeAttributes = ["string"],
            Filters = JsonSerializer.Deserialize<JsonElement>("{}"),
            GroupBy = [JsonSerializer.Deserialize<JsonElement>("{}")],
            IncludeAttributes = true,
            Limit = 0,
            RankBy = JsonSerializer.Deserialize<JsonElement>("{}"),
            TopK = 0,
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Query>(json, ModelBase.SerializerOptions);

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new Query
        {
            AggregateBy = new Dictionary<string, JsonElement>()
            {
                { "foo", JsonSerializer.SerializeToElement("bar") },
            },
            ComputeAttributes = new Dictionary<string, JsonElement>()
            {
                { "foo", JsonSerializer.SerializeToElement("bar") },
            },
            DistanceMetric = DistanceMetric.CosineDistance,
            ExcludeAttributes = ["string"],
            Filters = JsonSerializer.Deserialize<JsonElement>("{}"),
            GroupBy = [JsonSerializer.Deserialize<JsonElement>("{}")],
            IncludeAttributes = true,
            Limit = 0,
            RankBy = JsonSerializer.Deserialize<JsonElement>("{}"),
            TopK = 0,
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Query>(element, ModelBase.SerializerOptions);
        Assert.NotNull(deserialized);

        Dictionary<string, JsonElement> expectedAggregateBy = new()
        {
            { "foo", JsonSerializer.SerializeToElement("bar") },
        };
        Dictionary<string, JsonElement> expectedComputeAttributes = new()
        {
            { "foo", JsonSerializer.SerializeToElement("bar") },
        };
        ApiEnum<string, DistanceMetric> expectedDistanceMetric = DistanceMetric.CosineDistance;
        List<string> expectedExcludeAttributes = ["string"];
        JsonElement expectedFilters = JsonSerializer.Deserialize<JsonElement>("{}");
        List<JsonElement> expectedGroupBy = [JsonSerializer.Deserialize<JsonElement>("{}")];
        IncludeAttributes expectedIncludeAttributes = true;
        QueryLimit expectedLimit = 0;
        JsonElement expectedRankBy = JsonSerializer.Deserialize<JsonElement>("{}");
        long expectedTopK = 0;

        Assert.NotNull(deserialized.AggregateBy);
        Assert.Equal(expectedAggregateBy.Count, deserialized.AggregateBy.Count);
        foreach (var item in expectedAggregateBy)
        {
            Assert.True(deserialized.AggregateBy.TryGetValue(item.Key, out var value));

            Assert.True(JsonElement.DeepEquals(value, deserialized.AggregateBy[item.Key]));
        }
        Assert.NotNull(deserialized.ComputeAttributes);
        Assert.Equal(expectedComputeAttributes.Count, deserialized.ComputeAttributes.Count);
        foreach (var item in expectedComputeAttributes)
        {
            Assert.True(deserialized.ComputeAttributes.TryGetValue(item.Key, out var value));

            Assert.True(JsonElement.DeepEquals(value, deserialized.ComputeAttributes[item.Key]));
        }
        Assert.Equal(expectedDistanceMetric, deserialized.DistanceMetric);
        Assert.NotNull(deserialized.ExcludeAttributes);
        Assert.Equal(expectedExcludeAttributes.Count, deserialized.ExcludeAttributes.Count);
        for (int i = 0; i < expectedExcludeAttributes.Count; i++)
        {
            Assert.Equal(expectedExcludeAttributes[i], deserialized.ExcludeAttributes[i]);
        }
        Assert.NotNull(deserialized.Filters);
        Assert.True(JsonElement.DeepEquals(expectedFilters, deserialized.Filters.Value));
        Assert.NotNull(deserialized.GroupBy);
        Assert.Equal(expectedGroupBy.Count, deserialized.GroupBy.Count);
        for (int i = 0; i < expectedGroupBy.Count; i++)
        {
            Assert.True(JsonElement.DeepEquals(expectedGroupBy[i], deserialized.GroupBy[i]));
        }
        Assert.Equal(expectedIncludeAttributes, deserialized.IncludeAttributes);
        Assert.Equal(expectedLimit, deserialized.Limit);
        Assert.NotNull(deserialized.RankBy);
        Assert.True(JsonElement.DeepEquals(expectedRankBy, deserialized.RankBy.Value));
        Assert.Equal(expectedTopK, deserialized.TopK);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new Query
        {
            AggregateBy = new Dictionary<string, JsonElement>()
            {
                { "foo", JsonSerializer.SerializeToElement("bar") },
            },
            ComputeAttributes = new Dictionary<string, JsonElement>()
            {
                { "foo", JsonSerializer.SerializeToElement("bar") },
            },
            DistanceMetric = DistanceMetric.CosineDistance,
            ExcludeAttributes = ["string"],
            Filters = JsonSerializer.Deserialize<JsonElement>("{}"),
            GroupBy = [JsonSerializer.Deserialize<JsonElement>("{}")],
            IncludeAttributes = true,
            Limit = 0,
            RankBy = JsonSerializer.Deserialize<JsonElement>("{}"),
            TopK = 0,
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new Query { };

        Assert.Null(model.AggregateBy);
        Assert.False(model.RawData.ContainsKey("aggregate_by"));
        Assert.Null(model.ComputeAttributes);
        Assert.False(model.RawData.ContainsKey("compute_attributes"));
        Assert.Null(model.DistanceMetric);
        Assert.False(model.RawData.ContainsKey("distance_metric"));
        Assert.Null(model.ExcludeAttributes);
        Assert.False(model.RawData.ContainsKey("exclude_attributes"));
        Assert.Null(model.Filters);
        Assert.False(model.RawData.ContainsKey("filters"));
        Assert.Null(model.GroupBy);
        Assert.False(model.RawData.ContainsKey("group_by"));
        Assert.Null(model.IncludeAttributes);
        Assert.False(model.RawData.ContainsKey("include_attributes"));
        Assert.Null(model.Limit);
        Assert.False(model.RawData.ContainsKey("limit"));
        Assert.Null(model.RankBy);
        Assert.False(model.RawData.ContainsKey("rank_by"));
        Assert.Null(model.TopK);
        Assert.False(model.RawData.ContainsKey("top_k"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new Query { };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new Query
        {
            // Null should be interpreted as omitted for these properties
            AggregateBy = null,
            ComputeAttributes = null,
            DistanceMetric = null,
            ExcludeAttributes = null,
            Filters = null,
            GroupBy = null,
            IncludeAttributes = null,
            Limit = null,
            RankBy = null,
            TopK = null,
        };

        Assert.Null(model.AggregateBy);
        Assert.False(model.RawData.ContainsKey("aggregate_by"));
        Assert.Null(model.ComputeAttributes);
        Assert.False(model.RawData.ContainsKey("compute_attributes"));
        Assert.Null(model.DistanceMetric);
        Assert.False(model.RawData.ContainsKey("distance_metric"));
        Assert.Null(model.ExcludeAttributes);
        Assert.False(model.RawData.ContainsKey("exclude_attributes"));
        Assert.Null(model.Filters);
        Assert.False(model.RawData.ContainsKey("filters"));
        Assert.Null(model.GroupBy);
        Assert.False(model.RawData.ContainsKey("group_by"));
        Assert.Null(model.IncludeAttributes);
        Assert.False(model.RawData.ContainsKey("include_attributes"));
        Assert.Null(model.Limit);
        Assert.False(model.RawData.ContainsKey("limit"));
        Assert.Null(model.RankBy);
        Assert.False(model.RawData.ContainsKey("rank_by"));
        Assert.Null(model.TopK);
        Assert.False(model.RawData.ContainsKey("top_k"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new Query
        {
            // Null should be interpreted as omitted for these properties
            AggregateBy = null,
            ComputeAttributes = null,
            DistanceMetric = null,
            ExcludeAttributes = null,
            Filters = null,
            GroupBy = null,
            IncludeAttributes = null,
            Limit = null,
            RankBy = null,
            TopK = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new Query
        {
            AggregateBy = new Dictionary<string, JsonElement>()
            {
                { "foo", JsonSerializer.SerializeToElement("bar") },
            },
            ComputeAttributes = new Dictionary<string, JsonElement>()
            {
                { "foo", JsonSerializer.SerializeToElement("bar") },
            },
            DistanceMetric = DistanceMetric.CosineDistance,
            ExcludeAttributes = ["string"],
            Filters = JsonSerializer.Deserialize<JsonElement>("{}"),
            GroupBy = [JsonSerializer.Deserialize<JsonElement>("{}")],
            IncludeAttributes = true,
            Limit = 0,
            RankBy = JsonSerializer.Deserialize<JsonElement>("{}"),
            TopK = 0,
        };

        Query copied = new(model);

        Assert.Equal(model, copied);
    }
}

public class QueryLimitTest : TestBase
{
    [Fact]
    public void LongValidationWorks()
    {
        QueryLimit value = 0;
        value.Validate();
    }

    [Fact]
    public void LimitValidationWorks()
    {
        QueryLimit value = new NamespaceLimit()
        {
            Total = 0,
            Per = new() { Attributes = ["string"], Limit = 0 },
        };
        value.Validate();
    }

    [Fact]
    public void LongSerializationRoundtripWorks()
    {
        QueryLimit value = 0;
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<QueryLimit>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void LimitSerializationRoundtripWorks()
    {
        QueryLimit value = new NamespaceLimit()
        {
            Total = 0,
            Per = new() { Attributes = ["string"], Limit = 0 },
        };
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<QueryLimit>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}

public class NamespaceMultiQueryParamsConsistencyTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new NamespaceMultiQueryParamsConsistency
        {
            Level = NamespaceMultiQueryParamsConsistencyLevel.Strong,
        };

        ApiEnum<string, NamespaceMultiQueryParamsConsistencyLevel> expectedLevel =
            NamespaceMultiQueryParamsConsistencyLevel.Strong;

        Assert.Equal(expectedLevel, model.Level);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new NamespaceMultiQueryParamsConsistency
        {
            Level = NamespaceMultiQueryParamsConsistencyLevel.Strong,
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<NamespaceMultiQueryParamsConsistency>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new NamespaceMultiQueryParamsConsistency
        {
            Level = NamespaceMultiQueryParamsConsistencyLevel.Strong,
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<NamespaceMultiQueryParamsConsistency>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        ApiEnum<string, NamespaceMultiQueryParamsConsistencyLevel> expectedLevel =
            NamespaceMultiQueryParamsConsistencyLevel.Strong;

        Assert.Equal(expectedLevel, deserialized.Level);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new NamespaceMultiQueryParamsConsistency
        {
            Level = NamespaceMultiQueryParamsConsistencyLevel.Strong,
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new NamespaceMultiQueryParamsConsistency { };

        Assert.Null(model.Level);
        Assert.False(model.RawData.ContainsKey("level"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new NamespaceMultiQueryParamsConsistency { };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new NamespaceMultiQueryParamsConsistency
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
        var model = new NamespaceMultiQueryParamsConsistency
        {
            // Null should be interpreted as omitted for these properties
            Level = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new NamespaceMultiQueryParamsConsistency
        {
            Level = NamespaceMultiQueryParamsConsistencyLevel.Strong,
        };

        NamespaceMultiQueryParamsConsistency copied = new(model);

        Assert.Equal(model, copied);
    }
}

public class NamespaceMultiQueryParamsConsistencyLevelTest : TestBase
{
    [Theory]
    [InlineData(NamespaceMultiQueryParamsConsistencyLevel.Strong)]
    [InlineData(NamespaceMultiQueryParamsConsistencyLevel.Eventual)]
    public void Validation_Works(NamespaceMultiQueryParamsConsistencyLevel rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<string, NamespaceMultiQueryParamsConsistencyLevel> value = rawValue;
        value.Validate();
    }

    [Fact]
    public void InvalidEnumValidationThrows_Works()
    {
        var value = JsonSerializer.Deserialize<
            ApiEnum<string, NamespaceMultiQueryParamsConsistencyLevel>
        >(JsonSerializer.SerializeToElement("invalid value"), ModelBase.SerializerOptions);

        Assert.NotNull(value);
        Assert.Throws<TurbopufferInvalidDataException>(() => value.Validate());
    }

    [Theory]
    [InlineData(NamespaceMultiQueryParamsConsistencyLevel.Strong)]
    [InlineData(NamespaceMultiQueryParamsConsistencyLevel.Eventual)]
    public void SerializationRoundtrip_Works(NamespaceMultiQueryParamsConsistencyLevel rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<string, NamespaceMultiQueryParamsConsistencyLevel> value = rawValue;

        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<
            ApiEnum<string, NamespaceMultiQueryParamsConsistencyLevel>
        >(json, ModelBase.SerializerOptions);

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void InvalidEnumSerializationRoundtrip_Works()
    {
        var value = JsonSerializer.Deserialize<
            ApiEnum<string, NamespaceMultiQueryParamsConsistencyLevel>
        >(JsonSerializer.SerializeToElement("invalid value"), ModelBase.SerializerOptions);
        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<
            ApiEnum<string, NamespaceMultiQueryParamsConsistencyLevel>
        >(json, ModelBase.SerializerOptions);

        Assert.Equal(value, deserialized);
    }
}

public class NamespaceMultiQueryParamsLimitTest : TestBase
{
    [Fact]
    public void LongValidationWorks()
    {
        NamespaceMultiQueryParamsLimit value = 0;
        value.Validate();
    }

    [Fact]
    public void TotalValidationWorks()
    {
        NamespaceMultiQueryParamsLimit value = new Total(0);
        value.Validate();
    }

    [Fact]
    public void LongSerializationRoundtripWorks()
    {
        NamespaceMultiQueryParamsLimit value = 0;
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<NamespaceMultiQueryParamsLimit>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void TotalSerializationRoundtripWorks()
    {
        NamespaceMultiQueryParamsLimit value = new Total(0);
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<NamespaceMultiQueryParamsLimit>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}

public class TotalTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new Total { TotalValue = 0 };

        long expectedTotalValue = 0;

        Assert.Equal(expectedTotalValue, model.TotalValue);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new Total { TotalValue = 0 };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Total>(json, ModelBase.SerializerOptions);

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new Total { TotalValue = 0 };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Total>(element, ModelBase.SerializerOptions);
        Assert.NotNull(deserialized);

        long expectedTotalValue = 0;

        Assert.Equal(expectedTotalValue, deserialized.TotalValue);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new Total { TotalValue = 0 };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new Total { TotalValue = 0 };

        Total copied = new(model);

        Assert.Equal(model, copied);
    }
}
