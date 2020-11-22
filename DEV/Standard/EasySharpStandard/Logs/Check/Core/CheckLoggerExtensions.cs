using EasySharp.Logs.CheckLogs.Implementations;

namespace EasySharp.Logs.CheckLogs.Core
{
    public static class CheckLoggerExtensions
    {
        public static ICheckLogger<TErrorCode> Resolve<TErrorCode>(this ICheckLogger<TErrorCode> checkLogger)
        {
            return checkLogger ?? new DefaultCheckLogger<TErrorCode>();
        }
    }
}
