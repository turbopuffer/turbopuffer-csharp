using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Client.Core;

namespace Turbopuffer.Client.Models;

[JsonConverter(
    typeof(JsonModelConverter<ClientNamespacesPageResponse, ClientNamespacesPageResponseFromRaw>)
)]
public sealed record class ClientNamespacesPageResponse : JsonModel
{
    /// <summary>
    /// The list of namespaces.
    /// </summary>
    public IReadOnlyList<NamespaceSummary>? Namespaces
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<ImmutableArray<NamespaceSummary>>("namespaces");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set<ImmutableArray<NamespaceSummary>?>(
                "namespaces",
                value == null ? null : ImmutableArray.ToImmutableArray(value)
            );
        }
    }

    /// <summary>
    /// The cursor to use to retrieve the next page of results.
    /// </summary>
    public string? NextCursor
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<string>("next_cursor");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("next_cursor", value);
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        foreach (var item in this.Namespaces ?? [])
        {
            item.Validate();
        }
        _ = this.NextCursor;
    }

    public ClientNamespacesPageResponse() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public ClientNamespacesPageResponse(ClientNamespacesPageResponse clientNamespacesPageResponse)
        : base(clientNamespacesPageResponse) { }
#pragma warning restore CS8618

    public ClientNamespacesPageResponse(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    ClientNamespacesPageResponse(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="ClientNamespacesPageResponseFromRaw.FromRawUnchecked"/>
    public static ClientNamespacesPageResponse FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class ClientNamespacesPageResponseFromRaw : IFromRawJson<ClientNamespacesPageResponse>
{
    /// <inheritdoc/>
    public ClientNamespacesPageResponse FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => ClientNamespacesPageResponse.FromRawUnchecked(rawData);
}
