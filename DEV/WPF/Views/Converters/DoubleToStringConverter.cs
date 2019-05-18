﻿using System;
using System.Globalization;
using System.Windows.Data;

namespace EasySharpWpf.Views.Converters
{
    public class DoubleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (double.TryParse(value as string, out var result))
            {
                return result;
            }

            return 0;
        }
    }
}