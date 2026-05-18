using System.Net.Http;

namespace Turbopuffer.Exceptions;

public class TurbopufferNotFoundException : Turbopuffer4xxException
{
    public TurbopufferNotFoundException(HttpRequestException? innerException = null)
        : base(innerException) { }
}
