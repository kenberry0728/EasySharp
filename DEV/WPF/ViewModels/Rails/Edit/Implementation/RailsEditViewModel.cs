﻿using EasySharpStandard.Validations;
using EasySharpWpf.ViewModels.Core;
using EasySharpWpf.Views.Rails.Core;
using EasySharpWpf.Views.Rails.Implementations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace EasySharpWpf.ViewModels.Rails.Core.Edit
{
    internal class RailsEditViewModel
        : ViewModelBase, IViewModelWithModel, IRailsEditViewModel, INotifyDataErrorInfo
    {
        #region INotifyDataErrorInfo

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        bool INotifyDataErrorInfo.HasErrors => this.Model.Validate().Any();

        IEnumerable INotifyDataErrorInfo.GetErrors(string propertyPath)
        {
            var propertyName = RailsBindCreator.GetRailsPropertyName(propertyPath);
            var property = this.properties.FirstOrDefault(p => p.Name == propertyName);
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

                this.OnPropertyChanged("Item[]");
                this.OnPropertyChanged(nameof(this.Content));
                //this.OnErrorsChanged(RailsBindCreator.GetRailsProperyPath(property));
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