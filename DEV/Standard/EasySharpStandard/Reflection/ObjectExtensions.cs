namespace EasySharp.Reflection
{
    public static class ObjectExtensions
    {
        public static object GetPropertyValue(this object model, string propertyName)
        {
            return model.GetType().GetProperty(propertyName)?.GetValue(model);
        }
    }
}
