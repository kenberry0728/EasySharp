namespace EasySharpStandardMvvm.ViewModels
{
    public class UIValue<TValue, TDisplayValue>
    {
        public UIValue(TValue value, TDisplayValue displayValue)
        {
            this.Value = value;
            this.DisplayValue = displayValue;
        }

        public TValue Value { get; }

        public TDisplayValue DisplayValue { get; }
    }
}
