using System;

namespace EasySharp
{
    public class Err<T> : Result<T>
    {
        public Err(T value, Exception exception)
            : base(false, value)
        {
            this.Exception = exception;
        }

        public Exception Exception { get; }
    }
}
