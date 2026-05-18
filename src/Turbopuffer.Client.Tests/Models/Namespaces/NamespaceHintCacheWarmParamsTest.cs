using System;
using Turbopuffer.Client.Models.Namespaces;

namespace Turbopuffer.Client.Tests.Models.Namespaces;

public class NamespaceHintCacheWarmParamsTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var parameters = new NamespaceHintCacheWarmParams { Namespace = "namespace" };

        string expectedNamespace = "namespace";

        Assert.Equal(expectedNamespace, parameters.Namespace);
    }

    [Fact]
    public void Url_Works()
    {
        NamespaceHintCacheWarmParams parameters = new() { Namespace = "namespace" };

        var url = parameters.Url(new() { Region = "gcp-us-central1", ApiKey = "tpuf_A1..." });

        Assert.True(
            TestBase.UrisEqual(
                new Uri(
                    "https://gcp-us-central1.turbopuffer.com/v1/namespaces/namespace/hint_cache_warm"
                ),
                url
            )
        );
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var parameters = new NamespaceHintCacheWarmParams { Namespace = "namespace" };

        NamespaceHintCacheWarmParams copied = new(parameters);

        Assert.Equal(parameters, copied);
    }
}
