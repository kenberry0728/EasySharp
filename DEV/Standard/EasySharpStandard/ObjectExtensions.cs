using System;
using System.Collections.Generic;
using System.Linq;

namespace EasySharp
{
    public static class ObjectExtensions
    {
        public static void ThrowExceptionIfNull<T>(this T argument, string argumentName)
            where T : class
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void ThrowArgumentNullOrEmptyException<T>(this IEnumerable<T> argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }

            if (!argument.Any())
            {
                throw new ArgumentException(argumentName);
            }
        }
    }
}