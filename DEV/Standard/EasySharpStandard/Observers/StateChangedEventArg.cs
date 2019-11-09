namespace EasySharp.Observers
{
    public class StateChangedEventArg<TStateStruct>
           where TStateStruct : struct
    {
        public StateChangedEventArg(TStateStruct old, TStateStruct newState)
        {
            this.OldState = old;
            this.NewState = newState;
        }

        public TStateStruct OldState { get; protected set; }

        public TStateStruct NewState { get; protected set; }
    }
}
