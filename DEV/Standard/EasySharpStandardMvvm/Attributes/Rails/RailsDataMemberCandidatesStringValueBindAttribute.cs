using System;

namespace EasySharpStandardMvvm.Attributes.Rails
{
    [AttributeUsage(
        AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = false)]
    public class RailsDataMemberCandidatesStringValueBindAttribute : RailsDataMemberBindAttribute
    {
        #region Constructors

        public RailsDataMemberCandidatesStringValueBindAttribute()
        : this(true)
        {
        }

        public RailsDataMemberCandidatesStringValueBindAttribute(bool userVisible)
            : base(userVisible)
        {
        }

        public RailsDataMemberCandidatesStringValueBindAttribute(string elementName)
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
