using System.Net.Http;

namespace Turbopuffer.Client.Exceptions;

public class TurbopufferNotFoundException : Turbopuffer4xxException
{
    public TurbopufferNotFoundException(HttpRequestException? innerException = null)
        : base(innerException) { }
}
