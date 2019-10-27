using EasySharpStandard.Logs.TextLogs.Core;
using System.Diagnostics;

namespace EasySharpStandard.Logs.TextLogs.Implementations
{
    internal class DefaultTextLogger : ITextLogger
    {
        public void Write(string message)
        {
            Debug.Write(message);
        }

        public void WriteLine(params string[] messages)
        {
            Debug.WriteLine(string.Join("\t", messages));
        }
    }
}
