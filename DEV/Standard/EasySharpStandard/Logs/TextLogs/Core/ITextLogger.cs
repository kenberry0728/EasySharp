namespace EasySharp.Logs.TextLogs.Core
{
    public interface ITextLogger
    {
        void Write(string message);

        void WriteLine(params string[] messages);
    }
}
