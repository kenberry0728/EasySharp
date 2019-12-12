using System;

namespace EasySharp
{
    public class Result<T>
    {
        public Result(bool ok, T value)
        {
            this.Ok = ok;
            this.Value = value;
        }

        public bool Ok { get; }

        public T Value { get; }
    }

    public class Ok<T> : Result<T> 
    {
        public Ok(T value)
            : base(true, value)
        {
        }
    }

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
