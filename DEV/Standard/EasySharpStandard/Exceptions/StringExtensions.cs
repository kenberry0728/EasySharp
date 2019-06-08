using System;

namespace EasySharpStandard.Exceptions
{
    public static class StringExtensions
    {
        public static void ThrowArgumentNullOrEmptyException(this string argument, string argumentName)
        {
            if (string.IsNullOrEmpty(argumentName))
            {
                throw new ArgumentException(argumentName);
            }
        }
    }
}