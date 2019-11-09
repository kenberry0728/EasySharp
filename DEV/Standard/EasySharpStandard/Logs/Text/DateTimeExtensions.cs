using System;

namespace EasySharp.Logs.Text
{
    public static class DateTimeExtensions
    {
        public static string ToShortDateFileNameString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMdd");
        }
    }
}
