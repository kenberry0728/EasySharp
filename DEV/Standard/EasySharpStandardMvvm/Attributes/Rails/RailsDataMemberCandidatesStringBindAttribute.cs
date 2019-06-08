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

        public RailsDataMemberCandidatesStringBindAttribute(string candidatesPropertyName)
        : this(true, candidatesPropertyName)
        {
        }

        public RailsDataMemberCandidatesStringBindAttribute(bool userVisible, string candidatesPropertyName)
            : base(userVisible)
        {
            this.CandidatesPropertyName = candidatesPropertyName;
        }

        public RailsDataMemberCandidatesStringBindAttribute(string elementName, string candidatesPropertyName)
            : base(elementName)
        {
            this.CandidatesPropertyName = candidatesPropertyName;
        }

        #endregion

        #region Properties

        public string CandidatesPropertyName { get; set; }

        public string DependentPropertyName{ get; set; }

        #endregion
    }
}
