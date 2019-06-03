using EasySharpStandard.Attributes.Core;
using EasySharpStandard.Collections.Core;
using EasySharpStandard.Reflections.Core;
using EasySharpStandardMvvm.Commands.Core;
using EasySharpStandardMvvm.Models.Rails.Core;
using EasySharpStandardMvvm.Rails.Attributes;
using EasySharpWpf.Commands.Core.Dialogs;
using EasySharpWpf.ViewModels.Rails.Core.Edit;
using EasySharpWpf.ViewModels.Rails.Edit.Core;
using EasySharpWpf.Views.Converters;
using EasySharpWpf.Views.Extensions;
using EasySharpWpf.Views.Rails.Core.Index;
using EasySharpWpf.Views.Rails.Core.Index.Interfaces;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;

namespace EasySharpWpf.Views.Rails.Core.Edit
{
    public class DefaultRailsEditViewFactory
        : DefaultRailsEditViewFactoryBase, IRailsEditViewFactory
    {
        #region Fields

        #endregion

        public DefaultRailsEditViewFactory(
            IRailsIndexViewFactory railsIndexViewFactory = null,
            IRailsEditViewModelFactory railsEditViewModelFactory = null)
            :base(railsIndexViewFactory.Resolve(), railsEditViewModelFactory.Resolve())
        {
        }

        #region Properties

        #endregion

        #region Public Methods

        public override UIElement CreateEditView(object model, Type type = null)
        {
            type = type ?? model.GetType();

            var viewModel = this.RailsEditViewModelFactory.Create(model);
            var grid = new Grid() { DataContext = viewModel };
            grid.AddColumnDefinition(GridLength.Auto);
            grid.AddColumnDefinition(new GridLength(1.0, GridUnitType.Star));

            var gridRow = 0;
            foreach (var property in type.GetProperties()
                                         .Where(p => p.HasVisibleRailsBindAttribute()))
            {
                var railsBind = property.GetCustomAttribute<RailsBindAttribute>();

                Debug.Assert(property.CanRead && property.CanWrite);

                var uiElement = CreatePropertyEditControl(model, property, railsBind);

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

        public override bool? ShowEditView(object initialValueModel, Type type, out object editedModel)
        {
            editedModel = type.New();
            if (initialValueModel != null)
            {
                initialValueModel.CopyRailsBindPropertyValues(editedModel, type);
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

            mainGrid.AddRowDefinition(new GridLength(1.0, GridUnitType.Auto));
            mainGrid.AddChild(button, 1, 0);
            return window.ShowDialog();
        }

        private static Grid CreateOKCancelGrid(object editedModel, Window window)
        {
            var okButton = CreateOKButton(editedModel, window);
            var cancelButton = CreateCancelButton(window);

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
                Command = new CompleteEditDialogCommand(window)
            };
        }

        private static Button CreateCancelButton(Window window)
        {
            return new Button()
            {
                Content = "Cancel",
                IsCancel = true,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Command = new CloseWindowCommand(window)
            };
        }

        #endregion

        #region Protected Methods

        protected override UIElement CreateEditDoubleControl(Binding valueBinding)
        {
            valueBinding.Converter = new DoubleToStringConverter();
            var textBox = new TextBox();
            textBox.SetBinding(TextBox.TextProperty, valueBinding);
            return textBox;
        }

        protected override UIElement CreateEditIntegerControl(Binding valueBinding)
        {
            valueBinding.Converter = new IntToStringConverter();
            var textBox = new TextBox();
            textBox.SetBinding(TextBox.TextProperty, valueBinding);
            return textBox;
        }

        protected override UIElement CreateEditBooleanControl(Binding valueBinding)
        {
            var checkBox = new CheckBox() { VerticalAlignment = VerticalAlignment.Center };
            checkBox.SetBinding(ToggleButton.IsCheckedProperty, valueBinding);
            return checkBox;
        }

        protected override UIElement CreateEditStringControl(Binding valueBinding)
        {
            var textBox = new TextBox();
            textBox.SetBinding(TextBox.TextProperty, valueBinding);
            return textBox;
        }

        protected override UIElement CreateEditClassControl(object propertyValue)
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

        protected override UIElement CreateEditListClassControl(object propertyValue, RailsListBindAttribute railsListBindAttribute)
        {
            return base.CreateEditListClassControl(propertyValue, railsListBindAttribute);
        }

        protected override UIElement CreateEditEnumControl(Type enumType, Binding valueBinding)
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
    }
}