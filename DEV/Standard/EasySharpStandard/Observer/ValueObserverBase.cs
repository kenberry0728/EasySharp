using EasySharp.Logs.Text;

namespace EasySharp.Observer
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1063:Implement IDisposable Correctly", Justification = "<Pending>")]
    public abstract class ValueObserverBase<TStateStruct> 
        : StateObserverBase<TStateStruct>, IStateObserver<TStateStruct>, IDisposablePattern
    {
        protected ValueObserverBase(ITextLogger textLogger = null)
            : base(textLogger)
        {
        }

        protected override ValueChangedEventArg<TStateStruct> SetCurrentValue(TStateStruct value)
        {
            var oldValue = this.CurrentValue;
            this.CurrentValue = value;
            var newValue = this.CurrentValue;
            return new ValueChangedEventArg<TStateStruct>(oldValue, newValue);
        }
    }
}
