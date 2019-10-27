using EasySharpStandard.DiskIO;
using EasySharpStandard.Logs.TextLogs.Core;
using EasySharpStandard.SafeCodes.Core;
using System.IO;
using System.Text;

namespace EasySharpStandard.Logs.TextLogs.Implementations
{
    public class TextFileLogger : ITextLogger
    {
        private readonly string filePath;
        private readonly bool throwException;

        public TextFileLogger(string filePath, bool throwException)
        {
            this.filePath = filePath;
            this.throwException = throwException;

            this.filePath.EnsureDirectoryForFile();
        }

        public virtual void Write(string message)
        {
            var result = Retry.Run(() =>
            {
                using (var sw = new StreamWriter(this.filePath, true, Encoding.UTF8))
                {
                    sw.Write(message);
                }
            });

            if (throwException && !result)
            {
                throw new IOException();
            }
        }

        public virtual void WriteLine(params string[] messages)
        {
            var result = Retry.Run(() =>
            {
                using (var sw = new StreamWriter(this.filePath, true, Encoding.UTF8))
                {
                    sw.WriteLine(string.Join("\t", messages));
                }
            });

            if (throwException && !result)
            {
                throw new IOException();
            }
        }
    }
}