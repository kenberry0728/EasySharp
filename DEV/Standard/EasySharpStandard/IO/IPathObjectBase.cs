namespace EasySharp.IO
{
    public interface IPathObjectBase
    {
        bool IsAbolutePath { get; }

        bool Equals(object obj);
        int GetHashCode();
        string ToString();
    }
}