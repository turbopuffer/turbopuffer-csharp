using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Turbopuffer.Core;

/// <summary>
/// An <see cref="HttpContent"/> gzip-compressed up front, so that compressing is timed apart from
/// sending and the compressed length is known. The inner content's headers are copied through and
/// <c>Content-Encoding: gzip</c> is added.
/// </summary>
sealed class GzipContent : HttpContent
{
    readonly MemoryStream _compressed;

    GzipContent(HttpContent inner, MemoryStream compressed)
    {
        _compressed = compressed;
        foreach (var header in inner.Headers)
        {
            // The compressed length replaces the inner one.
            if (!string.Equals(header.Key, "Content-Length", StringComparison.OrdinalIgnoreCase))
            {
                Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
        }
        Headers.ContentEncoding.Add("gzip");
    }

    /// <summary>
    /// Compresses the inner content, which is disposed.
    /// </summary>
    internal static async Task<GzipContent> Compress(HttpContent inner)
    {
        MemoryStream compressed = new();
        using (GZipStream gzip = new(compressed, CompressionMode.Compress, leaveOpen: true))
        {
            await inner.CopyToAsync(gzip).ConfigureAwait(false);
        }
        GzipContent content = new(inner, compressed);
        inner.Dispose();
        return content;
    }

    protected override Task SerializeToStreamAsync(Stream stream, TransportContext? context) =>
        stream.WriteAsync(_compressed.GetBuffer(), 0, (int)_compressed.Length);

    protected override bool TryComputeLength(out long length)
    {
        length = _compressed.Length;
        return true;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _compressed.Dispose();
        }
        base.Dispose(disposing);
    }
}
