namespace EasySharp
{
    public interface IFromStringFactory<T>
    {
        T FromString(string value);
    }
}