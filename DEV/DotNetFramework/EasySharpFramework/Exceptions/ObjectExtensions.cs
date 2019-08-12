using System;

namespace EasySharpStandard.Exceptions
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