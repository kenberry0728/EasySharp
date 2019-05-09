using EasySharpWpf.ViewModels.Core;
using EasySharpWpf.Views.Rails.Core;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace EasySharpWpf.ViewModels.Rails.Core.Edit
{
    public class RailsEditViewModel<T> : ViewModelBase, IViewModelWithModel
    {
        private readonly IEnumerable<PropertyInfo> properties;

        public RailsEditViewModel(T model)
        {
            this.Model = model;
            this.properties = typeof(T).GetProperties()
                .Where(p => p.HasVisibleRailsBindAttribute());
        }

        public object Model { get; }

        public string GetBindingPath(PropertyInfo propertyInfo)
        {
            return $"[{propertyInfo.Name}]";
        }

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
