using EasySharp.IO;
using EasySharp.Location;
using System;
using System.Runtime.CompilerServices;

namespace EasySharp.Runtime.CompilerServices
{
    public static class StringExtensions
    {
        public static void ThrowArgumentExceptionIfNullOrEmpty(
            this string argument,
            string argumentName,
            [CallerFilePath] string callerFilePath = "",
            [CallerLineNumber] int callerLineNumber = -1,
            [CallerMemberName] string callerMemberName = "")
        {
            if (argument.IsNullOrEmpty())
            {
                var location = new TextFileLocation(callerFilePath.ToFilePath(), callerLineNumber, 0);
                var message = new string[] { argumentName, location.ToString(), callerMemberName }.Join("\t");
                throw new ArgumentException(message);
            }
        }
    }
}
