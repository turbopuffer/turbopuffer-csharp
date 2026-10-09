using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Exceptions;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class ReadOnlyOptimizeResponseTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new ReadOnlyOptimizeResponse
        {
            Status = ReadOnlyOptimizeResponseStatus.Ok,
            Billing = new()
            {
                BillableLogicalBytesWritten = 0,
                Query = new() { BillableLogicalBytesQueried = 0, BillableLogicalBytesReturned = 0 },
            },
            Message = "message",
        };

        ApiEnum<string, ReadOnlyOptimizeResponseStatus> expectedStatus =
            ReadOnlyOptimizeResponseStatus.Ok;
        WriteBilling expectedBilling = new()
        {
            BillableLogicalBytesWritten = 0,
            Query = new() { BillableLogicalBytesQueried = 0, BillableLogicalBytesReturned = 0 },
        };
        string expectedMessage = "message";

        Assert.Equal(expectedStatus, model.Status);
        Assert.Equal(expectedBilling, model.Billing);
        Assert.Equal(expectedMessage, model.Message);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new ReadOnlyOptimizeResponse
        {
            Status = ReadOnlyOptimizeResponseStatus.Ok,
            Billing = new()
            {
                BillableLogicalBytesWritten = 0,
                Query = new() { BillableLogicalBytesQueried = 0, BillableLogicalBytesReturned = 0 },
            },
            Message = "message",
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ReadOnlyOptimizeResponse>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new ReadOnlyOptimizeResponse
        {
            Status = ReadOnlyOptimizeResponseStatus.Ok,
            Billing = new()
            {
                BillableLogicalBytesWritten = 0,
                Query = new() { BillableLogicalBytesQueried = 0, BillableLogicalBytesReturned = 0 },
            },
            Message = "message",
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ReadOnlyOptimizeResponse>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        ApiEnum<string, ReadOnlyOptimizeResponseStatus> expectedStatus =
            ReadOnlyOptimizeResponseStatus.Ok;
        WriteBilling expectedBilling = new()
        {
            BillableLogicalBytesWritten = 0,
            Query = new() { BillableLogicalBytesQueried = 0, BillableLogicalBytesReturned = 0 },
        };
        string expectedMessage = "message";

        Assert.Equal(expectedStatus, deserialized.Status);
        Assert.Equal(expectedBilling, deserialized.Billing);
        Assert.Equal(expectedMessage, deserialized.Message);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new ReadOnlyOptimizeResponse
        {
            Status = ReadOnlyOptimizeResponseStatus.Ok,
            Billing = new()
            {
                BillableLogicalBytesWritten = 0,
                Query = new() { BillableLogicalBytesQueried = 0, BillableLogicalBytesReturned = 0 },
            },
            Message = "message",
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new ReadOnlyOptimizeResponse { Status = ReadOnlyOptimizeResponseStatus.Ok };

        Assert.Null(model.Billing);
        Assert.False(model.RawData.ContainsKey("billing"));
        Assert.Null(model.Message);
        Assert.False(model.RawData.ContainsKey("message"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new ReadOnlyOptimizeResponse { Status = ReadOnlyOptimizeResponseStatus.Ok };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new ReadOnlyOptimizeResponse
        {
            Status = ReadOnlyOptimizeResponseStatus.Ok,

            // Null should be interpreted as omitted for these properties
            Billing = null,
            Message = null,
        };

        Assert.Null(model.Billing);
        Assert.False(model.RawData.ContainsKey("billing"));
        Assert.Null(model.Message);
        Assert.False(model.RawData.ContainsKey("message"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new ReadOnlyOptimizeResponse
        {
            Status = ReadOnlyOptimizeResponseStatus.Ok,

            // Null should be interpreted as omitted for these properties
            Billing = null,
            Message = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new ReadOnlyOptimizeResponse
        {
            Status = ReadOnlyOptimizeResponseStatus.Ok,
            Billing = new()
            {
                BillableLogicalBytesWritten = 0,
                Query = new() { BillableLogicalBytesQueried = 0, BillableLogicalBytesReturned = 0 },
            },
            Message = "message",
        };

        ReadOnlyOptimizeResponse copied = new(model);

        Assert.Equal(model, copied);
    }
}

public class ReadOnlyOptimizeResponseStatusTest : TestBase
{
    [Theory]
    [InlineData(ReadOnlyOptimizeResponseStatus.Ok)]
    [InlineData(ReadOnlyOptimizeResponseStatus.Accepted)]
    public void Validation_Works(ReadOnlyOptimizeResponseStatus rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<string, ReadOnlyOptimizeResponseStatus> value = rawValue;
        value.Validate();
    }

    [Fact]
    public void InvalidEnumValidationThrows_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<string, ReadOnlyOptimizeResponseStatus>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );

        Assert.NotNull(value);
        Assert.Throws<TurbopufferInvalidDataException>(() => value.Validate());
    }

    [Theory]
    [InlineData(ReadOnlyOptimizeResponseStatus.Ok)]
    [InlineData(ReadOnlyOptimizeResponseStatus.Accepted)]
    public void SerializationRoundtrip_Works(ReadOnlyOptimizeResponseStatus rawValue)
    {
        // force implicit conversion because Theory can't do that for us
        ApiEnum<string, ReadOnlyOptimizeResponseStatus> value = rawValue;

        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<
            ApiEnum<string, ReadOnlyOptimizeResponseStatus>
        >(json, ModelBase.SerializerOptions);

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void InvalidEnumSerializationRoundtrip_Works()
    {
        var value = JsonSerializer.Deserialize<ApiEnum<string, ReadOnlyOptimizeResponseStatus>>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );
        string json = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<
            ApiEnum<string, ReadOnlyOptimizeResponseStatus>
        >(json, ModelBase.SerializerOptions);

        Assert.Equal(value, deserialized);
    }
}
