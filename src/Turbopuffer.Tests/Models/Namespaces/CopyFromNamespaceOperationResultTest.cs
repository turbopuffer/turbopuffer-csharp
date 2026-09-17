using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class CopyFromNamespaceOperationResultTest : TestBase
{
    [Fact]
    public void SuccessValidationWorks()
    {
        CopyFromNamespaceOperationResult value = new Success(
            new WriteResult()
            {
                Billing = new()
                {
                    BillableLogicalBytesWritten = 0,
                    Query = new()
                    {
                        BillableLogicalBytesQueried = 0,
                        BillableLogicalBytesReturned = 0,
                    },
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
            }
        );
        value.Validate();
    }

    [Fact]
    public void ErrorValidationWorks()
    {
        CopyFromNamespaceOperationResult value = new Error(
            new OperationError() { Detail = new("error"), StatusCode = 0 }
        );
        value.Validate();
    }

    [Fact]
    public void SuccessSerializationRoundtripWorks()
    {
        CopyFromNamespaceOperationResult value = new Success(
            new WriteResult()
            {
                Billing = new()
                {
                    BillableLogicalBytesWritten = 0,
                    Query = new()
                    {
                        BillableLogicalBytesQueried = 0,
                        BillableLogicalBytesReturned = 0,
                    },
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
            }
        );
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<CopyFromNamespaceOperationResult>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void ErrorSerializationRoundtripWorks()
    {
        CopyFromNamespaceOperationResult value = new Error(
            new OperationError() { Detail = new("error"), StatusCode = 0 }
        );
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<CopyFromNamespaceOperationResult>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}

public class SuccessTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new Success
        {
            SuccessValue = new()
            {
                Billing = new()
                {
                    BillableLogicalBytesWritten = 0,
                    Query = new()
                    {
                        BillableLogicalBytesQueried = 0,
                        BillableLogicalBytesReturned = 0,
                    },
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
            },
        };

        WriteResult expectedSuccessValue = new()
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

        Assert.Equal(expectedSuccessValue, model.SuccessValue);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new Success
        {
            SuccessValue = new()
            {
                Billing = new()
                {
                    BillableLogicalBytesWritten = 0,
                    Query = new()
                    {
                        BillableLogicalBytesQueried = 0,
                        BillableLogicalBytesReturned = 0,
                    },
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
            },
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Success>(json, ModelBase.SerializerOptions);

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new Success
        {
            SuccessValue = new()
            {
                Billing = new()
                {
                    BillableLogicalBytesWritten = 0,
                    Query = new()
                    {
                        BillableLogicalBytesQueried = 0,
                        BillableLogicalBytesReturned = 0,
                    },
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
            },
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Success>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        WriteResult expectedSuccessValue = new()
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

        Assert.Equal(expectedSuccessValue, deserialized.SuccessValue);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new Success
        {
            SuccessValue = new()
            {
                Billing = new()
                {
                    BillableLogicalBytesWritten = 0,
                    Query = new()
                    {
                        BillableLogicalBytesQueried = 0,
                        BillableLogicalBytesReturned = 0,
                    },
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
            },
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new Success
        {
            SuccessValue = new()
            {
                Billing = new()
                {
                    BillableLogicalBytesWritten = 0,
                    Query = new()
                    {
                        BillableLogicalBytesQueried = 0,
                        BillableLogicalBytesReturned = 0,
                    },
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
            },
        };

        Success copied = new(model);

        Assert.Equal(model, copied);
    }
}

public class ErrorTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new Error
        {
            ErrorValue = new() { Detail = new("error"), StatusCode = 0 },
        };

        OperationError expectedErrorValue = new() { Detail = new("error"), StatusCode = 0 };

        Assert.Equal(expectedErrorValue, model.ErrorValue);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new Error
        {
            ErrorValue = new() { Detail = new("error"), StatusCode = 0 },
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Error>(json, ModelBase.SerializerOptions);

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new Error
        {
            ErrorValue = new() { Detail = new("error"), StatusCode = 0 },
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Error>(element, ModelBase.SerializerOptions);
        Assert.NotNull(deserialized);

        OperationError expectedErrorValue = new() { Detail = new("error"), StatusCode = 0 };

        Assert.Equal(expectedErrorValue, deserialized.ErrorValue);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new Error
        {
            ErrorValue = new() { Detail = new("error"), StatusCode = 0 },
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new Error
        {
            ErrorValue = new() { Detail = new("error"), StatusCode = 0 },
        };

        Error copied = new(model);

        Assert.Equal(model, copied);
    }
}
