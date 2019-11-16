using System;

namespace EasySharp
{
    public class DateTimeWrapper : IDateTime
    {
        public DateTime Now => DateTime.Now;

        public DateTime Today => DateTime.Today;

        public DateTime UtcNow => DateTime.UtcNow;
    }
}
