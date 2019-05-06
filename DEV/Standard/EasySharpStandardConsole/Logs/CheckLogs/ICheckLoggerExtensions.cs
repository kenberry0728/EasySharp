using EasySharpStandard.Logs.CheckLogs.Core;
using EasySharpStandardConsole.Logs.CheckLogs.Implementations;

namespace EasySharpStandardConsole.Logs.CheckLogs
{
    public static class ICheckLoggerExtensions
    {
        public static ICheckLogger Resolve(this ICheckLogger checkLogger)
        {
            return checkLogger ?? new ConsoleCheckLogger();
        }
    }
}
