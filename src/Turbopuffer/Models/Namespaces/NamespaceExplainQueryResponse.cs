using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Core;

namespace Turbopuffer.Models.Namespaces;

/// <summary>
/// The response to a successful query explain.
/// </summary>
[JsonConverter(
    typeof(JsonModelConverter<NamespaceExplainQueryResponse, NamespaceExplainQueryResponseFromRaw>)
)]
public sealed record class NamespaceExplainQueryResponse : JsonModel
{
    /// <summary>
    /// The textual representation of the query plan.
    /// </summary>
    public string? PlanText
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("plan_text");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("plan_text", value);
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.PlanText;
    }

    public NamespaceExplainQueryResponse() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public NamespaceExplainQueryResponse(
        NamespaceExplainQueryResponse namespaceExplainQueryResponse
    )
        : base(namespaceExplainQueryResponse) { }
#pragma warning restore CS8618

    public NamespaceExplainQueryResponse(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    NamespaceExplainQueryResponse(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="NamespaceExplainQueryResponseFromRaw.FromRawUnchecked"/>
    public static NamespaceExplainQueryResponse FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class NamespaceExplainQueryResponseFromRaw : IFromRawJson<NamespaceExplainQueryResponse>
{
    /// <inheritdoc/>
    public NamespaceExplainQueryResponse FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => NamespaceExplainQueryResponse.FromRawUnchecked(rawData);
}
