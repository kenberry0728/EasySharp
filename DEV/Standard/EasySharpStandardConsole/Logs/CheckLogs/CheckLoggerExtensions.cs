using EasySharp.Logs.CheckLogs.Core;
using EasySharpStandardConsole.Logs.CheckLogs.Implementations;

namespace EasySharpStandardConsole.Logs.CheckLogs
{
    public static class CheckLoggerExtensions
    {
        public static ICheckLogger<TErrorCode, TLocation> Resolve<TErrorCode, TLocation>(this ICheckLogger<TErrorCode, TLocation> checkLogger)
        {
            return checkLogger ?? new ConsoleCheckLogger<TErrorCode, TLocation>();
        }
    }
}
