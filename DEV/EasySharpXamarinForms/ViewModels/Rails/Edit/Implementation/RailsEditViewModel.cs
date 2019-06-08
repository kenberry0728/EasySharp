using EasySharpStandard.Validations.Core;
using EasySharpStandardMvvm.ViewModels.Core;
using EasySharpStandardMvvm.ViewModels.Rails.Edit.Core;
using EasySharpXamarinForms.Sample.ViewModels.Rails.Edit.Implementation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using EasySharpStandardMvvm.Views.Rails.Core;

namespace EasySharpWpf.ViewModels.Rails.Core.Edit
{
    internal class RailsEditViewModel
        : RailsBindCreator, IViewModelWithModel, IRailsEditViewModel, INotifyDataErrorInfo
    {
        #region INotifyDataErrorInfo

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        bool INotifyDataErrorInfo.HasErrors => this.Model.Validate().Any();

        IEnumerable INotifyDataErrorInfo.GetErrors(string propertyPath)
        {
            if (string.IsNullOrEmpty(propertyPath)) { return null; }

            var propertyName = this.GetRailsPropertyName(propertyPath);
            var property = this.properties.FirstOrDefault(p => p.Name == propertyName);

            if (property == null) { return null; }

            return this.Model.ValidateProperty(property).Select(v => v.ErrorMessage);
        }

        #endregion

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

        public string Content => this.Model?.ToString() ?? "None";

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

                this.OnPropertyChanged("Item[]");
                this.OnPropertyChanged(nameof(this.Content));
            }
        }

        #region Private Methods

        private void OnErrorsChanged(string propertyName)
        {
            this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion
    }
}
