using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class IncludeAttributesTest : TestBase
{
    [Fact]
    public void BoolValidationWorks()
    {
        IncludeAttributes value = true;
        value.Validate();
    }

    [Fact]
    public void StringsValidationWorks()
    {
        IncludeAttributes value = new(["string"]);
        value.Validate();
    }

    [Fact]
    public void BoolSerializationRoundtripWorks()
    {
        IncludeAttributes value = true;
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<IncludeAttributes>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void StringsSerializationRoundtripWorks()
    {
        IncludeAttributes value = new(["string"]);
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<IncludeAttributes>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}
