using EasySharp.Logs.CheckLogs.Core;
using EasySharpStandardConsole.Logs.CheckLogs.Implementations;

namespace EasySharpStandardConsole.Logs.CheckLogs
{
    public static class CheckLoggerExtensions
    {
        public static ICheckLogger Resolve(this ICheckLogger checkLogger)
        {
            return checkLogger ?? new ConsoleCheckLogger();
        }
    }
}
