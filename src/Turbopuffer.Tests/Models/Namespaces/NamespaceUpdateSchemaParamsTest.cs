using System;
using System.Collections.Generic;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class NamespaceUpdateSchemaParamsTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var parameters = new NamespaceUpdateSchemaParams
        {
            Namespace = "namespace",
            Schema = new Dictionary<string, AttributeSchema>() { { "foo", "string" } },
        };

        string expectedNamespace = "namespace";
        Dictionary<string, AttributeSchema> expectedSchema = new() { { "foo", "string" } };

        Assert.Equal(expectedNamespace, parameters.Namespace);
        Assert.NotNull(parameters.Schema);
        Assert.Equal(expectedSchema.Count, parameters.Schema.Count);
        foreach (var item in expectedSchema)
        {
            Assert.True(parameters.Schema.TryGetValue(item.Key, out var value));

            Assert.Equal(value, parameters.Schema[item.Key]);
        }
    }

    [Fact]
    public void Url_Works()
    {
        NamespaceUpdateSchemaParams parameters = new() { Namespace = "namespace" };

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
        var parameters = new NamespaceUpdateSchemaParams
        {
            Namespace = "namespace",
            Schema = new Dictionary<string, AttributeSchema>() { { "foo", "string" } },
        };

        NamespaceUpdateSchemaParams copied = new(parameters);

        Assert.Equal(parameters, copied);
    }
}
