namespace EasySharpStandardMvvm.ViewModels.Core
{
    public class ValueAndDisplayValue<TValueType>
    {
        public ValueAndDisplayValue(TValueType value, string displayValue)
        {
            this.Value = value;
            this.DisplayValue = displayValue;
        }

        public TValueType Value { get; set; }

        public string DisplayValue { get; set; }

        public static string ValuePath => nameof(Value);

        public static string DisplayValuePath => nameof(DisplayValue);
    }
}
