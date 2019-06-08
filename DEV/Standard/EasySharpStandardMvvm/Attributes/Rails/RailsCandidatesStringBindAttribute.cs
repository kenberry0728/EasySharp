﻿using System;

namespace EasySharpStandardMvvm.Attributes.Rails
{
    [AttributeUsage(
        AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = false)]
    public class RailsCandidatesStringBindAttribute : RailsBindAttribute
    {
        #region Constructors

        public RailsCandidatesStringBindAttribute()
        : this(true)
        {
        }

        public RailsCandidatesStringBindAttribute(bool userVisible)
            : base(userVisible)
        {
        }

        public RailsCandidatesStringBindAttribute(string elementName)
            : base(elementName)
        {
            this.ElementName = elementName;
        }

        #endregion

        #region Properties

        public string CandidatesPropertyName { get; set; }

        public string DependentPropertyName{ get; set; }

        #endregion
    }
}
