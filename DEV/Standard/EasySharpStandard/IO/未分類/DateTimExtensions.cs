using System;

namespace EasySharp.IO
{
    public static class DateTimExtensions
    {
        public static string ToShortDateFileNameString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMdd");
        }
    }
}
