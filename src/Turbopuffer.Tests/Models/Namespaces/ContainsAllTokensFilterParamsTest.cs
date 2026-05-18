using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class ContainsAllTokensFilterParamsTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new ContainsAllTokensFilterParams { LastAsPrefix = true };

        bool expectedLastAsPrefix = true;

        Assert.Equal(expectedLastAsPrefix, model.LastAsPrefix);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new ContainsAllTokensFilterParams { LastAsPrefix = true };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ContainsAllTokensFilterParams>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new ContainsAllTokensFilterParams { LastAsPrefix = true };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<ContainsAllTokensFilterParams>(
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
        var model = new ContainsAllTokensFilterParams { LastAsPrefix = true };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new ContainsAllTokensFilterParams { };

        Assert.Null(model.LastAsPrefix);
        Assert.False(model.RawData.ContainsKey("last_as_prefix"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new ContainsAllTokensFilterParams { };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new ContainsAllTokensFilterParams
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
        var model = new ContainsAllTokensFilterParams
        {
            // Null should be interpreted as omitted for these properties
            LastAsPrefix = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new ContainsAllTokensFilterParams { LastAsPrefix = true };

        ContainsAllTokensFilterParams copied = new(model);

        Assert.Equal(model, copied);
    }
}
