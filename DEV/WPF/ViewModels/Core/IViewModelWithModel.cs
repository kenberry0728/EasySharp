﻿using System.Reflection;

namespace EasySharpWpf.ViewModels.Core
{
    public interface IViewModelWithModel
    {
        object Model { get; }

        string GetBindingPath(PropertyInfo propertyInfo);
    }
}
