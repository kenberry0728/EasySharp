using EasySharpWpf.ViewModels.Core;
using System;
using System.Reflection;

namespace EasySharpWpf.ViewModels.Rails.Core.Edit
{
    public interface IRailsEditViewModel : IViewModelWithModel
    {
        object this[string key] { get; set; }

        string Content { get; }

        Type Type { get; }

        void SetProperty(MemberInfo propertyInfo, object value);
    }
}