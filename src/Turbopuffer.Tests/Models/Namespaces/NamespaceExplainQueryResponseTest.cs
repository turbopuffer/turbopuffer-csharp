using System.Text.Json;
using Turbopuffer.Core;
using Turbopuffer.Models.Namespaces;

namespace Turbopuffer.Tests.Models.Namespaces;

public class NamespaceExplainQueryResponseTest : TestBase
{
    [Fact]
    public void FieldRoundtrip_Works()
    {
        var model = new NamespaceExplainQueryResponse { PlanText = "plan_text" };

        string expectedPlanText = "plan_text";

        Assert.Equal(expectedPlanText, model.PlanText);
    }

    [Fact]
    public void SerializationRoundtrip_Works()
    {
        var model = new NamespaceExplainQueryResponse { PlanText = "plan_text" };

        string json = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<NamespaceExplainQueryResponse>(
            json,
            ModelBase.SerializerOptions
        );

        Assert.Equal(model, deserialized);
    }

    [Fact]
    public void FieldRoundtripThroughSerialization_Works()
    {
        var model = new NamespaceExplainQueryResponse { PlanText = "plan_text" };

        string element = JsonSerializer.Serialize(model, ModelBase.SerializerOptions);
        var deserialized = JsonSerializer.Deserialize<NamespaceExplainQueryResponse>(
            element,
            ModelBase.SerializerOptions
        );
        Assert.NotNull(deserialized);

        string expectedPlanText = "plan_text";

        Assert.Equal(expectedPlanText, deserialized.PlanText);
    }

    [Fact]
    public void Validation_Works()
    {
        var model = new NamespaceExplainQueryResponse { PlanText = "plan_text" };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetAreNotSet_Works()
    {
        var model = new NamespaceExplainQueryResponse { };

        Assert.Null(model.PlanText);
        Assert.False(model.RawData.ContainsKey("plan_text"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesUnsetValidation_Works()
    {
        var model = new NamespaceExplainQueryResponse { };

        model.Validate();
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullAreNotSet_Works()
    {
        var model = new NamespaceExplainQueryResponse
        {
            // Null should be interpreted as omitted for these properties
            PlanText = null,
        };

        Assert.Null(model.PlanText);
        Assert.False(model.RawData.ContainsKey("plan_text"));
    }

    [Fact]
    public void OptionalNonNullablePropertiesSetToNullValidation_Works()
    {
        var model = new NamespaceExplainQueryResponse
        {
            // Null should be interpreted as omitted for these properties
            PlanText = null,
        };

        model.Validate();
    }

    [Fact]
    public void CopyConstructor_Works()
    {
        var model = new NamespaceExplainQueryResponse { PlanText = "plan_text" };

        NamespaceExplainQueryResponse copied = new(model);

        Assert.Equal(model, copied);
    }
}
