using System;
using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class CopyFromNamespaceOperationTest : TestBase
{
    [Fact]
    public void RunningValidationWorks()
    {
        CopyFromNamespaceOperation value = new Running()
        {
            StartTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Progress = "progress",
        };
        value.Validate();
    }

    [Fact]
    public void FinishedValidationWorks()
    {
        CopyFromNamespaceOperation value = new Finished()
        {
            FinishTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Result = new Success(
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
            ),
            StartTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
        };
        value.Validate();
    }

    [Fact]
    public void RunningSerializationRoundtripWorks()
    {
        CopyFromNamespaceOperation value = new Running()
        {
            StartTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Progress = "progress",
        };
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<CopyFromNamespaceOperation>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void FinishedSerializationRoundtripWorks()
    {
        CopyFromNamespaceOperation value = new Finished()
        {
            FinishTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Result = new Success(
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
            ),
            StartTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
        };
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<CopyFromNamespaceOperation>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}

public class RunningTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new Running
        {
            StartTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Progress = "progress",
        };

        DateTimeOffset expectedStartTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z");
        JsonElement expectedStatus = JsonSerializer.SerializeToElement("running");
        string expectedProgress = "progress";

        Assert.Equal(expectedStartTime, model.StartTime);
        Assert.True(JsonElement.DeepEquals(expectedStatus, model.Status));
        Assert.Equal(expectedProgress, model.Progress);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new Running
        {
            StartTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Progress = "progress",
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Running>(json, ModelBase.SerializerOptions);

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new Running
        {
            StartTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Progress = "progress",
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Running>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        DateTimeOffset expectedStartTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z");
        JsonElement expectedStatus = JsonSerializer.SerializeToElement("running");
        string expectedProgress = "progress";

        Assert.Equal(expectedStartTime, deserialized.StartTime);
        Assert.True(JsonElement.DeepEquals(expectedStatus, deserialized.Status));
        Assert.Equal(expectedProgress, deserialized.Progress);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new Running
        {
            StartTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Progress = "progress",
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new Running { StartTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z") };

        Assert.Null(model.Progress);
        Assert.False(model.RawData.ContainsKey("progress"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new Running { StartTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z") };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new Running
        {
            StartTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),

            // Null should be interpreted as omitted for these properties
            Progress = null,
        };

        Assert.Null(model.Progress);
        Assert.False(model.RawData.ContainsKey("progress"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new Running
        {
            StartTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),

            // Null should be interpreted as omitted for these properties
            Progress = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new Running
        {
            StartTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Progress = "progress",
        };

        Running copied = new(model);

        Assert.Equal(model, copied);
    }
}

public class FinishedTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new Finished
        {
            FinishTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Result = new Success(
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
            ),
            StartTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
        };

        DateTimeOffset expectedFinishTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z");
        CopyFromNamespaceOperationResult expectedResult = new Success(
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
        DateTimeOffset expectedStartTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z");
        JsonElement expectedStatus = JsonSerializer.SerializeToElement("finished");

        Assert.Equal(expectedFinishTime, model.FinishTime);
        Assert.Equal(expectedResult, model.Result);
        Assert.Equal(expectedStartTime, model.StartTime);
        Assert.True(JsonElement.DeepEquals(expectedStatus, model.Status));
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new Finished
        {
            FinishTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Result = new Success(
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
            ),
            StartTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Finished>(json, ModelBase.SerializerOptions);

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new Finished
        {
            FinishTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Result = new Success(
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
            ),
            StartTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Finished>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        DateTimeOffset expectedFinishTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z");
        CopyFromNamespaceOperationResult expectedResult = new Success(
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
        DateTimeOffset expectedStartTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z");
        JsonElement expectedStatus = JsonSerializer.SerializeToElement("finished");

        Assert.Equal(expectedFinishTime, deserialized.FinishTime);
        Assert.Equal(expectedResult, deserialized.Result);
        Assert.Equal(expectedStartTime, deserialized.StartTime);
        Assert.True(JsonElement.DeepEquals(expectedStatus, deserialized.Status));
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new Finished
        {
            FinishTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Result = new Success(
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
            ),
            StartTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new Finished
        {
            FinishTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Result = new Success(
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
            ),
            StartTime = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
        };

        Finished copied = new(model);

        Assert.Equal(model, copied);
    }
}
