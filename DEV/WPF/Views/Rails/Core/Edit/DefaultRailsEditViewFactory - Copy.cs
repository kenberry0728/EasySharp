using EasySharpStandard.Attributes.Core;
using EasySharpStandard.Reflections.Core;
using EasySharpWpf.Commands.Core;
using EasySharpWpf.ViewModels.Rails.Attributes;
using EasySharpWpf.ViewModels.Rails.Core.Edit;
using EasySharpWpf.Views.Extensions;
using EasySharpWpf.Views.Rails.Core.Edit.Interfaces;
using EasySharpWpf.Views.Rails.Implementations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace EasySharpWpf.Views.Rails.Core.Edit
{
    public class DefaultRailsEditViewFactory2
    {
        #region Fields

        #endregion

        #region Constructors

        public DefaultRailsEditViewFactory2()
        {
        }

        #endregion

        #region Public Methods

        public FrameworkElement CreateEditView(object model, Type type = null)
        {
            type = type ?? model.GetType();

            var viewModel = new RailsEditViewModel2(model);
            var grid = new Grid() { DataContext = viewModel };
            grid.AddColumnDefinition(GridLength.Auto);
            grid.AddColumnDefinition(new GridLength(1.0, GridUnitType.Star));
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
        
        private bool? ShowEditWindow(object model, Type type, out object editedModel)
        {
            editedModel = Activator.CreateInstance(type);
            CopyRailsBindPropertyValues(model, editedModel, model.GetType());

            var windowContent = new StackPanel();
            windowContent.Children.Add(this.CreateEditView(model));
            var window = new Window
            {
                Content = windowContent,
                Width = 500,
                SizeToContent = SizeToContent.Height,
                Title = "編集：" + type.GetDisplayName()
            };

            var button = new Button()
            {
                Content = "OK",
                IsDefault = true,
                HorizontalAlignment = HorizontalAlignment.Center,
                Command = new DelegateCommand(x => CompleteEdit(model, window))
            };

            windowContent.Children.Add(button);
            return window.ShowDialog();
        }

        internal bool? ShowEditWindowInternal(object model, Type type, out object editedModel)
        {
            var result = this.ShowEditWindow(model, type, out editedModel);
            return result;
        }

        #endregion

        #region Protected Methods

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
                case Type type when type.IsClass:
                    uiElement = CreateEditButton(property, binding);
                    break;
                // TODO: ENum combobox 対応
            }

            return uiElement;
        }

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

        protected virtual UIElement CreateEditButton(PropertyInfo propertyInfo, Binding valueBinding)
        {
            var button = new Button()
            {
                Command = new DelegateCommand(Edit),
            };

            button.SetBinding(Button.CommandParameterProperty, valueBinding);

            return button;
        }

        #endregion

        #region Private Methods

        private static void CompleteEdit(object model, Window window)
        {
            if (CanCompleteEdit(model))
            {
                window.DialogResult = true;
            }
        }

        private static bool CanCompleteEdit(object model)
        {
            var type = model.GetType();
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model, null, null);

            Validator.TryValidateObject(
                model,
                validationContext,
                validationResults,
                true);

            if (validationResults.Any())
            {
                MessageBox.Show(
                    string.Join(
                        Environment.NewLine,
                        validationResults.Select(r => r.ErrorMessage)));
                return false;
            }
            else
            {
                return true;
            }
        }

        private void Edit(object arg)
        {
            var editInstanceType = arg.GetType();
            if (!editInstanceType.IsClass)
            {
                // TODO: where句の確認. new()
                return;
            }

            var factoryType = typeof(DefaultRailsEditViewFactory<>);
            var genericType = factoryType.MakeGenericType(editInstanceType);
            dynamic factory = Activator.CreateInstance(genericType);

            if (factory.ShowEditWindowInternal(arg, out object editInstance) != true)
            {
                return;
            }

            CopyRailsBindPropertyValues(editInstance, arg, editInstanceType);


            //foreach (var property in
            //    type.GetProperties()
            //        .Where(p => p.HasVisibleRailsBindAttribute()))
            //{
            //    viewModel.SetProperty(property, property.GetValue(editInstance));
            //}

        }

        private static void CopyRailsBindPropertyValues(object from, object to, Type type)
        {
            type.CopyPropertyValues(
                from,
                to,
                p => p.HasCustomAttribute<RailsBindAttribute>()
                     && p.CanRead
                     && p.CanWrite);
        }

        #endregion
    }
}