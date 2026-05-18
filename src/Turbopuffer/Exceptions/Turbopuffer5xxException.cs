using System.Net.Http;

namespace Turbopuffer.Exceptions;

public class Turbopuffer5xxException : TurbopufferApiException
{
    public Turbopuffer5xxException(HttpRequestException? innerException = null)
        : base(innerException) { }
}
