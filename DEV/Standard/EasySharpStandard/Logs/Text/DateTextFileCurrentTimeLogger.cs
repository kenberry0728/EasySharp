using System;
using System.IO;
using System.Linq;

namespace EasySharp.Logs.Text
{
    public class DateTextFileCurrentTimeLogger : TextFileLogger
    {
        public DateTextFileCurrentTimeLogger(bool throwException)
            : this(DateTime.Today.ToShortDateFileNameString() + ".log", throwException)
        {
        }

        public DateTextFileCurrentTimeLogger(string directory, string prefix, string sufix, bool throwException)
            : this(Path.Combine(directory, prefix + DateTime.Today.ToShortDateFileNameString() + sufix), throwException)
        {
        }

        public DateTextFileCurrentTimeLogger(string filePath, bool throwException)
            : base(filePath, throwException)
        {
        }

        public override string WriteLine(params string[] messages)
        {
            return base.WriteLine(new string[] { DateTime.Now.ToLongTimeString() }.Concat(messages).ToArray());
        }
    }
}
