using System.Collections.Frozen;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Turbopuffer.Client.Core;

namespace Turbopuffer.Client.Models.Namespaces;

/// <summary>
/// The performance information for a write request.
/// </summary>
[JsonConverter(typeof(JsonModelConverter<WritePerformance, WritePerformanceFromRaw>))]
public sealed record class WritePerformance : JsonModel
{
    /// <summary>
    /// Request time measured on the server, in milliseconds.
    /// </summary>
    public required long ServerTotalMs
    {
        get
        {
            this._rawData.Freeze();
            return this._rawData.GetNotNullStruct<long>("server_total_ms");
        }
        init { this._rawData.Set("server_total_ms", value); }
    }

    /// <inheritdoc/>
    public override void Validate()
    {
        _ = this.ServerTotalMs;
    }

    public WritePerformance() { }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    public WritePerformance(WritePerformance writePerformance)
        : base(writePerformance) { }
#pragma warning restore CS8618

    public WritePerformance(IReadOnlyDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }

#pragma warning disable CS8618
    [SetsRequiredMembers]
    WritePerformance(FrozenDictionary<string, JsonElement> rawData)
    {
        this._rawData = new(rawData);
    }
#pragma warning restore CS8618

    /// <inheritdoc cref="WritePerformanceFromRaw.FromRawUnchecked"/>
    public static WritePerformance FromRawUnchecked(
        IReadOnlyDictionary<string, JsonElement> rawData
    )
    {
        return new(FrozenDictionary.ToFrozenDictionary(rawData));
    }

    [SetsRequiredMembers]
    public WritePerformance(long serverTotalMs)
        : this()
    {
        this.ServerTotalMs = serverTotalMs;
    }
}

class WritePerformanceFromRaw : IFromRawJson<WritePerformance>
{
    /// <inheritdoc/>
    public WritePerformance FromRawUnchecked(IReadOnlyDictionary<string, JsonElement> rawData) =>
        WritePerformance.FromRawUnchecked(rawData);
}
