using EasySharpStandard.Attributes.Core;
using EasySharpStandard.Collections.Core;
using EasySharpStandard.Reflections.Core;
using EasySharpStandardMvvm.Attributes.Rails;
using EasySharpStandardMvvm.Commands.Core;
using EasySharpStandardMvvm.Models.Rails.Core;
using EasySharpWpf.ViewModels.Rails.Core.Edit;
using EasySharpXamarinForms.ViewModels.Rails.Edit.Core;
using EasySharpXamarinForms.Views.Converters;
using EasySharpXamarinForms.Views.Layouts.Core;
using EasySharpXamarinForms.Views.Rails.Core.Edit.Interfaces;
using EasySharpXamarinForms.Views.Rails.Core.Index;
using EasySharpXamarinForms.Views.Rails.Core.Index.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EasySharpStandardMvvm.Views.Layouts.ViewModels.Core;
using Xamarin.Forms;

namespace EasySharpXamarinForms.Views.Rails.Core.Edit
{
    public class DefaultRailsEditViewFactory 
        : DefaultRailsEditViewFactoryBase
    {
        #region Fields

        #endregion

        #region Constructor

        public DefaultRailsEditViewFactory(
            IRailsIndexViewFactory railsIndexViewFactory = null,
            IRailsEditViewModelFactory railsEditViewModelFactory = null,
            IGridService gridService = null)
            : base(
                  railsIndexViewFactory.Resolve(),
                  railsEditViewModelFactory.Resolve(),
                  gridService.Resolve())
        {
        }

        #endregion

        #region Properties

        #endregion

        #region Public Methods

        public override bool? ShowEditView(object initialValueModel, Type type, out object editedModel)
        {
            editedModel = type.New();
            if (initialValueModel != null)
            {
                initialValueModel.CopyRailsBindPropertyValues(editedModel, type);
            }

            var mainGrid = this.GridService.Create();
            var window = new ContentPage
            {
                Content = mainGrid,
                //SizeToContent = SizeToContent.WidthAndHeight,
                Title = "編集：" + type.GetDisplayName(),
            };

            this.GridService.AddRowDefinition(mainGrid);
            this.GridService.AddChild(mainGrid, this.CreateEditView(editedModel), 0, 0);

            var button = CreateOKCancelGrid(editedModel, window);

            this.GridService.AddRowDefinition(mainGrid);
            this.GridService.AddChild(mainGrid, button, 1, 0);
            // await Navigation.PushAsync(window);?
            return true;
        }

        private Grid CreateOKCancelGrid(object editedModel, ContentPage window)
        {
            var okButton = CreateOKButton(editedModel, window);
            var cancelButton = CreateCancelButton(window);

            var grid = this.GridService.Create();

            this.GridService.AddStarColumnDefinition(grid);
            this.GridService.AddStarColumnDefinition(grid);

            this.GridService.AddChild(grid, okButton, 0, 0);
            this.GridService.AddChild(grid, cancelButton, 0, 1);

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

        protected override View CreateSelectFromCandidateControl(object model,
            PropertyInfo dependentPropertyInfo,
            IDictionary<string, List<ValueAndDisplayValue<string>>> selectableItems,
            Binding valueBinding)
        {
            throw new NotImplementedException();
        }

        protected override View CreateEditListClassControl(object propertyValue, RailsDataMemberListBindAttribute railsDataMemberListBindAttribute)
        {
            return base.CreateEditListClassControl(propertyValue, railsDataMemberListBindAttribute);
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

        protected override View CreateLabelControl(PropertyInfo property)
        {
            return new Label() { Text = property.GetDisplayName() };
        }

        protected override View CreateSelectFromCandidateControl(
            Binding valueBinding, 
            Binding itemsSourceBinding,
            string valuePath = null,
            string displayMemberPath = null)
        {
            var comboBox = new Picker();
            var itemsSource = itemsSourceBinding;
            comboBox.SetBinding(Picker.ItemsSourceProperty, itemsSourceBinding);
            //comboBox.Item = "Value";
            comboBox.ItemDisplayBinding = new Binding(ValueAndDisplayValue<string>.DisplayValuePath);
            comboBox.SetBinding(Picker.SelectedItemProperty, valueBinding);
            return comboBox;
        }

        #endregion
    }
}