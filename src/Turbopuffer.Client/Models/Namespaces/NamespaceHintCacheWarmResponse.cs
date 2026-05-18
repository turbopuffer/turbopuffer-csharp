using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Client.Core;
using Turbopuffer.Client.Exceptions;

namespace Turbopuffer.Client.Models.Namespaces;

/// <summary>
/// The response to a successful cache warm request.
/// </summary>
[JsonConverter(
    typeof(JsonModelConverter<
        NamespaceHintCacheWarmResponse,
        NamespaceHintCacheWarmResponseFromRaw
    >)
)]
public sealed record class NamespaceHintCacheWarmResponse : JsonModel
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

    public string? Message
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("message");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("message", value);
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        if (!JsonElement.DeepEquals(this.Status, JsonSerializer.SerializeToElement("ACCEPTED")))
        {
            throw new TurbopufferInvalidDataException("Invalid value given for constant");
        }
        _ = this.Message;
    }

    public NamespaceHintCacheWarmResponse()
    {
        this.Status = JsonSerializer.SerializeToElement("ACCEPTED");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public NamespaceHintCacheWarmResponse(
        NamespaceHintCacheWarmResponse namespaceHintCacheWarmResponse
    )
        : base(namespaceHintCacheWarmResponse) { }
#pragma warning restore CS8618

    public NamespaceHintCacheWarmResponse(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);

        this.Status = JsonSerializer.SerializeToElement("ACCEPTED");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    NamespaceHintCacheWarmResponse(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="NamespaceHintCacheWarmResponseFromRaw.FromRawUnchecked"/>
    public static NamespaceHintCacheWarmResponse FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class NamespaceHintCacheWarmResponseFromRaw : IFromRawJson<NamespaceHintCacheWarmResponse>
{
    /// <inheritdoc/>
    public NamespaceHintCacheWarmResponse FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => NamespaceHintCacheWarmResponse.FromRawUnchecked(rawData);
}
