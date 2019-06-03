using EasySharpStandard.Attributes.Core;
using EasySharpStandard.Collections.Core;
using EasySharpStandard.Reflections.Core;
using EasySharpStandardMvvm.Commands.Core;
using EasySharpStandardMvvm.Models.Rails.Core;
using EasySharpStandardMvvm.Rails.Attributes;
using EasySharpWpf.ViewModels.Rails.Core.Edit;
using EasySharpWpf.Views.Rails.Core;
using EasySharpXamarinForms.ViewModels.Rails.Edit.Core;
using EasySharpXamarinForms.Views.Converters;
using EasySharpXamarinForms.Views.Extensions;
using EasySharpXamarinForms.Views.Rails.Core.Edit;
using EasySharpXamarinForms.Views.Rails.Core.Edit.Interfaces;
using EasySharpXamarinForms.Views.Rails.Core.Index;
using EasySharpXamarinForms.Views.Rails.Core.Index.Interfaces;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace EasySharpXamarinForms.Views.Rails.Core.Edit
{
    public class DefaultRailsEditViewFactory : DefaultRailsEditViewFactoryBase, IRailsEditViewFactory
    {
        #region Fields


        #endregion

        public DefaultRailsEditViewFactory(
            IRailsIndexViewFactory railsIndexViewFactory = null,
            IRailsEditViewModelFactory railsEditViewModelFactory = null)
            : base(railsIndexViewFactory.Resolve(), railsEditViewModelFactory.Resolve())
        {
        }

        #region Properties

        #endregion

        #region Public Methods

        public override View CreateEditView(object model, Type type = null)
        {
            type = type ?? model.GetType();

            var viewModel = this.RailsEditViewModelFactory.Create(model);
            var grid = new Grid() { BindingContext = viewModel };
            grid.AddColumnDefinition(GridLength.Auto);
            grid.AddColumnDefinition(new GridLength(1.0, GridUnitType.Star));

            var gridRow = 0;
            foreach (var property in type.GetProperties()
                                         .Where(p => p.HasVisibleRailsBindAttribute()))
            {
                var railsBind = property.GetCustomAttribute<RailsBindAttribute>();

                Debug.Assert(property.CanRead && property.CanWrite);

                var uiElement = this.CreatePropertyEditControl(model, property, railsBind);

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

                    var label = new Label() { Text = property.GetDisplayName() };
                    grid.AddChild(label, gridRow, 0);
                    grid.AddChild(uiElement, gridRow, 1);
                    gridRow++;
                }
            }

            return grid;
        }

        public override bool? ShowEditWindow(object initialValueModel, Type type, out object editedModel)
        {
            editedModel = type.New();
            if (initialValueModel != null)
            {
                initialValueModel.CopyRailsBindPropertyValues(editedModel, type);
            }

            var mainGrid = new Grid();
            var window = new ContentPage
            {
                Content = mainGrid,
                //SizeToContent = SizeToContent.WidthAndHeight,
                Title = "編集：" + type.GetDisplayName(),
            };

            mainGrid.AddRowDefinition();
            mainGrid.AddChild(this.CreateEditView(editedModel), 0, 0);

            var button = CreateOKCancelGrid(editedModel, window);

            mainGrid.AddRowDefinition();
            mainGrid.AddChild(button, 1, 0);
            // await Navigation.PushAsync(window);?
            return true;
        }

        private static Grid CreateOKCancelGrid(object editedModel, ContentPage window)
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

        private static Button CreateOKButton(object editedModel, ContentPage window)
        {
            return new Button()
            {
                Text = "OK",
                //IsDefault = true,
                //HorizontalAlignment = HorizontalAlignment.Stretch,
                CommandParameter = editedModel,
                //Command = new CompleteEditDialogCommand(window)
            };
        }

        private static Button CreateCancelButton(ContentPage window)
        {
            return new Button()
            {
                Text = "Cancel",
                //IsCancel = true,
                //HorizontalAlignment = HorizontalAlignment.Stretch,
                //Command = new CloseWindowCommand(window)
            };
        }

        #endregion

        #region Protected Methods

        protected override View CreateEditDoubleControl(Binding valueBinding)
        {
            valueBinding.Converter = new DoubleToStringConverter();
            var textBox = new Editor();
            textBox.SetBinding(Editor.TextProperty, valueBinding);
            return textBox;
        }

        protected override View CreateEditIntegerControl(Binding valueBinding)
        {
            valueBinding.Converter = new IntToStringConverter();
            var textBox = new Editor();
            textBox.SetBinding(Editor.TextProperty, valueBinding);
            return textBox;
        }

        protected override View CreateEditBooleanControl(Binding valueBinding)
        {
            var checkBox = new Xamarin.Forms.Switch();
            checkBox.SetBinding(Xamarin.Forms.Switch.IsToggledProperty, valueBinding);
            return checkBox;
        }

        protected override View CreateEditStringControl(Binding valueBinding)
        {
            var textBox = new Editor();
            textBox.SetBinding(Editor.TextProperty, valueBinding);
            return textBox;
        }

        protected override View CreateEditClassControl(object propertyValue)
        {
            var viewModel = new RailsEditViewModel(propertyValue);
            var button = new Button()
            {
                Command = new DelegateCommand(x => Edit(x as RailsEditViewModel)),
                BindingContext = viewModel,
            };

            button.SetBinding(Button.TextProperty, new Binding(nameof(viewModel.Content)));
            button.SetBinding(Button.CommandParameterProperty, new Binding());

            return button;
        }

        protected override View CreateEditListClassControl(object propertyValue, RailsListBindAttribute railsListBindAttribute)
        {
            return base.CreateEditListClassControl(propertyValue, railsListBindAttribute);
        }

        protected override View CreateEditEnumControl(Type enumType, Binding valueBinding)
        {
            var comboBox = new Picker();
            var itemsSource =
                Enum.GetValues(enumType).ToEnumerable()
                    .Select(v => new { Value = v, DisplayValue = enumType.GetEnumDisplayValue(v) }).ToList();
            comboBox.ItemsSource = itemsSource;
            //comboBox.Item = "Value";
            comboBox.ItemDisplayBinding = new Binding("DisplayValue");
            comboBox.SetBinding(Picker.SelectedItemProperty, valueBinding);
            return comboBox;
        }
        
        #endregion
    }
}