using EasySharp.Location;
using EasySharp.Logs.CheckLogs.Core;
using EasySharp.Logs.CheckLogs.Core.Models;
using System;

namespace EasySharpStandardConsole.Logs.CheckLogs.Implementations
{
    internal class ConsoleCheckLogger<TErrorCode, TLocation> : ICheckLogger<TErrorCode, TLocation>
    {
        public void Write(CheckResultCategory category, CheckResult<TErrorCode, TLocation> result, ILocation location, string message)
        {
            Console.WriteLine(
                string.Join("\t", category, result, location.ToString(), message));
        }
    }
}
