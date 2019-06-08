using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using EasySharpStandard.Validations.Core;
using EasySharpStandardMvvm.Attributes.Rails;
using EasySharpStandardMvvm.ViewModels.Rails.Edit.Core;
using EasySharpStandardMvvm.Views.Rails.Core;

namespace EasySharpWpf.ViewModels.Rails.Edit.Implementation
{
    // TODO: Add BaseClass
    internal class RailsEditViewModel
        : RailsBindCreator, IRailsEditViewModel, INotifyDataErrorInfo
    {
        #region INotifyDataErrorInfo

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        bool INotifyDataErrorInfo.HasErrors => this.Model.Validate().Any();

        IEnumerable INotifyDataErrorInfo.GetErrors(string propertyPath)
        {
            if (string.IsNullOrEmpty(propertyPath)) { return null; }

            var propertyName = this.GetRailsPropertyName(propertyPath);
            var property = this.valueProperties.FirstOrDefault(p => p.Name == propertyName);

            if (property == null) { return null; }

            return this.Model.ValidateProperty(property).Select(v => v.ErrorMessage);
        }

        #endregion

        #region Fields

        private readonly IEnumerable<PropertyInfo> valueProperties;
        private readonly IEnumerable<PropertyInfo> sourceProperties;

        #endregion

        public RailsEditViewModel(object model, Type type = null)
        {
            this.Type = type ?? model.GetType();
            this.Model = model;
            this.valueProperties = this.Type.GetProperties()
                .Where(p => p.HasVisibleRailsBindAttribute());
            this.sourceProperties = this.Type.GetProperties()
                .Where(p => p.HasRailsSourceBindAttribute());
        }

        #region Properties

        public Type Type { get; }

        public object Model { get; }

        public string Content => this.Model?.ToString() ?? "None";

        public object this[string key]
        {
            get
            {
                var valueProperty = this.valueProperties.FirstOrDefault(p => p.Name == key);
                if (valueProperty != null)
                {
                    return valueProperty.GetValue(this.Model);
                }

                var sourceProperty = this.sourceProperties.FirstOrDefault(p => p.Name == key);
                if (sourceProperty != null)
                {
                    var sourceBindAttribute = sourceProperty.GetCustomAttribute<RailsCandidatesStringSourceBindAttribute>();
                    if (string.IsNullOrEmpty(sourceBindAttribute?.DependentPropertyName))
                    {
                        return sourceProperty.GetValue(this.Model);
                    }

                    dynamic dictionary = sourceProperty.GetValue(this.Model);
                    var dependentValue = this[sourceBindAttribute.DependentPropertyName].ToString();
                    return dictionary[dependentValue];
                }

                return null;
            }

            set
            {
                var property = this.valueProperties.FirstOrDefault(p => p.Name == key);
                property?.SetValue(this.Model, value);

                this.OnPropertyChanged("Item[]");
                this.OnPropertyChanged(nameof(this.Content));
            }
        }

        #endregion

        #region Private Methods

        private void OnErrorsChanged(string propertyName)
        {
            this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion
    }
}
