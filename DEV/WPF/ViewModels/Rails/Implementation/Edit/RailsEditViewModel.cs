using EasySharpStandard.Attributes.Core;
using EasySharpWpf.ViewModels.Core;
using EasySharpWpf.Views.Rails.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasySharpWpf.ViewModels.Rails.Core.Edit
{
    internal class RailsEditViewModel : ViewModelBase, IViewModelWithModel, IRailsEditViewModel
    {
        private readonly IEnumerable<PropertyInfo> properties;

        public RailsEditViewModel(object model, Type type = null)
        {
            this.Type = type ?? model.GetType();
            this.Model = model;
            this.properties = this.Type.GetProperties()
                .Where(p => p.HasVisibleRailsBindAttribute());
        }

        public Type Type { get; }

        public object Model { get; }

        public string Content
        {
            get
            {
                var contents = this.properties.Select(p => $"{p.GetDisplayName()}: {p.GetValue(this.Model)}");
                return string.Join(", ", contents);
            }
        }

        public string GetBindingPath(PropertyInfo propertyInfo)
        {
            return $"[{propertyInfo.Name}]";
        }

        public void SetProperty(MemberInfo propertyInfo, object value)
        {
            this[propertyInfo.Name] = value;
            this.OnPropertyChanged(nameof(this.Content));
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
