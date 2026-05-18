using System.Text.Json;
using Turbopuffer.Client.Core;
using Turbopuffer.Client.Models.Namespaces;

namespace Turbopuffer.Client.Tests.Models.Namespaces;

public class IDTest : TestBase
{
    [Fact]
    public void StringValidationWorks()
    {
        ID value = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e";
        value.Validate();
    }

    [Fact]
    public void LongValidationWorks()
    {
        ID value = 0;
        value.Validate();
    }

    [Fact]
    public void StringSerializationRoundtripWorks()
    {
        ID value = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e";
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ID>(element, ModelBase.SerializerOptions);

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void LongSerializationRoundtripWorks()
    {
        ID value = 0;
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ID>(element, ModelBase.SerializerOptions);

        Assert.Equal(value, deserialized);
    }
}
