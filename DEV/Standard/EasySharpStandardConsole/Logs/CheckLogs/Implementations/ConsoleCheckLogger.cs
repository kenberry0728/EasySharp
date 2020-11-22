using EasySharp.Logs.CheckLogs.Core;
using EasySharp.Logs.CheckLogs.Core.Models;
using System;

namespace EasySharpStandardConsole.Logs.CheckLogs.Implementations
{
    internal class ConsoleCheckLogger<TErrorCode, TLocation> : ICheckLogger<TErrorCode, TLocation>
    {
        public void Write(CheckResult<TErrorCode, TLocation> result, string message)
        {
            Console.WriteLine(
                string.Join("\t", result, message));
        }
    }
}
