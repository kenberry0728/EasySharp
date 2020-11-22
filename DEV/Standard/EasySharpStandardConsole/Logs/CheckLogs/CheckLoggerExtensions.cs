using EasySharp.Logs.CheckLogs.Core;
using EasySharpStandardConsole.Logs.CheckLogs.Implementations;

namespace EasySharpStandardConsole.Logs.CheckLogs
{
    public static class CheckLoggerExtensions
    {
        public static ICheckLogger<TErrorCode> Resolve<TErrorCode>(this ICheckLogger<TErrorCode> checkLogger)
        {
            return checkLogger ?? new ConsoleCheckLogger<TErrorCode>();
        }
    }
}
