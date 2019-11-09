using EasySharp.Logs.CheckLogs.Implementations;

namespace EasySharp.Logs.CheckLogs.Core
{
    public static class CheckLoggerExtensions
    {
        public static ICheckLogger Resolve(this ICheckLogger checkLogger)
        {
            return checkLogger ?? new DefaultCheckLogger();
        }
    }
}
