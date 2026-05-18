using System.Net.Http;

namespace Turbopuffer.Client.Exceptions;

public class TurbopufferBadRequestException : Turbopuffer4xxException
{
    public TurbopufferBadRequestException(HttpRequestException? innerException = null)
        : base(innerException) { }
}
