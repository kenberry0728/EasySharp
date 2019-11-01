namespace EasySharpStandard.SafeCodes.Core
{
    public static class DefaultValueExtensions
    {
        public static bool IsDefaultValue<T>(this T instance) where T : class
        {
            return instance == default(T);
        }

        public static bool IsDefaultStructValue<T>(this T instance) where T : struct
        {
            return instance.Equals(new T());
        }

        public static bool IsNull<T>(this T instance) where T : class
        {
            return instance == null;
        }

        public static bool IsNullOrEmpty(this string instance)
        {
            return string.IsNullOrEmpty(instance);
        }
    }
}
