using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class NamespaceVectorTest : TestBase
{
    [Fact]
    public void DoublesValidationWorks()
    {
        NamespaceVector value = new([0]);
        value.Validate();
    }

    [Fact]
    public void StringValidationWorks()
    {
        NamespaceVector value = "string";
        value.Validate();
    }

    [Fact]
    public void DoublesSerializationRoundtripWorks()
    {
        NamespaceVector value = new([0]);
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<NamespaceVector>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void StringSerializationRoundtripWorks()
    {
        NamespaceVector value = "string";
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<NamespaceVector>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}
