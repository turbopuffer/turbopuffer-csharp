using System.Collections.Generic;
using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class NamespaceMultiQueryResponseTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new NamespaceMultiQueryResponse
        {
            Billing = new() { BillableLogicalBytesQueried = 0, BillableLogicalBytesReturned = 0 },
            Performance = new()
            {
                ApproxNamespaceSize = 0,
                CacheHitRatio = 0,
                CacheTemperature = "cache_temperature",
                ExhaustiveSearchCount = 0,
                QueryExecutionMs = 0,
                ServerTotalMs = 0,
            },
            Results =
            [
                new()
                {
                    AggregationGroups =
                    [
                        new Dictionary<string, JsonElement>()
                        {
                            { "foo", JsonSerializer.SerializeToElement("bar") },
                        },
                    ],
                    Aggregations = new Dictionary<string, JsonElement>()
                    {
                        { "foo", JsonSerializer.SerializeToElement("bar") },
                    },
                    Rows =
                    [
                        new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) },
                    ],
                },
            ],
        };

        QueryBilling expectedBilling = new()
        {
            BillableLogicalBytesQueried = 0,
            BillableLogicalBytesReturned = 0,
        };
        QueryPerformance expectedPerformance = new()
        {
            ApproxNamespaceSize = 0,
            CacheHitRatio = 0,
            CacheTemperature = "cache_temperature",
            ExhaustiveSearchCount = 0,
            QueryExecutionMs = 0,
            ServerTotalMs = 0,
        };
        List<Result> expectedResults =
        [
            new()
            {
                AggregationGroups =
                [
                    new Dictionary<string, JsonElement>()
                    {
                        { "foo", JsonSerializer.SerializeToElement("bar") },
                    },
                ],
                Aggregations = new Dictionary<string, JsonElement>()
                {
                    { "foo", JsonSerializer.SerializeToElement("bar") },
                },
                Rows = [new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) }],
            },
        ];

        Assert.Equal(expectedBilling, model.Billing);
        Assert.Equal(expectedPerformance, model.Performance);
        Assert.Equal(expectedResults.Count, model.Results.Count);
        for (int i = 0; i < expectedResults.Count; i++)
        {
            Assert.Equal(expectedResults[i], model.Results[i]);
        }
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new NamespaceMultiQueryResponse
        {
            Billing = new() { BillableLogicalBytesQueried = 0, BillableLogicalBytesReturned = 0 },
            Performance = new()
            {
                ApproxNamespaceSize = 0,
                CacheHitRatio = 0,
                CacheTemperature = "cache_temperature",
                ExhaustiveSearchCount = 0,
                QueryExecutionMs = 0,
                ServerTotalMs = 0,
            },
            Results =
            [
                new()
                {
                    AggregationGroups =
                    [
                        new Dictionary<string, JsonElement>()
                        {
                            { "foo", JsonSerializer.SerializeToElement("bar") },
                        },
                    ],
                    Aggregations = new Dictionary<string, JsonElement>()
                    {
                        { "foo", JsonSerializer.SerializeToElement("bar") },
                    },
                    Rows =
                    [
                        new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) },
                    ],
                },
            ],
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<NamespaceMultiQueryResponse>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new NamespaceMultiQueryResponse
        {
            Billing = new() { BillableLogicalBytesQueried = 0, BillableLogicalBytesReturned = 0 },
            Performance = new()
            {
                ApproxNamespaceSize = 0,
                CacheHitRatio = 0,
                CacheTemperature = "cache_temperature",
                ExhaustiveSearchCount = 0,
                QueryExecutionMs = 0,
                ServerTotalMs = 0,
            },
            Results =
            [
                new()
                {
                    AggregationGroups =
                    [
                        new Dictionary<string, JsonElement>()
                        {
                            { "foo", JsonSerializer.SerializeToElement("bar") },
                        },
                    ],
                    Aggregations = new Dictionary<string, JsonElement>()
                    {
                        { "foo", JsonSerializer.SerializeToElement("bar") },
                    },
                    Rows =
                    [
                        new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) },
                    ],
                },
            ],
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<NamespaceMultiQueryResponse>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        QueryBilling expectedBilling = new()
        {
            BillableLogicalBytesQueried = 0,
            BillableLogicalBytesReturned = 0,
        };
        QueryPerformance expectedPerformance = new()
        {
            ApproxNamespaceSize = 0,
            CacheHitRatio = 0,
            CacheTemperature = "cache_temperature",
            ExhaustiveSearchCount = 0,
            QueryExecutionMs = 0,
            ServerTotalMs = 0,
        };
        List<Result> expectedResults =
        [
            new()
            {
                AggregationGroups =
                [
                    new Dictionary<string, JsonElement>()
                    {
                        { "foo", JsonSerializer.SerializeToElement("bar") },
                    },
                ],
                Aggregations = new Dictionary<string, JsonElement>()
                {
                    { "foo", JsonSerializer.SerializeToElement("bar") },
                },
                Rows = [new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) }],
            },
        ];

        Assert.Equal(expectedBilling, deserialized.Billing);
        Assert.Equal(expectedPerformance, deserialized.Performance);
        Assert.Equal(expectedResults.Count, deserialized.Results.Count);
        for (int i = 0; i < expectedResults.Count; i++)
        {
            Assert.Equal(expectedResults[i], deserialized.Results[i]);
        }
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new NamespaceMultiQueryResponse
        {
            Billing = new() { BillableLogicalBytesQueried = 0, BillableLogicalBytesReturned = 0 },
            Performance = new()
            {
                ApproxNamespaceSize = 0,
                CacheHitRatio = 0,
                CacheTemperature = "cache_temperature",
                ExhaustiveSearchCount = 0,
                QueryExecutionMs = 0,
                ServerTotalMs = 0,
            },
            Results =
            [
                new()
                {
                    AggregationGroups =
                    [
                        new Dictionary<string, JsonElement>()
                        {
                            { "foo", JsonSerializer.SerializeToElement("bar") },
                        },
                    ],
                    Aggregations = new Dictionary<string, JsonElement>()
                    {
                        { "foo", JsonSerializer.SerializeToElement("bar") },
                    },
                    Rows =
                    [
                        new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) },
                    ],
                },
            ],
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new NamespaceMultiQueryResponse
        {
            Billing = new() { BillableLogicalBytesQueried = 0, BillableLogicalBytesReturned = 0 },
            Performance = new()
            {
                ApproxNamespaceSize = 0,
                CacheHitRatio = 0,
                CacheTemperature = "cache_temperature",
                ExhaustiveSearchCount = 0,
                QueryExecutionMs = 0,
                ServerTotalMs = 0,
            },
            Results =
            [
                new()
                {
                    AggregationGroups =
                    [
                        new Dictionary<string, JsonElement>()
                        {
                            { "foo", JsonSerializer.SerializeToElement("bar") },
                        },
                    ],
                    Aggregations = new Dictionary<string, JsonElement>()
                    {
                        { "foo", JsonSerializer.SerializeToElement("bar") },
                    },
                    Rows =
                    [
                        new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) },
                    ],
                },
            ],
        };

        NamespaceMultiQueryResponse copied = new(model);

        Assert.Equal(model, copied);
    }
}

public class ResultTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new Result
        {
            AggregationGroups =
            [
                new Dictionary<string, JsonElement>()
                {
                    { "foo", JsonSerializer.SerializeToElement("bar") },
                },
            ],
            Aggregations = new Dictionary<string, JsonElement>()
            {
                { "foo", JsonSerializer.SerializeToElement("bar") },
            },
            Rows = [new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) }],
        };

        List<Dictionary<string, JsonElement>> expectedAggregationGroups =
        [
            new Dictionary<string, JsonElement>()
            {
                { "foo", JsonSerializer.SerializeToElement("bar") },
            },
        ];
        Dictionary<string, JsonElement> expectedAggregations = new()
        {
            { "foo", JsonSerializer.SerializeToElement("bar") },
        };
        List<Row> expectedRows =
        [
            new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) },
        ];

        Assert.NotNull(model.AggregationGroups);
        Assert.Equal(expectedAggregationGroups.Count, model.AggregationGroups.Count);
        for (int i = 0; i < expectedAggregationGroups.Count; i++)
        {
            Assert.Equal(expectedAggregationGroups[i].Count, model.AggregationGroups[i].Count);
            foreach (var item in expectedAggregationGroups[i])
            {
                Assert.True(model.AggregationGroups[i].TryGetValue(item.Key, out var value));

                Assert.True(JsonElement.DeepEquals(value, model.AggregationGroups[i][item.Key]));
            }
        }
        Assert.NotNull(model.Aggregations);
        Assert.Equal(expectedAggregations.Count, model.Aggregations.Count);
        foreach (var item in expectedAggregations)
        {
            Assert.True(model.Aggregations.TryGetValue(item.Key, out var value));

            Assert.True(JsonElement.DeepEquals(value, model.Aggregations[item.Key]));
        }
        Assert.NotNull(model.Rows);
        Assert.Equal(expectedRows.Count, model.Rows.Count);
        for (int i = 0; i < expectedRows.Count; i++)
        {
            Assert.Equal(expectedRows[i], model.Rows[i]);
        }
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new Result
        {
            AggregationGroups =
            [
                new Dictionary<string, JsonElement>()
                {
                    { "foo", JsonSerializer.SerializeToElement("bar") },
                },
            ],
            Aggregations = new Dictionary<string, JsonElement>()
            {
                { "foo", JsonSerializer.SerializeToElement("bar") },
            },
            Rows = [new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) }],
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Result>(json, ModelBase.SerializerOptions);

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new Result
        {
            AggregationGroups =
            [
                new Dictionary<string, JsonElement>()
                {
                    { "foo", JsonSerializer.SerializeToElement("bar") },
                },
            ],
            Aggregations = new Dictionary<string, JsonElement>()
            {
                { "foo", JsonSerializer.SerializeToElement("bar") },
            },
            Rows = [new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) }],
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Result>(element, ModelBase.SerializerOptions);
        Assert.NotNull(deserialized);

        List<Dictionary<string, JsonElement>> expectedAggregationGroups =
        [
            new Dictionary<string, JsonElement>()
            {
                { "foo", JsonSerializer.SerializeToElement("bar") },
            },
        ];
        Dictionary<string, JsonElement> expectedAggregations = new()
        {
            { "foo", JsonSerializer.SerializeToElement("bar") },
        };
        List<Row> expectedRows =
        [
            new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) },
        ];

        Assert.NotNull(deserialized.AggregationGroups);
        Assert.Equal(expectedAggregationGroups.Count, deserialized.AggregationGroups.Count);
        for (int i = 0; i < expectedAggregationGroups.Count; i++)
        {
            Assert.Equal(
                expectedAggregationGroups[i].Count,
                deserialized.AggregationGroups[i].Count
            );
            foreach (var item in expectedAggregationGroups[i])
            {
                Assert.True(deserialized.AggregationGroups[i].TryGetValue(item.Key, out var value));

                Assert.True(
                    JsonElement.DeepEquals(value, deserialized.AggregationGroups[i][item.Key])
                );
            }
        }
        Assert.NotNull(deserialized.Aggregations);
        Assert.Equal(expectedAggregations.Count, deserialized.Aggregations.Count);
        foreach (var item in expectedAggregations)
        {
            Assert.True(deserialized.Aggregations.TryGetValue(item.Key, out var value));

            Assert.True(JsonElement.DeepEquals(value, deserialized.Aggregations[item.Key]));
        }
        Assert.NotNull(deserialized.Rows);
        Assert.Equal(expectedRows.Count, deserialized.Rows.Count);
        for (int i = 0; i < expectedRows.Count; i++)
        {
            Assert.Equal(expectedRows[i], deserialized.Rows[i]);
        }
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new Result
        {
            AggregationGroups =
            [
                new Dictionary<string, JsonElement>()
                {
                    { "foo", JsonSerializer.SerializeToElement("bar") },
                },
            ],
            Aggregations = new Dictionary<string, JsonElement>()
            {
                { "foo", JsonSerializer.SerializeToElement("bar") },
            },
            Rows = [new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) }],
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new Result { };

        Assert.Null(model.AggregationGroups);
        Assert.False(model.RawData.ContainsKey("aggregation_groups"));
        Assert.Null(model.Aggregations);
        Assert.False(model.RawData.ContainsKey("aggregations"));
        Assert.Null(model.Rows);
        Assert.False(model.RawData.ContainsKey("rows"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new Result { };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new Result
        {
            // Null should be interpreted as omitted for these properties
            AggregationGroups = null,
            Aggregations = null,
            Rows = null,
        };

        Assert.Null(model.AggregationGroups);
        Assert.False(model.RawData.ContainsKey("aggregation_groups"));
        Assert.Null(model.Aggregations);
        Assert.False(model.RawData.ContainsKey("aggregations"));
        Assert.Null(model.Rows);
        Assert.False(model.RawData.ContainsKey("rows"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new Result
        {
            // Null should be interpreted as omitted for these properties
            AggregationGroups = null,
            Aggregations = null,
            Rows = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new Result
        {
            AggregationGroups =
            [
                new Dictionary<string, JsonElement>()
                {
                    { "foo", JsonSerializer.SerializeToElement("bar") },
                },
            ],
            Aggregations = new Dictionary<string, JsonElement>()
            {
                { "foo", JsonSerializer.SerializeToElement("bar") },
            },
            Rows = [new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) }],
        };

        Result copied = new(model);

        Assert.Equal(model, copied);
    }
}
