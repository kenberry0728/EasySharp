using System;

namespace EasySharpStandardMvvm.Attributes.Rails
{
    [AttributeUsage(
        AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = false)]
    public class RailsDataMemberListBindAttribute : RailsDataMemberBindAttribute
    {
        public RailsDataMemberListBindAttribute(Type elementType)
            : base()
        {
            this.ElementType = elementType;
        }

        public RailsDataMemberListBindAttribute(Type elementType, bool userVisible)
            : base(userVisible)
        {
            this.ElementType = elementType;
        }

        public RailsDataMemberListBindAttribute(Type elementType, string elementName)
            : base(elementName)
        {
            this.ElementType = elementType;
        }

        public Type ElementType { get; }
    }
}
