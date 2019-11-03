
using System;

namespace EasySharpStandard.Observers
{
    public abstract class StateObserverBase<TStateStruct> : IStateObserver<TStateStruct>
        where TStateStruct : struct
    {
        public event EventHandler<StateChangedEventArg<TStateStruct>> StateChange;

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
            this.StateChange?.Invoke(sender, e);
        }
    }
}
