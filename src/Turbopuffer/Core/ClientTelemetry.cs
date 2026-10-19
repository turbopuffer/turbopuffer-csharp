using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;

namespace Turbopuffer.Core;

/// <summary>
/// Client telemetry reported to turbopuffer on subsequent requests.
///
/// <para>When enabled, the client buffers a small timing record for each request and attaches
/// buffered records to a later request in the <c>X-turbopuffer-Client-Telemetry</c> header. The
/// server controls how many requests are recorded by advertising a sample rate in the
/// <c>X-turbopuffer-Client-Telemetry-Sample-Rate</c> response header.</para>
/// </summary>
internal static partial class ClientTelemetry
{
    internal const string EnvVar = "TURBOPUFFER_TELEMETRY";
    internal const string Header = "X-turbopuffer-Client-Telemetry";
    internal const string SampleRateHeader = "X-turbopuffer-Client-Telemetry-Sample-Rate";

    internal const int Version = 1;

    // Keeps the header well under the 8 KB per-line limit of common proxies.
    internal const int MaxRecordsPerHeader = 20;
    internal const int MaxBufferedRecords = 1000;

    static readonly HashSet<string> TrueValues = ["1", "true", "yes", "on"];
    static readonly HashSet<string> FalseValues = ["", "0", "false", "no", "off"];

    const string NamespacePatternText = @"^(/v\d+/namespaces/)[^/?]+";
    const string OperationPatternText = @"(/operations/)[^/?]+";

#if NET
    [GeneratedRegex(NamespacePatternText)]
    private static partial Regex NamespacePattern();

    [GeneratedRegex(OperationPatternText)]
    private static partial Regex OperationPattern();
#else
    static readonly Regex NamespaceRegex = new(NamespacePatternText);
    static readonly Regex OperationRegex = new(OperationPatternText);

    static Regex NamespacePattern() => NamespaceRegex;

    static Regex OperationPattern() => OperationRegex;
#endif

    static readonly object SharedLock = new();
    static readonly Dictionary<
        (string, string),
        WeakReference<TelemetryObserver>
    > SharedObservers = [];

    /// <summary>
    /// Resolves whether telemetry is enabled, preferring an explicit value over the environment.
    /// </summary>
    internal static bool ResolveEnabled(bool? value) =>
        value ?? ParseEnabled(Environment.GetEnvironmentVariable(EnvVar));

    internal static bool ParseEnabled(string? raw)
    {
        var value = (raw ?? "").Trim().ToLowerInvariant();
        if (TrueValues.Contains(value))
        {
            return true;
        }
        if (!FalseValues.Contains(value))
        {
            Trace.TraceWarning("Ignoring invalid {0}={1}, expected 1 or 0", EnvVar, raw);
        }
        return false;
    }

    /// <summary>
    /// Returns the method and path of a request with identifiers like the namespace replaced. The
    /// path keeps its own query, which tells some operations apart.
    /// </summary>
    internal static string RouteTemplate(string method, string url)
    {
        var path = url;
        if (path.Contains("://") && Uri.TryCreate(path, UriKind.Absolute, out var uri))
        {
            path = uri.PathAndQuery;
        }
        path = NamespacePattern().Replace(path, "${1}{ns}", 1);
        path = OperationPattern().Replace(path, "${1}{token}");
        return $"{method.ToUpperInvariant()} {path}";
    }

    /// <summary>
    /// Returns options observed by the telemetry observer shared by clients with the same API key
    /// and base URL, or without it if telemetry is disabled. An observer installed by other code is
    /// kept.
    ///
    /// <para>Sharing lets records ride on any client's requests, including copies, but never to
    /// another org or region.</para>
    /// </summary>
    internal static ClientOptions Install(ClientOptions options)
    {
        if (options.RequestObserver is not (null or TelemetryObserver))
        {
            return options;
        }
        options.RequestObserver = options.TelemetryEnabled ? SharedObserver(options) : null;
        return options;
    }

    static TelemetryObserver? SharedObserver(ClientOptions options)
    {
        (string, string) key;
        try
        {
            key = (options.ApiKey, options.BaseUrl);
        }
        catch (Exception)
        {
            // Requests fail without these anyway.
            return null;
        }
        lock (SharedLock)
        {
            if (
                SharedObservers.TryGetValue(key, out var reference)
                && reference.TryGetTarget(out var observer)
            )
            {
                return observer;
            }
            // Clients that are gone no longer need their observers.
            foreach (
                var dead in SharedObservers
                    .Where(entry => !entry.Value.TryGetTarget(out _))
                    .ToList()
            )
            {
                SharedObservers.Remove(dead.Key);
            }
            observer = new(new());
            SharedObservers[key] = new(observer);
            return observer;
        }
    }

    /// <summary>
    /// Returns the duration in nanoseconds of each client-side phase of an attempt ending at
    /// <paramref name="end"/>, in the order they're reported. Phases that weren't observed are left
    /// out.
    /// </summary>
    internal static List<(string Name, long Nanoseconds)> ComputeDurations(
        Timings timings,
        long end
    )
    {
        List<(string, long)> durations = [];
        if (timings.AttemptBegin is not { } attemptBegin)
        {
            return durations;
        }
        durations.Add(("total", end - attemptBegin));

        if (
            timings.SerializeBegin is { } serializeBegin
            && timings.SerializeEnd is { } serializeEnd
        )
        {
            durations.Add(("serialize", serializeEnd - serializeBegin));
        }

        long? compressBegin = timings.Extra.TryGetValue("compress_begin", out var start)
            ? start
            : null;
        long? compressEnd = timings.Extra.TryGetValue("compress_end", out var stop) ? stop : null;
        if (compressBegin != null && compressEnd != null)
        {
            durations.Add(("compress", compressEnd.Value - compressBegin.Value));
        }

        // Waiting for a free connection, until connecting a new one or sending on a reused one.
        var poolBegin = compressEnd ?? timings.RequestReady;
        var connectionReady = timings.ConnectBegin ?? timings.SendBegin;
        if (poolBegin != null && connectionReady != null)
        {
            durations.Add(("pool", Math.Max(connectionReady.Value - poolBegin.Value, 0)));
        }

        if (timings.ConnectBegin is { } connectBegin && timings.ConnectEnd is { } connectEnd)
        {
            durations.Add(("connect", connectEnd - connectBegin));
        }

        if (timings.SendBegin is { } sendBegin && timings.SendEnd is { } sendEnd)
        {
            durations.Add(("send", sendEnd - sendBegin));
        }

        // Covers the network round trip and the server. Some clients finish sending the body after
        // the response headers arrive.
        if (timings.SendEnd is { } sent && timings.ResponseHeaders is { } headers)
        {
            durations.Add(("wait", Math.Max(headers - sent, 0)));
        }

        if (timings.BodyEnd is { } bodyEnd && timings.ResponseHeaders is { } responded)
        {
            durations.Add(("body_read", bodyEnd - responded));
        }

        if (
            timings.DeserializeBegin is { } deserializeBegin
            && timings.DeserializeEnd is { } deserializeEnd
        )
        {
            durations.Add(("deserialize", deserializeEnd - deserializeBegin));
        }

        return durations;
    }

    /// <summary>
    /// Returns <c>performance.server_total_ms</c> from a deserialized response, if it has one.
    /// </summary>
    internal static double? ServerTotalMs(object? parsed)
    {
        if (
            parsed is JsonModel model
            && model.RawData.TryGetValue("performance", out var performance)
            && performance.ValueKind == JsonValueKind.Object
            && performance.TryGetProperty("server_total_ms", out var serverTotalMs)
            && serverTotalMs.ValueKind == JsonValueKind.Number
        )
        {
            return serverTotalMs.GetDouble();
        }
        return null;
    }
}

/// <summary>
/// One request's telemetry, as sent to the server.
/// </summary>
internal sealed class TelemetryRecord
{
    public required string Route { get; init; }

    public required int Status { get; init; }

    public string? Error { get; init; }

    public int Retries { get; init; }

    public double? SampleRate { get; init; }

    /// <summary>
    /// Phase durations in whole microseconds.
    /// </summary>
    public required List<(string Name, long Microseconds)> Durations { get; init; }

    public long? ServerTotalUs { get; init; }

    public bool? Reused { get; init; }
}

/// <summary>
/// Records taken from a buffer, encoded as a header value.
/// </summary>
internal sealed class TelemetryBatch
{
    internal TelemetryBatch(List<(long RecordedAt, TelemetryRecord Record)> records, long dropped)
    {
        this.Records = records;
        this.Dropped = dropped;

        var now = Timings.Now();
        using MemoryStream stream = new();
        using (Utf8JsonWriter writer = new(stream))
        {
            writer.WriteStartObject();
            writer.WriteNumber("version", ClientTelemetry.Version);
            writer.WriteStartArray("records");
            foreach (var (recordedAt, record) in records)
            {
                Write(writer, record, ageMs: (long)Math.Round((now - recordedAt) / 1e6));
            }
            writer.WriteEndArray();
            if (dropped > 0)
            {
                writer.WriteNumber("dropped", dropped);
            }
            writer.WriteEndObject();
        }
        this.Header = Encoding.UTF8.GetString(stream.ToArray());
    }

    internal List<(long RecordedAt, TelemetryRecord Record)> Records { get; }

    internal long Dropped { get; }

    internal string Header { get; }

    static void Write(Utf8JsonWriter writer, TelemetryRecord record, long ageMs)
    {
        writer.WriteStartObject();
        writer.WriteString("route", record.Route);
        writer.WriteNumber("status", record.Status);
        if (record.Error != null)
        {
            writer.WriteString("error", record.Error);
        }
        if (record.Retries != 0)
        {
            writer.WriteNumber("retries", record.Retries);
        }
        if (record.SampleRate is { } sampleRate)
        {
            writer.WriteNumber("sample_rate", sampleRate);
        }
        foreach (var (name, microseconds) in record.Durations)
        {
            writer.WriteNumber($"{name}_us", microseconds);
        }
        if (record.ServerTotalUs is { } serverTotalUs)
        {
            writer.WriteNumber("server_total_us", serverTotalUs);
        }
        if (record.Reused is { } reused)
        {
            writer.WriteBoolean("reused", reused);
        }
        writer.WriteNumber("age_ms", ageMs);
        writer.WriteEndObject();
    }
}

/// <summary>
/// Buffers per-request telemetry records until a later request can carry them.
/// </summary>
internal sealed class TelemetryBuffer
{
#if NET
    static double DefaultRandom() => System.Random.Shared.NextDouble();
#else
    static readonly ThreadLocal<System.Random> ThreadRandom = new(() => new());

    static double DefaultRandom() => ThreadRandom.Value!.NextDouble();
#endif

    readonly object _lock = new();
    readonly LinkedList<(long RecordedAt, TelemetryRecord Record)> _records = new();
    long _dropped;

    // Used until the server advertises a rate.
    double _sampleRate = 1.0;

    /// <summary>
    /// Returns a number from 0 to 1 to sample with. Tests replace it.
    /// </summary>
    internal Func<double> Random { get; set; } = DefaultRandom;

    internal double SampleRate
    {
        get { return Volatile.Read(ref _sampleRate); }
    }

    /// <summary>
    /// Adopts the sample rate advertised by the server, ignoring invalid values.
    /// </summary>
    internal void ObserveResponse(HttpResponseMessage response)
    {
        if (
            !response.Headers.TryGetValues(ClientTelemetry.SampleRateHeader, out var values)
            || !double.TryParse(
                values.FirstOrDefault(),
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out var rate
            )
        )
        {
            return;
        }
        // Also rejects NaN, since every comparison with NaN is false.
        if (rate >= 0.0 && rate <= 1.0)
        {
            Volatile.Write(ref _sampleRate, rate);
        }
    }

    /// <summary>
    /// Samples and buffers a record for one request. <paramref name="durations"/> are in
    /// nanoseconds.
    /// </summary>
    internal void Record(
        string method,
        string url,
        int status,
        int retries,
        IEnumerable<(string Name, long Nanoseconds)> durations,
        double? serverTotalMs = null,
        string? error = null,
        bool? reused = null
    )
    {
        var rate = this.SampleRate;
        if (rate <= 0.0 || (rate < 1.0 && this.Random() >= rate))
        {
            return;
        }

        List<(string, long)> microseconds = [];
        foreach (var (name, nanoseconds) in durations)
        {
            // A zero compress duration means the body wasn't compressed.
            if (name == "compress" && nanoseconds == 0)
            {
                continue;
            }
            microseconds.Add((name, FloorDiv(nanoseconds, 1000)));
        }
        TelemetryRecord record = new()
        {
            Route = ClientTelemetry.RouteTemplate(method, url),
            Status = status,
            Error = error,
            Retries = retries,
            SampleRate = rate < 1.0 ? rate : null,
            Durations = microseconds,
            ServerTotalUs = serverTotalMs is { } ms ? (long)Math.Round(ms * 1000) : null,
            Reused = reused,
        };

        lock (_lock)
        {
            if (_records.Count >= ClientTelemetry.MaxBufferedRecords)
            {
                _records.RemoveFirst();
                _dropped++;
            }
            _records.AddLast((Timings.Now(), record));
        }
    }

    /// <summary>
    /// Removes up to <see cref="ClientTelemetry.MaxRecordsPerHeader"/> of the oldest records.
    /// </summary>
    internal TelemetryBatch? Take()
    {
        List<(long, TelemetryRecord)> records;
        long dropped;
        lock (_lock)
        {
            if (_records.Count == 0)
            {
                return null;
            }
            var count = Math.Min(_records.Count, ClientTelemetry.MaxRecordsPerHeader);
            records = new(count);
            for (var i = 0; i < count; i++)
            {
                records.Add(_records.First!.Value);
                _records.RemoveFirst();
            }
            dropped = _dropped;
            _dropped = 0;
        }
        return new(records, dropped);
    }

    /// <summary>
    /// Returns a batch that never reached the server to the front of the buffer.
    /// </summary>
    internal void Restore(TelemetryBatch batch)
    {
        lock (_lock)
        {
            _dropped += batch.Dropped;
            for (var i = batch.Records.Count - 1; i >= 0; i--)
            {
                // The batch holds the oldest records, so they're the ones to drop when full.
                if (_records.Count >= ClientTelemetry.MaxBufferedRecords)
                {
                    _dropped++;
                    continue;
                }
                _records.AddFirst(batch.Records[i]);
            }
        }
    }

    static long FloorDiv(long a, long b) => a >= 0 ? a / b : -((-a + b - 1) / b);
}

/// <summary>
/// Records telemetry for a client's requests.
/// </summary>
internal sealed class TelemetryObserver(TelemetryBuffer buffer) : RequestObserver
{
    internal TelemetryBuffer Buffer { get; } = buffer;

    public override RequestScope? Start(RequestInfo info) => new Scope(this.Buffer, info);

    sealed class Scope(TelemetryBuffer buffer, RequestInfo info) : RequestScope
    {
        // Kept for later attempts until one of them may have reached the server.
        TelemetryBatch? _batch;

        public override void AttemptBegin(int attempt, HttpRequestMessage request)
        {
            // Taken only once a request is about to be sent, so one that fails before then can't lose it.
            if (attempt == 0)
            {
                _batch = buffer.Take();
            }
            if (_batch != null)
            {
                request.Headers.Remove(ClientTelemetry.Header);
                request.Headers.TryAddWithoutValidation(ClientTelemetry.Header, _batch.Header);
            }
        }

        public override void AttemptEnd(AttemptResult result)
        {
            if (result.ReachedServer != false)
            {
                _batch = null;
            }
            if (result.Response != null)
            {
                buffer.ObserveResponse(result.Response);
            }
        }

        public override void Finish(RequestResult result)
        {
            // A batch that never reached the server goes back for a later request.
            if (_batch != null)
            {
                buffer.Restore(_batch);
                _batch = null;
            }

            var timings = result.Timings;
            var durations = ClientTelemetry.ComputeDurations(
                timings,
                timings.DeserializeEnd ?? Timings.Now()
            );
            buffer.Record(
                method: info.Method.Method,
                url: info.Url,
                status: result.Response != null ? (int)result.Response.StatusCode : 0,
                retries: result.Retries,
                durations: durations,
                serverTotalMs: ClientTelemetry.ServerTotalMs(result.Parsed),
                error: result.ErrorKind switch
                {
                    null => null,
                    ErrorKind.Canceled => "canceled",
                    ErrorKind.Other => "other",
                    ErrorKind.PoolTimeout
                    or ErrorKind.ConnectTimeout
                    or ErrorKind.WriteTimeout
                    or ErrorKind.ReadTimeout
                    or ErrorKind.Timeout => "timeout",
                    _ => "connect",
                },
                reused: timings.Reused
            );
        }
    }
}
