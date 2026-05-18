using System.Net.Http;

namespace Turbopuffer.Client.Exceptions;

public class Turbopuffer4xxException : TurbopufferApiException
{
    public Turbopuffer4xxException(HttpRequestException? innerException = null)
        : base(innerException) { }
}
