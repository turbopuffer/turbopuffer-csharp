using System;
using System.Collections.Generic;
using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class NamespaceWriteParamsTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var parameters = new NamespaceWriteParams
        {
            Namespace = "namespace",
            BranchFromNamespace = "string",
            CopyFromNamespace = "string",
            DeleteByFilter = JsonSerializer.Deserialize<JsonElement>("{}"),
            DeleteByFilterAllowPartial = true,
            DeleteCondition = JsonSerializer.Deserialize<JsonElement>("{}"),
            Deletes = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],
            DisableBackpressure = true,
            DistanceMetric = DistanceMetric.CosineDistance,
            Encryption = new CustomerManaged("key_name"),
            PatchByFilter = new()
            {
                Filters = new FilterRaw(JsonSerializer.Deserialize<JsonElement>("{}")),
                Patch = new Dictionary<string, JsonElement>()
                {
                    { "foo", JsonSerializer.SerializeToElement("bar") },
                },
            },
            PatchByFilterAllowPartial = true,
            PatchColumns = new()
            {
                ID = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],
                Vector = new([new([0])]),
            },
            PatchCondition = JsonSerializer.Deserialize<JsonElement>("{}"),
            PatchRows = [new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) }],
            ReturnAffectedIds = true,
            Schema = new Dictionary<string, AttributeSchema>() { { "foo", "string" } },
            UpsertColumns = new()
            {
                ID = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],
                Vector = new([new([0])]),
            },
            UpsertCondition = JsonSerializer.Deserialize<JsonElement>("{}"),
            UpsertRows = [new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) }],
        };

        string expectedNamespace = "namespace";
        BranchFromNamespaceParams expectedBranchFromNamespace = "string";
        CopyFromNamespaceParams expectedCopyFromNamespace = "string";
        JsonElement expectedDeleteByFilter = JsonSerializer.Deserialize<JsonElement>("{}");
        bool expectedDeleteByFilterAllowPartial = true;
        JsonElement expectedDeleteCondition = JsonSerializer.Deserialize<JsonElement>("{}");
        List<ID> expectedDeletes = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"];
        bool expectedDisableBackpressure = true;
        ApiEnum<string, DistanceMetric> expectedDistanceMetric = DistanceMetric.CosineDistance;
        Encryption expectedEncryption = new CustomerManaged("key_name");
        PatchByFilter expectedPatchByFilter = new()
        {
            Filters = new FilterRaw(JsonSerializer.Deserialize<JsonElement>("{}")),
            Patch = new Dictionary<string, JsonElement>()
            {
                { "foo", JsonSerializer.SerializeToElement("bar") },
            },
        };
        bool expectedPatchByFilterAllowPartial = true;
        Columns expectedPatchColumns = new()
        {
            ID = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],
            Vector = new([new([0])]),
        };
        JsonElement expectedPatchCondition = JsonSerializer.Deserialize<JsonElement>("{}");
        List<Row> expectedPatchRows =
        [
            new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) },
        ];
        bool expectedReturnAffectedIds = true;
        Dictionary<string, AttributeSchema> expectedSchema = new() { { "foo", "string" } };
        Columns expectedUpsertColumns = new()
        {
            ID = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],
            Vector = new([new([0])]),
        };
        JsonElement expectedUpsertCondition = JsonSerializer.Deserialize<JsonElement>("{}");
        List<Row> expectedUpsertRows =
        [
            new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) },
        ];

        Assert.Equal(expectedNamespace, parameters.Namespace);
        Assert.Equal(expectedBranchFromNamespace, parameters.BranchFromNamespace);
        Assert.Equal(expectedCopyFromNamespace, parameters.CopyFromNamespace);
        Assert.NotNull(parameters.DeleteByFilter);
        Assert.True(
            JsonElement.DeepEquals(expectedDeleteByFilter, parameters.DeleteByFilter.Value)
        );
        Assert.Equal(expectedDeleteByFilterAllowPartial, parameters.DeleteByFilterAllowPartial);
        Assert.NotNull(parameters.DeleteCondition);
        Assert.True(
            JsonElement.DeepEquals(expectedDeleteCondition, parameters.DeleteCondition.Value)
        );
        Assert.NotNull(parameters.Deletes);
        Assert.Equal(expectedDeletes.Count, parameters.Deletes.Count);
        for (int i = 0; i < expectedDeletes.Count; i++)
        {
            Assert.Equal(expectedDeletes[i], parameters.Deletes[i]);
        }
        Assert.Equal(expectedDisableBackpressure, parameters.DisableBackpressure);
        Assert.Equal(expectedDistanceMetric, parameters.DistanceMetric);
        Assert.Equal(expectedEncryption, parameters.Encryption);
        Assert.Equal(expectedPatchByFilter, parameters.PatchByFilter);
        Assert.Equal(expectedPatchByFilterAllowPartial, parameters.PatchByFilterAllowPartial);
        Assert.Equal(expectedPatchColumns, parameters.PatchColumns);
        Assert.NotNull(parameters.PatchCondition);
        Assert.True(
            JsonElement.DeepEquals(expectedPatchCondition, parameters.PatchCondition.Value)
        );
        Assert.NotNull(parameters.PatchRows);
        Assert.Equal(expectedPatchRows.Count, parameters.PatchRows.Count);
        for (int i = 0; i < expectedPatchRows.Count; i++)
        {
            Assert.Equal(expectedPatchRows[i], parameters.PatchRows[i]);
        }
        Assert.Equal(expectedReturnAffectedIds, parameters.ReturnAffectedIds);
        Assert.NotNull(parameters.Schema);
        Assert.Equal(expectedSchema.Count, parameters.Schema.Count);
        foreach (var item in expectedSchema)
        {
            Assert.True(parameters.Schema.TryGetValue(item.Key, out var value));

            Assert.Equal(value, parameters.Schema[item.Key]);
        }
        Assert.Equal(expectedUpsertColumns, parameters.UpsertColumns);
        Assert.NotNull(parameters.UpsertCondition);
        Assert.True(
            JsonElement.DeepEquals(expectedUpsertCondition, parameters.UpsertCondition.Value)
        );
        Assert.NotNull(parameters.UpsertRows);
        Assert.Equal(expectedUpsertRows.Count, parameters.UpsertRows.Count);
        for (int i = 0; i < expectedUpsertRows.Count; i++)
        {
            Assert.Equal(expectedUpsertRows[i], parameters.UpsertRows[i]);
        }
    }

    [Fact]
    public void OptionalNonNullableParamsUnsetAreNotSet_Works()
    {
        var parameters = new NamespaceWriteParams { Namespace = "namespace" };

        Assert.Null(parameters.BranchFromNamespace);
        Assert.False(parameters.RawBodyData.ContainsKey("branch_from_namespace"));
        Assert.Null(parameters.CopyFromNamespace);
        Assert.False(parameters.RawBodyData.ContainsKey("copy_from_namespace"));
        Assert.Null(parameters.DeleteByFilter);
        Assert.False(parameters.RawBodyData.ContainsKey("delete_by_filter"));
        Assert.Null(parameters.DeleteByFilterAllowPartial);
        Assert.False(parameters.RawBodyData.ContainsKey("delete_by_filter_allow_partial"));
        Assert.Null(parameters.DeleteCondition);
        Assert.False(parameters.RawBodyData.ContainsKey("delete_condition"));
        Assert.Null(parameters.Deletes);
        Assert.False(parameters.RawBodyData.ContainsKey("deletes"));
        Assert.Null(parameters.DisableBackpressure);
        Assert.False(parameters.RawBodyData.ContainsKey("disable_backpressure"));
        Assert.Null(parameters.DistanceMetric);
        Assert.False(parameters.RawBodyData.ContainsKey("distance_metric"));
        Assert.Null(parameters.Encryption);
        Assert.False(parameters.RawBodyData.ContainsKey("encryption"));
        Assert.Null(parameters.PatchByFilter);
        Assert.False(parameters.RawBodyData.ContainsKey("patch_by_filter"));
        Assert.Null(parameters.PatchByFilterAllowPartial);
        Assert.False(parameters.RawBodyData.ContainsKey("patch_by_filter_allow_partial"));
        Assert.Null(parameters.PatchColumns);
        Assert.False(parameters.RawBodyData.ContainsKey("patch_columns"));
        Assert.Null(parameters.PatchCondition);
        Assert.False(parameters.RawBodyData.ContainsKey("patch_condition"));
        Assert.Null(parameters.PatchRows);
        Assert.False(parameters.RawBodyData.ContainsKey("patch_rows"));
        Assert.Null(parameters.ReturnAffectedIds);
        Assert.False(parameters.RawBodyData.ContainsKey("return_affected_ids"));
        Assert.Null(parameters.Schema);
        Assert.False(parameters.RawBodyData.ContainsKey("schema"));
        Assert.Null(parameters.UpsertColumns);
        Assert.False(parameters.RawBodyData.ContainsKey("upsert_columns"));
        Assert.Null(parameters.UpsertCondition);
        Assert.False(parameters.RawBodyData.ContainsKey("upsert_condition"));
        Assert.Null(parameters.UpsertRows);
        Assert.False(parameters.RawBodyData.ContainsKey("upsert_rows"));
    }

    [Fact]
    public void OptionalNonNullableParamsSetToNullAreNotSet_Works()
    {
        var parameters = new NamespaceWriteParams
        {
            Namespace = "namespace",

            // Null should be interpreted as omitted for these properties
            BranchFromNamespace = null,
            CopyFromNamespace = null,
            DeleteByFilter = null,
            DeleteByFilterAllowPartial = null,
            DeleteCondition = null,
            Deletes = null,
            DisableBackpressure = null,
            DistanceMetric = null,
            Encryption = null,
            PatchByFilter = null,
            PatchByFilterAllowPartial = null,
            PatchColumns = null,
            PatchCondition = null,
            PatchRows = null,
            ReturnAffectedIds = null,
            Schema = null,
            UpsertColumns = null,
            UpsertCondition = null,
            UpsertRows = null,
        };

        Assert.Null(parameters.BranchFromNamespace);
        Assert.False(parameters.RawBodyData.ContainsKey("branch_from_namespace"));
        Assert.Null(parameters.CopyFromNamespace);
        Assert.False(parameters.RawBodyData.ContainsKey("copy_from_namespace"));
        Assert.Null(parameters.DeleteByFilter);
        Assert.False(parameters.RawBodyData.ContainsKey("delete_by_filter"));
        Assert.Null(parameters.DeleteByFilterAllowPartial);
        Assert.False(parameters.RawBodyData.ContainsKey("delete_by_filter_allow_partial"));
        Assert.Null(parameters.DeleteCondition);
        Assert.False(parameters.RawBodyData.ContainsKey("delete_condition"));
        Assert.Null(parameters.Deletes);
        Assert.False(parameters.RawBodyData.ContainsKey("deletes"));
        Assert.Null(parameters.DisableBackpressure);
        Assert.False(parameters.RawBodyData.ContainsKey("disable_backpressure"));
        Assert.Null(parameters.DistanceMetric);
        Assert.False(parameters.RawBodyData.ContainsKey("distance_metric"));
        Assert.Null(parameters.Encryption);
        Assert.False(parameters.RawBodyData.ContainsKey("encryption"));
        Assert.Null(parameters.PatchByFilter);
        Assert.False(parameters.RawBodyData.ContainsKey("patch_by_filter"));
        Assert.Null(parameters.PatchByFilterAllowPartial);
        Assert.False(parameters.RawBodyData.ContainsKey("patch_by_filter_allow_partial"));
        Assert.Null(parameters.PatchColumns);
        Assert.False(parameters.RawBodyData.ContainsKey("patch_columns"));
        Assert.Null(parameters.PatchCondition);
        Assert.False(parameters.RawBodyData.ContainsKey("patch_condition"));
        Assert.Null(parameters.PatchRows);
        Assert.False(parameters.RawBodyData.ContainsKey("patch_rows"));
        Assert.Null(parameters.ReturnAffectedIds);
        Assert.False(parameters.RawBodyData.ContainsKey("return_affected_ids"));
        Assert.Null(parameters.Schema);
        Assert.False(parameters.RawBodyData.ContainsKey("schema"));
        Assert.Null(parameters.UpsertColumns);
        Assert.False(parameters.RawBodyData.ContainsKey("upsert_columns"));
        Assert.Null(parameters.UpsertCondition);
        Assert.False(parameters.RawBodyData.ContainsKey("upsert_condition"));
        Assert.Null(parameters.UpsertRows);
        Assert.False(parameters.RawBodyData.ContainsKey("upsert_rows"));
    }

    [Fact]
    public void Url_Works()
    {
        NamespaceWriteParams parameters = new() { Namespace = "namespace" };

        var url = parameters.Url(new() { Region = "gcp-us-central1", ApiKey = "tpuf_A1..." });

        Assert.True(
            TestBase.UrisEqual(
                new Uri("https://gcp-us-central1.turbopuffer.com/v2/namespaces/namespace"),
                url
            )
        );
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var parameters = new NamespaceWriteParams
        {
            Namespace = "namespace",
            BranchFromNamespace = "string",
            CopyFromNamespace = "string",
            DeleteByFilter = JsonSerializer.Deserialize<JsonElement>("{}"),
            DeleteByFilterAllowPartial = true,
            DeleteCondition = JsonSerializer.Deserialize<JsonElement>("{}"),
            Deletes = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],
            DisableBackpressure = true,
            DistanceMetric = DistanceMetric.CosineDistance,
            Encryption = new CustomerManaged("key_name"),
            PatchByFilter = new()
            {
                Filters = new FilterRaw(JsonSerializer.Deserialize<JsonElement>("{}")),
                Patch = new Dictionary<string, JsonElement>()
                {
                    { "foo", JsonSerializer.SerializeToElement("bar") },
                },
            },
            PatchByFilterAllowPartial = true,
            PatchColumns = new()
            {
                ID = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],
                Vector = new([new([0])]),
            },
            PatchCondition = JsonSerializer.Deserialize<JsonElement>("{}"),
            PatchRows = [new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) }],
            ReturnAffectedIds = true,
            Schema = new Dictionary<string, AttributeSchema>() { { "foo", "string" } },
            UpsertColumns = new()
            {
                ID = ["182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e"],
                Vector = new([new([0])]),
            },
            UpsertCondition = JsonSerializer.Deserialize<JsonElement>("{}"),
            UpsertRows = [new() { ID = "182bd5e5-6e1a-4fe4-a799-aa6d9a6ab26e", Vector = new([0]) }],
        };

        NamespaceWriteParams copied = new(parameters);

        Assert.Equal(parameters, copied);
    }
}

public class PatchByFilterTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new PatchByFilter
        {
            Filters = new FilterRaw(JsonSerializer.Deserialize<JsonElement>("{}")),
            Patch = new Dictionary<string, JsonElement>()
            {
                { "foo", JsonSerializer.SerializeToElement("bar") },
            },
        };

        JsonElement expectedFilters = JsonSerializer.Deserialize<JsonElement>("{}");
        Dictionary<string, JsonElement> expectedPatch = new()
        {
            { "foo", JsonSerializer.SerializeToElement("bar") },
        };

        Assert.True(JsonElement.DeepEquals(expectedFilters, ((FilterRaw)model.Filters).Value));
        Assert.Equal(expectedPatch.Count, model.Patch.Count);
        foreach (var item in expectedPatch)
        {
            Assert.True(model.Patch.TryGetValue(item.Key, out var value));

            Assert.True(JsonElement.DeepEquals(value, model.Patch[item.Key]));
        }
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new PatchByFilter
        {
            Filters = new FilterRaw(JsonSerializer.Deserialize<JsonElement>("{}")),
            Patch = new Dictionary<string, JsonElement>()
            {
                { "foo", JsonSerializer.SerializeToElement("bar") },
            },
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<PatchByFilter>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new PatchByFilter
        {
            Filters = new FilterRaw(JsonSerializer.Deserialize<JsonElement>("{}")),
            Patch = new Dictionary<string, JsonElement>()
            {
                { "foo", JsonSerializer.SerializeToElement("bar") },
            },
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<PatchByFilter>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        JsonElement expectedFilters = JsonSerializer.Deserialize<JsonElement>("{}");
        Dictionary<string, JsonElement> expectedPatch = new()
        {
            { "foo", JsonSerializer.SerializeToElement("bar") },
        };

        Assert.True(
            JsonElement.DeepEquals(expectedFilters, ((FilterRaw)deserialized.Filters).Value)
        );
        Assert.Equal(expectedPatch.Count, deserialized.Patch.Count);
        foreach (var item in expectedPatch)
        {
            Assert.True(deserialized.Patch.TryGetValue(item.Key, out var value));

            Assert.True(JsonElement.DeepEquals(value, deserialized.Patch[item.Key]));
        }
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new PatchByFilter
        {
            Filters = new FilterRaw(JsonSerializer.Deserialize<JsonElement>("{}")),
            Patch = new Dictionary<string, JsonElement>()
            {
                { "foo", JsonSerializer.SerializeToElement("bar") },
            },
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new PatchByFilter
        {
            Filters = new FilterRaw(JsonSerializer.Deserialize<JsonElement>("{}")),
            Patch = new Dictionary<string, JsonElement>()
            {
                { "foo", JsonSerializer.SerializeToElement("bar") },
            },
        };

        PatchByFilter copied = new(model);

        Assert.Equal(model, copied);
    }
}
