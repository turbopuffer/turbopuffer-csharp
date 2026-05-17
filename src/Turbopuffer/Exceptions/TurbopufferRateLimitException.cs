using System.Net.Http;

namespace Turbopuffer.Exceptions;

public class TurbopufferRateLimitException : Turbopuffer4xxException
{
    public TurbopufferRateLimitException(HttpRequestException? innerException = null)
        : base(innerException) { }
}
