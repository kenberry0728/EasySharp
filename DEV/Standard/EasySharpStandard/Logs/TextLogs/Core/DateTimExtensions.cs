using System;

namespace EasySharp.Logs.TextLogs.Core
{
    public static class DateTimExtensions
    {
        public static string ToShortDateFileNameString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMdd");
        }
    }
}
