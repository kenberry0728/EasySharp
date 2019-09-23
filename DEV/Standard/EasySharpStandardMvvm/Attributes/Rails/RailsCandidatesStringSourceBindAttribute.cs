using System;

namespace EasySharpStandardMvvm.Attributes.Rails
{
    [AttributeUsage(
        AttributeTargets.Property,
        AllowMultiple = false,
        Inherited = false)]
    public class RailsCandidatesStringSourceBindAttribute : RailsSourceBindAttribute
    {
        public string DependentPropertyName { get; set; }

        public string DislayMemberPath { get; set; }

        public string SelectedValuePath { get; set; }
    }
}
