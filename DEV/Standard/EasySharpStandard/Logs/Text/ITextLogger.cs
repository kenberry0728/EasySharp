using System;

namespace EasySharp.Logs.Text
{
    public interface ITextLogger : IDisposable
    {
        void Write(string message);

        string WriteLine(params string[] messages);
    }
}
