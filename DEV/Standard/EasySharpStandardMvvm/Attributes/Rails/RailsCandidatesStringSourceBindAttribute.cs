using System;

namespace EasySharpStandardMvvm.Attributes.Rails
{
    [AttributeUsage(
        AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = false)]
    public class RailsCandidatesStringSourceBindAttribute : RailsSourceBindAttribute
    {
        // TODO: stringに限定する必要なし？
        #region Constructors

        #endregion

        #region Properties

        public string DependentPropertyName { get; set; }

        #endregion
    }
}
