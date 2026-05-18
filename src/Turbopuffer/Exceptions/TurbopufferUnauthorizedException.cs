using System.Net.Http;

namespace Turbopuffer.Exceptions;

public class TurbopufferUnauthorizedException : Turbopuffer4xxException
{
    public TurbopufferUnauthorizedException(HttpRequestException? innerException = null)
        : base(innerException) { }
}
