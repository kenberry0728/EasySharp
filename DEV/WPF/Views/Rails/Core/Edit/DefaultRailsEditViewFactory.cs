using EasySharpStandard.Attributes.Core;
using EasySharpStandard.Collections.Core;
using EasySharpStandard.Reflections.Core;
using EasySharpStandard.Validations.Core;
using EasySharpStandardMvvm.Rails.Attributes;
using EasySharpWpf.Commands.Core;
using EasySharpWpf.ViewModels.Rails.Core.Edit;
using EasySharpWpf.ViewModels.Rails.Edit.Core;
using EasySharpWpf.Views.Converters;
using EasySharpWpf.Views.Extensions;
using EasySharpWpf.Views.Rails.Core.Index;
using EasySharpWpf.Views.Rails.Core.Index.Interfaces;
using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace EasySharpWpf.Views.Rails.Core.Edit
{
    public class DefaultRailsEditViewFactory : IRailsEditViewFactory
    {
        #region Fields

        private readonly IRailsEditViewModelFactory railsEditViewModelFactory;
        private readonly IRailsIndexViewFactory railsIndexViewFactory;

        #endregion

        public DefaultRailsEditViewFactory(
            IRailsIndexViewFactory railsIndexViewFactory = null,
            IRailsEditViewModelFactory railsEditViewModelFactory = null)
        {
            this.railsIndexViewFactory = railsIndexViewFactory.Resolve(this);
            this.railsEditViewModelFactory = railsEditViewModelFactory.Resolve();
            this.RailsBindCreator = this.railsEditViewModelFactory.RailsBindCreator;
        }

        #region Properties

        public IRailsBindCreator RailsBindCreator { get; }

        #endregion

        #region Public Methods

        public FrameworkElement CreateEditView(object model, Type type = null)
        {
            type = type ?? model.GetType();

            var viewModel = new RailsEditViewModel(model);
            var grid = new Grid() { DataContext = viewModel };
            grid.AddColumnDefinition(GridLength.Auto);
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
                    if (railsBind is RailsListBindAttribute)
                    {
                        grid.AddRowDefinition(new GridLength(1.0, GridUnitType.Star));
                    }
                    else
                    {
                        grid.AddRowDefinition(GridLength.Auto);
                    }

                    var label = new Label() { Content = property.GetDisplayName() };
                    grid.AddChild(label, gridRow, 0);
                    grid.AddChild(uiElement, gridRow, 1);
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

            var mainGrid = new Grid();
            var window = new Window
            {
                Content = mainGrid,
                SizeToContent = SizeToContent.WidthAndHeight,
                Title = "編集：" + type.GetDisplayName(),
            };

            mainGrid.AddRowDefinition();
            mainGrid.AddChild(this.CreateEditView(editedModel), 0, 0);

            var button = CreateOKCancelGrid(editedModel, window);

            mainGrid.AddRowDefinition();
            mainGrid.AddChild(button, 1, 0);
            return window.ShowDialog();
        }

        private static Grid CreateOKCancelGrid(object editedModel, Window window)
        {
            var okButton = CreateOKButton(editedModel, window);
            var cancelButton = CreateCancelButton(editedModel, window);

            var grid = new Grid();
            grid.AddColumnDefinition(new GridLength(1.0, GridUnitType.Star));
            grid.AddColumnDefinition(new GridLength(1.0, GridUnitType.Star));

            grid.AddChild(okButton, 0, 0);
            grid.AddChild(cancelButton, 0, 1);

            return grid;
        }

        private static Button CreateOKButton(object editedModel, Window window)
        {
            return new Button()
            {
                Content = "OK",
                IsDefault = true,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                CommandParameter = editedModel,
                Command = new DelegateCommand(x => CompleteEdit(x, window))
            };
        }

        private static Button CreateCancelButton(object editedModel, Window window)
        {
            return new Button()
            {
                Content = "Cancel",
                IsCancel = true,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                CommandParameter = editedModel,
                Command = new DelegateCommand(x => CancelEdit(window))
            };
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
                var propertyName = this.RailsBindCreator.GetPropertyName(property);
                viewModel[propertyName] = property.GetValue(editInstance);
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
                    uiElement = CreateEditStringControl(this.RailsBindCreator.CreateRailsBinding(property));
                    break;
                case Type type when type == typeof(int):
                    uiElement = CreateEditIntegerControl(this.RailsBindCreator.CreateRailsBinding(property));
                    break;
                case Type type when type == typeof(double):
                    uiElement = CreateEditDoubleControl(this.RailsBindCreator.CreateRailsBinding(property));
                    break;
                case Type type when type == typeof(bool):
                    uiElement = CreateEditBooleanControl(this.RailsBindCreator.CreateRailsBinding(property));
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
                    uiElement = CreateEditEnumControl(type, this.RailsBindCreator.CreateRailsBinding(property));
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

        private static void CompleteEdit(object model, Window window)
        {
            var validationResults = model.Validate();
            if (validationResults.Any())
            {
                MessageBox.Show(
                    string.Join(
                        Environment.NewLine,
                        validationResults.Select(r => r.ErrorMessage)));
            }
            else
            {
                window.DialogResult = true;
            }
        }

        private static void CancelEdit(Window window)
        {
            window.Close();
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