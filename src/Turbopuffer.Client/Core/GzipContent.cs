using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Turbopuffer.Client.Core;

/// <summary>
/// Wraps an <see cref="HttpContent"/> and gzip-compresses it as it is written to
/// the wire. The inner content's headers are copied through and
/// <c>Content-Encoding: gzip</c> is added.
/// </summary>
sealed class GzipContent : HttpContent
{
    readonly HttpContent _inner;

    internal GzipContent(HttpContent inner)
    {
        _inner = inner;
        foreach (var header in inner.Headers)
        {
            Headers.TryAddWithoutValidation(header.Key, header.Value);
        }
        // The compressed length is not known up front, so drop any inherited
        // Content-Length and let the transport use chunked encoding.
        Headers.ContentLength = null;
        Headers.ContentEncoding.Add("gzip");
    }

    protected override async Task SerializeToStreamAsync(Stream stream, TransportContext? context)
    {
        using var gzip = new GZipStream(stream, CompressionMode.Compress, leaveOpen: true);
        await _inner.CopyToAsync(gzip).ConfigureAwait(false);
    }

    protected override bool TryComputeLength(out long length)
    {
        length = -1;
        return false;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _inner.Dispose();
        }
        base.Dispose(disposing);
    }
}
