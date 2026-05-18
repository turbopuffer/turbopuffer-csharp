using System.Text.Json;
using Turbopuffer.Client.Core;
using Turbopuffer.Client.Exceptions;
using Turbopuffer.Client.Models.Namespaces;

namespace Turbopuffer.Client.Tests.Models.Namespaces;

public class EncryptionTest : TestBase
{
    [Fact]
    public void CustomerManagedValidationWorks()
    {
        Encryption value = new CustomerManaged("key_name");
        value.Validate();
    }

    [Fact]
    public void DefaultValidationWorks()
    {
        Encryption value = new Default();
        value.Validate();
    }

    [Fact]
    public void CustomerManagedSerializationRoundtripWorks()
    {
        Encryption value = new CustomerManaged("key_name");
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Encryption>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void DefaultSerializationRoundtripWorks()
    {
        Encryption value = new Default();
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Encryption>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}

public class CustomerManagedTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new CustomerManaged { KeyName = "key_name" };

        string expectedKeyName = "key_name";
        JsonElement expectedMode = JsonSerializer.SerializeToElement("customer-managed");

        Assert.Equal(expectedKeyName, model.KeyName);
        Assert.True(JsonElement.DeepEquals(expectedMode, model.Mode));
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new CustomerManaged { KeyName = "key_name" };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<CustomerManaged>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new CustomerManaged { KeyName = "key_name" };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<CustomerManaged>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        string expectedKeyName = "key_name";
        JsonElement expectedMode = JsonSerializer.SerializeToElement("customer-managed");

        Assert.Equal(expectedKeyName, deserialized.KeyName);
        Assert.True(JsonElement.DeepEquals(expectedMode, deserialized.Mode));
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new CustomerManaged { KeyName = "key_name" };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new CustomerManaged { KeyName = "key_name" };

        CustomerManaged copied = new(model);

        Assert.Equal(model, copied);
    }
}

public class DefaultTest : TestBase
{
    [Fact]
    public void DefaultValidation_Works()
    {
        var constant = new Default();
        constant.Validate();
    }

    [Fact]
    public void ValidConstantValidation_Works()
    {
        var constant = JsonSerializer.Deserialize<Default>(
            JsonSerializer.Deserialize<JsonElement>(
                """
                {
                  "mode": "default"
                }
                """
            ),
            ModelBase.SerializerOptions
        );

        Assert.NotNull(constant);
        constant.Validate();
    }

    [Fact]
    public void InvalidConstantValidationThrows_Works()
    {
        var constant = JsonSerializer.Deserialize<Default>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );

        Assert.NotNull(constant);
        Assert.Throws<TurbopufferInvalidDataException>(() => constant.Validate());
    }

    [Fact]
    public void DefaultRoundtrip_Works()
    {
        var constant = new Default();
        string element = JsonSerializer.Serialize(constant, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Default>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(constant, deserialized);
    }

    [Fact]
    public void ValidConstantRoundtrip_Works()
    {
        var constant = JsonSerializer.Deserialize<Default>(
            JsonSerializer.Deserialize<JsonElement>(
                """
                {
                  "mode": "default"
                }
                """
            ),
            ModelBase.SerializerOptions
        );
        string element = JsonSerializer.Serialize(constant, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Default>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(constant, deserialized);
    }

    [Fact]
    public void InvalidConstantRoundtrip_Works()
    {
        var constant = JsonSerializer.Deserialize<Default>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );
        string element = JsonSerializer.Serialize(constant, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Default>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(constant, deserialized);
    }
}
