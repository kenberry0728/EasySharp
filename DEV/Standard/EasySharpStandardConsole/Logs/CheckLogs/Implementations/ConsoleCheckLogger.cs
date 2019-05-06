using EasySharpStandard.Locations.Core;
using EasySharpStandard.Logs.CheckLogs.Core;
using EasySharpStandard.Logs.CheckLogs.Core.Models;
using System;

namespace EasySharpStandardConsole.Logs.CheckLogs.Implementations
{
    internal class ConsoleCheckLogger : ICheckLogger
    {
        public ConsoleCheckLogger(){}

        public void Write(CheckResultCategories category, CheckResutl code, ILocation location, string message)
        {
            Console.WriteLine(
                string.Join("\t", category, code, location.LocationText, message));
        }
    }
}
