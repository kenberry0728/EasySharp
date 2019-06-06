﻿using System.Reflection;

namespace EasySharpWpf.ViewModels.Rails.Edit.Core
{
    public interface IRailsBindCreator<TBinding>
    {
        string GetPropertyName(PropertyInfo propertyInfo);
        string GetRailsPropertyName(string bindingPath);
        string GetRailsProperyPath(PropertyInfo propertyInfo);
        TBinding CreateRailsBinding(PropertyInfo propertyInfo);
    }
}