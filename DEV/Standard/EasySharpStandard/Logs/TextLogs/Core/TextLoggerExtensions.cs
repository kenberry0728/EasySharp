
using EasySharp.Logs.TextLogs.Implementations;

namespace EasySharp.Logs.TextLogs.Core
{
    public static class TextLoggerExtensions
    {
        public static ITextLogger Resolve(this ITextLogger textLogger)
        {
            return textLogger ?? new DefaultTextLogger();
        }
    }
}
