using EasySharp.Logs.Text;

namespace EasySharp.Observer
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "<Pending>")]
    public abstract class StructObserverBase<TValue> : ValueObserverBase<TValue>
        where TValue : struct
    {
        protected StructObserverBase(ITextLogger textLogger = null)
            : base(textLogger)
        {
        }

        protected override ValueChangedEventArg<TValue> SetCurrentValue(TValue value)
        {
            var oldValue = this.CurrentValue;
            this.CurrentValue = value;
            var newValue = this.CurrentValue;
            return new ValueChangedEventArg<TValue>(oldValue, newValue);
        }
    }
}
