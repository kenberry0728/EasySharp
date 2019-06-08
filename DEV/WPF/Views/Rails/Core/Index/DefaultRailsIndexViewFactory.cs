using EasySharpStandard.Attributes.Core;
using EasySharpStandardMvvm.Attributes.Rails;
using EasySharpStandardMvvm.Commands.Core;
using EasySharpStandardMvvm.Views.Rails.Core;
using EasySharpWpf.ViewModels.Rails.Edit.Implementation;
using EasySharpWpf.ViewModels.Rails.Implementation.Index;
using EasySharpWpf.Views.EasyViews.Core;
using EasySharpWpf.Views.Layouts.Core;
using EasySharpWpf.Views.Rails.Core.Edit;
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
using System.Windows.Input;

namespace EasySharpWpf.Views.Rails.Core.Index
{
    public class DefaultRailsIndexViewFactory : IRailsIndexViewFactory
    {
        #region Fields

        private readonly IRailsEditViewFactory railsEditViewFactory;
        private readonly IGridService gridService;

        #endregion

        #region Constructors

        public DefaultRailsIndexViewFactory(
            IRailsEditViewFactory railsEditViewFactory = null,
            IGridService gridService = null)
        {
            this.railsEditViewFactory = railsEditViewFactory.Resolve(this);
            this.gridService = gridService.Resolve();
        }

        #endregion

        #region Public Methods

        public UIElement CreateIndexView(IList modelList, Type type)
        {
            var grid = this.gridService.Create();
            var viewModel = new RailsIndexViewModel(modelList, type);

            this.gridService.AddStarRowDefinition(grid);
            this.gridService.AddChild(grid, CreateTable(viewModel), thickness: 0);

            this.gridService.AddStarRowDefinition(grid);
            this.gridService.AddChild(grid, CreateAddButton(viewModel), 1, thickness: 0);

            return grid;
        }

        #endregion

        #region Protected Methods

        #endregion

        #region Private Methods

        private FrameworkElement CreateTable(RailsIndexViewModel viewModel)
        {
            var dataGrid = new DataGrid
            {
                DataContext = viewModel,
                AutoGenerateColumns = false,
                CanUserAddRows = false,
                ItemsSource = viewModel.ItemsSource
            };

            foreach (var property in viewModel.ItemType.GetProperties()
                                              .Where(p => p.HasVisibleRailsBindAttribute()))
            {
                Debug.Assert(property.CanRead);

                var railsBind = property.GetCustomAttribute<RailsDataMemberBindAttribute>();
                dataGrid.Columns.Add(CreateRailsBindColumn(property));
            }

            dataGrid.Columns.Add(CreateEditColumn());
            dataGrid.Columns.Add(CreateDeleteColumn(viewModel));

            return dataGrid;
        }

        private DataGridTextColumn CreateRailsBindColumn(
            PropertyInfo property)
        {
            var bindingPath = this.railsEditViewFactory.RailsBindCreator.GetRailsPropertyPath(property);
            var binding = new Binding(bindingPath)
            {
                Mode = BindingMode.OneWay,
            };

            return new DataGridTextColumn
            {
                Binding = binding,
                IsReadOnly = true,
                Header = property.GetDisplayName()
            };
        }

        private static DataGridTemplateColumn CreateButtonColumn(ICommand command, string commandLabel)
        {
            var templateColumn = new DataGridTemplateColumn();
            var editDataTemplate = new DataTemplate();
            var buttonElementFactory = new FrameworkElementFactory(typeof(Button));
            buttonElementFactory.SetValue(ButtonBase.CommandProperty, command);
            buttonElementFactory.SetValue(ButtonBase.CommandParameterProperty, new Binding());
            buttonElementFactory.SetValue(ContentControl.ContentProperty, commandLabel);
            editDataTemplate.VisualTree = buttonElementFactory;
            templateColumn.CellTemplate = editDataTemplate;
            return templateColumn;
        }

        #region Edit

        private DataGridTemplateColumn CreateEditColumn()
        {
            return CreateButtonColumn(
                new DelegateCommand(x => this.railsEditViewFactory.Edit(x as RailsEditViewModel)),
                "編集");
        }

        #endregion

        #region Delete

        private static DataGridTemplateColumn CreateDeleteColumn(RailsIndexViewModel viewModel)
        {
            return CreateButtonColumn(
                new DelegateCommand(x => Delete(x, viewModel)),
                "削除");
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

        #region Add

        private UIElement CreateAddButton(RailsIndexViewModel viewModel)
        {
            var button = new Button
            {
                Content = "+",
                HorizontalAlignment = HorizontalAlignment.Left
            };
            button.Command = new DelegateCommand((x) => AddNewItem(viewModel));

            return button;
        }

        private void AddNewItem(RailsIndexViewModel indexViewModel)
        {
            if (this.railsEditViewFactory.ShowEditWindow(indexViewModel.ItemType, out var editedInstance) == true)
            {
                indexViewModel.ItemsSource.Add(new RailsEditViewModel(editedInstance));
            }
        }

        #endregion

        #endregion
    }
}