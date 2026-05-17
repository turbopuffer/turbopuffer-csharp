using System.Net.Http;

namespace Turbopuffer.Exceptions;

public class TurbopufferUnexpectedStatusCodeException : TurbopufferApiException
{
    public TurbopufferUnexpectedStatusCodeException(HttpRequestException? innerException = null)
        : base(innerException) { }
}
