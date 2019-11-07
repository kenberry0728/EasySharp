namespace EasySharpStandard.Reflections.Core
{
    public static class StructExtensions
    {
        public static string ToFormattedString<T>(
            this T instance,
            string propertySeparator = "\t",
            string valueSeparator = "=")
            where T : struct
        {
            var type = instance.GetType();
            var properties = type.GetProperties();
            string[] values = new string[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                var property = properties[i];
                values[i] = $"{property.Name}{valueSeparator}{property.GetValue(instance)}";
            }

            return string.Join(propertySeparator, values);
        }
    }
}
