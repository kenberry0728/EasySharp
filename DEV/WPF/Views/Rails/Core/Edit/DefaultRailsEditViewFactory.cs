using EasySharpStandard.Attributes.Core;
using EasySharpStandard.Collections.Core;
using EasySharpStandard.Reflections.Core;
using EasySharpStandardMvvm.Attributes.Rails;
using EasySharpStandardMvvm.Commands.Core;
using EasySharpStandardMvvm.Models.Rails.Core;
using EasySharpStandardMvvm.ViewModels.Core;
using EasySharpWpf.Commands.Core.Dialogs;
using EasySharpWpf.ViewModels.Rails.Edit.Core;
using EasySharpWpf.ViewModels.Rails.Edit.Implementation;
using EasySharpWpf.Views.Converters;
using EasySharpWpf.Views.Layouts.Core;
using EasySharpWpf.Views.Rails.Core.Index;
using EasySharpWpf.Views.Rails.Core.Index.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using EasySharpStandardMvvm.Views.Layouts.ViewModels.Core;

namespace EasySharpWpf.Views.Rails.Core.Edit
{
    public class DefaultRailsEditViewFactory
        : DefaultRailsEditViewFactoryBase, IRailsEditViewFactory
    {
        #region Fields

        #endregion

        public DefaultRailsEditViewFactory(
            IRailsIndexViewFactory railsIndexViewFactory = null,
            IRailsEditViewModelFactory railsEditViewModelFactory = null,
            IGridService gridService = null)
            :base(
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
            if (initialValueModel != null)
            {
                initialValueModel.CopyRailsBindPropertyValues(editedModel, type);
            }

            var mainGrid = this.GridService.Create(null);

            var window = new Window
            {
                Content = mainGrid,
                SizeToContent = SizeToContent.WidthAndHeight,
                Title = "編集：" + type.GetDisplayName(),
            };

            this.GridService.AddRowDefinition(mainGrid);
            this.GridService.AddChild(mainGrid, this.CreateEditView(editedModel), 0, 0);

            var button = CreateOKCancelGrid(editedModel, window);

            this.GridService.AddStarRowDefinition(mainGrid);
            this.GridService.AddChild(mainGrid, button, 1, 0);
            return window.ShowDialog();
        }

        private Grid CreateOKCancelGrid(object editedModel, Window window)
        {
            var okButton = CreateOKButton(editedModel, window);
            var cancelButton = CreateCancelButton(window);

            var grid = this.GridService.Create(null);
            this.GridService.AddStarColumnDefinition(grid);
            this.GridService.AddStarColumnDefinition(grid);

            this.GridService.AddChild(grid, okButton, 0, 0);
            this.GridService.AddChild(grid, cancelButton, 0, 1);

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

        protected override UIElement CreateSelectFromCandidateControl(IList<ValueAndDisplayValue<string>> selectableItems, Binding valueBinding)
        {
            var comboBox = new ComboBox();
            comboBox.ItemsSource = selectableItems.ToList();
            comboBox.SelectedValuePath = ValueAndDisplayValue<string>.ValuePath;
            comboBox.DisplayMemberPath = ValueAndDisplayValue<string>.DisplayValuePath;
            comboBox.SetBinding(Selector.SelectedValueProperty, valueBinding);
            return comboBox;
        }

        protected override UIElement CreateSelectFromCandidateControl(
            object model,
            PropertyInfo dependentPropertyInfo,
            IDictionary<string, List<ValueAndDisplayValue<string>>> selectableItems,
            Binding valueBinding)
        {
            var container = new DependentCandidateContainer(model, dependentPropertyInfo, selectableItems);

            //　考え中： ViewModelにPropertyを足すのか？
            // Bind用のViewModelを作って、Propertyアクセッサーに渡す。
            // とりあえず、[PropertyName_SelectableItemContainer].SelectableItems的な。
            // Getterのclassがでやったほうがいいな
            // OnPropertyChangedはどうやって受け取ろう。
            // どっちかといえば、あっち側のプロパティかな
            var comboBox = new ComboBox();
            comboBox.ItemsSource = selectableItems.ToList();
            comboBox.SelectedValuePath = ValueAndDisplayValue<string>.ValuePath;
            comboBox.DisplayMemberPath = ValueAndDisplayValue<string>.DisplayValuePath;
            comboBox.SetBinding(Selector.SelectedValueProperty, valueBinding);
            return comboBox;
        }


        protected override UIElement CreateLabelControl(PropertyInfo property)
        {
            return new Label() { Content = property.GetDisplayName() };
        }


        #endregion
    }
}