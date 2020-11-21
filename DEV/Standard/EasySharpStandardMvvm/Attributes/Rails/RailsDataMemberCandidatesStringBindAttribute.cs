using EasySharp;
using EasySharp.Runtime.CompilerServices;
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
            candidatesPropertyName.ThrowArgumentExceptionIfNullOrEmpty(nameof(candidatesPropertyName));
            this.CandidatesPropertyName = candidatesPropertyName;
        }

        public RailsDataMemberCandidatesStringBindAttribute(string elementName, string candidatesPropertyName)
            : base(elementName)
        {
            candidatesPropertyName.ThrowArgumentExceptionIfNullOrEmpty(nameof(candidatesPropertyName));
            this.CandidatesPropertyName = candidatesPropertyName;
        }

        #endregion

        #region Properties

        public string CandidatesPropertyName { get; set; }

        #endregion
    }
}
