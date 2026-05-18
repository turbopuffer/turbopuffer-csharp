using System.Net.Http;

namespace Turbopuffer.Exceptions;

public class TurbopufferUnprocessableEntityException : Turbopuffer4xxException
{
    public TurbopufferUnprocessableEntityException(HttpRequestException? innerException = null)
        : base(innerException) { }
}
