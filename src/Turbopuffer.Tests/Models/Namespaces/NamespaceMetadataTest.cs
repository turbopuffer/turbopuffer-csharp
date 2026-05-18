using System;
using System.Collections.Generic;
using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Exceptions;
using Namespaces = Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class NamespaceMetadataTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new Namespaces::NamespaceMetadata
        {
            ApproxLogicalBytes = 0,
            ApproxRowCount = 0,
            CreatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Encryption = new Namespaces::CustomerManaged("key_name"),
            Index = new Namespaces::IndexUpToDate(),
            Schema = new Dictionary<string, Namespaces::AttributeSchemaConfig>()
            {
                {
                    "foo",
                    new()
                    {
                        Type = "string",
                        Ann = true,
                        Filterable = true,
                        FullTextSearch = true,
                        Fuzzy = true,
                        Glob = true,
                        Regex = true,
                        SparseKnn = new(Namespaces::SparseDistanceMetric.DotProduct),
                    }
                },
            },
            UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Pinning = new()
            {
                Replicas = 1,
                Status = new()
                {
                    ReadyReplicas = 0,
                    UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
                    Utilization = 0,
                },
            },
        };

        long expectedApproxLogicalBytes = 0;
        long expectedApproxRowCount = 0;
        DateTimeOffset expectedCreatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z");
        Namespaces::Encryption expectedEncryption = new Namespaces::CustomerManaged("key_name");
        Namespaces::Index expectedIndex = new Namespaces::IndexUpToDate();
        Dictionary<string, Namespaces::AttributeSchemaConfig> expectedSchema = new()
        {
            {
                "foo",
                new()
                {
                    Type = "string",
                    Ann = true,
                    Filterable = true,
                    FullTextSearch = true,
                    Fuzzy = true,
                    Glob = true,
                    Regex = true,
                    SparseKnn = new(Namespaces::SparseDistanceMetric.DotProduct),
                }
            },
        };
        DateTimeOffset expectedUpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z");
        Namespaces::NamespaceMetadataPinning expectedPinning = new()
        {
            Replicas = 1,
            Status = new()
            {
                ReadyReplicas = 0,
                UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
                Utilization = 0,
            },
        };

        Assert.Equal(expectedApproxLogicalBytes, model.ApproxLogicalBytes);
        Assert.Equal(expectedApproxRowCount, model.ApproxRowCount);
        Assert.Equal(expectedCreatedAt, model.CreatedAt);
        Assert.Equal(expectedEncryption, model.Encryption);
        Assert.Equal(expectedIndex, model.Index);
        Assert.Equal(expectedSchema.Count, model.Schema.Count);
        foreach (var item in expectedSchema)
        {
            Assert.True(model.Schema.TryGetValue(item.Key, out var value));

            Assert.Equal(value, model.Schema[item.Key]);
        }
        Assert.Equal(expectedUpdatedAt, model.UpdatedAt);
        Assert.Equal(expectedPinning, model.Pinning);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new Namespaces::NamespaceMetadata
        {
            ApproxLogicalBytes = 0,
            ApproxRowCount = 0,
            CreatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Encryption = new Namespaces::CustomerManaged("key_name"),
            Index = new Namespaces::IndexUpToDate(),
            Schema = new Dictionary<string, Namespaces::AttributeSchemaConfig>()
            {
                {
                    "foo",
                    new()
                    {
                        Type = "string",
                        Ann = true,
                        Filterable = true,
                        FullTextSearch = true,
                        Fuzzy = true,
                        Glob = true,
                        Regex = true,
                        SparseKnn = new(Namespaces::SparseDistanceMetric.DotProduct),
                    }
                },
            },
            UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Pinning = new()
            {
                Replicas = 1,
                Status = new()
                {
                    ReadyReplicas = 0,
                    UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
                    Utilization = 0,
                },
            },
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Namespaces::NamespaceMetadata>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new Namespaces::NamespaceMetadata
        {
            ApproxLogicalBytes = 0,
            ApproxRowCount = 0,
            CreatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Encryption = new Namespaces::CustomerManaged("key_name"),
            Index = new Namespaces::IndexUpToDate(),
            Schema = new Dictionary<string, Namespaces::AttributeSchemaConfig>()
            {
                {
                    "foo",
                    new()
                    {
                        Type = "string",
                        Ann = true,
                        Filterable = true,
                        FullTextSearch = true,
                        Fuzzy = true,
                        Glob = true,
                        Regex = true,
                        SparseKnn = new(Namespaces::SparseDistanceMetric.DotProduct),
                    }
                },
            },
            UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Pinning = new()
            {
                Replicas = 1,
                Status = new()
                {
                    ReadyReplicas = 0,
                    UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
                    Utilization = 0,
                },
            },
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Namespaces::NamespaceMetadata>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        long expectedApproxLogicalBytes = 0;
        long expectedApproxRowCount = 0;
        DateTimeOffset expectedCreatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z");
        Namespaces::Encryption expectedEncryption = new Namespaces::CustomerManaged("key_name");
        Namespaces::Index expectedIndex = new Namespaces::IndexUpToDate();
        Dictionary<string, Namespaces::AttributeSchemaConfig> expectedSchema = new()
        {
            {
                "foo",
                new()
                {
                    Type = "string",
                    Ann = true,
                    Filterable = true,
                    FullTextSearch = true,
                    Fuzzy = true,
                    Glob = true,
                    Regex = true,
                    SparseKnn = new(Namespaces::SparseDistanceMetric.DotProduct),
                }
            },
        };
        DateTimeOffset expectedUpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z");
        Namespaces::NamespaceMetadataPinning expectedPinning = new()
        {
            Replicas = 1,
            Status = new()
            {
                ReadyReplicas = 0,
                UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
                Utilization = 0,
            },
        };

        Assert.Equal(expectedApproxLogicalBytes, deserialized.ApproxLogicalBytes);
        Assert.Equal(expectedApproxRowCount, deserialized.ApproxRowCount);
        Assert.Equal(expectedCreatedAt, deserialized.CreatedAt);
        Assert.Equal(expectedEncryption, deserialized.Encryption);
        Assert.Equal(expectedIndex, deserialized.Index);
        Assert.Equal(expectedSchema.Count, deserialized.Schema.Count);
        foreach (var item in expectedSchema)
        {
            Assert.True(deserialized.Schema.TryGetValue(item.Key, out var value));

            Assert.Equal(value, deserialized.Schema[item.Key]);
        }
        Assert.Equal(expectedUpdatedAt, deserialized.UpdatedAt);
        Assert.Equal(expectedPinning, deserialized.Pinning);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new Namespaces::NamespaceMetadata
        {
            ApproxLogicalBytes = 0,
            ApproxRowCount = 0,
            CreatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Encryption = new Namespaces::CustomerManaged("key_name"),
            Index = new Namespaces::IndexUpToDate(),
            Schema = new Dictionary<string, Namespaces::AttributeSchemaConfig>()
            {
                {
                    "foo",
                    new()
                    {
                        Type = "string",
                        Ann = true,
                        Filterable = true,
                        FullTextSearch = true,
                        Fuzzy = true,
                        Glob = true,
                        Regex = true,
                        SparseKnn = new(Namespaces::SparseDistanceMetric.DotProduct),
                    }
                },
            },
            UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Pinning = new()
            {
                Replicas = 1,
                Status = new()
                {
                    ReadyReplicas = 0,
                    UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
                    Utilization = 0,
                },
            },
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new Namespaces::NamespaceMetadata
        {
            ApproxLogicalBytes = 0,
            ApproxRowCount = 0,
            CreatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Encryption = new Namespaces::CustomerManaged("key_name"),
            Index = new Namespaces::IndexUpToDate(),
            Schema = new Dictionary<string, Namespaces::AttributeSchemaConfig>()
            {
                {
                    "foo",
                    new()
                    {
                        Type = "string",
                        Ann = true,
                        Filterable = true,
                        FullTextSearch = true,
                        Fuzzy = true,
                        Glob = true,
                        Regex = true,
                        SparseKnn = new(Namespaces::SparseDistanceMetric.DotProduct),
                    }
                },
            },
            UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
        };

        Assert.Null(model.Pinning);
        Assert.False(model.RawData.ContainsKey("pinning"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new Namespaces::NamespaceMetadata
        {
            ApproxLogicalBytes = 0,
            ApproxRowCount = 0,
            CreatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Encryption = new Namespaces::CustomerManaged("key_name"),
            Index = new Namespaces::IndexUpToDate(),
            Schema = new Dictionary<string, Namespaces::AttributeSchemaConfig>()
            {
                {
                    "foo",
                    new()
                    {
                        Type = "string",
                        Ann = true,
                        Filterable = true,
                        FullTextSearch = true,
                        Fuzzy = true,
                        Glob = true,
                        Regex = true,
                        SparseKnn = new(Namespaces::SparseDistanceMetric.DotProduct),
                    }
                },
            },
            UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new Namespaces::NamespaceMetadata
        {
            ApproxLogicalBytes = 0,
            ApproxRowCount = 0,
            CreatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Encryption = new Namespaces::CustomerManaged("key_name"),
            Index = new Namespaces::IndexUpToDate(),
            Schema = new Dictionary<string, Namespaces::AttributeSchemaConfig>()
            {
                {
                    "foo",
                    new()
                    {
                        Type = "string",
                        Ann = true,
                        Filterable = true,
                        FullTextSearch = true,
                        Fuzzy = true,
                        Glob = true,
                        Regex = true,
                        SparseKnn = new(Namespaces::SparseDistanceMetric.DotProduct),
                    }
                },
            },
            UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),

            // Null should be interpreted as omitted for these properties
            Pinning = null,
        };

        Assert.Null(model.Pinning);
        Assert.False(model.RawData.ContainsKey("pinning"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new Namespaces::NamespaceMetadata
        {
            ApproxLogicalBytes = 0,
            ApproxRowCount = 0,
            CreatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Encryption = new Namespaces::CustomerManaged("key_name"),
            Index = new Namespaces::IndexUpToDate(),
            Schema = new Dictionary<string, Namespaces::AttributeSchemaConfig>()
            {
                {
                    "foo",
                    new()
                    {
                        Type = "string",
                        Ann = true,
                        Filterable = true,
                        FullTextSearch = true,
                        Fuzzy = true,
                        Glob = true,
                        Regex = true,
                        SparseKnn = new(Namespaces::SparseDistanceMetric.DotProduct),
                    }
                },
            },
            UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),

            // Null should be interpreted as omitted for these properties
            Pinning = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new Namespaces::NamespaceMetadata
        {
            ApproxLogicalBytes = 0,
            ApproxRowCount = 0,
            CreatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Encryption = new Namespaces::CustomerManaged("key_name"),
            Index = new Namespaces::IndexUpToDate(),
            Schema = new Dictionary<string, Namespaces::AttributeSchemaConfig>()
            {
                {
                    "foo",
                    new()
                    {
                        Type = "string",
                        Ann = true,
                        Filterable = true,
                        FullTextSearch = true,
                        Fuzzy = true,
                        Glob = true,
                        Regex = true,
                        SparseKnn = new(Namespaces::SparseDistanceMetric.DotProduct),
                    }
                },
            },
            UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Pinning = new()
            {
                Replicas = 1,
                Status = new()
                {
                    ReadyReplicas = 0,
                    UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
                    Utilization = 0,
                },
            },
        };

        Namespaces::NamespaceMetadata copied = new(model);

        Assert.Equal(model, copied);
    }
}

public class IndexTest : TestBase
{
    [Fact]
    public void UpToDateValidationWorks()
    {
        Namespaces::Index value = new Namespaces::IndexUpToDate();
        value.Validate();
    }

    [Fact]
    public void UpdatingValidationWorks()
    {
        Namespaces::Index value = new Namespaces::IndexUpdating(0);
        value.Validate();
    }

    [Fact]
    public void UpToDateSerializationRoundtripWorks()
    {
        Namespaces::Index value = new Namespaces::IndexUpToDate();
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Namespaces::Index>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }

    [Fact]
    public void UpdatingSerializationRoundtripWorks()
    {
        Namespaces::Index value = new Namespaces::IndexUpdating(0);
        string element = JsonSerializer.Serialize(value, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Namespaces::Index>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(value, deserialized);
    }
}

public class IndexUpToDateTest : TestBase
{
    [Fact]
    public void DefaultValidation_Works()
    {
        var constant = new Namespaces::IndexUpToDate();
        constant.Validate();
    }

    [Fact]
    public void ValidConstantValidation_Works()
    {
        var constant = JsonSerializer.Deserialize<Namespaces::IndexUpToDate>(
            JsonSerializer.Deserialize<JsonElement>(
                """
                {
                  "status": "up-to-date"
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
        var constant = JsonSerializer.Deserialize<Namespaces::IndexUpToDate>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );

        Assert.NotNull(constant);
        Assert.Throws<TurbopufferInvalidDataException>(() => constant.Validate());
    }

    [Fact]
    public void DefaultRoundtrip_Works()
    {
        var constant = new Namespaces::IndexUpToDate();
        string element = JsonSerializer.Serialize(constant, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Namespaces::IndexUpToDate>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(constant, deserialized);
    }

    [Fact]
    public void ValidConstantRoundtrip_Works()
    {
        var constant = JsonSerializer.Deserialize<Namespaces::IndexUpToDate>(
            JsonSerializer.Deserialize<JsonElement>(
                """
                {
                  "status": "up-to-date"
                }
                """
            ),
            ModelBase.SerializerOptions
        );
        string element = JsonSerializer.Serialize(constant, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Namespaces::IndexUpToDate>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(constant, deserialized);
    }

    [Fact]
    public void InvalidConstantRoundtrip_Works()
    {
        var constant = JsonSerializer.Deserialize<Namespaces::IndexUpToDate>(
            JsonSerializer.SerializeToElement("invalid value"),
            ModelBase.SerializerOptions
        );
        string element = JsonSerializer.Serialize(constant, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Namespaces::IndexUpToDate>(
            element,
            ModelBase.SerializerOptions
        );

        Assert.Equal(constant, deserialized);
    }
}

public class IndexUpdatingTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new Namespaces::IndexUpdating { UnindexedBytes = 0 };

        JsonElement expectedStatus = JsonSerializer.SerializeToElement("updating");
        long expectedUnindexedBytes = 0;

        Assert.True(JsonElement.DeepEquals(expectedStatus, model.Status));
        Assert.Equal(expectedUnindexedBytes, model.UnindexedBytes);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new Namespaces::IndexUpdating { UnindexedBytes = 0 };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Namespaces::IndexUpdating>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new Namespaces::IndexUpdating { UnindexedBytes = 0 };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Namespaces::IndexUpdating>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        JsonElement expectedStatus = JsonSerializer.SerializeToElement("updating");
        long expectedUnindexedBytes = 0;

        Assert.True(JsonElement.DeepEquals(expectedStatus, deserialized.Status));
        Assert.Equal(expectedUnindexedBytes, deserialized.UnindexedBytes);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new Namespaces::IndexUpdating { UnindexedBytes = 0 };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new Namespaces::IndexUpdating { UnindexedBytes = 0 };

        Namespaces::IndexUpdating copied = new(model);

        Assert.Equal(model, copied);
    }
}

public class NamespaceMetadataPinningTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new Namespaces::NamespaceMetadataPinning
        {
            Replicas = 1,
            Status = new()
            {
                ReadyReplicas = 0,
                UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
                Utilization = 0,
            },
        };

        long expectedReplicas = 1;
        Namespaces::Status expectedStatus = new()
        {
            ReadyReplicas = 0,
            UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Utilization = 0,
        };

        Assert.Equal(expectedReplicas, model.Replicas);
        Assert.Equal(expectedStatus, model.Status);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new Namespaces::NamespaceMetadataPinning
        {
            Replicas = 1,
            Status = new()
            {
                ReadyReplicas = 0,
                UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
                Utilization = 0,
            },
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Namespaces::NamespaceMetadataPinning>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new Namespaces::NamespaceMetadataPinning
        {
            Replicas = 1,
            Status = new()
            {
                ReadyReplicas = 0,
                UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
                Utilization = 0,
            },
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Namespaces::NamespaceMetadataPinning>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        long expectedReplicas = 1;
        Namespaces::Status expectedStatus = new()
        {
            ReadyReplicas = 0,
            UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Utilization = 0,
        };

        Assert.Equal(expectedReplicas, deserialized.Replicas);
        Assert.Equal(expectedStatus, deserialized.Status);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new Namespaces::NamespaceMetadataPinning
        {
            Replicas = 1,
            Status = new()
            {
                ReadyReplicas = 0,
                UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
                Utilization = 0,
            },
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new Namespaces::NamespaceMetadataPinning { };

        Assert.Null(model.Replicas);
        Assert.False(model.RawData.ContainsKey("replicas"));
        Assert.Null(model.Status);
        Assert.False(model.RawData.ContainsKey("status"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new Namespaces::NamespaceMetadataPinning { };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new Namespaces::NamespaceMetadataPinning
        {
            // Null should be interpreted as omitted for these properties
            Replicas = null,
            Status = null,
        };

        Assert.Null(model.Replicas);
        Assert.False(model.RawData.ContainsKey("replicas"));
        Assert.Null(model.Status);
        Assert.False(model.RawData.ContainsKey("status"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new Namespaces::NamespaceMetadataPinning
        {
            // Null should be interpreted as omitted for these properties
            Replicas = null,
            Status = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new Namespaces::NamespaceMetadataPinning
        {
            Replicas = 1,
            Status = new()
            {
                ReadyReplicas = 0,
                UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
                Utilization = 0,
            },
        };

        Namespaces::NamespaceMetadataPinning copied = new(model);

        Assert.Equal(model, copied);
    }
}

public class IntersectionMember1Test : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new Namespaces::IntersectionMember1
        {
            Status = new()
            {
                ReadyReplicas = 0,
                UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
                Utilization = 0,
            },
        };

        Namespaces::Status expectedStatus = new()
        {
            ReadyReplicas = 0,
            UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Utilization = 0,
        };

        Assert.Equal(expectedStatus, model.Status);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new Namespaces::IntersectionMember1
        {
            Status = new()
            {
                ReadyReplicas = 0,
                UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
                Utilization = 0,
            },
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Namespaces::IntersectionMember1>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new Namespaces::IntersectionMember1
        {
            Status = new()
            {
                ReadyReplicas = 0,
                UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
                Utilization = 0,
            },
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Namespaces::IntersectionMember1>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        Namespaces::Status expectedStatus = new()
        {
            ReadyReplicas = 0,
            UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Utilization = 0,
        };

        Assert.Equal(expectedStatus, deserialized.Status);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new Namespaces::IntersectionMember1
        {
            Status = new()
            {
                ReadyReplicas = 0,
                UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
                Utilization = 0,
            },
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new Namespaces::IntersectionMember1 { };

        Assert.Null(model.Status);
        Assert.False(model.RawData.ContainsKey("status"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new Namespaces::IntersectionMember1 { };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new Namespaces::IntersectionMember1
        {
            // Null should be interpreted as omitted for these properties
            Status = null,
        };

        Assert.Null(model.Status);
        Assert.False(model.RawData.ContainsKey("status"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new Namespaces::IntersectionMember1
        {
            // Null should be interpreted as omitted for these properties
            Status = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new Namespaces::IntersectionMember1
        {
            Status = new()
            {
                ReadyReplicas = 0,
                UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
                Utilization = 0,
            },
        };

        Namespaces::IntersectionMember1 copied = new(model);

        Assert.Equal(model, copied);
    }
}

public class StatusTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new Namespaces::Status
        {
            ReadyReplicas = 0,
            UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Utilization = 0,
        };

        long expectedReadyReplicas = 0;
        DateTimeOffset expectedUpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z");
        double expectedUtilization = 0;

        Assert.Equal(expectedReadyReplicas, model.ReadyReplicas);
        Assert.Equal(expectedUpdatedAt, model.UpdatedAt);
        Assert.Equal(expectedUtilization, model.Utilization);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new Namespaces::Status
        {
            ReadyReplicas = 0,
            UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Utilization = 0,
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Namespaces::Status>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new Namespaces::Status
        {
            ReadyReplicas = 0,
            UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Utilization = 0,
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Namespaces::Status>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        long expectedReadyReplicas = 0;
        DateTimeOffset expectedUpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z");
        double expectedUtilization = 0;

        Assert.Equal(expectedReadyReplicas, deserialized.ReadyReplicas);
        Assert.Equal(expectedUpdatedAt, deserialized.UpdatedAt);
        Assert.Equal(expectedUtilization, deserialized.Utilization);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new Namespaces::Status
        {
            ReadyReplicas = 0,
            UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Utilization = 0,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new Namespaces::Status
        {
            ReadyReplicas = 0,
            UpdatedAt = DateTimeOffset.Parse("2019-12-27T18:11:19.117Z"),
            Utilization = 0,
        };

        Namespaces::Status copied = new(model);

        Assert.Equal(model, copied);
    }
}
