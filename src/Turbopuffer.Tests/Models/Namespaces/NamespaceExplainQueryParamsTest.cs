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
            Consistency = new Strong(),
            DistanceMetric = DistanceMetric.CosineDistance,
            ExcludeAttributes = ["string"],
            Filters = JsonSerializer.Deserialize<JsonElement>("{}"),
            GroupBy = [JsonSerializer.Deserialize<JsonElement>("{}")],
            IncludeAttributes = true,
            Limit = 0,
            Offset = 0,
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
        Consistency expectedConsistency = new Strong();
        ApiEnum<string, DistanceMetric> expectedDistanceMetric = DistanceMetric.CosineDistance;
        List<string> expectedExcludeAttributes = ["string"];
        JsonElement expectedFilters = JsonSerializer.Deserialize<JsonElement>("{}");
        List<JsonElement> expectedGroupBy = [JsonSerializer.Deserialize<JsonElement>("{}")];
        IncludeAttributes expectedIncludeAttributes = true;
        Limit expectedLimit = 0;
        long expectedOffset = 0;
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
        Assert.Equal(expectedOffset, parameters.Offset);
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
        Assert.Null(parameters.Offset);
        Assert.False(parameters.RawBodyData.ContainsKey("offset"));
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
            Offset = null,
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
        Assert.Null(parameters.Offset);
        Assert.False(parameters.RawBodyData.ContainsKey("offset"));
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
            Consistency = new Strong(),
            DistanceMetric = DistanceMetric.CosineDistance,
            ExcludeAttributes = ["string"],
            Filters = JsonSerializer.Deserialize<JsonElement>("{}"),
            GroupBy = [JsonSerializer.Deserialize<JsonElement>("{}")],
            IncludeAttributes = true,
            Limit = 0,
            Offset = 0,
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
    public void StrongValidationWorks()
    {
        Consistency value = new Strong();
        value.Validate();
    }

    [Fact]
    public void EventualValidationWorks()
    {
        Consistency value = new Eventual() { MaxStalenessSeconds = 1 };
        value.Validate();
    }

    [Fact]
    public void StrongSerializationRoundtripWorks()
    {
        Consistency value = new Strong();
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Consistency>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void EventualSerializationRoundtripWorks()
    {
        Consistency value = new Eventual() { MaxStalenessSeconds = 1 };
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Consistency>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}

public class StrongTest : TestBase
{
    [Fact]
    public void DefaultValidation_Works()
    {
        var constant = new Strong();
        constant.Validate();
    }

    [Fact]
    public void ValidConstantValidation_Works()
    {
        var constant = JsonSerializer.Deserialize<Strong>(
            JsonSerializer.Deserialize<JsonElement>(
                """
                {
                  "level": "strong"
                }
                """
            ),
            ModelBase.SerializerOptions
        );

        Assert.NotNull(constant);
        constant.Validate();
    }

    [Fact]
    public void InvalidConstantValidationThrows_Works()
    {
        var constant = JsonSerializer.Deserialize<Strong>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );

        Assert.NotNull(constant);
        Assert.Throws<TurbopufferInvalidDataException>(() => constant.Validate());
    }

    [Fact]
    public void DefaultRoundtrip_Works()
    {
        var constant = new Strong();
        string element = JsonSerializer.Serialize(constant, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Strong>(element, ModelBase.SerializerOptions);

        Assert.Equal(constant, deserialized);
    }

    [Fact]
    public void ValidConstantRoundtrip_Works()
    {
        var constant = JsonSerializer.Deserialize<Strong>(
            JsonSerializer.Deserialize<JsonElement>(
                """
                {
                  "level": "strong"
                }
                """
            ),
            ModelBase.SerializerOptions
        );
        string element = JsonSerializer.Serialize(constant, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Strong>(element, ModelBase.SerializerOptions);

        Assert.Equal(constant, deserialized);
    }

    [Fact]
    public void InvalidConstantRoundtrip_Works()
    {
        var constant = JsonSerializer.Deserialize<Strong>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );
        string element = JsonSerializer.Serialize(constant, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Strong>(element, ModelBase.SerializerOptions);

        Assert.Equal(constant, deserialized);
    }
}

public class EventualTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new Eventual { MaxStalenessSeconds = 1 };

        JsonElement expectedLevel = JsonSerializer.SerializeToElement("eventual");
        long expectedMaxStalenessSeconds = 1;

        Assert.True(JsonElement.DeepEquals(expectedLevel, model.Level));
        Assert.Equal(expectedMaxStalenessSeconds, model.MaxStalenessSeconds);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new Eventual { MaxStalenessSeconds = 1 };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Eventual>(json, ModelBase.SerializerOptions);

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new Eventual { MaxStalenessSeconds = 1 };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Eventual>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        JsonElement expectedLevel = JsonSerializer.SerializeToElement("eventual");
        long expectedMaxStalenessSeconds = 1;

        Assert.True(JsonElement.DeepEquals(expectedLevel, deserialized.Level));
        Assert.Equal(expectedMaxStalenessSeconds, deserialized.MaxStalenessSeconds);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new Eventual { MaxStalenessSeconds = 1 };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new Eventual { };

        Assert.Null(model.MaxStalenessSeconds);
        Assert.False(model.RawData.ContainsKey("max_staleness_seconds"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new Eventual { };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new Eventual
        {
            // Null should be interpreted as omitted for these properties
            MaxStalenessSeconds = null,
        };

        Assert.Null(model.MaxStalenessSeconds);
        Assert.False(model.RawData.ContainsKey("max_staleness_seconds"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new Eventual
        {
            // Null should be interpreted as omitted for these properties
            MaxStalenessSeconds = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new Eventual { MaxStalenessSeconds = 1 };

        Eventual copied = new(model);

        Assert.Equal(model, copied);
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
