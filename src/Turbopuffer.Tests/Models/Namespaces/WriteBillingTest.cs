using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class WriteBillingTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new WriteBilling
        {
            BillableLogicalBytesWritten = 0,
            Query = new() { BillableLogicalBytesQueried = 0, BillableLogicalBytesReturned = 0 },
        };

        long expectedBillableLogicalBytesWritten = 0;
        QueryBilling expectedQuery = new()
        {
            BillableLogicalBytesQueried = 0,
            BillableLogicalBytesReturned = 0,
        };

        Assert.Equal(expectedBillableLogicalBytesWritten, model.BillableLogicalBytesWritten);
        Assert.Equal(expectedQuery, model.Query);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new WriteBilling
        {
            BillableLogicalBytesWritten = 0,
            Query = new() { BillableLogicalBytesQueried = 0, BillableLogicalBytesReturned = 0 },
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<WriteBilling>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new WriteBilling
        {
            BillableLogicalBytesWritten = 0,
            Query = new() { BillableLogicalBytesQueried = 0, BillableLogicalBytesReturned = 0 },
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<WriteBilling>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        long expectedBillableLogicalBytesWritten = 0;
        QueryBilling expectedQuery = new()
        {
            BillableLogicalBytesQueried = 0,
            BillableLogicalBytesReturned = 0,
        };

        Assert.Equal(expectedBillableLogicalBytesWritten, deserialized.BillableLogicalBytesWritten);
        Assert.Equal(expectedQuery, deserialized.Query);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new WriteBilling
        {
            BillableLogicalBytesWritten = 0,
            Query = new() { BillableLogicalBytesQueried = 0, BillableLogicalBytesReturned = 0 },
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new WriteBilling { BillableLogicalBytesWritten = 0 };

        Assert.Null(model.Query);
        Assert.False(model.RawData.ContainsKey("query"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new WriteBilling { BillableLogicalBytesWritten = 0 };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new WriteBilling
        {
            BillableLogicalBytesWritten = 0,

            // Null should be interpreted as omitted for these properties
            Query = null,
        };

        Assert.Null(model.Query);
        Assert.False(model.RawData.ContainsKey("query"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new WriteBilling
        {
            BillableLogicalBytesWritten = 0,

            // Null should be interpreted as omitted for these properties
            Query = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new WriteBilling
        {
            BillableLogicalBytesWritten = 0,
            Query = new() { BillableLogicalBytesQueried = 0, BillableLogicalBytesReturned = 0 },
        };

        WriteBilling copied = new(model);

        Assert.Equal(model, copied);
    }
}
