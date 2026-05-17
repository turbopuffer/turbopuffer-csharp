using System;
using System.Net.Http;

namespace Turbopuffer.Exceptions;

public class TurbopufferIOException : TurbopufferException
{
    public new HttpRequestException InnerException
    {
        get
        {
            if (base.InnerException == null)
            {
                throw new ArgumentNullException();
            }
            return (HttpRequestException)base.InnerException;
        }
    }

    public TurbopufferIOException(string message, HttpRequestException? innerException = null)
        : base(message, innerException) { }
}
