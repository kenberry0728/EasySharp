using System;
using System.Globalization;

namespace EasySharpStandardMvvm.Views.Converters
{
    public class IntToStringConverterBase
    {
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.ToString();
        }

        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (int.TryParse(value as string, out var result))
            {
                return result;
            }

            return 0;
        }
    }
}