namespace EasySharpStandard.DataStructures.Trees.Core
{
    public interface IIndentedItem<T>
    {
        T Content { get; }
        int Depth { get; }
    }
}