namespace EasySharp
{
    public interface ICloneable<T>
        where T : class
    {
        T Clone();
    }
}
