using System;

namespace Turbopuffer.Client.Exceptions;

public class TurbopufferInvalidDataException : TurbopufferException
{
    public TurbopufferInvalidDataException(string message, Exception? innerException = null)
        : base(message, innerException) { }
}
