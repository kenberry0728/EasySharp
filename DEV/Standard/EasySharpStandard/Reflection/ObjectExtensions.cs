namespace EasySharp.Reflection
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Extensions")]
    public static class ObjectExtensions
    {
        public static object GetPropertyValue(this object model, string propertyName)
        {
            return model.GetType().GetProperty(propertyName)?.GetValue(model);
        }
    }
}
