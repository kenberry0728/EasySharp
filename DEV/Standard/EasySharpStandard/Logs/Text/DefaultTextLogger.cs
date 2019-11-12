using System.Diagnostics;

namespace EasySharp.Logs.Text
{
    internal class DefaultTextLogger : ITextLogger
    {
        public void Write(string message)
        {
            Debug.Write(message);
        }

        public string WriteLine(params string[] messages)
        {
            var lineText = messages.ToTabSeparated();
            Debug.WriteLine(lineText);
            return lineText;
        }
    }
}
