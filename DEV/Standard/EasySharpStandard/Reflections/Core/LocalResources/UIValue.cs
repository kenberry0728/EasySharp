namespace EasySharpStandard.Reflections.Core.LocalResources
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
