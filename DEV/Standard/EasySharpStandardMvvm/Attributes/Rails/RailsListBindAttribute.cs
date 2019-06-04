﻿using System;

namespace EasySharpStandardMvvm.Attributes.Rails
{
    [AttributeUsage(
        AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = false)]
    public class RailsListBindAttribute : RailsBindAttribute
    {
        public RailsListBindAttribute(Type elementType)
            : base()
        {
            this.ElementType = elementType;
        }

        public RailsListBindAttribute(Type elementType, bool userVisible)
            : base(userVisible)
        {
            this.ElementType = elementType;
        }

        public RailsListBindAttribute(Type elementType, string elementName)
            : base(elementName)
        {
            this.ElementType = elementType;
        }

        public Type ElementType { get; }
    }
}
