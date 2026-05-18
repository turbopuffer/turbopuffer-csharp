using System.Text.Json;
using Turbopuffer.Client.Core;
using Turbopuffer.Client.Models.Namespaces;

namespace Turbopuffer.Client.Tests.Models.Namespaces;

public class BranchFromNamespaceParamsTest : TestBase
{
    [Fact]
    public void StringValidationWorks()
    {
        BranchFromNamespaceParams value = "string";
        value.Validate();
    }

    [Fact]
    public void ConfigValidationWorks()
    {
        BranchFromNamespaceParams value = new BranchFromNamespaceConfig("source_namespace");
        value.Validate();
    }

    [Fact]
    public void StringSerializationRoundtripWorks()
    {
        BranchFromNamespaceParams value = "string";
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<BranchFromNamespaceParams>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void ConfigSerializationRoundtripWorks()
    {
        BranchFromNamespaceParams value = new BranchFromNamespaceConfig("source_namespace");
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<BranchFromNamespaceParams>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}

public class BranchFromNamespaceConfigTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new BranchFromNamespaceConfig { SourceNamespace = "source_namespace" };

        string expectedSourceNamespace = "source_namespace";

        Assert.Equal(expectedSourceNamespace, model.SourceNamespace);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new BranchFromNamespaceConfig { SourceNamespace = "source_namespace" };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<BranchFromNamespaceConfig>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new BranchFromNamespaceConfig { SourceNamespace = "source_namespace" };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<BranchFromNamespaceConfig>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        string expectedSourceNamespace = "source_namespace";

        Assert.Equal(expectedSourceNamespace, deserialized.SourceNamespace);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new BranchFromNamespaceConfig { SourceNamespace = "source_namespace" };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new BranchFromNamespaceConfig { SourceNamespace = "source_namespace" };

        BranchFromNamespaceConfig copied = new(model);

        Assert.Equal(model, copied);
    }
}
