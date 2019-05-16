using EasySharpStandard.Attributes.Core;
using EasySharpStandard.Collections.Core;
using EasySharpStandard.Reflections.Core;
using EasySharpWpf.Commands.Core;
using EasySharpWpf.ViewModels.Rails.Attributes;
using EasySharpWpf.ViewModels.Rails.Core.Edit;
using EasySharpWpf.Views.Converters;
using EasySharpWpf.Views.Extensions;
using EasySharpWpf.Views.Rails.Core.Index;
using EasySharpWpf.Views.Rails.Core.Index.Interfaces;
using EasySharpWpf.Views.Rails.Implementations;
using System;
using System.Collections;
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
    public class DefaultRailsEditViewFactory : IRailsEditViewFactory
    {
        #region Fields

        private readonly IRailsIndexViewFactory railsIndexViewFactory;

        #endregion

        public DefaultRailsEditViewFactory(IRailsIndexViewFactory railsIndexViewFactory = null)
        {
            this.railsIndexViewFactory = railsIndexViewFactory.Resolve(this);
        }

        #region Public Methods

        public FrameworkElement CreateEditView(object model, Type type = null)
        {
            type = type ?? model.GetType();

            var viewModel = new RailsEditViewModel(model);
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

                var uiElement = CreateUiElement(model, property, railsBind);

                if (uiElement != null)
                {
                    grid.RowDefinitions.Add(new RowDefinition());
                    var label = new Label() { Content = property.GetDisplayName() };
                    grid.AddChild(label, gridRow, 0, new Thickness(10));
                    grid.AddChild(uiElement, gridRow, 1, new Thickness(10));
                    gridRow++;
                }
            }

            return grid;
        }

        public bool? ShowEditWindow(Type type, out object editedModel)
        {
            return this.ShowEditWindow(null, type, out editedModel);
        }

        public bool? ShowEditWindow(object initialValueModel, Type type, out object editedModel)
        {
            editedModel = type.New();
            if (initialValueModel != null)
            {
                CopyRailsBindPropertyValues(initialValueModel, editedModel, type);
            }

            var windowContent = new StackPanel();
            windowContent.Children.Add(this.CreateEditView(editedModel));
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
                CommandParameter = editedModel,
                Command = new DelegateCommand(x => CompleteEdit(x, window))
            };

            windowContent.Children.Add(button);
            return window.ShowDialog();
        }

        public void Edit(IRailsEditViewModel viewModel)
        {
            var subModel = viewModel.Model;
            var type = viewModel.Type;
            if (!type.IsClass)
            {
                return;
            }

            if (this.ShowEditWindow(subModel, type, out object editInstance) != true)
            {
                return;
            }

            foreach (var property in type.GetProperties()     
                                         .Where(p => p.HasVisibleRailsBindAttribute()))
            {
                viewModel.SetProperty(property, property.GetValue(editInstance));
            }
        }

        #endregion

        #region Protected Methods

        private UIElement CreateUiElement(object model, PropertyInfo property, RailsBindAttribute railsBind)
        {
            UIElement uiElement = null;
            switch (property.PropertyType)
            {
                case Type type when type == typeof(string):
                    uiElement = CreateEditStringControl(property.CreateRailsBinding());
                    break;
                case Type type when type == typeof(int):
                    uiElement = CreateEditIntegerControl(property.CreateRailsBinding());
                    break;
                case Type type when type == typeof(double):
                    uiElement = CreateEditDoubleControl(property.CreateRailsBinding());
                    break;
                case Type type when type == typeof(bool):
                    uiElement = CreateEditBooleanControl(property.CreateRailsBinding());
                    break;
                case Type type when type.IsClass:
                    if (railsBind is RailsListBindAttribute railsListBindAttribute)
                    {
                        uiElement = CreateEditListClassControl(property.GetValue(model), railsListBindAttribute);
                        break;
                    }
                    else
                    {
                        uiElement = CreateEditClassControl(property.GetValue(model));
                        break;
                    }
                case Type type when type.IsClass:
                    uiElement = CreateEditClassControl(property.GetValue(model));
                    break;
                case Type type when type.IsEnum:
                    uiElement = CreateEditEnumControl(type, property.CreateRailsBinding());
                    break;
            }

            return uiElement;
        }

        protected virtual UIElement CreateEditDoubleControl(Binding valueBinding)
        {
            valueBinding.Converter = new DoubleToStringConverter();
            var textBox = new TextBox();
            textBox.SetBinding(TextBox.TextProperty, valueBinding);
            return textBox;
        }

        protected virtual UIElement CreateEditIntegerControl(Binding valueBinding)
        {
            valueBinding.Converter = new IntToStringConverter();
            var textBox = new TextBox();
            textBox.SetBinding(TextBox.TextProperty, valueBinding);
            return textBox;
        }

        protected virtual UIElement CreateEditBooleanControl(Binding valueBinding)
        {
            var checkBox = new CheckBox() { VerticalAlignment = VerticalAlignment.Center };
            checkBox.SetBinding(ToggleButton.IsCheckedProperty, valueBinding);
            return checkBox;
        }

        protected virtual UIElement CreateEditStringControl(Binding valueBinding)
        {
            var textBox = new TextBox();
            textBox.SetBinding(TextBox.TextProperty, valueBinding);
            return textBox;
        }

        protected virtual UIElement CreateEditClassControl(object propertyValue)
        {
            var viewModel = new RailsEditViewModel(propertyValue);
            var button = new Button()
            {
                Command = new DelegateCommand(x => Edit(x as RailsEditViewModel)),
                DataContext = viewModel,
            };

            button.SetBinding(Button.ContentProperty, new Binding(nameof(viewModel.Content)));
            button.SetBinding(Button.CommandParameterProperty, new Binding());

            return button;
        }

        protected virtual UIElement CreateEditListClassControl(object propertyValue, RailsListBindAttribute railsListBindAttribute)
        {
            return this.railsIndexViewFactory.CreateIndexView(propertyValue  as IList, railsListBindAttribute.ElementType);
        }

        protected virtual UIElement CreateEditEnumControl(Type enumType, Binding valueBinding)
        {
            var comboBox = new ComboBox();
            var itemsSource =
                Enum.GetValues(enumType).ToEnumerable()
                    .Select(v => new { Value = v, DisplayValue = enumType.GetEnumDisplayValue(v) });
            comboBox.ItemsSource = itemsSource;
            comboBox.SelectedValuePath = "Value";
            comboBox.DisplayMemberPath = "DisplayValue";
            comboBox.SetBinding(Selector.SelectedValueProperty, valueBinding);
            return comboBox;
        }

        #endregion

        #region Private Methods

        private static void CompleteEdit(object sender, Window window)
        {
            if (CanCompleteEdit(sender))
            {
                window.DialogResult = true;
            }
            else
            {
                // TODO: Error Message
            }
        }

        private static bool CanCompleteEdit(object model)
        {
            var type = model.GetType();
            var validationResults = new List<ValidationResult>();
            // Note: second argument can be used to inject external service.
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