using System.Net.Http;

namespace Turbopuffer.Client.Exceptions;

public class TurbopufferForbiddenException : Turbopuffer4xxException
{
    public TurbopufferForbiddenException(HttpRequestException? innerException = null)
        : base(innerException) { }
}
