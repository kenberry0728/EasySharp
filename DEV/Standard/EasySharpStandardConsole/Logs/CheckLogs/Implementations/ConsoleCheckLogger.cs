using EasySharp.Location;
using EasySharp.Logs.CheckLogs.Core;
using EasySharp.Logs.CheckLogs.Core.Models;
using System;

namespace EasySharpStandardConsole.Logs.CheckLogs.Implementations
{
    internal class ConsoleCheckLogger : ICheckLogger
    {
        public void Write(CheckResultCategory category, CheckResult code, ILocation location, string message)
        {
            Console.WriteLine(
                string.Join("\t", category, code, location.ToString(), message));
        }
    }
}
