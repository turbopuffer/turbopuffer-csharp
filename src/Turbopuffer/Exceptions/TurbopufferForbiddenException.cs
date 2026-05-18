using System.Net.Http;

namespace Turbopuffer.Exceptions;

public class TurbopufferForbiddenException : Turbopuffer4xxException
{
    public TurbopufferForbiddenException(HttpRequestException? innerException = null)
        : base(innerException) { }
}
