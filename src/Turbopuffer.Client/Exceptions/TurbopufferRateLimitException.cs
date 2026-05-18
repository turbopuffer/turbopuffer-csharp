using System.Net.Http;

namespace Turbopuffer.Client.Exceptions;

public class TurbopufferRateLimitException : Turbopuffer4xxException
{
    public TurbopufferRateLimitException(HttpRequestException? innerException = null)
        : base(innerException) { }
}
