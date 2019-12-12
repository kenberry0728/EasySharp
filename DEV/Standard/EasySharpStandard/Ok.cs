using System;

namespace EasySharp
{
    public class Ok<T> : Result<T> 
    {
        public Ok(T value)
            : base(true, value)
        {
        }
    }
}
