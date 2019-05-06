namespace EasySharpStandard.Logs.TextLogs.Core
{
    public interface ITextLogger
    {
        void Write(string message);

        void WriteLine(string message);
    }
}
