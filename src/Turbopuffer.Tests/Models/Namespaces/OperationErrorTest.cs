using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class OperationErrorTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new OperationError { Detail = new("error"), StatusCode = 0 };

        Detail expectedDetail = new("error");
        long expectedStatusCode = 0;

        Assert.Equal(expectedDetail, model.Detail);
        Assert.Equal(expectedStatusCode, model.StatusCode);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new OperationError { Detail = new("error"), StatusCode = 0 };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<OperationError>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new OperationError { Detail = new("error"), StatusCode = 0 };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<OperationError>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        Detail expectedDetail = new("error");
        long expectedStatusCode = 0;

        Assert.Equal(expectedDetail, deserialized.Detail);
        Assert.Equal(expectedStatusCode, deserialized.StatusCode);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new OperationError { Detail = new("error"), StatusCode = 0 };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new OperationError { Detail = new("error"), StatusCode = 0 };

        OperationError copied = new(model);

        Assert.Equal(model, copied);
    }
}

public class DetailTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new Detail { Error = "error" };

        string expectedError = "error";
        JsonElement expectedStatus = JsonSerializer.SerializeToElement("error");

        Assert.Equal(expectedError, model.Error);
        Assert.True(JsonElement.DeepEquals(expectedStatus, model.Status));
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new Detail { Error = "error" };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Detail>(json, ModelBase.SerializerOptions);

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new Detail { Error = "error" };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Detail>(element, ModelBase.SerializerOptions);
        Assert.NotNull(deserialized);

        string expectedError = "error";
        JsonElement expectedStatus = JsonSerializer.SerializeToElement("error");

        Assert.Equal(expectedError, deserialized.Error);
        Assert.True(JsonElement.DeepEquals(expectedStatus, deserialized.Status));
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new Detail { Error = "error" };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new Detail { Error = "error" };

        Detail copied = new(model);

        Assert.Equal(model, copied);
    }
}
