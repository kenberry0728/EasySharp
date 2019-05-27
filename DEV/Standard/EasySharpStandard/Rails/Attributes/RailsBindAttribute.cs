using System;

namespace EasySharpStandard.Rails.Attributes
{
    [AttributeUsage(
        AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = false)]
    public class RailsBindAttribute : Attribute
    {
        public RailsBindAttribute()
            : this(true)
        {
        }

        public RailsBindAttribute(bool userVisible)
        {
            this.UserVisible = userVisible;
        }

        public RailsBindAttribute(string elementName)
        {
            this.ElementName = elementName;
        }

        public string ElementName { get; set; }

        public bool UserVisible { get; }
    }
}
