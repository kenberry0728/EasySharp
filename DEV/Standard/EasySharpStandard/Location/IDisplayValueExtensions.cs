namespace EasySharp.Location
{
    public static class DisplayValueExtensions
    {
        public static string ToDisplayText(this IDisplayValue displayValue)
        {
            return displayValue.DisplayText ?? displayValue.ToString();
        }
    }
}
