using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class HighlightConfigParamsTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new HighlightConfigParams
        {
            FragmentBy = HighlightFragmentBy.None,
            FragmentLimit = 0,
            IncludeOffsets = HighlightOffsetUnits.Utf8,
            RankFragmentsBy = JsonSerializer.Deserialize<JsonElement>("{}"),
        };

        ApiEnum<string, HighlightFragmentBy> expectedFragmentBy = HighlightFragmentBy.None;
        long expectedFragmentLimit = 0;
        ApiEnum<string, HighlightOffsetUnits> expectedIncludeOffsets = HighlightOffsetUnits.Utf8;
        JsonElement expectedRankFragmentsBy = JsonSerializer.Deserialize<JsonElement>("{}");

        Assert.Equal(expectedFragmentBy, model.FragmentBy);
        Assert.Equal(expectedFragmentLimit, model.FragmentLimit);
        Assert.Equal(expectedIncludeOffsets, model.IncludeOffsets);
        Assert.NotNull(model.RankFragmentsBy);
        Assert.True(JsonElement.DeepEquals(expectedRankFragmentsBy, model.RankFragmentsBy.Value));
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new HighlightConfigParams
        {
            FragmentBy = HighlightFragmentBy.None,
            FragmentLimit = 0,
            IncludeOffsets = HighlightOffsetUnits.Utf8,
            RankFragmentsBy = JsonSerializer.Deserialize<JsonElement>("{}"),
        };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<HighlightConfigParams>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new HighlightConfigParams
        {
            FragmentBy = HighlightFragmentBy.None,
            FragmentLimit = 0,
            IncludeOffsets = HighlightOffsetUnits.Utf8,
            RankFragmentsBy = JsonSerializer.Deserialize<JsonElement>("{}"),
        };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<HighlightConfigParams>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        ApiEnum<string, HighlightFragmentBy> expectedFragmentBy = HighlightFragmentBy.None;
        long expectedFragmentLimit = 0;
        ApiEnum<string, HighlightOffsetUnits> expectedIncludeOffsets = HighlightOffsetUnits.Utf8;
        JsonElement expectedRankFragmentsBy = JsonSerializer.Deserialize<JsonElement>("{}");

        Assert.Equal(expectedFragmentBy, deserialized.FragmentBy);
        Assert.Equal(expectedFragmentLimit, deserialized.FragmentLimit);
        Assert.Equal(expectedIncludeOffsets, deserialized.IncludeOffsets);
        Assert.NotNull(deserialized.RankFragmentsBy);
        Assert.True(
            JsonElement.DeepEquals(expectedRankFragmentsBy, deserialized.RankFragmentsBy.Value)
        );
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new HighlightConfigParams
        {
            FragmentBy = HighlightFragmentBy.None,
            FragmentLimit = 0,
            IncludeOffsets = HighlightOffsetUnits.Utf8,
            RankFragmentsBy = JsonSerializer.Deserialize<JsonElement>("{}"),
        };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new HighlightConfigParams { };

        Assert.Null(model.FragmentBy);
        Assert.False(model.RawData.ContainsKey("fragment_by"));
        Assert.Null(model.FragmentLimit);
        Assert.False(model.RawData.ContainsKey("fragment_limit"));
        Assert.Null(model.IncludeOffsets);
        Assert.False(model.RawData.ContainsKey("include_offsets"));
        Assert.Null(model.RankFragmentsBy);
        Assert.False(model.RawData.ContainsKey("rank_fragments_by"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new HighlightConfigParams { };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new HighlightConfigParams
        {
            // Null should be interpreted as omitted for these properties
            FragmentBy = null,
            FragmentLimit = null,
            IncludeOffsets = null,
            RankFragmentsBy = null,
        };

        Assert.Null(model.FragmentBy);
        Assert.False(model.RawData.ContainsKey("fragment_by"));
        Assert.Null(model.FragmentLimit);
        Assert.False(model.RawData.ContainsKey("fragment_limit"));
        Assert.Null(model.IncludeOffsets);
        Assert.False(model.RawData.ContainsKey("include_offsets"));
        Assert.Null(model.RankFragmentsBy);
        Assert.False(model.RawData.ContainsKey("rank_fragments_by"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new HighlightConfigParams
        {
            // Null should be interpreted as omitted for these properties
            FragmentBy = null,
            FragmentLimit = null,
            IncludeOffsets = null,
            RankFragmentsBy = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new HighlightConfigParams
        {
            FragmentBy = HighlightFragmentBy.None,
            FragmentLimit = 0,
            IncludeOffsets = HighlightOffsetUnits.Utf8,
            RankFragmentsBy = JsonSerializer.Deserialize<JsonElement>("{}"),
        };

        HighlightConfigParams copied = new(model);

        Assert.Equal(model, copied);
    }
}
