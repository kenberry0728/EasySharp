namespace CommonLibrary.TreeData.TreeStructure
{
    public interface IIndentedItem<T>
    {
        T Content { get; }
        int Depth { get; }
    }
}