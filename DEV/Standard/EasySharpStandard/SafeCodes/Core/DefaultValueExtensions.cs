namespace EasySharpStandard.SafeCodes.Core
{
    public static class DefaultValueExtensions
    {
        public static bool IsDefaultValue<T>(this T instance)
        {
            return instance == default;
        }

        public static bool IsNull<T>(this T instance) where T : class
        {
            return instance == null;
        }
    }
}
