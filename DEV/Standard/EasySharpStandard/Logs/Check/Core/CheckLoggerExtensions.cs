using EasySharp.Logs.CheckLogs.Implementations;

namespace EasySharp.Logs.CheckLogs.Core
{
    public static class CheckLoggerExtensions
    {
        public static ICheckLogger<TErrorCode, TLocation> Resolve<TErrorCode, TLocation>(this ICheckLogger<TErrorCode, TLocation> checkLogger)
        {
            return checkLogger ?? new DebugCheckLogger<TErrorCode, TLocation>();
        }
    }
}
