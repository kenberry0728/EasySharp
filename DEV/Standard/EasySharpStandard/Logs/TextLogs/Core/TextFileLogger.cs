using EasySharpStandard.Logs.TextLogs.Core;
using EasySharpStandard.SafeCodes.Core;
using System;
using System.IO;
using System.Text;

namespace EasySharpStandard.Logs.TextLogs.Implementations
{
    public class TextFileLogger : ITextLogger
    {
        private readonly string fileName;

        public TextFileLogger(string prefix, DateTime date, string suffix)
        {
            this.fileName = $"{prefix}{date}{suffix}";
        }

        public void Write(string message)
        {
            Retry.Run(() =>
            {
                using (var sw = new StreamWriter(this.fileName, true, Encoding.UTF8))
                {
                    sw.Write(message);
                }
            });
        }

        public void WriteLine(params string[] messages)
        {
            using (var sw = new StreamWriter(this.fileName, true, Encoding.UTF8))
            {
                sw.WriteLine(string.Join("\t", messages));
            }
        }
    }
}
