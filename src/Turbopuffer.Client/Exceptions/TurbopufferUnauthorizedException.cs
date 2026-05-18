using System.Net.Http;

namespace Turbopuffer.Client.Exceptions;

public class TurbopufferUnauthorizedException : Turbopuffer4xxException
{
    public TurbopufferUnauthorizedException(HttpRequestException? innerException = null)
        : base(innerException) { }
}
