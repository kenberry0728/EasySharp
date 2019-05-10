using EasySharpStandard.Attributes.Core;
using EasySharpWpf.Commands.Core;
using EasySharpWpf.ViewModels.Rails.Attributes;
using EasySharpWpf.ViewModels.Rails.Core.Edit;
using EasySharpWpf.ViewModels.Rails.Core.Index;
using EasySharpWpf.Views.EasyViews.Core;
using EasySharpWpf.Views.Rails.Core.Edit;
using EasySharpWpf.Views.Rails.Core.Index.Interfaces;
using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;


namespace EasySharpWpf.Views.Rails.Core.Index
{
    public class DefaultRailsIndexViewFactory : IRailsIndexViewFactory
    {
        #region Fields

        private readonly IRailsEditViewFactory railsEditViewFactory;

        #endregion

        #region Constructors

        public DefaultRailsIndexViewFactory()
            : this(null)
        {
        }

        public DefaultRailsIndexViewFactory(IRailsEditViewFactory railsEditViewFactory)
        {
            this.railsEditViewFactory = railsEditViewFactory ?? railsEditViewFactory.Resolve();
        }

        #endregion

        #region Public Methods

        public FrameworkElement CreateIndexView(IList modelList, Type type)
        {
            var stackPanel = new StackPanel();
            var viewModel = new RailsIndexViewModel(modelList, type);

            stackPanel.Children.Add(CreateTable(viewModel));
            stackPanel.Children.Add(CreateAddButton(viewModel));

            return stackPanel;
        }

        #endregion

        #region Protected Methods

        #endregion

        #region Private Methods

        private UIElement CreateAddButton(RailsIndexViewModel viewModel)
        {
            var button = new Button()
            {
                Content = "+",
                HorizontalAlignment = HorizontalAlignment.Left
            };
            button.Command = new DelegateCommand((x) => AddNewItem(viewModel));

            return button;
        }

        private void AddNewItem(RailsIndexViewModel indexViewModel)
        {
            if (this.railsEditViewFactory.ShowEditWindow(indexViewModel.Type, out var editedInstance) == true)
            {
                indexViewModel.ItemsSource.Add(new RailsEditViewModel(editedInstance));
            }
        }

        private FrameworkElement CreateTable(RailsIndexViewModel viewModel)
        {
            var dataGrid = new DataGrid
            {
                DataContext = viewModel,
                AutoGenerateColumns = false,
                CanUserAddRows = false,
                ItemsSource = viewModel.ItemsSource
            };

            foreach (var property in viewModel.Type.GetProperties()
                                              .Where(p => p.HasVisibleRailsBindAttribute()))
            {
                Debug.Assert(property.CanRead);

                var railsBind = property.GetCustomAttribute<RailsBindAttribute>();
                dataGrid.Columns.Add(CreateRailsBindColumn(viewModel, property));
            }

            dataGrid.Columns.Add(CreateEditColumn());
            dataGrid.Columns.Add(CreateDeleteColumn(viewModel));

            return dataGrid;
        }

        private static DataGridTextColumn CreateRailsBindColumn(RailsIndexViewModel viewModel, PropertyInfo property)
        {
            return new DataGridTextColumn
            {
                Binding = CreateRailsBinding(viewModel.ItemsSource.FirstOrDefault(), property),
                IsReadOnly = true,
                Header = property.GetDisplayName()
            };
        }

        private static Binding CreateRailsBinding(
            IRailsEditViewModel viewModel,
            PropertyInfo property)
        {
            var bindingPath = viewModel.GetBindingPath(property);
            var binding = new Binding(bindingPath)
            {
                Mode = BindingMode.OneWay,
            };

            return binding;
        }

        private DataGridTemplateColumn CreateEditColumn()
        {
            return CreateButtonColumn(
                new DelegateCommand(x => this.railsEditViewFactory.Edit(x as RailsEditViewModel)),
                "編集");
        }

        private static DataGridTemplateColumn CreateDeleteColumn(RailsIndexViewModel viewModel)
        {
            return CreateButtonColumn(
                new DelegateCommand(x => Delete(x, viewModel)),
                "削除");
        }

        private static DataGridTemplateColumn CreateButtonColumn(ICommand command, string commandLabel)
        {
            var templateColumn = new DataGridTemplateColumn();
            var editDataTemplate = new DataTemplate();
            var buttonElementFactory = new FrameworkElementFactory(typeof(Button));
            buttonElementFactory.SetValue(Button.CommandProperty, command);
            buttonElementFactory.SetValue(Button.CommandParameterProperty, new Binding());
            buttonElementFactory.SetValue(Button.ContentProperty, commandLabel);
            editDataTemplate.VisualTree = buttonElementFactory;
            templateColumn.CellTemplate = editDataTemplate;
            return templateColumn;
        }

        private static void Delete(object arg, RailsIndexViewModel indexViewModel)
        {
            if (!(arg is RailsEditViewModel itemViewModel))
            {
                return;
            }

            if (MessageBoxResult.OK != MessageBox.Show("本当に削除しますか？", "削除", MessageBoxButton.OKCancel))
            {
                return;
            }

            indexViewModel.ItemsSource.Remove(itemViewModel);
        }
        
        #endregion
    }
}