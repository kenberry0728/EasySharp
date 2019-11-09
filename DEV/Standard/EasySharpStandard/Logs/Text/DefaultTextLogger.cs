using System.Diagnostics;

namespace EasySharp.Log.Text
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
