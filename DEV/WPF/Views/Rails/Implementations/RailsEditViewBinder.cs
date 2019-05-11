﻿using EasySharpWpf.ViewModels.Rails.Attributes;
using EasySharpWpf.ViewModels.Rails.Core.Edit;
using EasySharpWpf.Views.Rails.Core;
using EasySharpWpf.Views.Rails.Core.Edit.Interfaces;
using EasySharpWpf.Views.VisualTrees;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace EasySharpWpf.Views.Rails.Implementations
{
    internal class RailsEditViewBinder : IRailsEditViewBinder
    {
        public void ApplyRailsBinding(FrameworkElement rootElement, object model)
        {
            this.ApplyRailsBinding(rootElement, model, model.GetType());
        }

        public void ApplyRailsBinding(FrameworkElement rootElement, object model, Type type)
        {
            var viewModel = new RailsEditViewModel(model);
            rootElement.DataContext = viewModel;

            var properties = type.GetProperties()
                .Where(p => p.HasVisibleRailsBindAttribute());
            foreach (var property in properties)
            {
                var railsBind = property.GetCustomAttribute<RailsBindAttribute>();
                if (string.IsNullOrEmpty(railsBind.ElementName))
                {
                    continue;
                }

                Debug.Assert(railsBind.UserVisible);

                var bindTargetElement = rootElement.GetDescendants().OfType<FrameworkElement>()
                      .FirstOrDefault(fe => fe.Name == railsBind.ElementName);
                Debug.Assert(bindTargetElement != null);
                BindToElement(property, bindTargetElement);
            }
        }

        private static void BindToElement(
            PropertyInfo property,
            FrameworkElement bindTargetElement)
        {
            var binding = RailsBindCreator.CreateRailsBinding(property);
            switch (bindTargetElement)
            {
                case TextBox textBox:
                    textBox.SetBinding(TextBox.TextProperty, binding);
                    break;

                case TextBlock textBlock:
                    textBlock.SetBinding(TextBlock.TextProperty, binding);
                    break;

                case ToggleButton toggleButton: // CheckBox as well.
                    toggleButton.SetBinding(ToggleButton.IsCheckedProperty, binding);
                    break;
            }
        }
    }
}
