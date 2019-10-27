using EasySharpStandard.Logs.TextLogs.Implementations;
using System;
using System.IO;
using System.Linq;

namespace EasySharpStandard.Logs.TextLogs.Core
{
    public class DateTextFileCurrentTimeLogger : TextFileLogger
    {
        public DateTextFileCurrentTimeLogger(bool throwException)
            : this(DateTime.Today.ToShortDateString() + ".log", throwException)
        {
        }

        public DateTextFileCurrentTimeLogger(string directory, string prefix, string sufix, bool throwException)
            : this(Path.Combine(directory, prefix + DateTime.Today.ToShortDateString() + sufix), throwException)
        {
        }

        public DateTextFileCurrentTimeLogger(string filePath, bool throwException)
            : base(filePath, throwException)
        {
        }

        public override void WriteLine(params string[] messages)
        {
            base.WriteLine(new string[] { DateTime.Now.ToString() }.Concat(messages).ToArray());
        }
    }
}
