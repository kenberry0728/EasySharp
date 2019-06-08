using System;

namespace EasySharpStandardMvvm.Attributes.Rails
{
    [AttributeUsage(
        AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = false)]
    public class RailsDataMemberBindAttribute : Attribute
    {
        public RailsDataMemberBindAttribute()
            : this(true)
        {
        }

        public RailsDataMemberBindAttribute(bool userVisible)
        {
            this.UserVisible = userVisible;
        }

        public RailsDataMemberBindAttribute(string elementName)
        {
            this.ElementName = elementName;
        }

        public string ElementName { get; }

        public bool UserVisible { get; }
    }
}
