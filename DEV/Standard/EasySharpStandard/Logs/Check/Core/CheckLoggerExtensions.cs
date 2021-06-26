using EasySharp.Logs.Check.Implementations;

namespace EasySharp.Logs.Check.Core
{
    public static class CheckLoggerExtensions
    {
        public static ICheckLogger<TErrorCode, TLocation> Resolve<TErrorCode, TLocation>(this ICheckLogger<TErrorCode, TLocation> checkLogger)
        {
            return checkLogger ?? new DebugCheckLogger<TErrorCode, TLocation>();
        }
    }
}
