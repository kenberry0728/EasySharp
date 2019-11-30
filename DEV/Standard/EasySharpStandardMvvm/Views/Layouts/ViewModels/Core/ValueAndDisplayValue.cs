namespace EasySharpStandardMvvm.Views.Layouts.ViewModels.Core
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

#pragma warning disable CA1000 // Do not declare static members on generic types
        public static string ValuePath => nameof(Value);
        public static string DisplayValuePath => nameof(DisplayValue);
#pragma warning restore CA1000 // Do not declare static members on generic types
    }
}
