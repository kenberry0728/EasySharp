using EasySharp.Logs.Text;

namespace EasySharp.Observer
{
    public abstract class DisposableValueObserverBase<TStateStruct> 
        : ValueObserverBase<TStateStruct>, IDisposableValueObserver<TStateStruct>, IDisposablePattern
        where TStateStruct : struct
    {
        protected DisposableValueObserverBase(ITextLogger textLogger = null)
            : base(textLogger)
        {
        }

        ~DisposableValueObserverBase()
        {
            this.OnDestruct();
        }

        public void Dispose()
        {
            this.OnDispose();
        }

        public abstract void DisposeNativeResources();

        public abstract void DisposeManagedResources();
    }
}
