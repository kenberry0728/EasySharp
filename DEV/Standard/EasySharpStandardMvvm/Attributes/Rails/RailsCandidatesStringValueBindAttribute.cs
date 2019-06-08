using System;

namespace EasySharpStandardMvvm.Attributes.Rails
{
    [AttributeUsage(
        AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = false)]
    public class RailsCandidatesStringValueBindAttribute : RailsBindAttribute
    {
        #region Constructors

        public RailsCandidatesStringValueBindAttribute()
        : this(true)
        {
        }

        public RailsCandidatesStringValueBindAttribute(bool userVisible)
            : base(userVisible)
        {
        }

        public RailsCandidatesStringValueBindAttribute(string elementName)
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
