namespace EasySharp.IO
{
    public interface IPathObjectBase : IValueObjectBase<string>
    {
        bool IsAbolutePath { get; }
    }
}