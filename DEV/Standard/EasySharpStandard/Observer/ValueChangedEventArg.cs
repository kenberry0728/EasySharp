namespace EasySharp.Observer
{
    public class ValueChangedEventArg<TValue>
    {
        public ValueChangedEventArg(TValue oldValue, TValue newValue)
        {
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }

        public TValue OldValue { get; protected set; }

        public TValue NewValue { get; protected set; }
    }
}
