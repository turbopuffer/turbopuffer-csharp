using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Turbopuffer.Client.Exceptions;
using Threading = System.Threading;

namespace Turbopuffer.Client.Core;

public class HttpResponse : IDisposable
{
    public required HttpResponseMessage RawMessage { get; init; }

    public IEnumerable<KeyValuePair<string, IEnumerable<string>>> Headers
    {
        get { return RawMessage.Headers; }
    }

    public bool IsSuccessStatusCode
    {
        get { return RawMessage.IsSuccessStatusCode; }
    }

    public HttpStatusCode StatusCode
    {
        get { return RawMessage.StatusCode; }
    }

    public Threading::CancellationToken CancellationToken { get; init; } = default;

    public IEnumerable<string> GetHeaderValues(string name) => RawMessage.Headers.GetValues(name);

    public bool TryGetHeaderValues(
        string name,
        [NotNullWhen(true)] out IEnumerable<string>? values
    ) => RawMessage.Headers.TryGetValues(name, out values);

    public sealed override string ToString() => this.RawMessage.ToString();

    public override bool Equals(object? obj)
    {
        if (obj is not HttpResponse other)
        {
            return false;
        }

        return this.RawMessage.Equals(other.RawMessage);
    }

    public override int GetHashCode() => this.RawMessage.GetHashCode();

    public async Task<T> Deserialize<T>(Threading::CancellationToken cancellationToken = default)
    {
        using var cts = Threading::CancellationTokenSource.CreateLinkedTokenSource(
            this.CancellationToken,
            cancellationToken
        );
        try
        {
            return await JsonSerializer
                    .DeserializeAsync<T>(
                        await this.ReadAsStream(cts.Token).ConfigureAwait(false),
                        ModelBase.SerializerOptions,
                        cts.Token
                    )
                    .ConfigureAwait(false)
                ?? throw new TurbopufferInvalidDataException("Response cannot be null");
        }
        catch (HttpRequestException e)
        {
            throw new TurbopufferIOException("I/O Exception", e);
        }
    }

    public async Task<Stream> ReadAsStream(Threading::CancellationToken cancellationToken = default)
    {
        using var cts = Threading::CancellationTokenSource.CreateLinkedTokenSource(
            this.CancellationToken,
            cancellationToken
        );
        var stream = await RawMessage.Content.ReadAsStreamAsync(
#if NET
            cts.Token
#endif
        ).ConfigureAwait(false);
        return Decompress(stream);
    }

    public async Task<string> ReadAsString(Threading::CancellationToken cancellationToken = default)
    {
        // Read via ReadAsStream rather than Content.ReadAsStringAsync so that the
        // body is decompressed when the server returns Content-Encoding: gzip.
        using var stream = await this.ReadAsStream(cancellationToken).ConfigureAwait(false);
        using var reader = new StreamReader(stream, Encoding.UTF8);
        return await reader.ReadToEndAsync(
#if NET
            cancellationToken
#endif
        ).ConfigureAwait(false);
    }

    /// <summary>
    /// Wraps the raw response body in a decompressing stream when the server
    /// returned a compressed body. Automatic decompression is disabled on the
    /// default <see cref="HttpClient"/> so the SDK can honor
    /// <see cref="ClientOptions.Compression"/>; this restores transparent
    /// decompression for gzip responses.
    /// </summary>
    Stream Decompress(Stream stream)
    {
        var encodings = RawMessage.Content.Headers.ContentEncoding;
        // A user-supplied HttpClient with its own auto-decompressing handler will
        // have already stripped Content-Encoding, so an empty list is a no-op.
        if (encodings.Count == 0)
        {
            return stream;
        }

        // Only the last applied (outermost) encoding needs undoing here.
        var encoding = encodings.Last();
        if (string.Equals(encoding, "gzip", StringComparison.OrdinalIgnoreCase))
        {
            return new GZipStream(stream, CompressionMode.Decompress);
        }
        if (string.Equals(encoding, "identity", StringComparison.OrdinalIgnoreCase))
        {
            return stream;
        }

        throw new TurbopufferIOException(
            $"Cannot decode response with unsupported Content-Encoding '{encoding}'"
        );
    }

    public void Dispose()
    {
        this.RawMessage.Dispose();
        GC.SuppressFinalize(this);
    }
}

public sealed class HttpResponse<T> : HttpResponse
{
    readonly Func<Threading::CancellationToken, Task<T>> _deserialize;

    internal HttpResponse(Func<Threading::CancellationToken, Task<T>> deserialize)
    {
        this._deserialize = deserialize;
    }

    [SetsRequiredMembers]
    internal HttpResponse(
        HttpResponse response,
        Func<Threading::CancellationToken, Task<T>> deserialize
    )
        : this(deserialize)
    {
        this.RawMessage = response.RawMessage;
        this.CancellationToken = response.CancellationToken;
    }

    public Task<T> Deserialize(Threading::CancellationToken cancellationToken = default)
    {
        using var cts = Threading::CancellationTokenSource.CreateLinkedTokenSource(
            this.CancellationToken,
            cancellationToken
        );
        return this._deserialize(cts.Token);
    }
}

public sealed class StreamingHttpResponse<T> : HttpResponse
{
    readonly Func<Threading::CancellationToken, IAsyncEnumerable<T>> _enumerate;

    internal StreamingHttpResponse(
        Func<Threading::CancellationToken, IAsyncEnumerable<T>> enumerate
    )
    {
        this._enumerate = enumerate;
    }

    [SetsRequiredMembers]
    internal StreamingHttpResponse(
        HttpResponse response,
        Func<Threading::CancellationToken, IAsyncEnumerable<T>> enumerate
    )
        : this(enumerate)
    {
        this.RawMessage = response.RawMessage;
        this.CancellationToken = response.CancellationToken;
    }

    public async IAsyncEnumerable<T> Enumerate(
        [EnumeratorCancellationAttribute] Threading::CancellationToken cancellationToken = default
    )
    {
        using var cts = Threading::CancellationTokenSource.CreateLinkedTokenSource(
            this.CancellationToken,
            cancellationToken
        );
        await foreach (var item in this._enumerate(cts.Token))
        {
            yield return item;
        }
    }
}
