using System.Net.Http;

namespace Turbopuffer.Exceptions;

public class Turbopuffer4xxException : TurbopufferApiException
{
    public Turbopuffer4xxException(HttpRequestException? innerException = null)
        : base(innerException) { }
}
