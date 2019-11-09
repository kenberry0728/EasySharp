namespace EasySharp.Log.Text
{
    public interface ITextLogger
    {
        void Write(string message);

        void WriteLine(params string[] messages);
    }
}
