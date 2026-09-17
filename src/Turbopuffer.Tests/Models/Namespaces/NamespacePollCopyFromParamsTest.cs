using System;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class NamespacePollCopyFromParamsTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var parameters = new NamespacePollCopyFromParams
        {
            Namespace = "namespace",
            Token = "token",
        };

        string expectedNamespace = "namespace";
        string expectedToken = "token";

        Assert.Equal(expectedNamespace, parameters.Namespace);
        Assert.Equal(expectedToken, parameters.Token);
    }

    [Fact]
    public void Url_Works()
    {
        NamespacePollCopyFromParams parameters = new() { Namespace = "namespace", Token = "token" };

        var url = parameters.Url(new() { Region = "gcp-us-central1", ApiKey = "tpuf_A1..." });

        Assert.True(
            TestBase.UrisEqual(
                new Uri(
                    "https://gcp-us-central1.turbopuffer.com/v1/namespaces/namespace/operations/token?stainless_overload=pollCopyFrom"
                ),
                url
            )
        );
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var parameters = new NamespacePollCopyFromParams
        {
            Namespace = "namespace",
            Token = "token",
        };

        NamespacePollCopyFromParams copied = new(parameters);

        Assert.Equal(parameters, copied);
    }
}
