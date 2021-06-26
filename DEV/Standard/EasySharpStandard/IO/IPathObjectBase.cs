namespace EasySharp.IO
{
    public interface IPathObjectBase : IValueObjectBase<string>
    {
        bool IsAbsolutePath { get; }

        bool Exists();
    }
}