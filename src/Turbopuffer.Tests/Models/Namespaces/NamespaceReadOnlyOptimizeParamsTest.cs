using System;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class NamespaceReadOnlyOptimizeParamsTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var parameters = new NamespaceReadOnlyOptimizeParams { Namespace = "namespace" };

        string expectedNamespace = "namespace";

        Assert.Equal(expectedNamespace, parameters.Namespace);
    }

    [Fact]
    public void Url_Works()
    {
        NamespaceReadOnlyOptimizeParams parameters = new() { Namespace = "namespace" };

        var url = parameters.Url(new() { Region = "gcp-us-central1", ApiKey = "tpuf_A1..." });

        Assert.True(
            TestBase.UrisEqual(
                new Uri(
                    "https://gcp-us-central1.turbopuffer.com/v2/namespaces/namespace/read_only_optimize"
                ),
                url
            )
        );
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var parameters = new NamespaceReadOnlyOptimizeParams { Namespace = "namespace" };

        NamespaceReadOnlyOptimizeParams copied = new(parameters);

        Assert.Equal(parameters, copied);
    }
}
