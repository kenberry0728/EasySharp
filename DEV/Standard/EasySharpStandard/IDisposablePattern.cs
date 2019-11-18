namespace EasySharp
{
    public interface IDisposablePattern
    {
        void DisposeNativeResources();

        void DisposeManagedResources();
    }
}
