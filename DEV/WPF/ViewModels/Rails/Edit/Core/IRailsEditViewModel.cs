using EasySharpWpf.ViewModels.Core;
using System;
using System.Reflection;

namespace EasySharpWpf.ViewModels.Rails.Core.Edit
{
    public interface IRailsEditViewModel : IViewModelWithModel
    {
        string Content { get; }

        Type Type { get; }

        object this[string propertyName] { get; set; }

        void SetProperty(MemberInfo propertyInfo, object value);
    }
}