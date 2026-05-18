using System;
using Turbopuffer.Client.Models.Namespaces;

namespace Turbopuffer.Client.Tests.Models.Namespaces;

public class NamespaceCopyFromParamsTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var parameters = new NamespaceCopyFromParams
        {
            Namespace = "namespace",
            SourceNamespace = "source_namespace",
            SourceApiKey = "source_api_key",
            SourceRegion = "source_region",
        };

        string expectedNamespace = "namespace";
        string expectedSourceNamespace = "source_namespace";
        string expectedSourceApiKey = "source_api_key";
        string expectedSourceRegion = "source_region";

        Assert.Equal(expectedNamespace, parameters.Namespace);
        Assert.Equal(expectedSourceNamespace, parameters.SourceNamespace);
        Assert.Equal(expectedSourceApiKey, parameters.SourceApiKey);
        Assert.Equal(expectedSourceRegion, parameters.SourceRegion);
    }

    [Fact]
    public void OptionalNonNullableParamsUnsetAreNotSet_Works()
    {
        var parameters = new NamespaceCopyFromParams
        {
            Namespace = "namespace",
            SourceNamespace = "source_namespace",
        };

        Assert.Null(parameters.SourceApiKey);
        Assert.False(parameters.RawBodyData.ContainsKey("source_api_key"));
        Assert.Null(parameters.SourceRegion);
        Assert.False(parameters.RawBodyData.ContainsKey("source_region"));
    }

    [Fact]
    public void OptionalNonNullableParamsSetToNullAreNotSet_Works()
    {
        var parameters = new NamespaceCopyFromParams
        {
            Namespace = "namespace",
            SourceNamespace = "source_namespace",

            // Null should be interpreted as omitted for these properties
            SourceApiKey = null,
            SourceRegion = null,
        };

        Assert.Null(parameters.SourceApiKey);
        Assert.False(parameters.RawBodyData.ContainsKey("source_api_key"));
        Assert.Null(parameters.SourceRegion);
        Assert.False(parameters.RawBodyData.ContainsKey("source_region"));
    }

    [Fact]
    public void Url_Works()
    {
        NamespaceCopyFromParams parameters = new()
        {
            Namespace = "namespace",
            SourceNamespace = "source_namespace",
        };

        var url = parameters.Url(new() { Region = "gcp-us-central1", ApiKey = "tpuf_A1..." });

        Assert.True(
            TestBase.UrisEqual(
                new Uri(
                    "https://gcp-us-central1.turbopuffer.com/v2/namespaces/namespace?stainless_overload=copyFrom"
                ),
                url
            )
        );
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var parameters = new NamespaceCopyFromParams
        {
            Namespace = "namespace",
            SourceNamespace = "source_namespace",
            SourceApiKey = "source_api_key",
            SourceRegion = "source_region",
        };

        NamespaceCopyFromParams copied = new(parameters);

        Assert.Equal(parameters, copied);
    }
}
