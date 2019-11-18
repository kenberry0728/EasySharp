using System;
using System.IO;
using System.Linq;

namespace EasySharp.Logs.Text
{
    public class DateTextFileCurrentTimeLogger : TextFileLogger
    {
        public DateTextFileCurrentTimeLogger()
            : this(DateTime.Today.ToShortDateFileNameString() + ".log")
        {
        }

        public DateTextFileCurrentTimeLogger(string directory, string prefix, string sufix)
            : this(Path.Combine(directory, prefix + DateTime.Today.ToShortDateFileNameString() + sufix))
        {
        }

        public DateTextFileCurrentTimeLogger(string filePath)
            : base(filePath)
        {
        }

        public override string WriteLine(params string[] messages)
        {
            return base.WriteLine(new string[] { DateTime.Now.ToLongTimeString() }.Concat(messages).ToArray());
        }
    }
}
