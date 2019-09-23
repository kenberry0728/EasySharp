namespace EasySharpStandardMvvm.ViewModels
{
    public class UIValue<TValue, TDisplayValue>
    {
        public UIValue(TDisplayValue displayValue, TValue value)
        {
            this.DisplayValue = displayValue;
            this.Value = value;
        }

        public TDisplayValue DisplayValue { get; }

        public TValue Value { get; }
    }
}
