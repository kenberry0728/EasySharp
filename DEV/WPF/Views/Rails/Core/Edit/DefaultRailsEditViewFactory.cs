using EasySharpStandard.Attributes.Core;
using EasySharpStandard.Collections.Core;
using EasySharpStandard.Reflections.Core;
using EasySharpStandardMvvm.Attributes.Rails;
using EasySharpStandardMvvm.Commands.Core;
using EasySharpStandardMvvm.Models.Rails.Core;
using EasySharpWpf.Commands.Core.Dialogs;
using EasySharpWpf.ViewModels.Rails.Edit.Core;
using EasySharpWpf.Views.Converters;
using EasySharpWpf.Views.Layouts.Core;
using EasySharpWpf.Views.Rails.Core.Index;
using EasySharpWpf.Views.Rails.Core.Index.Interfaces;
using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using EasySharpStandardMvvm.Views.Layouts.ViewModels.Rails.Edit.Core;

namespace EasySharpWpf.Views.Rails.Core.Edit
{
    public class DefaultRailsEditViewFactory : DefaultRailsEditViewFactoryBase
    {
        #region Fields

        #endregion

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

        #region Properties

        #endregion

        #region Public Methods

        public override bool? ShowEditView(object initialValueModel, Type type, out object editedModel)
        {
            editedModel = type.New();
            initialValueModel?.CopyRailsBindPropertyValues(editedModel, type);

            var mainGrid = this.GridService.Create(null);

            var window = new Window
            {
                Content = mainGrid,
                SizeToContent = SizeToContent.WidthAndHeight,
                Title = "編集：" + type.GetDisplayName(),
            };

            // <Grid name="mainGrid">
            //  <Grid.RowDefinitions>
            //     <RowDefinition Height="*">
            //     <RowDefinition Height="Auto"/>
            //  </Grid.RowDefinitions>
            //  <EditView Grid.Row=0>
            //  <Grid name = "okCancelGrid" Grid.Row=1/>

            this.GridService.AddStarRowDefinition(mainGrid);
            this.GridService.AddChild(mainGrid, this.CreateEditView(editedModel), 0, 0);

            var okCancelGrid = CreateOkCancelGrid(editedModel, window);
            this.GridService.AddAutoRowDefinition(mainGrid);
            this.GridService.AddChild(mainGrid, okCancelGrid, 1, 0);

            return window.ShowDialog();
        }

        private Grid CreateOkCancelGrid(object editedModel, Window window)
        {
            var okButton = CreateOkButton(editedModel, window);
            var cancelButton = CreateCancelButton(window);

            var grid = this.GridService.Create(null);
            this.GridService.AddStarColumnDefinition(grid);
            this.GridService.AddStarColumnDefinition(grid);

            this.GridService.AddChild(grid, okButton, 0, 0);
            this.GridService.AddChild(grid, cancelButton, 0, 1);

            return grid;
        }

        private static Button CreateOkButton(object editedModel, Window window)
        {
            return new Button
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
            return new Button
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
            var checkBox = new CheckBox { VerticalAlignment = VerticalAlignment.Center };
            checkBox.SetBinding(ToggleButton.IsCheckedProperty, valueBinding);
            return checkBox;
        }

        protected override UIElement CreateEditStringControl(Binding valueBinding)
        {
            valueBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
            var textBox = new TextBox();
            textBox.SetBinding(TextBox.TextProperty, valueBinding);
            return textBox;
        }

        protected override UIElement CreateEditDateTimeControl(Type type, Binding valueBinding)
        {
            var datePicker = new DatePicker();
            datePicker.SetBinding(DatePicker.SelectedDateProperty, valueBinding);
            return datePicker;
        }

        protected override UIElement CreateEditClassControl(object propertyValue)
        {
            var viewModel = this.RailsEditViewModelFactory.Create(propertyValue);
            var button = new Button
            {
                Command = new DelegateCommand(x => Edit(x as IRailsEditViewModel)),
                DataContext = viewModel,
            };

            button.SetBinding(ContentControl.ContentProperty, new Binding(nameof(viewModel.Content)));
            button.SetBinding(ButtonBase.CommandParameterProperty, new Binding());

            return button;
        }

        protected override UIElement CreateEditListClassControl(object propertyValue, RailsDataMemberListBindAttribute railsDataMemberListBindAttribute)
        {
            return base.CreateEditListClassControl(propertyValue, railsDataMemberListBindAttribute);
        }

        protected override UIElement CreateEditEnumControl(Type enumType, Binding valueBinding)
        {
            // TODO: Creating ComboBoxService.
            // TODO: Using Value and DisplayValue.
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

        protected override UIElement CreateSelectFromCandidateControl(
            Binding valueBinding,
            Binding itemsSourceBinding,
            string valuePath = null,
            string displayMemberPath = null)
        {
            var comboBox = new ComboBox();
            comboBox.SelectedValuePath = valuePath;
            comboBox.DisplayMemberPath = displayMemberPath;
            comboBox.SetBinding(Selector.SelectedValueProperty, valueBinding);
            comboBox.SetBinding(ItemsControl.ItemsSourceProperty, itemsSourceBinding);
            return comboBox;
        }

        protected override UIElement CreateLabelControl(PropertyInfo property)
        {
            return new Label { Content = property.GetDisplayName() };
        }

        #endregion
    }
}