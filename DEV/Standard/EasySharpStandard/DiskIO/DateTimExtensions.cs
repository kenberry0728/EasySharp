using System;

namespace EasySharpStandard.DiskIO
{
    public static class DateTimExtensions
    {
        public static string ToShortDateFileNameString(this DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMdd");
        }
    }
}
