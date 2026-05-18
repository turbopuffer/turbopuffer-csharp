using System.Collections.Generic;
using System.Text.Json;
using Turbopuffer.Client.Core;
using Turbopuffer.Client.Models;

namespace Turbopuffer.Client.Tests.Models;

public class ClientNamespacesPageResponseTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new ClientNamespacesPageResponse
        {
            Namespaces = [new("id")],
            NextCursor = "next_cursor",
        };

        List<NamespaceSummary> expectedNamespaces = [new("id")];
        string expectedNextCursor = "next_cursor";

        Assert.NotNull(model.Namespaces);
        Assert.Equal(expectedNamespaces.Count, model.Namespaces.Count);
        for (int i = 0; i < expectedNamespaces.Count; i++)
        {
            Assert.Equal(expectedNamespaces[i], model.Namespaces[i]);
        }
        Assert.Equal(expectedNextCursor, model.NextCursor);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new ClientNamespacesPageResponse
        {
            Namespaces = [new("id")],
            NextCursor = "next_cursor",
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ClientNamespacesPageResponse>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new ClientNamespacesPageResponse
        {
            Namespaces = [new("id")],
            NextCursor = "next_cursor",
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ClientNamespacesPageResponse>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        List<NamespaceSummary> expectedNamespaces = [new("id")];
        string expectedNextCursor = "next_cursor";

        Assert.NotNull(deserialized.Namespaces);
        Assert.Equal(expectedNamespaces.Count, deserialized.Namespaces.Count);
        for (int i = 0; i < expectedNamespaces.Count; i++)
        {
            Assert.Equal(expectedNamespaces[i], deserialized.Namespaces[i]);
        }
        Assert.Equal(expectedNextCursor, deserialized.NextCursor);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new ClientNamespacesPageResponse
        {
            Namespaces = [new("id")],
            NextCursor = "next_cursor",
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new ClientNamespacesPageResponse { };

        Assert.Null(model.Namespaces);
        Assert.False(model.RawData.ContainsKey("namespaces"));
        Assert.Null(model.NextCursor);
        Assert.False(model.RawData.ContainsKey("next_cursor"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new ClientNamespacesPageResponse { };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new ClientNamespacesPageResponse
        {
            // Null should be interpreted as omitted for these properties
            Namespaces = null,
            NextCursor = null,
        };

        Assert.Null(model.Namespaces);
        Assert.False(model.RawData.ContainsKey("namespaces"));
        Assert.Null(model.NextCursor);
        Assert.False(model.RawData.ContainsKey("next_cursor"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new ClientNamespacesPageResponse
        {
            // Null should be interpreted as omitted for these properties
            Namespaces = null,
            NextCursor = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new ClientNamespacesPageResponse
        {
            Namespaces = [new("id")],
            NextCursor = "next_cursor",
        };

        ClientNamespacesPageResponse copied = new(model);

        Assert.Equal(model, copied);
    }
}
