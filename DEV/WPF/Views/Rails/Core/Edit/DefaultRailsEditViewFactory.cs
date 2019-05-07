using EasySharpStandard.Attributes.Core;
using EasySharpWpf.ViewModels.Rails.Attributes;
using EasySharpWpf.ViewModels.Rails.Core.Edit;
using EasySharpWpf.Views.Extensions;
using EasySharpWpf.Views.Rails.Implementations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace EasySharpWpf.Views.Rails.Core.Edit
{
    public class DefaultRailsEditViewFactory<T> : IRailsEditViewFactory<T>
    {
        #region Fields

        private readonly Type type = typeof(T);

        #endregion

        #region Constructors

        internal protected DefaultRailsEditViewFactory()
        {
        }

        #endregion

        #region Public Methods

        public FrameworkElement CreateEditView(T model)
        {
            var viewModel = new RailsEditViewModel<T>(model);
            var grid = new Grid() { DataContext = viewModel };
            grid.AddColumnDefinition(GridLength.Auto);
            grid.AddColumnDefinition(new GridLength(1.0, GridUnitType.Star));

            var gridRow = 0;
            foreach (var property in type.GetProperties()
                                         .Where(p => p.HasVisibleRailsBindAttribute()))
            {
                var railsBind = property.GetCustomAttribute<RailsBindAttribute>();

                Debug.Assert(property.CanRead && property.CanWrite);

                var binding = RailsBindCreator.CreateRailsBinding(viewModel, property);

                UIElement uiElement = CreateUiElement(property, binding);

                if (uiElement != null)
                {
                    grid.RowDefinitions.Add(new RowDefinition());
                    var label = new Label() { Content = property.GetDisplayName() };
                    grid.AddChild(label, gridRow, 0);
                    grid.AddChild(uiElement, gridRow, 1);
                    gridRow++;
                }
            }

            return grid;
        }

        private UIElement CreateUiElement(PropertyInfo property, Binding binding)
        {
            UIElement uiElement = null;
            switch (property.PropertyType)
            {
                case Type type when type == typeof(string)
                                  || type == typeof(int)
                                  || type == typeof(double):
                    uiElement = CreateTextBox(property, binding);
                    break;
                case Type type when type == typeof(bool):
                    uiElement = CreateCheckBox(property, binding);
                    break;
            }

            return uiElement;
        }

        #endregion

        #region Protected Methods

        protected virtual UIElement CreateCheckBox(PropertyInfo propertyInfo, Binding valueBinding)
        {
            var checkBox = new CheckBox() { VerticalAlignment = VerticalAlignment.Center };
            checkBox.SetBinding(ToggleButton.IsCheckedProperty, valueBinding);
            return checkBox;
        }

        protected virtual UIElement CreateTextBox(PropertyInfo propertyInfo, Binding valueBinding)
        {
            var textBox = new TextBox();
            textBox.SetBinding(TextBox.TextProperty, valueBinding);
            return textBox;
        }

        #endregion
    }
}