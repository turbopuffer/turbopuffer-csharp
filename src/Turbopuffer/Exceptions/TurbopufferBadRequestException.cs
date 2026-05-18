using System.Net.Http;

namespace Turbopuffer.Exceptions;

public class TurbopufferBadRequestException : Turbopuffer4xxException
{
    public TurbopufferBadRequestException(HttpRequestException? innerException = null)
        : base(innerException) { }
}
