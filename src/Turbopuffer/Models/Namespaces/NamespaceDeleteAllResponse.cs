using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Core;
using Turbopuffer.Exceptions;

namespace Turbopuffer.Models.Namespaces;

/// <summary>
/// The response to a successful namespace deletion request.
/// </summary>
[JsonConverter(
    typeof(JsonModelConverter<NamespaceDeleteAllResponse, NamespaceDeleteAllResponseFromRaw>)
)]
public sealed record class NamespaceDeleteAllResponse : JsonModel
{
    /// <summary>
    /// The status of the request.
    /// </summary>
    public JsonElement Status
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<JsonElement>("status");
        }
        init { this._rawData.Set("status", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        if (!JsonElement.DeepEquals(this.Status, JsonSerializer.SerializeToElement("OK")))
        {
            throw new TurbopufferInvalidDataException("Invalid value given for constant");
        }
    }

    public NamespaceDeleteAllResponse()
    {
        this.Status = JsonSerializer.SerializeToElement("OK");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public NamespaceDeleteAllResponse(NamespaceDeleteAllResponse namespaceDeleteAllResponse)
        : base(namespaceDeleteAllResponse) { }
#pragma warning restore CS8618

    public NamespaceDeleteAllResponse(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);

        this.Status = JsonSerializer.SerializeToElement("OK");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    NamespaceDeleteAllResponse(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="NamespaceDeleteAllResponseFromRaw.FromRawUnchecked"/>
    public static NamespaceDeleteAllResponse FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class NamespaceDeleteAllResponseFromRaw : IFromRawJson<NamespaceDeleteAllResponse>
{
    /// <inheritdoc/>
    public NamespaceDeleteAllResponse FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => NamespaceDeleteAllResponse.FromRawUnchecked(rawData);
}
