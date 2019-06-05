using System;

namespace EasySharpStandardMvvm.Attributes.Rails
{
    [AttributeUsage(
        AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = false)]
    public class RailsCandidatesStringAttribute : RailsBindAttribute
    {
        #region Constructors

        public RailsCandidatesStringAttribute()
        : this(true)
        {
        }

        public RailsCandidatesStringAttribute(bool userVisible)
            : base(userVisible)
        {
        }

        public RailsCandidatesStringAttribute(string elementName)
            : base(elementName)
        {
            this.ElementName = elementName;
        }

        #endregion

        public string CandidatesFilePath { get; set; }
    }
}
