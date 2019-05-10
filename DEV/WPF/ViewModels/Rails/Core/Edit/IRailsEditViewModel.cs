using System;
using System.Reflection;

namespace EasySharpWpf.ViewModels.Rails.Core.Edit
{
    public interface IRailsEditViewModel
    {
        object this[string key] { get; set; }

        string Content { get; }
        object Model { get; }
        Type Type { get; }

        string GetBindingPath(PropertyInfo propertyInfo);
        void SetProperty(MemberInfo propertyInfo, object value);
    }
}