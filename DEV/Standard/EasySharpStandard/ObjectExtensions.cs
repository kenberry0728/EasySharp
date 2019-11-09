using System;

namespace EasySharp
{
    public static class ObjectExtensions
    {
        public static void ThrowArgumentNullException(this object argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}