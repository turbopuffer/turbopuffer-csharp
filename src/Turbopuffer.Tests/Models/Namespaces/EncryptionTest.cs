using System.Collections.Generic;
using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Exceptions;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class EncryptionTest : TestBase
{
    [Fact]
    public void CustomerManagedValidationWorks()
    {
        Encryption value = new CustomerManaged()
        {
            KeyName = "key_name",
            EncryptionContext = new Dictionary<string, string>() { { "foo", "string" } },
        };
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
        Encryption value = new CustomerManaged()
        {
            KeyName = "key_name",
            EncryptionContext = new Dictionary<string, string>() { { "foo", "string" } },
        };
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
        var model = new CustomerManaged
        {
            KeyName = "key_name",
            EncryptionContext = new Dictionary<string, string>() { { "foo", "string" } },
        };

        string expectedKeyName = "key_name";
        JsonElement expectedMode = JsonSerializer.SerializeToElement("customer-managed");
        Dictionary<string, string> expectedEncryptionContext = new() { { "foo", "string" } };

        Assert.Equal(expectedKeyName, model.KeyName);
        Assert.True(JsonElement.DeepEquals(expectedMode, model.Mode));
        Assert.NotNull(model.EncryptionContext);
        Assert.Equal(expectedEncryptionContext.Count, model.EncryptionContext.Count);
        foreach (var item in expectedEncryptionContext)
        {
            Assert.True(model.EncryptionContext.TryGetValue(item.Key, out var value));

            Assert.Equal(value, model.EncryptionContext[item.Key]);
        }
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new CustomerManaged
        {
            KeyName = "key_name",
            EncryptionContext = new Dictionary<string, string>() { { "foo", "string" } },
        };

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
        var model = new CustomerManaged
        {
            KeyName = "key_name",
            EncryptionContext = new Dictionary<string, string>() { { "foo", "string" } },
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<CustomerManaged>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        string expectedKeyName = "key_name";
        JsonElement expectedMode = JsonSerializer.SerializeToElement("customer-managed");
        Dictionary<string, string> expectedEncryptionContext = new() { { "foo", "string" } };

        Assert.Equal(expectedKeyName, deserialized.KeyName);
        Assert.True(JsonElement.DeepEquals(expectedMode, deserialized.Mode));
        Assert.NotNull(deserialized.EncryptionContext);
        Assert.Equal(expectedEncryptionContext.Count, deserialized.EncryptionContext.Count);
        foreach (var item in expectedEncryptionContext)
        {
            Assert.True(deserialized.EncryptionContext.TryGetValue(item.Key, out var value));

            Assert.Equal(value, deserialized.EncryptionContext[item.Key]);
        }
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new CustomerManaged
        {
            KeyName = "key_name",
            EncryptionContext = new Dictionary<string, string>() { { "foo", "string" } },
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new CustomerManaged { KeyName = "key_name" };

        Assert.Null(model.EncryptionContext);
        Assert.False(model.RawData.ContainsKey("encryption_context"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new CustomerManaged { KeyName = "key_name" };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new CustomerManaged
        {
            KeyName = "key_name",

            // Null should be interpreted as omitted for these properties
            EncryptionContext = null,
        };

        Assert.Null(model.EncryptionContext);
        Assert.False(model.RawData.ContainsKey("encryption_context"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new CustomerManaged
        {
            KeyName = "key_name",

            // Null should be interpreted as omitted for these properties
            EncryptionContext = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new CustomerManaged
        {
            KeyName = "key_name",
            EncryptionContext = new Dictionary<string, string>() { { "foo", "string" } },
        };

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
