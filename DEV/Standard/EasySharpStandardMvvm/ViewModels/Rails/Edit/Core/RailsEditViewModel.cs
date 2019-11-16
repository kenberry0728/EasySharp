using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using EasySharp;
using EasySharp.ComponentModel.DataAnnotations;
using EasySharpStandardMvvm.Attributes.Rails;
using EasySharpStandardMvvm.Views.Layouts.ViewModels.Rails.Edit.Core;
using EasySharpStandardMvvm.Views.Rails.Core;

namespace EasySharpStandardMvvm.ViewModels.Rails.Edit.Core
{
    public class RailsEditViewModel : RailsEditViewModelPathDefinition, IRailsEditViewModel, INotifyDataErrorInfo
    {
        #region INotifyDataErrorInfo

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        bool INotifyDataErrorInfo.HasErrors => this.Model.Validate().Any();

        IEnumerable INotifyDataErrorInfo.GetErrors(string propertyPath)
        {
            if (propertyPath.IsNullOrEmpty()) { return null; }

            var propertyName = this.GetRailsPropertyName(propertyPath);
            var property = this.valueProperties.FirstOrDefault(p => p.Name == propertyName);

            if (property == null)
            {
                return null;
            }

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
                if (TryGetFromValueProperty(key, out var valueResult))
                {
                    return valueResult;
                }

                if (TryGetFromSourceProperty(key, out var sourceResult))
                {
                    return sourceResult;
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

        private bool TryGetFromValueProperty(string key, out object item)
        {
            var valueProperty = this.valueProperties.FirstOrDefault(p => p.Name == key);
            if (valueProperty != null)
            {
                item = valueProperty.GetValue(this.Model);
                return true;
            }

            return Try.Failed(out item);
        }

        private bool TryGetFromSourceProperty(string key, out object result)
        {
            var sourceProperty = this.sourceProperties.FirstOrDefault(p => p.Name == key);
            if (sourceProperty != null)
            {
                var sourceBindAttribute = sourceProperty.GetCustomAttribute<RailsCandidatesStringSourceBindAttribute>();
                dynamic sourceValue = sourceProperty.GetValue(this.Model);
                if (string.IsNullOrEmpty(sourceBindAttribute?.DependentPropertyName))
                {
                    result = sourceValue;
                    return true;
                }

                if (TryGetFromValueProperty(sourceBindAttribute.DependentPropertyName, out var dependentValue)
                    && sourceValue.ContainsKey(dependentValue.ToString()))
                {
                    result = sourceValue[dependentValue.ToString()];
                    return true;
                }
            }

            return Try.Failed(out result);
        }

        private void OnErrorsChanged(string propertyName)
        {
            this.ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion
    }
}
