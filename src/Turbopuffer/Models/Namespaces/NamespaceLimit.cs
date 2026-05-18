using System.Collections.Frozen;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Core;

namespace Turbopuffer.Models.Namespaces;

/// <summary>
/// Limits the documents returned by a query.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<NamespaceLimit, NamespaceLimitFromRaw>))]
public sealed record class NamespaceLimit : JsonModel
{
    /// <summary>
    /// Limits the total number of documents returned.
    /// </summary>
    public required long Total
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<long>("total");
        }
        init { this._rawData.Set("total", value); }
    }

    /// <summary>
    /// Limits the number of documents with the same value for a set of attributes
    /// (the "limit key") that can appear in the results.
    /// </summary>
    public Per? Per
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<Per>("per");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("per", value);
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.Total;
        this.Per?.Validate();
    }

    public NamespaceLimit() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public NamespaceLimit(NamespaceLimit namespaceLimit)
        : base(namespaceLimit) { }
#pragma warning restore CS8618

    public NamespaceLimit(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    NamespaceLimit(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="NamespaceLimitFromRaw.FromRawUnchecked"/>
    public static NamespaceLimit FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }

    [SetsRequiredMembers]
    public NamespaceLimit(long total)
        : this()
    {
        this.Total = total;
    }
}

class NamespaceLimitFromRaw : IFromRawJson<NamespaceLimit>
{
    /// <inheritdoc/>
    public NamespaceLimit FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        NamespaceLimit.FromRawUnchecked(rawData);
}

/// <summary>
/// Limits the number of documents with the same value for a set of attributes (the
/// "limit key") that can appear in the results.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<Per, PerFromRaw>))]
public sealed record class Per : JsonModel
{
    /// <summary>
    /// The attributes to include in the limit key.
    /// </summary>
    public required IReadOnlyList<string> Attributes
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<ImmutableArray<string>>("attributes");
        }
        init
        {
            this._rawData.Set<ImmutableArray<string>>(
                "attributes",
                ImmutableArray.ToImmutableArray(value)
            );
        }
    }

    /// <summary>
    /// The maximum number of documents to return for each value of the limit key.
    /// </summary>
    public required long Limit
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<long>("limit");
        }
        init { this._rawData.Set("limit", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.Attributes;
        _ = this.Limit;
    }

    public Per() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public Per(Per per)
        : base(per) { }
#pragma warning restore CS8618

    public Per(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    Per(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="PerFromRaw.FromRawUnchecked"/>
    public static Per FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class PerFromRaw : IFromRawJson<Per>
{
    /// <inheritdoc/>
    public Per FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        Per.FromRawUnchecked(rawData);
}
