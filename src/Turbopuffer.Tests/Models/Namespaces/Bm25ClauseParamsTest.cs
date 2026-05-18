using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class Bm25ClauseParamsTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new Bm25ClauseParams { LastAsPrefix = true };

        bool expectedLastAsPrefix = true;

        Assert.Equal(expectedLastAsPrefix, model.LastAsPrefix);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new Bm25ClauseParams { LastAsPrefix = true };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Bm25ClauseParams>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new Bm25ClauseParams { LastAsPrefix = true };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<Bm25ClauseParams>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        bool expectedLastAsPrefix = true;

        Assert.Equal(expectedLastAsPrefix, deserialized.LastAsPrefix);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new Bm25ClauseParams { LastAsPrefix = true };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new Bm25ClauseParams { };

        Assert.Null(model.LastAsPrefix);
        Assert.False(model.RawData.ContainsKey("last_as_prefix"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new Bm25ClauseParams { };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new Bm25ClauseParams
        {
            // Null should be interpreted as omitted for these properties
            LastAsPrefix = null,
        };

        Assert.Null(model.LastAsPrefix);
        Assert.False(model.RawData.ContainsKey("last_as_prefix"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new Bm25ClauseParams
        {
            // Null should be interpreted as omitted for these properties
            LastAsPrefix = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new Bm25ClauseParams { LastAsPrefix = true };

        Bm25ClauseParams copied = new(model);

        Assert.Equal(model, copied);
    }
}
