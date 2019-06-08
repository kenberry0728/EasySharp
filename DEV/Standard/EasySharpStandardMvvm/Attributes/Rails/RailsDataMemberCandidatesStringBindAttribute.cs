using System;

namespace EasySharpStandardMvvm.Attributes.Rails
{
    [AttributeUsage(
        AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = false)]
    public class RailsDataMemberCandidatesStringBindAttribute : RailsDataMemberBindAttribute
    {
        #region Constructors

        public RailsDataMemberCandidatesStringBindAttribute()
        : this(true)
        {
        }

        public RailsDataMemberCandidatesStringBindAttribute(bool userVisible)
            : base(userVisible)
        {
        }

        public RailsDataMemberCandidatesStringBindAttribute(string elementName)
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
