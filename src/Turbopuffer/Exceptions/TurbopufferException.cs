using System;
using System.Net.Http;

namespace Turbopuffer.Exceptions;

public class TurbopufferException : Exception
{
    public TurbopufferException(string message, Exception? innerException = null)
        : base(message, innerException) { }

    protected TurbopufferException(HttpRequestException? innerException)
        : base(null, innerException) { }
}
