using EasySharpStandard.Logs.TextLogs.Implementations;
using System;
using System.Linq;

namespace EasySharpStandard.Logs.TextLogs.Core
{
    public class DateTextFileCurrentTimeLogger : TextFileLogger
    {
        public DateTextFileCurrentTimeLogger(bool throwException)
            : base(DateTime.Today.ToShortDateString(), throwException)
        {
        }

        public override void WriteLine(params string[] messages)
        {
            base.WriteLine(new string[] { DateTime.Now.ToString() }.Concat(messages).ToArray());
        }
    }
}
