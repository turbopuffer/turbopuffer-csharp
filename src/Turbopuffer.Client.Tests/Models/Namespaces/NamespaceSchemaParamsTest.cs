using System;
using Turbopuffer.Client.Models.Namespaces;

namespace Turbopuffer.Client.Tests.Models.Namespaces;

public class NamespaceSchemaParamsTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var parameters = new NamespaceSchemaParams { Namespace = "namespace" };

        string expectedNamespace = "namespace";

        Assert.Equal(expectedNamespace, parameters.Namespace);
    }

    [Fact]
    public void Url_Works()
    {
        NamespaceSchemaParams parameters = new() { Namespace = "namespace" };

        var url = parameters.Url(new() { Region = "gcp-us-central1", ApiKey = "tpuf_A1..." });

        Assert.True(
            TestBase.UrisEqual(
                new Uri("https://gcp-us-central1.turbopuffer.com/v1/namespaces/namespace/schema"),
                url
            )
        );
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var parameters = new NamespaceSchemaParams { Namespace = "namespace" };

        NamespaceSchemaParams copied = new(parameters);

        Assert.Equal(parameters, copied);
    }
}
