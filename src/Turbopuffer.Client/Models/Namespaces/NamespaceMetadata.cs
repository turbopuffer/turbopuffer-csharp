using System;
using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Client.Core;
using Turbopuffer.Client.Exceptions;

namespace Turbopuffer.Client.Models.Namespaces;

/// <summary>
/// Metadata about a namespace.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<NamespaceMetadata, NamespaceMetadataFromRaw>))]
public sealed record class NamespaceMetadata : JsonModel
{
    /// <summary>
    /// The approximate number of logical bytes in the namespace.
    /// </summary>
    public required long ApproxLogicalBytes
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<long>("approx_logical_bytes");
        }
        init { this._rawData.Set("approx_logical_bytes", value); }
    }

    /// <summary>
    /// The approximate number of rows in the namespace.
    /// </summary>
    public required long ApproxRowCount
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<long>("approx_row_count");
        }
        init { this._rawData.Set("approx_row_count", value); }
    }

    /// <summary>
    /// The timestamp when the namespace was created.
    /// </summary>
    public required DateTimeOffset CreatedAt
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<DateTimeOffset>("created_at");
        }
        init { this._rawData.Set("created_at", value); }
    }

    /// <summary>
    /// The encryption configuration for a namespace.
    /// </summary>
    public required Encryption Encryption
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<Encryption>("encryption");
        }
        init { this._rawData.Set("encryption", value); }
    }

    public required global::Turbopuffer.Client.Models.Namespaces.Index Index
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<global::Turbopuffer.Client.Models.Namespaces.Index>(
                "index"
            );
        }
        init { this._rawData.Set("index", value); }
    }

    /// <summary>
    /// The schema of the namespace.
    /// </summary>
    public required IReadOnlyDictionary<string, AttributeSchemaConfig> Schema
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullClass<FrozenDictionary<string, AttributeSchemaConfig>>(
                "schema"
            );
        }
        init
        {
            this._rawData.Set<FrozenDictionary<string, AttributeSchemaConfig>>(
                "schema",
                FrozenDictionary.ToFrozenDictionary(value)
            );
        }
    }

    /// <summary>
    /// The timestamp when the namespace was last modified by a write operation.
    /// </summary>
    public required DateTimeOffset UpdatedAt
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<DateTimeOffset>("updated_at");
        }
        init { this._rawData.Set("updated_at", value); }
    }

    /// <summary>
    /// Configuration for namespace pinning, along with the current status of the
    /// pinned namespace.
    /// </summary>
    public NamespaceMetadataPinning? Pinning
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<NamespaceMetadataPinning>("pinning");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("pinning", value);
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.ApproxLogicalBytes;
        _ = this.ApproxRowCount;
        _ = this.CreatedAt;
        this.Encryption.Validate();
        this.Index.Validate();
        foreach (var item in this.Schema.Values)
        {
            item.Validate();
        }
        _ = this.UpdatedAt;
        this.Pinning?.Validate();
    }

    public NamespaceMetadata() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public NamespaceMetadata(NamespaceMetadata namespaceMetadata)
        : base(namespaceMetadata) { }
#pragma warning restore CS8618

    public NamespaceMetadata(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    NamespaceMetadata(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="NamespaceMetadataFromRaw.FromRawUnchecked"/>
    public static NamespaceMetadata FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class NamespaceMetadataFromRaw : IFromRawJson<NamespaceMetadata>
{
    /// <inheritdoc/>
    public NamespaceMetadata FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        NamespaceMetadata.FromRawUnchecked(rawData);
}

[JsonConverter(typeof(IndexConverter))]
public record class Index : ModelBase
{
    public object? Value { get; } = null;

    JsonElement? _element = null;

    public JsonElement Json
    {
        get
        {
            return this._element ??= JsonSerializer.SerializeToElement(
                this.Value,
                ModelBase.SerializerOptions
            );
        }
    }

    public Index(IndexUpToDate value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public Index(IndexUpdating value, JsonElement? element = null)
    {
        this.Value = value;
        this._element = element;
    }

    public Index(JsonElement element)
    {
        this._element = element;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="IndexUpToDate"/>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickUpToDate(out var value)) {
    ///     // `value` is of type `IndexUpToDate`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickUpToDate([NotNullWhen(true)] out IndexUpToDate? value)
    {
        value = this.Value as IndexUpToDate;
        return value != null;
    }

    /// <summary>
    /// Returns true and sets the <c>out</c> parameter if the instance was constructed with a variant of
    /// type <see cref="IndexUpdating"/>.
    ///
    /// <para>Consider using <see cref="Switch"/> or <see cref="Match"/> if you need to handle every variant.</para>
    ///
    /// <example>
    /// <code>
    /// if (instance.TryPickUpdating(out var value)) {
    ///     // `value` is of type `IndexUpdating`
    ///     Console.WriteLine(value);
    /// }
    /// </code>
    /// </example>
    /// </summary>
    public bool TryPickUpdating([NotNullWhen(true)] out IndexUpdating? value)
    {
        value = this.Value as IndexUpdating;
        return value != null;
    }

    /// <summary>
    /// Calls the function parameter corresponding to the variant the instance was constructed with.
    ///
    /// <para>Use the <c>TryPick</c> method(s) if you don't need to handle every variant, or <see cref="Match"/>
    /// if you need your function parameters to return something.</para>
    ///
    /// <exception cref="TurbopufferInvalidDataException">
    /// Thrown when the instance was constructed with an unknown variant (e.g. deserialized from raw data
    /// that doesn't match any variant's expected shape).
    /// </exception>
    ///
    /// <example>
    /// <code>
    /// instance.Switch(
    ///     (IndexUpToDate value) =&gt; {...},
    ///     (IndexUpdating value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public void Switch(Action<IndexUpToDate> upToDate, Action<IndexUpdating> updating)
    {
        switch (this.Value)
        {
            case IndexUpToDate value:
                upToDate(value);
                break;
            case IndexUpdating value:
                updating(value);
                break;
            default:
                throw new TurbopufferInvalidDataException(
                    "Data did not match any variant of Index"
                );
        }
    }

    /// <summary>
    /// Calls the function parameter corresponding to the variant the instance was constructed with and
    /// returns its result.
    ///
    /// <para>Use the <c>TryPick</c> method(s) if you don't need to handle every variant, or <see cref="Switch"/>
    /// if you don't need your function parameters to return a value.</para>
    ///
    /// <exception cref="TurbopufferInvalidDataException">
    /// Thrown when the instance was constructed with an unknown variant (e.g. deserialized from raw data
    /// that doesn't match any variant's expected shape).
    /// </exception>
    ///
    /// <example>
    /// <code>
    /// var result = instance.Match(
    ///     (IndexUpToDate value) =&gt; {...},
    ///     (IndexUpdating value) =&gt; {...}
    /// );
    /// </code>
    /// </example>
    /// </summary>
    public T Match<T>(Func<IndexUpToDate, T> upToDate, Func<IndexUpdating, T> updating)
    {
        return this.Value switch
        {
            IndexUpToDate value => upToDate(value),
            IndexUpdating value => updating(value),
            _ => throw new TurbopufferInvalidDataException(
                "Data did not match any variant of Index"
            ),
        };
    }

    public static implicit operator global::Turbopuffer.Client.Models.Namespaces.Index(
        IndexUpToDate value
    ) => new(value);

    public static implicit operator global::Turbopuffer.Client.Models.Namespaces.Index(
        IndexUpdating value
    ) => new(value);

    /// <summary>
    /// Validates that the instance was constructed with a known variant and that this variant is valid
    /// (based on its own <c>Validate</c> method).
    ///
    /// <para>This is useful for instances constructed from raw JSON data (e.g. deserialized from an API response).</para>
    ///
    /// <exception cref="TurbopufferInvalidDataException">
    /// Thrown when the instance does not pass validation.
    /// </exception>
    /// </summary>
    public override void Validate()
    {
        if (this.Value == null)
        {
            throw new TurbopufferInvalidDataException("Data did not match any variant of Index");
        }
        this.Switch((upToDate) => upToDate.Validate(), (updating) => updating.Validate());
    }

    public virtual bool Equals(global::Turbopuffer.Client.Models.Namespaces.Index? other) =>
        other != null
        && this.VariantIndex() == other.VariantIndex()
        && JsonElement.DeepEquals(this.Json, other.Json);

    public override int GetHashCode()
    {
        return 0;
    }

    public override string ToString() =>
        JsonSerializer.Serialize(
            FriendlyJsonPrinter.PrintValue(this.Json),
            ModelBase.ToStringSerializerOptions
        );

    int VariantIndex()
    {
        return this.Value switch
        {
            IndexUpToDate _ => 0,
            IndexUpdating _ => 1,
            _ => -1,
        };
    }
}

sealed class IndexConverter : JsonConverter<global::Turbopuffer.Client.Models.Namespaces.Index>
{
    public override global::Turbopuffer.Client.Models.Namespaces.Index? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var element = JsonSerializer.Deserialize<JsonElement>(ref reader, options);
        try
        {
            var deserialized = JsonSerializer.Deserialize<IndexUpToDate>(element, options);
            if (deserialized != null)
            {
                deserialized.Validate();
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is TurbopufferInvalidDataException)
        {
            // ignore
        }

        try
        {
            var deserialized = JsonSerializer.Deserialize<IndexUpdating>(element, options);
            if (deserialized != null)
            {
                deserialized.Validate();
                return new(deserialized, element);
            }
        }
        catch (Exception e) when (e is JsonException || e is TurbopufferInvalidDataException)
        {
            // ignore
        }

        return new(element);
    }

    public override void Write(
        Utf8JsonWriter writer,
        global::Turbopuffer.Client.Models.Namespaces.Index value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value.Json, options);
    }
}

[JsonConverter(typeof(IndexUpToDateConverter))]
public record class IndexUpToDate
{
    public JsonElement Element { get; private init; }

    public IndexUpToDate()
    {
        Element = JsonSerializer.Deserialize<JsonElement>(
            """
            {
              "status": "up-to-date"
            }
            """
        );
    }

    internal IndexUpToDate(JsonElement element)
    {
        Element = element;
    }

    /// <summary>
    /// Validates that the instance's underlying value is the expected constant.
    ///
    /// <para>This is useful for instances constructed from raw JSON data (e.g. deserialized from an API response).</para>
    ///
    /// <exception cref="TurbopufferInvalidDataException">
    /// Thrown when the instance does not pass validation.
    /// </exception>
    /// </summary>
    public void Validate()
    {
        if (this != new IndexUpToDate())
        {
            throw new TurbopufferInvalidDataException("Invalid value given for 'IndexUpToDate'");
        }
    }

    public override int GetHashCode()
    {
        return 0;
    }

    public virtual bool Equals(IndexUpToDate? other)
    {
        if (other == null)
        {
            return false;
        }

        return JsonElement.DeepEquals(this.Element, other.Element);
    }
}

class IndexUpToDateConverter : JsonConverter<IndexUpToDate>
{
    public override IndexUpToDate? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        return new(JsonSerializer.Deserialize<JsonElement>(ref reader, options));
    }

    public override void Write(
        Utf8JsonWriter writer,
        IndexUpToDate value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value.Element, options);
    }
}

[JsonConverter(typeof(JsonModelConverter<IndexUpdating, IndexUpdatingFromRaw>))]
public sealed record class IndexUpdating : JsonModel
{
    public JsonElement Status
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<JsonElement>("status");
        }
        init { this._rawData.Set("status", value); }
    }

    /// <summary>
    /// The number of bytes in the namespace that are in the write-ahead log but
    /// have not yet been indexed.
    /// </summary>
    public required long UnindexedBytes
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<long>("unindexed_bytes");
        }
        init { this._rawData.Set("unindexed_bytes", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        if (!JsonElement.DeepEquals(this.Status, JsonSerializer.SerializeToElement("updating")))
        {
            throw new TurbopufferInvalidDataException("Invalid value given for constant");
        }
        _ = this.UnindexedBytes;
    }

    public IndexUpdating()
    {
        this.Status = JsonSerializer.SerializeToElement("updating");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public IndexUpdating(IndexUpdating indexUpdating)
        : base(indexUpdating) { }
#pragma warning restore CS8618

    public IndexUpdating(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);

        this.Status = JsonSerializer.SerializeToElement("updating");
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    IndexUpdating(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="IndexUpdatingFromRaw.FromRawUnchecked"/>
    public static IndexUpdating FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }

    [SetsRequiredMembers]
    public IndexUpdating(long unindexedBytes)
        : this()
    {
        this.UnindexedBytes = unindexedBytes;
    }
}

class IndexUpdatingFromRaw : IFromRawJson<IndexUpdating>
{
    /// <inheritdoc/>
    public IndexUpdating FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        IndexUpdating.FromRawUnchecked(rawData);
}

/// <summary>
/// Configuration for namespace pinning, along with the current status of the pinned namespace.
/// </summary>
[JsonConverter(
    typeof(JsonModelConverter<NamespaceMetadataPinning, NamespaceMetadataPinningFromRaw>)
)]
public sealed record class NamespaceMetadataPinning : JsonModel
{
    /// <summary>
    /// The number of read replicas to provision. Defaults to 1 if not specified.
    /// </summary>
    public long? Replicas
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableStruct<long>("replicas");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("replicas", value);
        }
    }

    /// <summary>
    /// Operational status for a pinned namespace.
    /// </summary>
    public Status? Status
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<Status>("status");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("status", value);
        }
    }

    public static implicit operator PinningConfig(
        NamespaceMetadataPinning namespaceMetadataPinning
    ) => new() { Replicas = namespaceMetadataPinning.Replicas };

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.Replicas;
        this.Status?.Validate();
    }

    public NamespaceMetadataPinning() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public NamespaceMetadataPinning(NamespaceMetadataPinning namespaceMetadataPinning)
        : base(namespaceMetadataPinning) { }
#pragma warning restore CS8618

    public NamespaceMetadataPinning(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    NamespaceMetadataPinning(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="NamespaceMetadataPinningFromRaw.FromRawUnchecked"/>
    public static NamespaceMetadataPinning FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class NamespaceMetadataPinningFromRaw : IFromRawJson<NamespaceMetadataPinning>
{
    /// <inheritdoc/>
    public NamespaceMetadataPinning FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    ) => NamespaceMetadataPinning.FromRawUnchecked(rawData);
}

[JsonConverter(typeof(JsonModelConverter<IntersectionMember1, IntersectionMember1FromRaw>))]
public sealed record class IntersectionMember1 : JsonModel
{
    /// <summary>
    /// Operational status for a pinned namespace.
    /// </summary>
    public Status? Status
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNullableClass<Status>("status");
        }
        init
        {
            if (value == null)
            {
                return;
            }

            this._rawData.Set("status", value);
        }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        this.Status?.Validate();
    }

    public IntersectionMember1() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public IntersectionMember1(IntersectionMember1 intersectionMember1)
        : base(intersectionMember1) { }
#pragma warning restore CS8618

    public IntersectionMember1(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    IntersectionMember1(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="IntersectionMember1FromRaw.FromRawUnchecked"/>
    public static IntersectionMember1 FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class IntersectionMember1FromRaw : IFromRawJson<IntersectionMember1>
{
    /// <inheritdoc/>
    public IntersectionMember1 FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        IntersectionMember1.FromRawUnchecked(rawData);
}

/// <summary>
/// Operational status for a pinned namespace.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<Status, StatusFromRaw>))]
public sealed record class Status : JsonModel
{
    /// <summary>
    /// The number of replicas that are warm and serving traffic.
    /// </summary>
    public required long ReadyReplicas
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<long>("ready_replicas");
        }
        init { this._rawData.Set("ready_replicas", value); }
    }

    /// <summary>
    /// The timestamp of the latest pinning status snapshot.
    /// </summary>
    public required DateTimeOffset UpdatedAt
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<DateTimeOffset>("updated_at");
        }
        init { this._rawData.Set("updated_at", value); }
    }

    /// <summary>
    /// Aggregate utilization for the pinned namespace, reported as a value between
    /// 0.0 and 1.0.
    /// </summary>
    public required double Utilization
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<double>("utilization");
        }
        init { this._rawData.Set("utilization", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.ReadyReplicas;
        _ = this.UpdatedAt;
        _ = this.Utilization;
    }

    public Status() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public Status(Status status)
        : base(status) { }
#pragma warning restore CS8618

    public Status(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    Status(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="StatusFromRaw.FromRawUnchecked"/>
    public static Status FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }
}

class StatusFromRaw : IFromRawJson<Status>
{
    /// <inheritdoc/>
    public Status FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        Status.FromRawUnchecked(rawData);
}
