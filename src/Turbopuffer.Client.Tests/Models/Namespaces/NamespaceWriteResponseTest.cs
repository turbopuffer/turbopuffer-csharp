using System.Collections.Generic;
using System.Text.Json;
using Turbopuffer.Client.Core;
using Turbopuffer.Client.Models.Namespaces;

namespace Turbopuffer.Client.Tests.Models.Namespaces;

public class NamespaceWriteResponseTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new NamespaceWriteResponse
        {
            Billing = new()
            {
                BillableLogicalBytesWritten = 0,
                Query = new() { BillableLogicalBytesQueried = 0, BillableLogicalBytesReturned = 0 },
            },
            Message = "message",
            RowsAffected = 0,
            DeletedIds = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],
            PatchedIds = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],
            Performance = new(0),
            RowsDeleted = 0,
            RowsPatched = 0,
            RowsRemaining = true,
            RowsUpserted = 0,
            UpsertedIds = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],
        };

        WriteBilling expectedBilling = new()
        {
            BillableLogicalBytesWritten = 0,
            Query = new() { BillableLogicalBytesQueried = 0, BillableLogicalBytesReturned = 0 },
        };
        string expectedMessage = "message";
        long expectedRowsAffected = 0;
        JsonElement expectedStatus = JsonSerializer.SerializeToElement("OK");
        List<ID> expectedDeletedIds = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"];
        List<ID> expectedPatchedIds = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"];
        WritePerformance expectedPerformance = new(0);
        long expectedRowsDeleted = 0;
        long expectedRowsPatched = 0;
        bool expectedRowsRemaining = true;
        long expectedRowsUpserted = 0;
        List<ID> expectedUpsertedIds = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"];

        Assert.Equal(expectedBilling, model.Billing);
        Assert.Equal(expectedMessage, model.Message);
        Assert.Equal(expectedRowsAffected, model.RowsAffected);
        Assert.True(JsonElement.DeepEquals(expectedStatus, model.Status));
        Assert.NotNull(model.DeletedIds);
        Assert.Equal(expectedDeletedIds.Count, model.DeletedIds.Count);
        for (int i = 0; i < expectedDeletedIds.Count; i++)
        {
            Assert.Equal(expectedDeletedIds[i], model.DeletedIds[i]);
        }
        Assert.NotNull(model.PatchedIds);
        Assert.Equal(expectedPatchedIds.Count, model.PatchedIds.Count);
        for (int i = 0; i < expectedPatchedIds.Count; i++)
        {
            Assert.Equal(expectedPatchedIds[i], model.PatchedIds[i]);
        }
        Assert.Equal(expectedPerformance, model.Performance);
        Assert.Equal(expectedRowsDeleted, model.RowsDeleted);
        Assert.Equal(expectedRowsPatched, model.RowsPatched);
        Assert.Equal(expectedRowsRemaining, model.RowsRemaining);
        Assert.Equal(expectedRowsUpserted, model.RowsUpserted);
        Assert.NotNull(model.UpsertedIds);
        Assert.Equal(expectedUpsertedIds.Count, model.UpsertedIds.Count);
        for (int i = 0; i < expectedUpsertedIds.Count; i++)
        {
            Assert.Equal(expectedUpsertedIds[i], model.UpsertedIds[i]);
        }
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new NamespaceWriteResponse
        {
            Billing = new()
            {
                BillableLogicalBytesWritten = 0,
                Query = new() { BillableLogicalBytesQueried = 0, BillableLogicalBytesReturned = 0 },
            },
            Message = "message",
            RowsAffected = 0,
            DeletedIds = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],
            PatchedIds = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],
            Performance = new(0),
            RowsDeleted = 0,
            RowsPatched = 0,
            RowsRemaining = true,
            RowsUpserted = 0,
            UpsertedIds = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<NamespaceWriteResponse>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new NamespaceWriteResponse
        {
            Billing = new()
            {
                BillableLogicalBytesWritten = 0,
                Query = new() { BillableLogicalBytesQueried = 0, BillableLogicalBytesReturned = 0 },
            },
            Message = "message",
            RowsAffected = 0,
            DeletedIds = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],
            PatchedIds = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],
            Performance = new(0),
            RowsDeleted = 0,
            RowsPatched = 0,
            RowsRemaining = true,
            RowsUpserted = 0,
            UpsertedIds = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<NamespaceWriteResponse>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        WriteBilling expectedBilling = new()
        {
            BillableLogicalBytesWritten = 0,
            Query = new() { BillableLogicalBytesQueried = 0, BillableLogicalBytesReturned = 0 },
        };
        string expectedMessage = "message";
        long expectedRowsAffected = 0;
        JsonElement expectedStatus = JsonSerializer.SerializeToElement("OK");
        List<ID> expectedDeletedIds = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"];
        List<ID> expectedPatchedIds = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"];
        WritePerformance expectedPerformance = new(0);
        long expectedRowsDeleted = 0;
        long expectedRowsPatched = 0;
        bool expectedRowsRemaining = true;
        long expectedRowsUpserted = 0;
        List<ID> expectedUpsertedIds = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"];

        Assert.Equal(expectedBilling, deserialized.Billing);
        Assert.Equal(expectedMessage, deserialized.Message);
        Assert.Equal(expectedRowsAffected, deserialized.RowsAffected);
        Assert.True(JsonElement.DeepEquals(expectedStatus, deserialized.Status));
        Assert.NotNull(deserialized.DeletedIds);
        Assert.Equal(expectedDeletedIds.Count, deserialized.DeletedIds.Count);
        for (int i = 0; i < expectedDeletedIds.Count; i++)
        {
            Assert.Equal(expectedDeletedIds[i], deserialized.DeletedIds[i]);
        }
        Assert.NotNull(deserialized.PatchedIds);
        Assert.Equal(expectedPatchedIds.Count, deserialized.PatchedIds.Count);
        for (int i = 0; i < expectedPatchedIds.Count; i++)
        {
            Assert.Equal(expectedPatchedIds[i], deserialized.PatchedIds[i]);
        }
        Assert.Equal(expectedPerformance, deserialized.Performance);
        Assert.Equal(expectedRowsDeleted, deserialized.RowsDeleted);
        Assert.Equal(expectedRowsPatched, deserialized.RowsPatched);
        Assert.Equal(expectedRowsRemaining, deserialized.RowsRemaining);
        Assert.Equal(expectedRowsUpserted, deserialized.RowsUpserted);
        Assert.NotNull(deserialized.UpsertedIds);
        Assert.Equal(expectedUpsertedIds.Count, deserialized.UpsertedIds.Count);
        for (int i = 0; i < expectedUpsertedIds.Count; i++)
        {
            Assert.Equal(expectedUpsertedIds[i], deserialized.UpsertedIds[i]);
        }
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new NamespaceWriteResponse
        {
            Billing = new()
            {
                BillableLogicalBytesWritten = 0,
                Query = new() { BillableLogicalBytesQueried = 0, BillableLogicalBytesReturned = 0 },
            },
            Message = "message",
            RowsAffected = 0,
            DeletedIds = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],
            PatchedIds = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],
            Performance = new(0),
            RowsDeleted = 0,
            RowsPatched = 0,
            RowsRemaining = true,
            RowsUpserted = 0,
            UpsertedIds = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new NamespaceWriteResponse
        {
            Billing = new()
            {
                BillableLogicalBytesWritten = 0,
                Query = new() { BillableLogicalBytesQueried = 0, BillableLogicalBytesReturned = 0 },
            },
            Message = "message",
            RowsAffected = 0,
        };

        Assert.Null(model.DeletedIds);
        Assert.False(model.RawData.ContainsKey("deleted_ids"));
        Assert.Null(model.PatchedIds);
        Assert.False(model.RawData.ContainsKey("patched_ids"));
        Assert.Null(model.Performance);
        Assert.False(model.RawData.ContainsKey("performance"));
        Assert.Null(model.RowsDeleted);
        Assert.False(model.RawData.ContainsKey("rows_deleted"));
        Assert.Null(model.RowsPatched);
        Assert.False(model.RawData.ContainsKey("rows_patched"));
        Assert.Null(model.RowsRemaining);
        Assert.False(model.RawData.ContainsKey("rows_remaining"));
        Assert.Null(model.RowsUpserted);
        Assert.False(model.RawData.ContainsKey("rows_upserted"));
        Assert.Null(model.UpsertedIds);
        Assert.False(model.RawData.ContainsKey("upserted_ids"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new NamespaceWriteResponse
        {
            Billing = new()
            {
                BillableLogicalBytesWritten = 0,
                Query = new() { BillableLogicalBytesQueried = 0, BillableLogicalBytesReturned = 0 },
            },
            Message = "message",
            RowsAffected = 0,
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new NamespaceWriteResponse
        {
            Billing = new()
            {
                BillableLogicalBytesWritten = 0,
                Query = new() { BillableLogicalBytesQueried = 0, BillableLogicalBytesReturned = 0 },
            },
            Message = "message",
            RowsAffected = 0,

            // Null should be interpreted as omitted for these properties
            DeletedIds = null,
            PatchedIds = null,
            Performance = null,
            RowsDeleted = null,
            RowsPatched = null,
            RowsRemaining = null,
            RowsUpserted = null,
            UpsertedIds = null,
        };

        Assert.Null(model.DeletedIds);
        Assert.False(model.RawData.ContainsKey("deleted_ids"));
        Assert.Null(model.PatchedIds);
        Assert.False(model.RawData.ContainsKey("patched_ids"));
        Assert.Null(model.Performance);
        Assert.False(model.RawData.ContainsKey("performance"));
        Assert.Null(model.RowsDeleted);
        Assert.False(model.RawData.ContainsKey("rows_deleted"));
        Assert.Null(model.RowsPatched);
        Assert.False(model.RawData.ContainsKey("rows_patched"));
        Assert.Null(model.RowsRemaining);
        Assert.False(model.RawData.ContainsKey("rows_remaining"));
        Assert.Null(model.RowsUpserted);
        Assert.False(model.RawData.ContainsKey("rows_upserted"));
        Assert.Null(model.UpsertedIds);
        Assert.False(model.RawData.ContainsKey("upserted_ids"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new NamespaceWriteResponse
        {
            Billing = new()
            {
                BillableLogicalBytesWritten = 0,
                Query = new() { BillableLogicalBytesQueried = 0, BillableLogicalBytesReturned = 0 },
            },
            Message = "message",
            RowsAffected = 0,

            // Null should be interpreted as omitted for these properties
            DeletedIds = null,
            PatchedIds = null,
            Performance = null,
            RowsDeleted = null,
            RowsPatched = null,
            RowsRemaining = null,
            RowsUpserted = null,
            UpsertedIds = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new NamespaceWriteResponse
        {
            Billing = new()
            {
                BillableLogicalBytesWritten = 0,
                Query = new() { BillableLogicalBytesQueried = 0, BillableLogicalBytesReturned = 0 },
            },
            Message = "message",
            RowsAffected = 0,
            DeletedIds = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],
            PatchedIds = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],
            Performance = new(0),
            RowsDeleted = 0,
            RowsPatched = 0,
            RowsRemaining = true,
            RowsUpserted = 0,
            UpsertedIds = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],
        };

        NamespaceWriteResponse copied = new(model);

        Assert.Equal(model, copied);
    }
}
