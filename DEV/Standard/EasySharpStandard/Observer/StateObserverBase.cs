using EasySharp.Logs.Text;
using System;

namespace EasySharp.Observer
{
    public abstract class StateObserverBase<TStateStruct> : IStateObserver<TStateStruct>
        where TStateStruct : struct
    {
        private readonly ITextLogger textLogger;

        protected StateObserverBase(ITextLogger textLogger = null)
        {
            this.StateChangeEvent = new EventContainer<StateChangedEventArg<TStateStruct>>(
                handler => this.StateChange += handler,
                handler => this.StateChange -= handler);
            this.textLogger = textLogger;
        }

        public event EventHandler<StateChangedEventArg<TStateStruct>> StateChange;

        public IEventContainer<StateChangedEventArg<TStateStruct>> StateChangeEvent { get; }

        public TStateStruct CurrentState { get; private set; }

        public StateChangedEventArg<TStateStruct> SetCurrentState(TStateStruct value)
        {
            var oldState = this.CurrentState;
            this.CurrentState = value;
            var newState = this.CurrentState;
            return new StateChangedEventArg<TStateStruct>(oldState, newState);
        }

        protected virtual void OnStateChange(object sender, StateChangedEventArg<TStateStruct> e)
        {
            this.textLogger?.WriteLine(this.GetType().Name, e.NewState.ToString());
            this.StateChange?.Invoke(sender, e);
        }
    }
}
