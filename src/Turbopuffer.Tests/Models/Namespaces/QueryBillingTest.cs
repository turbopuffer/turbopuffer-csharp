using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class QueryBillingTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new QueryBilling
        {
            BillableLogicalBytesQueried = 0,
            BillableLogicalBytesReturned = 0,
        };

        long expectedBillableLogicalBytesQueried = 0;
        long expectedBillableLogicalBytesReturned = 0;

        Assert.Equal(expectedBillableLogicalBytesQueried, model.BillableLogicalBytesQueried);
        Assert.Equal(expectedBillableLogicalBytesReturned, model.BillableLogicalBytesReturned);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new QueryBilling
        {
            BillableLogicalBytesQueried = 0,
            BillableLogicalBytesReturned = 0,
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<QueryBilling>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new QueryBilling
        {
            BillableLogicalBytesQueried = 0,
            BillableLogicalBytesReturned = 0,
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<QueryBilling>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        long expectedBillableLogicalBytesQueried = 0;
        long expectedBillableLogicalBytesReturned = 0;

        Assert.Equal(expectedBillableLogicalBytesQueried, deserialized.BillableLogicalBytesQueried);
        Assert.Equal(
            expectedBillableLogicalBytesReturned,
            deserialized.BillableLogicalBytesReturned
        );
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new QueryBilling
        {
            BillableLogicalBytesQueried = 0,
            BillableLogicalBytesReturned = 0,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new QueryBilling
        {
            BillableLogicalBytesQueried = 0,
            BillableLogicalBytesReturned = 0,
        };

        QueryBilling copied = new(model);

        Assert.Equal(model, copied);
    }
}
