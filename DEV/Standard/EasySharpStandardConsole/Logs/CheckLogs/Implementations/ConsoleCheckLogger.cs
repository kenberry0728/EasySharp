using EasySharp.Location;
using EasySharp.Logs.CheckLogs.Core;
using EasySharp.Logs.CheckLogs.Core.Models;
using System;

namespace EasySharpStandardConsole.Logs.CheckLogs.Implementations
{
    internal class ConsoleCheckLogger<TErrorCode> : ICheckLogger<TErrorCode>
    {
        public void Write(CheckResultCategory category, CheckResult<TErrorCode> code, ILocation location, string message)
        {
            Console.WriteLine(
                string.Join("\t", category, code, location.ToString(), message));
        }
    }
}
