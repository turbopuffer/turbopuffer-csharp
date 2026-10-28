using System;
using System.Text.Json;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class NamespaceRecallParamsTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var parameters = new NamespaceRecallParams
        {
            Namespace = "namespace",
            Filters = JsonSerializer.Deserialize<JsonElement>("{}"),
            IncludeGroundTruth = true,
            Num = 0,
            RankBy = JsonSerializer.Deserialize<JsonElement>("{}"),
            TopK = 0,
        };

        string expectedNamespace = "namespace";
        JsonElement expectedFilters = JsonSerializer.Deserialize<JsonElement>("{}");
        bool expectedIncludeGroundTruth = true;
        long expectedNum = 0;
        JsonElement expectedRankBy = JsonSerializer.Deserialize<JsonElement>("{}");
        long expectedTopK = 0;

        Assert.Equal(expectedNamespace, parameters.Namespace);
        Assert.NotNull(parameters.Filters);
        Assert.True(JsonElement.DeepEquals(expectedFilters, parameters.Filters.Value));
        Assert.Equal(expectedIncludeGroundTruth, parameters.IncludeGroundTruth);
        Assert.Equal(expectedNum, parameters.Num);
        Assert.NotNull(parameters.RankBy);
        Assert.True(JsonElement.DeepEquals(expectedRankBy, parameters.RankBy.Value));
        Assert.Equal(expectedTopK, parameters.TopK);
    }

    [Fact]
    public void OptionalNonNullableParamsUnsetAreNotSet_Works()
    {
        var parameters = new NamespaceRecallParams { Namespace = "namespace" };

        Assert.Null(parameters.Filters);
        Assert.False(parameters.RawBodyData.ContainsKey("filters"));
        Assert.Null(parameters.IncludeGroundTruth);
        Assert.False(parameters.RawBodyData.ContainsKey("include_ground_truth"));
        Assert.Null(parameters.Num);
        Assert.False(parameters.RawBodyData.ContainsKey("num"));
        Assert.Null(parameters.RankBy);
        Assert.False(parameters.RawBodyData.ContainsKey("rank_by"));
        Assert.Null(parameters.TopK);
        Assert.False(parameters.RawBodyData.ContainsKey("top_k"));
    }

    [Fact]
    public void OptionalNonNullableParamsSetToNullAreNotSet_Works()
    {
        var parameters = new NamespaceRecallParams
        {
            Namespace = "namespace",

            // Null should be interpreted as omitted for these properties
            Filters = null,
            IncludeGroundTruth = null,
            Num = null,
            RankBy = null,
            TopK = null,
        };

        Assert.Null(parameters.Filters);
        Assert.False(parameters.RawBodyData.ContainsKey("filters"));
        Assert.Null(parameters.IncludeGroundTruth);
        Assert.False(parameters.RawBodyData.ContainsKey("include_ground_truth"));
        Assert.Null(parameters.Num);
        Assert.False(parameters.RawBodyData.ContainsKey("num"));
        Assert.Null(parameters.RankBy);
        Assert.False(parameters.RawBodyData.ContainsKey("rank_by"));
        Assert.Null(parameters.TopK);
        Assert.False(parameters.RawBodyData.ContainsKey("top_k"));
    }

    [Fact]
    public void Url_Works()
    {
        NamespaceRecallParams parameters = new() { Namespace = "namespace" };

        var url = parameters.Url(new() { Region = "gcp-us-central1", ApiKey = "tpuf_A1..." });

        Assert.True(
            TestBase.UrisEqual(
                new Uri(
                    "https://gcp-us-central1.turbopuffer.com/v1/namespaces/namespace/_debug/recall"
                ),
                url
            )
        );
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var parameters = new NamespaceRecallParams
        {
            Namespace = "namespace",
            Filters = JsonSerializer.Deserialize<JsonElement>("{}"),
            IncludeGroundTruth = true,
            Num = 0,
            RankBy = JsonSerializer.Deserialize<JsonElement>("{}"),
            TopK = 0,
        };

        NamespaceRecallParams copied = new(parameters);

        Assert.Equal(parameters, copied);
    }
}
