using EasySharpStandard.Logs.CheckLogs.Implementations;

namespace EasySharpStandard.Logs.CheckLogs.Core
{
    public static class CheckLoggerExtensions
    {
        public static ICheckLogger Resolve(this ICheckLogger checkLogger)
        {
            return checkLogger ?? new DefaultCheckLogger();
        }
    }
}
