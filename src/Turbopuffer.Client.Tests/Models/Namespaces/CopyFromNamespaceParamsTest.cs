using System.Text.Json;
using Turbopuffer.Client.Core;
using Turbopuffer.Client.Models.Namespaces;

namespace Turbopuffer.Client.Tests.Models.Namespaces;

public class CopyFromNamespaceParamsTest : TestBase
{
    [Fact]
    public void StringValidationWorks()
    {
        CopyFromNamespaceParams value = "string";
        value.Validate();
    }

    [Fact]
    public void ConfigValidationWorks()
    {
        CopyFromNamespaceParams value = new CopyFromNamespaceConfig()
        {
            SourceNamespace = "source_namespace",
            SourceApiKey = "source_api_key",
            SourceRegion = "source_region",
        };
        value.Validate();
    }

    [Fact]
    public void StringSerializationRoundtripWorks()
    {
        CopyFromNamespaceParams value = "string";
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<CopyFromNamespaceParams>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void ConfigSerializationRoundtripWorks()
    {
        CopyFromNamespaceParams value = new CopyFromNamespaceConfig()
        {
            SourceNamespace = "source_namespace",
            SourceApiKey = "source_api_key",
            SourceRegion = "source_region",
        };
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<CopyFromNamespaceParams>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}

public class CopyFromNamespaceConfigTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new CopyFromNamespaceConfig
        {
            SourceNamespace = "source_namespace",
            SourceApiKey = "source_api_key",
            SourceRegion = "source_region",
        };

        string expectedSourceNamespace = "source_namespace";
        string expectedSourceApiKey = "source_api_key";
        string expectedSourceRegion = "source_region";

        Assert.Equal(expectedSourceNamespace, model.SourceNamespace);
        Assert.Equal(expectedSourceApiKey, model.SourceApiKey);
        Assert.Equal(expectedSourceRegion, model.SourceRegion);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new CopyFromNamespaceConfig
        {
            SourceNamespace = "source_namespace",
            SourceApiKey = "source_api_key",
            SourceRegion = "source_region",
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<CopyFromNamespaceConfig>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new CopyFromNamespaceConfig
        {
            SourceNamespace = "source_namespace",
            SourceApiKey = "source_api_key",
            SourceRegion = "source_region",
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<CopyFromNamespaceConfig>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        string expectedSourceNamespace = "source_namespace";
        string expectedSourceApiKey = "source_api_key";
        string expectedSourceRegion = "source_region";

        Assert.Equal(expectedSourceNamespace, deserialized.SourceNamespace);
        Assert.Equal(expectedSourceApiKey, deserialized.SourceApiKey);
        Assert.Equal(expectedSourceRegion, deserialized.SourceRegion);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new CopyFromNamespaceConfig
        {
            SourceNamespace = "source_namespace",
            SourceApiKey = "source_api_key",
            SourceRegion = "source_region",
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new CopyFromNamespaceConfig { SourceNamespace = "source_namespace" };

        Assert.Null(model.SourceApiKey);
        Assert.False(model.RawData.ContainsKey("source_api_key"));
        Assert.Null(model.SourceRegion);
        Assert.False(model.RawData.ContainsKey("source_region"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new CopyFromNamespaceConfig { SourceNamespace = "source_namespace" };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new CopyFromNamespaceConfig
        {
            SourceNamespace = "source_namespace",

            // Null should be interpreted as omitted for these properties
            SourceApiKey = null,
            SourceRegion = null,
        };

        Assert.Null(model.SourceApiKey);
        Assert.False(model.RawData.ContainsKey("source_api_key"));
        Assert.Null(model.SourceRegion);
        Assert.False(model.RawData.ContainsKey("source_region"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new CopyFromNamespaceConfig
        {
            SourceNamespace = "source_namespace",

            // Null should be interpreted as omitted for these properties
            SourceApiKey = null,
            SourceRegion = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new CopyFromNamespaceConfig
        {
            SourceNamespace = "source_namespace",
            SourceApiKey = "source_api_key",
            SourceRegion = "source_region",
        };

        CopyFromNamespaceConfig copied = new(model);

        Assert.Equal(model, copied);
    }
}
