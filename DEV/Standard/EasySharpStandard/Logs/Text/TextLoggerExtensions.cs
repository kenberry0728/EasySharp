namespace EasySharp.Log.Text
{
    public static class TextLoggerExtensions
    {
        public static ITextLogger Resolve(this ITextLogger textLogger)
        {
            return textLogger ?? new DefaultTextLogger();
        }
    }
}
