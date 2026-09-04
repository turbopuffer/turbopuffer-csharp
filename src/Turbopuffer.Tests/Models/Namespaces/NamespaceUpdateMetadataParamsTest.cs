using System;
using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class NamespaceUpdateMetadataParamsTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var parameters = new NamespaceUpdateMetadataParams
        {
            Namespace = "namespace",
            Pinning = true,
            ReadOnly = true,
        };

        string expectedNamespace = "namespace";
        Pinning expectedPinning = true;
        bool expectedReadOnly = true;

        Assert.Equal(expectedNamespace, parameters.Namespace);
        Assert.Equal(expectedPinning, parameters.Pinning);
        Assert.Equal(expectedReadOnly, parameters.ReadOnly);
    }

    [Fact]
    public void OptionalNonNullableParamsUnsetAreNotSet_Works()
    {
        var parameters = new NamespaceUpdateMetadataParams
        {
            Namespace = "namespace",
            Pinning = true,
        };

        Assert.Null(parameters.ReadOnly);
        Assert.False(parameters.RawBodyData.ContainsKey("read_only"));
    }

    [Fact]
    public void OptionalNonNullableParamsSetToNullAreNotSet_Works()
    {
        var parameters = new NamespaceUpdateMetadataParams
        {
            Namespace = "namespace",
            Pinning = true,

            // Null should be interpreted as omitted for these properties
            ReadOnly = null,
        };

        Assert.Null(parameters.ReadOnly);
        Assert.False(parameters.RawBodyData.ContainsKey("read_only"));
    }

    [Fact]
    public void OptionalNullableParamsUnsetAreNotSet_Works()
    {
        var parameters = new NamespaceUpdateMetadataParams
        {
            Namespace = "namespace",
            ReadOnly = true,
        };

        Assert.Null(parameters.Pinning);
        Assert.False(parameters.RawBodyData.ContainsKey("pinning"));
    }

    [Fact]
    public void OptionalNullableParamsSetToNullAreSetToNull_Works()
    {
        var parameters = new NamespaceUpdateMetadataParams
        {
            Namespace = "namespace",
            ReadOnly = true,

            Pinning = null,
        };

        Assert.Null(parameters.Pinning);
        Assert.True(parameters.RawBodyData.ContainsKey("pinning"));
    }

    [Fact]
    public void Url_Works()
    {
        NamespaceUpdateMetadataParams parameters = new() { Namespace = "namespace" };

        var url = parameters.Url(new() { Region = "gcp-us-central1", ApiKey = "tpuf_A1..." });

        Assert.True(
            TestBase.UrisEqual(
                new Uri("https://gcp-us-central1.turbopuffer.com/v1/namespaces/namespace/metadata"),
                url
            )
        );
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var parameters = new NamespaceUpdateMetadataParams
        {
            Namespace = "namespace",
            Pinning = true,
            ReadOnly = true,
        };

        NamespaceUpdateMetadataParams copied = new(parameters);

        Assert.Equal(parameters, copied);
    }
}

public class PinningTest : TestBase
{
    [Fact]
    public void BoolValidationWorks()
    {
        Pinning value = true;
        value.Validate();
    }

    [Fact]
    public void ConfigValidationWorks()
    {
        Pinning value = new PinningConfig() { Replicas = 1 };
        value.Validate();
    }

    [Fact]
    public void BoolSerializationRoundtripWorks()
    {
        Pinning value = true;
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Pinning>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void ConfigSerializationRoundtripWorks()
    {
        Pinning value = new PinningConfig() { Replicas = 1 };
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Pinning>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}
