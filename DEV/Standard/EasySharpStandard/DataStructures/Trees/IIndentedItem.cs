namespace EasySharp.DataStructures.Trees
{
    public interface IIndentedItem<T>
    {
        T Content { get; }
        int Depth { get; }
    }
}