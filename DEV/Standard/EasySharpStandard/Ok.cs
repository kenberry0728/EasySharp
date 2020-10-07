using System;

namespace EasySharp
{
    public class Ok<T> : Result<T> 
    {
        public Ok(T value)
            : base(value)
        {
        }
    }

    public class Ok : Result
    {
        public Ok()
            : base(true)
        {
        }
    }
}
