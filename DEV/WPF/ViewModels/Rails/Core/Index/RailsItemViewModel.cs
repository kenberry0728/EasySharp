﻿using EasySharpWpf.ViewModels.Core;
using EasySharpWpf.Views.Rails.Core;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasySharpWpf.ViewModels.Rails.Core.Index
{
    public class RailsItemViewModel<T> : ViewModelBase, IViewModelWithModel<T>
    {
        private readonly IEnumerable<PropertyInfo> properties;

        public RailsItemViewModel(T model)
        {
            this.Model = model;
            this.properties = typeof(T).GetProperties()
                .Where(p => p.HasVisibleRailsBindAttribute());
        }

        public T Model { get; }

        public void SetProperty(MemberInfo propertyInfo, object value)
        {
            this[propertyInfo.Name] = value;
        }

        public object this[string key]
        {
            get
            {
                return this.properties.FirstOrDefault(p => p.Name == key)?.GetValue(this.Model);
            }

            set
            {
                var property = this.properties.FirstOrDefault(p => p.Name == key);
                property?.SetValue(this.Model, value);
                OnPropertyChanged("Item[]");
            }
        }
    }
}
