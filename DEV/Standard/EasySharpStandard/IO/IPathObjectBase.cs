namespace EasySharp.IO
{
    public interface IPathObjectBase : IValueObjectBase<string>
    {
        bool IsAbolutePath { get; }

        bool Equals(object obj);
        int GetHashCode();
        string ToString();
    }
}