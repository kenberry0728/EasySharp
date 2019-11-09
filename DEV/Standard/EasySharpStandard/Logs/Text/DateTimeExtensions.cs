using System;

namespace EasySharp.Log.Text
{
    public static class DateTimeExtensions
    {
        public static string ToShortDateFileNameString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMdd");
        }
    }
}
