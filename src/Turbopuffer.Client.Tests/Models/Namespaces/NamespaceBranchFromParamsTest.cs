using System;
using System.Text.Json;
using System.Threading.Tasks;
using Turbopuffer.Client.Models.Namespaces;

namespace Turbopuffer.Client.Tests.Models.Namespaces;

public class NamespaceBranchFromParamsTest : TestBase
{
    [Fact]
    public async Task BodyContent_WrapsInBranchFromNamespace()
    {
        NamespaceBranchFromParams parameters = new()
        {
            Namespace = "namespace",
            SourceNamespace = "source_namespace",
        };

#pragma warning disable xUnit1051 // net472 has no CancellationToken overload of ReadAsStringAsync
        var body = await parameters.BodyContent()!.ReadAsStringAsync();
#pragma warning restore xUnit1051
        using var document = JsonDocument.Parse(body);

        var wrapped = document.RootElement.GetProperty("branch_from_namespace");
        Assert.Equal(
            "source_namespace",
            wrapped.GetProperty("source_namespace").GetString()
        );
    }

    [Fact]
    public void FieldRoundtrip_Works()
    {
        var parameters = new NamespaceBranchFromParams
        {
            Namespace = "namespace",
            SourceNamespace = "source_namespace",
        };

        string expectedNamespace = "namespace";
        string expectedSourceNamespace = "source_namespace";

        Assert.Equal(expectedNamespace, parameters.Namespace);
        Assert.Equal(expectedSourceNamespace, parameters.SourceNamespace);
    }

    [Fact]
    public void Url_Works()
    {
        NamespaceBranchFromParams parameters = new()
        {
            Namespace = "namespace",
            SourceNamespace = "source_namespace",
        };

        var url = parameters.Url(new() { Region = "gcp-us-central1", ApiKey = "tpuf_A1..." });

        Assert.True(
            TestBase.UrisEqual(
                new Uri(
                    "https://gcp-us-central1.turbopuffer.com/v2/namespaces/namespace?stainless_overload=branchFrom"
                ),
                url
            )
        );
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var parameters = new NamespaceBranchFromParams
        {
            Namespace = "namespace",
            SourceNamespace = "source_namespace",
        };

        NamespaceBranchFromParams copied = new(parameters);

        Assert.Equal(parameters, copied);
    }
}
