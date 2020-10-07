using System;

namespace EasySharp
{
    public abstract class Result<T>
    {
        protected Result(T value)
        {
            this.Ok = true;
            this.Value = value;
        }

        protected Result()
        {
            this.Ok = false;
        }

        protected Result(Exception exception)
        {
            this.Ok = false;
            this.Exception = exception;
        }

        public bool Ok { get; }

        public T Value { get; }

        public Exception Exception { get; }
    }

    public abstract class Result
    {
        protected Result(bool ok)
        {
            this.Ok = ok;
        }

        protected Result(Exception exception)
        {
            this.Ok = false;
            this.Exception = exception;
        }

        public bool Ok { get; }

        public Exception Exception { get; }
    }
}
