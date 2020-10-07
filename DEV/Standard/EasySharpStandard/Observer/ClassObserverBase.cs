using EasySharp.Logs.Text;

namespace EasySharp.Observer
{
    public abstract class ClassObserverBase<TValue> : ValueObserverBase<TValue>
        where TValue : class, ICloneable<TValue>
    {
        protected ClassObserverBase(ITextLogger textLogger = null)
            : base(textLogger)
        {
        }

        protected override TValue CreateCurrentValueClone()
        {
            return this.CurrentValue?.Clone();
        }
    }
}
