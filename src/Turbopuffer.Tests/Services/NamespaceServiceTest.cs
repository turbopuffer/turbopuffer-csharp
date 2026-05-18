using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Services;

public class NamespaceServiceTest : TestBase
{
    [Fact(Skip = "Mock server tests are disabled")]
    public async Task BranchFrom_Works()
    {
        var response = await this
            .client.Namespace("namespace")
            .BranchFrom(
                new() { SourceNamespace = "source_namespace" },
                TestContext.Current.CancellationToken
            );
        response.Validate();
    }

    [Fact(Skip = "Mock server tests are disabled")]
    public async Task CopyFrom_Works()
    {
        var response = await this
            .client.Namespace("namespace")
            .CopyFrom(
                new() { SourceNamespace = "source_namespace" },
                TestContext.Current.CancellationToken
            );
        response.Validate();
    }

    [Fact(Skip = "Mock server tests are disabled")]
    public async Task DeleteAll_Works()
    {
        var response = await this
            .client.Namespace("namespace")
            .DeleteAll(new(), TestContext.Current.CancellationToken);
        response.Validate();
    }

    [Fact(Skip = "Mock server tests are disabled")]
    public async Task ExplainQuery_Works()
    {
        var response = await this
            .client.Namespace("namespace")
            .ExplainQuery(new(), TestContext.Current.CancellationToken);
        response.Validate();
    }

    [Fact(Skip = "Mock server tests are disabled")]
    public async Task HintCacheWarm_Works()
    {
        var response = await this
            .client.Namespace("namespace")
            .HintCacheWarm(new(), TestContext.Current.CancellationToken);
        response.Validate();
    }

    [Fact(Skip = "Mock server tests are disabled")]
    public async Task Metadata_Works()
    {
        var namespaceMetadata = await this
            .client.Namespace("namespace")
            .Metadata(new(), TestContext.Current.CancellationToken);
        namespaceMetadata.Validate();
    }

    [Fact(Skip = "Mock server tests are disabled")]
    public async Task MultiQuery_Works()
    {
        var response = await this
            .client.Namespace("namespace")
            .MultiQuery(
                new()
                {
                    Queries =
                    [
                        new()
                        {
                            AggregateBy = new Dictionary<string, JsonElement>()
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
                },
                TestContext.Current.CancellationToken
            );
        response.Validate();
    }

    [Fact(Skip = "Mock server tests are disabled")]
    public async Task Query_Works()
    {
        var response = await this
            .client.Namespace("namespace")
            .Query(new(), TestContext.Current.CancellationToken);
        response.Validate();
    }

    [Fact(Skip = "Mock server tests are disabled")]
    public async Task Recall_Works()
    {
        var response = await this
            .client.Namespace("namespace")
            .Recall(new(), TestContext.Current.CancellationToken);
        response.Validate();
    }

    [Fact(Skip = "Mock server tests are disabled")]
    public async Task Schema_Works()
    {
        var response = await this
            .client.Namespace("namespace")
            .Schema(new(), TestContext.Current.CancellationToken);
        foreach (var item in response.Values)
        {
            item.Validate();
        }
    }

    [Fact(Skip = "Mock server tests are disabled")]
    public async Task UpdateMetadata_Works()
    {
        var namespaceMetadata = await this
            .client.Namespace("namespace")
            .UpdateMetadata(new(), TestContext.Current.CancellationToken);
        namespaceMetadata.Validate();
    }

    [Fact(Skip = "Mock server tests are disabled")]
    public async Task UpdateSchema_Works()
    {
        var response = await this
            .client.Namespace("namespace")
            .UpdateSchema(new(), TestContext.Current.CancellationToken);
        foreach (var item in response.Values)
        {
            item.Validate();
        }
    }

    [Fact(Skip = "Mock server tests are disabled")]
    public async Task Write_Works()
    {
        var response = await this
            .client.Namespace("namespace")
            .Write(new(), TestContext.Current.CancellationToken);
        response.Validate();
    }
}
