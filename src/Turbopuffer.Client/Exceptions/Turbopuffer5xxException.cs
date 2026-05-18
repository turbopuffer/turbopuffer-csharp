using System.Net.Http;

namespace Turbopuffer.Client.Exceptions;

public class Turbopuffer5xxException : TurbopufferApiException
{
    public Turbopuffer5xxException(HttpRequestException? innerException = null)
        : base(innerException) { }
}
