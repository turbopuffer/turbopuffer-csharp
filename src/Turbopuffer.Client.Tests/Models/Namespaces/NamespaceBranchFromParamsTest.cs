using System;
using Turbopuffer.Client.Models.Namespaces;

namespace Turbopuffer.Client.Tests.Models.Namespaces;

public class NamespaceBranchFromParamsTest : TestBase
{
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
