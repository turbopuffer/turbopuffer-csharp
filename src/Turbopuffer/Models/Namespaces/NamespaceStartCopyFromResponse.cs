using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Core;

namespace Turbopuffer.Models.Namespaces;

[JsonConverter(
    typeof(JsonModelConverter<
        NamespaceStartCopyFromResponse,
        NamespaceStartCopyFromResponseFromRaw
    >)
)]
public sealed record class NamespaceStartCopyFromResponse : JsonModel
{
    /// <summary>
    /// The token identifying the copy operation.
    /// </summary>
    public required string Token
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<string>("token");
        }
        init { this._rawData.Set("token", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.Token;
    }

    public NamespaceStartCopyFromResponse() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public NamespaceStartCopyFromResponse(
        NamespaceStartCopyFromResponse namespaceStartCopyFromResponse
    )
        : base(namespaceStartCopyFromResponse) { }
#pragma warning restore CS8618

    public NamespaceStartCopyFromResponse(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    NamespaceStartCopyFromResponse(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="NamespaceStartCopyFromResponseFromRaw.FromRawUnchecked"/>
    public static NamespaceStartCopyFromResponse FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }

    [SetsRequiredMembers]
    public NamespaceStartCopyFromResponse(string token)
        : this()
    {
        this.Token = token;
    }
}

class NamespaceStartCopyFromResponseFromRaw : IFromRawJson<NamespaceStartCopyFromResponse>
{
    /// <inheritdoc/>
    public NamespaceStartCopyFromResponse FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => NamespaceStartCopyFromResponse.FromRawUnchecked(rawData);
}
