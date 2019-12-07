namespace EasySharp.Observer
{
    public class ValueChangedEventArg<TValue>
    {
        public ValueChangedEventArg(TValue old, TValue newState)
        {
            this.OldValue = old;
            this.NewValue = newState;
        }

        public TValue OldValue { get; protected set; }

        public TValue NewValue { get; protected set; }
    }
}
