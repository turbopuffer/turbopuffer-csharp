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
        var response = await this.client.Namespaces1.BranchFrom(
            new() { Namespace = "namespace", SourceNamespace = "source_namespace" },
            TestContext.Current.CancellationToken
        );
        response.Validate();
    }

    [Fact(Skip = "Mock server tests are disabled")]
    public async Task CopyFrom_Works()
    {
        var response = await this.client.Namespaces1.CopyFrom(
            new() { Namespace = "namespace", SourceNamespace = "source_namespace" },
            TestContext.Current.CancellationToken
        );
        response.Validate();
    }

    [Fact(Skip = "Mock server tests are disabled")]
    public async Task DeleteAll_Works()
    {
        var response = await this.client.Namespaces1.DeleteAll(
            new() { Namespace = "namespace" },
            TestContext.Current.CancellationToken
        );
        response.Validate();
    }

    [Fact(Skip = "Mock server tests are disabled")]
    public async Task ExplainQuery_Works()
    {
        var response = await this.client.Namespaces1.ExplainQuery(
            new() { Namespace = "namespace" },
            TestContext.Current.CancellationToken
        );
        response.Validate();
    }

    [Fact(Skip = "Mock server tests are disabled")]
    public async Task HintCacheWarm_Works()
    {
        var response = await this.client.Namespaces1.HintCacheWarm(
            new() { Namespace = "namespace" },
            TestContext.Current.CancellationToken
        );
        response.Validate();
    }

    [Fact(Skip = "Mock server tests are disabled")]
    public async Task Metadata_Works()
    {
        var namespaceMetadata = await this.client.Namespaces1.Metadata(
            new() { Namespace = "namespace" },
            TestContext.Current.CancellationToken
        );
        namespaceMetadata.Validate();
    }

    [Fact(Skip = "Mock server tests are disabled")]
    public async Task MultiQuery_Works()
    {
        var response = await this.client.Namespaces1.MultiQuery(
            new()
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
            },
            TestContext.Current.CancellationToken
        );
        response.Validate();
    }

    [Fact(Skip = "Mock server tests are disabled")]
    public async Task Query_Works()
    {
        var response = await this.client.Namespaces1.Query(
            new() { Namespace = "namespace" },
            TestContext.Current.CancellationToken
        );
        response.Validate();
    }

    [Fact(Skip = "Mock server tests are disabled")]
    public async Task Recall_Works()
    {
        var response = await this.client.Namespaces1.Recall(
            new() { Namespace = "namespace" },
            TestContext.Current.CancellationToken
        );
        response.Validate();
    }

    [Fact(Skip = "Mock server tests are disabled")]
    public async Task Schema_Works()
    {
        var response = await this.client.Namespaces1.Schema(
            new() { Namespace = "namespace" },
            TestContext.Current.CancellationToken
        );
        foreach (var item in response.Values)
        {
            item.Validate();
        }
    }

    [Fact(Skip = "Mock server tests are disabled")]
    public async Task UpdateMetadata_Works()
    {
        var namespaceMetadata = await this.client.Namespaces1.UpdateMetadata(
            new() { Namespace = "namespace" },
            TestContext.Current.CancellationToken
        );
        namespaceMetadata.Validate();
    }

    [Fact(Skip = "Mock server tests are disabled")]
    public async Task UpdateSchema_Works()
    {
        var response = await this.client.Namespaces1.UpdateSchema(
            new() { Namespace = "namespace" },
            TestContext.Current.CancellationToken
        );
        foreach (var item in response.Values)
        {
            item.Validate();
        }
    }

    [Fact(Skip = "Mock server tests are disabled")]
    public async Task Write_Works()
    {
        var response = await this.client.Namespaces1.Write(
            new() { Namespace = "namespace" },
            TestContext.Current.CancellationToken
        );
        response.Validate();
    }
}
