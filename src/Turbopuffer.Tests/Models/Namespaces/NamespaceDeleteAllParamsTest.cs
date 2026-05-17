using System;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class NamespaceDeleteAllParamsTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var parameters = new NamespaceDeleteAllParams { Namespace = "namespace" };

        string expectedNamespace = "namespace";

        Assert.Equal(expectedNamespace, parameters.Namespace);
    }

    [Fact]
    public void Url_Works()
    {
        NamespaceDeleteAllParams parameters = new() { Namespace = "namespace" };

        var url = parameters.Url(new() { Region = "gcp-us-central1", ApiKey = "tpuf_A1..." });

        Assert.True(
            TestBase.UrisEqual(
                new Uri("https://gcp-us-central1.turbopuffer.com/v2/namespaces/namespace"),
                url
            )
        );
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var parameters = new NamespaceDeleteAllParams { Namespace = "namespace" };

        NamespaceDeleteAllParams copied = new(parameters);

        Assert.Equal(parameters, copied);
    }
}
