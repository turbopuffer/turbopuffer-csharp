using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using Turbopuffer.Client.Core;

namespace Turbopuffer.Client.Models.Namespaces;

/// <summary>
/// Creates an instant, copy-on-write clone of a namespace.
///
/// <para>NOTE: Do not inherit from this type outside the SDK unless you're okay with
/// breaking changes in non-major versions. We may add new methods in the future that
/// cause existing derived classes to break.</para>
/// </summary>
public record class NamespaceBranchFromParams : ParamsBase
{
    readonly JsonDictionary _rawBodyData = new();
    public IReadOnlyDictionary<string, JsonElement> RawBodyData
    {
        get { return this._rawBodyData.Freeze(); }
    }

    public string? Namespace { get; init; }

    /// <summary>
    /// The namespace to create an instant, copy-on-write clone of.
    /// </summary>
    public required string SourceNamespace
    {
        get
        {
            this._rawBodyData.Freeze();
            return this._rawBodyData.GetNotNullClass<string>("source_namespace");
        }
        init { this._rawBodyData.Set("source_namespace", value); }
    }

    public NamespaceBranchFromParams() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public NamespaceBranchFromParams(NamespaceBranchFromParams namespaceBranchFromParams)
        : base(namespaceBranchFromParams)
    {
        this.Namespace = namespaceBranchFromParams.Namespace;

        this._rawBodyData = new(namespaceBranchFromParams._rawBodyData);
    }
#pragma warning restore CS8618

    public NamespaceBranchFromParams(
        IReadOnlyDictionary<string, JsonElement> rawHeaderData,
        IReadOnlyDictionary<string, JsonElement> rawQueryData,
        IReadOnlyDictionary<string, JsonElement> rawBodyData
    )
    {
        this._rawHeaderData = new(rawHeaderData);
        this._rawQueryData = new(rawQueryData);
        this._rawBodyData = new(rawBodyData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    NamespaceBranchFromParams(
        FrozenDictionary<string, JsonElement> rawHeaderData,
        FrozenDictionary<string, JsonElement> rawQueryData,
        FrozenDictionary<string, JsonElement> rawBodyData,
        string namespace_
    )
    {
        this._rawHeaderData = new(rawHeaderData);
        this._rawQueryData = new(rawQueryData);
        this._rawBodyData = new(rawBodyData);
        this.Namespace = namespace_;
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="IFromRawJson{T}.FromRawUnchecked"/>
    public static NamespaceBranchFromParams FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawHeaderData,
        IReadOnlyDictionary<string, JsonElement> rawQueryData,
        IReadOnlyDictionary<string, JsonElement> rawBodyData,
        string namespace_
    )
    {
        return new(
            FrozenDictionary.ToFrozenDictionary(rawHeaderData),
            FrozenDictionary.ToFrozenDictionary(rawQueryData),
            FrozenDictionary.ToFrozenDictionary(rawBodyData),
            namespace_
        );
    }

    public override string ToString() =>
        JsonSerializer.Serialize(
            FriendlyJsonPrinter.PrintValue(
                new Dictionary<string, JsonElement>()
                {
                    ["Namespace"] = JsonSerializer.SerializeToElement(this.Namespace),
                    ["HeaderData"] = FriendlyJsonPrinter.PrintValue(
                        JsonSerializer.SerializeToElement(this._rawHeaderData.Freeze())
                    ),
                    ["QueryData"] = FriendlyJsonPrinter.PrintValue(
                        JsonSerializer.SerializeToElement(this._rawQueryData.Freeze())
                    ),
                    ["BodyData"] = FriendlyJsonPrinter.PrintValue(this._rawBodyData.Freeze()),
                }
            ),
            ModelBase.ToStringSerializerOptions
        );

    public virtual bool Equals(NamespaceBranchFromParams? other)
    {
        if (other == null)
        {
            return false;
        }
        return (this.Namespace?.Equals(other.Namespace) ?? other.Namespace == null)
            && this._rawHeaderData.Equals(other._rawHeaderData)
            && this._rawQueryData.Equals(other._rawQueryData)
            && this._rawBodyData.Equals(other._rawBodyData);
    }

    public override Uri Url(ClientOptions options)
    {
        var queryString = this.QueryString(options);
        return new UriBuilder(
            options.BaseUrl.ToString().TrimEnd('/')
                + string.Format("/v2/namespaces/{0}", this.Namespace)
        )
        {
            Query = string.IsNullOrEmpty(queryString)
                ? "stainless_overload=branchFrom"
                : ("stainless_overload=branchFrom&" + queryString),
        }.Uri;
    }

    internal override HttpContent? BodyContent()
    {
        // The API expects the body wrapped in a `branch_from_namespace` key.
        var wrapped = new Dictionary<string, IReadOnlyDictionary<string, JsonElement>>
        {
            ["branch_from_namespace"] = this.RawBodyData,
        };
        return new StringContent(
            JsonSerializer.Serialize(wrapped, ModelBase.SerializerOptions),
            Encoding.UTF8,
            "application/json"
        );
    }

    internal override void AddHeadersToRequest(HttpRequestMessage request, ClientOptions options)
    {
        ParamsBase.AddDefaultHeaders(request, options);
        foreach (var item in this.RawHeaderData)
        {
            ParamsBase.AddHeaderElementToRequest(request, item.Key, item.Value);
        }
    }

    public override int GetHashCode()
    {
        return 0;
    }
}
