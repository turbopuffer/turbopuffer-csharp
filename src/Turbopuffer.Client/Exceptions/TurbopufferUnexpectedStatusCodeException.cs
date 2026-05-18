using System.Net.Http;

namespace Turbopuffer.Client.Exceptions;

public class TurbopufferUnexpectedStatusCodeException : TurbopufferApiException
{
    public TurbopufferUnexpectedStatusCodeException(HttpRequestException? innerException = null)
        : base(innerException) { }
}
