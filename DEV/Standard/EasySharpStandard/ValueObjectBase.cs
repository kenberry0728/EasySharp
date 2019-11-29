namespace EasySharp
{
    public abstract class ValueObjectBase<T> : IValueObjectBase<T>
    {
        public ValueObjectBase(T value)
        {
            this.Value = value;
        }

        public T Value { get; }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
