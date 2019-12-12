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
}
