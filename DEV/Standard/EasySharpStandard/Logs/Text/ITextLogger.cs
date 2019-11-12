namespace EasySharp.Logs.Text
{
    public interface ITextLogger
    {
        void Write(string message);

        string WriteLine(params string[] messages);
    }
}
