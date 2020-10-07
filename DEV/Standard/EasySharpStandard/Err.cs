using System;

namespace EasySharp
{
    public class Err<T> : Result<T>
    {
        public Err()
        {
        }

        public Err(Exception exception)
            : base(exception)
        {
        }
    }

    public class Err : Result
    {
        public Err()
            : base(false)
        {
        }

        public Err(Exception exception)
            : base(exception)
        {
        }
    }
}
