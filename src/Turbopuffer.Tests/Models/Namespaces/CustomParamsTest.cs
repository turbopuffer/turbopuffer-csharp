using System;
using System.Collections.Generic;
using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

// Covers the params fields that custom code retypes. Their generated tests are excluded in the
// test project, so these only touch the custom-typed fields and stay clear of generated changes.
public class CustomParamsTest : TestBase
{
    static JsonElement Json(string json) => JsonSerializer.Deserialize<JsonElement>(json);

    static JsonElement Serialized<T>(T value) =>
        JsonSerializer.SerializeToElement(value, ModelBase.SerializerOptions);

    static void AssertJson(string expected, JsonElement actual) =>
        Assert.True(JsonElement.DeepEquals(Json(expected), actual), actual.GetRawText());

    [Fact]
    public void QueryParams_CustomFields_Work()
    {
        var parameters = new NamespaceQueryParams
        {
            Namespace = "namespace",
            AggregateBy = new Dictionary<string, AggregateBy> { { "count", AggregateBy.Count() } },
            ComputeAttributes = new Dictionary<string, Expr>
            {
                { "foo", new ExprRaw(Json("\"bar\"")) },
            },
            Filters = Filter.Eq("attr", "value"),
            GroupBy = [GroupBy.Attr("attr")],
            RankBy = new RankByRaw(Json("""["id", "asc"]""")),
        };

        AssertJson("""{"count": ["Count"]}""", parameters.RawBodyData["aggregate_by"]);
        AssertJson("""{"foo": "bar"}""", parameters.RawBodyData["compute_attributes"]);
        AssertJson("""["attr", "Eq", "value"]""", parameters.RawBodyData["filters"]);
        AssertJson("""["attr"]""", parameters.RawBodyData["group_by"]);
        AssertJson("""["id", "asc"]""", parameters.RawBodyData["rank_by"]);

        AssertJson("""["Count"]""", Serialized(parameters.AggregateBy!["count"]));
        AssertJson("\"bar\"", Serialized(parameters.ComputeAttributes!["foo"]));
        AssertJson("""["attr", "Eq", "value"]""", Serialized(parameters.Filters!));
        AssertJson("\"attr\"", Serialized(parameters.GroupBy![0]));
        AssertJson("""["id", "asc"]""", Serialized(parameters.RankBy!));
    }

    [Fact]
    public void QueryParams_CustomFieldsUnset_AreNotSet()
    {
        var parameters = new NamespaceQueryParams { Namespace = "namespace" };

        Assert.Null(parameters.AggregateBy);
        Assert.Null(parameters.ComputeAttributes);
        Assert.Null(parameters.Filters);
        Assert.Null(parameters.GroupBy);
        Assert.Null(parameters.RankBy);
        Assert.False(parameters.RawBodyData.ContainsKey("filters"));
    }

    [Fact]
    public void ExplainQueryParams_CustomFields_Work()
    {
        var parameters = new NamespaceExplainQueryParams
        {
            Namespace = "namespace",
            AggregateBy = new Dictionary<string, AggregateBy> { { "count", AggregateBy.Count() } },
            ComputeAttributes = new Dictionary<string, Expr>
            {
                { "foo", new ExprRaw(Json("\"bar\"")) },
            },
            Filters = Filter.Eq("attr", "value"),
            GroupBy = [GroupBy.Attr("attr")],
            RankBy = new RankByRaw(Json("""["id", "asc"]""")),
        };

        AssertJson("""{"count": ["Count"]}""", parameters.RawBodyData["aggregate_by"]);
        AssertJson("""{"foo": "bar"}""", parameters.RawBodyData["compute_attributes"]);
        AssertJson("""["attr", "Eq", "value"]""", parameters.RawBodyData["filters"]);
        AssertJson("""["attr"]""", parameters.RawBodyData["group_by"]);
        AssertJson("""["id", "asc"]""", parameters.RawBodyData["rank_by"]);

        AssertJson("""["attr", "Eq", "value"]""", Serialized(parameters.Filters!));
        AssertJson("""["id", "asc"]""", Serialized(parameters.RankBy!));
    }

    [Fact]
    public void MultiQueryParams_CustomFields_Work()
    {
        var parameters = new NamespaceMultiQueryParams
        {
            Namespace = "namespace",
            Queries =
            [
                new()
                {
                    AggregateBy = new Dictionary<string, AggregateBy>
                    {
                        { "count", AggregateBy.Count() },
                    },
                    ComputeAttributes = new Dictionary<string, Expr>
                    {
                        { "foo", new ExprRaw(Json("\"bar\"")) },
                    },
                    Filters = Filter.Eq("attr", "value"),
                    GroupBy = [GroupBy.Attr("attr")],
                    RankBy = new RankByRaw(Json("""["id", "asc"]""")),
                },
            ],
            RerankBy = new RerankByRaw(Json("""["RRF"]""")),
        };

        AssertJson(
            """
            [{
                "aggregate_by": {"count": ["Count"]},
                "compute_attributes": {"foo": "bar"},
                "filters": ["attr", "Eq", "value"],
                "group_by": ["attr"],
                "rank_by": ["id", "asc"]
            }]
            """,
            parameters.RawBodyData["queries"]
        );
        AssertJson("""["RRF"]""", parameters.RawBodyData["rerank_by"]);

        var query = parameters.Queries[0];
        AssertJson("""["attr", "Eq", "value"]""", Serialized(query.Filters!));
        AssertJson("""["id", "asc"]""", Serialized(query.RankBy!));
        AssertJson("""["RRF"]""", Serialized(parameters.RerankBy!));
    }

    [Fact]
    public void RecallParams_CustomFields_Work()
    {
        var parameters = new NamespaceRecallParams
        {
            Namespace = "namespace",
            Filters = Filter.Eq("attr", "value"),
            RankBy = new RankByRaw(Json("""["id", "asc"]""")),
        };

        AssertJson("""["attr", "Eq", "value"]""", parameters.RawBodyData["filters"]);
        AssertJson("""["id", "asc"]""", parameters.RawBodyData["rank_by"]);
        AssertJson("""["attr", "Eq", "value"]""", Serialized(parameters.Filters!));
        AssertJson("""["id", "asc"]""", Serialized(parameters.RankBy!));
    }

    [Fact]
    public void WriteParams_CustomFields_Work()
    {
        var parameters = new NamespaceWriteParams
        {
            Namespace = "namespace",
            DeleteByFilter = Filter.Eq("a", 1),
            DeleteCondition = Filter.Eq("b", 2),
            PatchCondition = Filter.Eq("c", 3),
            Schema = new Dictionary<string, AttributeSchemaConfig>
            {
                {
                    "id",
                    new() { Type = "uuid" }
                },
            },
        };

        AssertJson("""["a", "Eq", 1]""", parameters.RawBodyData["delete_by_filter"]);
        AssertJson("""["b", "Eq", 2]""", parameters.RawBodyData["delete_condition"]);
        AssertJson("""["c", "Eq", 3]""", parameters.RawBodyData["patch_condition"]);
        AssertJson("""{"id": {"type": "uuid"}}""", parameters.RawBodyData["schema"]);
        Assert.Equal("uuid", parameters.Schema!["id"].Type);
    }

    [Fact]
    public void QueryResponse_Getters_Work()
    {
        var response = JsonSerializer.Deserialize<NamespaceQueryResponse>(
            """{"rows": [{"id": 1}], "aggregation_groups": [{"count": 2}]}""",
            ModelBase.SerializerOptions
        )!;

        Assert.Single(response.GetRows());
        Assert.Single(response.GetAggregationGroups());
        Assert.Throws<InvalidOperationException>(() => response.GetAggregations());
    }

    [Fact]
    public void MultiQueryResponseResult_Getters_Work()
    {
        var result = JsonSerializer.Deserialize<Result>(
            """{"aggregations": {"count": 2}}""",
            ModelBase.SerializerOptions
        )!;

        Assert.Equal(2, result.GetAggregations()["count"].GetInt32());
        Assert.Throws<InvalidOperationException>(() => result.GetRows());
        Assert.Throws<InvalidOperationException>(() => result.GetAggregationGroups());
    }
}
