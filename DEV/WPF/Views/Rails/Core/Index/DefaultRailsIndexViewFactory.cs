﻿using EasySharpStandard.Attributes.Core;
using EasySharpStandard.Reflections.Core;
using EasySharpWpf.Commands.Core;
using EasySharpWpf.ViewModels.Core;
using EasySharpWpf.ViewModels.Rails.Attributes;
using EasySharpWpf.ViewModels.Rails.Core.Index;
using EasySharpWpf.Views.EasyViews.Core;
using EasySharpWpf.Views.Rails.Core.Edit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace EasySharpWpf.Views.Rails.Core.Index
{
    public class DefaultRailsIndexViewFactory<T> : IRailsIndexViewFactory<T>
        where T : class, new()
    {
        #region Fields

        private readonly Type type = typeof(T);
        private readonly IRailsEditViewFactory<T> railsEditViewFactory;

        #endregion

        #region Constructors

        public DefaultRailsIndexViewFactory()
            : this(null)
        {
        }

        public DefaultRailsIndexViewFactory(IRailsEditViewFactory<T> railsEditViewFactory)
        {
            this.railsEditViewFactory = railsEditViewFactory ?? railsEditViewFactory.Resolve<T>();
        }

        #endregion

        #region Public Methods

        public FrameworkElement CreateIndexView(List<T> modelList)
        {
            var stackPanel = new StackPanel();
            var viewModel = new RailsIndexViewModel<T>(modelList);

            stackPanel.Children.Add(CreateTable(viewModel));
            stackPanel.Children.Add(CreateAddButton(viewModel));

            return stackPanel;
        }

        #endregion

        #region Protected Methods

        #endregion

        #region Private Methods

        private UIElement CreateAddButton(RailsIndexViewModel<T> viewModel)
        {
            var button = new Button()
            {
                Content = "+",
                HorizontalAlignment = HorizontalAlignment.Left
            };
            button.Command = new DelegateCommand((x) => AddNewItem(viewModel));

            return button;
        }

        private void AddNewItem(RailsIndexViewModel<T> indexViewModel)
        {
            var newModel = new T();
            if (ShowEditWindow(newModel) == true)
            {
                indexViewModel.ItemsSource.Add(new RailsItemViewModel<T>(newModel));
            }
        }

        private FrameworkElement CreateTable(RailsIndexViewModel<T> viewModel)
        {
            var dataGrid = new DataGrid
            {
                DataContext = viewModel,
                AutoGenerateColumns = false,
                CanUserAddRows = false,
                ItemsSource = viewModel.ItemsSource
            };

            foreach (var property in this.type.GetProperties()
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

        private static DataGridTextColumn CreateRailsBindColumn(RailsIndexViewModel<T> viewModel, PropertyInfo property)
        {
            return new DataGridTextColumn
            {
                Binding = CreateRailsBinding(viewModel.ItemsSource.FirstOrDefault(), property),
                IsReadOnly = true,
                Header = property.GetDisplayName()
            };
        }

        private static Binding CreateRailsBinding(
            RailsItemViewModel<T> viewModel,
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
            return CreateButtonColumn(new DelegateCommand(this.Edit), "編集");
        }

        private static DataGridTemplateColumn CreateDeleteColumn(RailsIndexViewModel<T> viewModel)
        {
            return CreateButtonColumn(new DelegateCommand(x => Delete(x, viewModel)), "削除");
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
        
        private void Edit(object arg)
        {
            if (!(arg is RailsItemViewModel<T> viewModel))
            {
                return;
            }

            var model = viewModel.Model;
            var editInstance = new T();
            CopyPropertyValues(model, editInstance);
            if (ShowEditWindow(editInstance) != true)
            {
                return;
            }

            foreach (var property in
                type.GetProperties()
                    .Where(p => p.HasVisibleRailsBindAttribute()))
            {
                viewModel.SetProperty(property, property.GetValue(editInstance));
            }
        }

        private static void Delete(object arg, RailsIndexViewModel<T> indexViewModel)
        {
            if (!(arg is RailsItemViewModel<T> itemViewModel))
            {
                return;
            }

            if (MessageBoxResult.OK != MessageBox.Show("本当に削除しますか？", "削除", MessageBoxButton.OKCancel))
            {
                return;
            }

            indexViewModel.ItemsSource.Remove(itemViewModel);
        }

        private static void CopyPropertyValues(T from, T to)
        {
            var type = typeof(T);
            type.CopyPropertyValues(
                from,
                to,
                p => p.HasCustomAttribute<RailsBindAttribute>()
                     && p.CanRead
                     && p.CanWrite);
        }

        private bool? ShowEditWindow(T model)
        {
            var windowContent = new StackPanel();
            windowContent.Children.Add(this.railsEditViewFactory.CreateEditView(model));
            var window = new Window
            {
                Content = windowContent,
                Width = 500,
                SizeToContent = SizeToContent.Height,
                Title = "編集：" + this.type.GetDisplayName()
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

        private static void CompleteEdit(T model, Window window)
        {
            if (CanCompleteEdit(model))
            {
                window.DialogResult = true;
            }
        }

        private static bool CanCompleteEdit(T model)
        {
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

        #endregion
    }
}