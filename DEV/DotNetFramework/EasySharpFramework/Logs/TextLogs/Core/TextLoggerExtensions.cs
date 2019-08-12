
using EasySharpStandard.Logs.TextLogs.Implementations;

namespace EasySharpStandard.Logs.TextLogs.Core
{
    public static class TextLoggerExtensions
    {
        public static ITextLogger Resolve(this ITextLogger textLogger)
        {
            return textLogger ?? new DefaultTextLogger();
        }
    }
}
