using System.Globalization;

namespace EasySharp
{
    public static class IntExtensions
    {
        public static string ToInvariantCultureString(this int value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
