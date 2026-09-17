using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class NamespaceStartCopyFromResponseTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new NamespaceStartCopyFromResponse { Token = "token" };

        string expectedToken = "token";

        Assert.Equal(expectedToken, model.Token);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new NamespaceStartCopyFromResponse { Token = "token" };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<NamespaceStartCopyFromResponse>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new NamespaceStartCopyFromResponse { Token = "token" };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<NamespaceStartCopyFromResponse>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        string expectedToken = "token";

        Assert.Equal(expectedToken, deserialized.Token);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new NamespaceStartCopyFromResponse { Token = "token" };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new NamespaceStartCopyFromResponse { Token = "token" };

        NamespaceStartCopyFromResponse copied = new(model);

        Assert.Equal(model, copied);
    }
}
