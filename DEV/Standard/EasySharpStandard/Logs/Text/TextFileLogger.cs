using EasySharp.IO;
using System;
using System.IO;
using System.Text;

namespace EasySharp.Logs.Text
{
    public class TextFileLogger :　DisposableBase, ITextLogger
    {
        private readonly string filePath;
        private readonly StreamWriter streamWriter;
        private readonly object lockObject = new object();

        public TextFileLogger(string filePath)
        {
            this.filePath = filePath;
            this.filePath.ToFilePath().EnsureDirectory();
            this.streamWriter = new StreamWriter(this.filePath, true, Encoding.UTF8);
            this.DisposeActions.Add(() =>
            {
                this.streamWriter.Dispose();
            });
        }

        public virtual void Write(string message)
        {
            lock (this.lockObject)
            {
                this.streamWriter.Write(message);
            }
        }

        public virtual string WriteLine(params string[] messages)
        {
            var lineText = messages.JoinWithTab();
            lock(this.lockObject)
            {
                this.streamWriter.WriteLine(lineText);
            };

            return lineText;
        }
    }
}