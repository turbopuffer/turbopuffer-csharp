using System.Net.Http;

namespace Turbopuffer.Client.Exceptions;

public class TurbopufferUnprocessableEntityException : Turbopuffer4xxException
{
    public TurbopufferUnprocessableEntityException(HttpRequestException? innerException = null)
        : base(innerException) { }
}
