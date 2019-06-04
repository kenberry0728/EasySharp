using EasySharpStandard.Attributes.Core;
using EasySharpStandardMvvm.Rails.Attributes;
using EasySharpStandardMvvm.ViewModels.Rails.Edit.Core;
using EasySharpStandardMvvm.ViewModels.Rails.Index.Core.Interfaces;
using EasySharpStandardMvvm.Views.Layouts.Core;
using EasySharpWpf.ViewModels.Rails.Edit.Core;
using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace EasySharpWpf.Views.Rails.Core.Edit
{
    public abstract class DefaultRailsEditViewFactoryBase<TBinding, TViewControl,TGrid> 
        : IRailsEditViewFactory<TBinding, TViewControl>
        where TGrid : TViewControl
    {
        #region Fields

        #endregion

        public DefaultRailsEditViewFactoryBase(
            IRailsIndexViewFactory<TViewControl> railsIndexViewFactory,
            IRailsEditViewModelFactory<TBinding> railsEditViewModelFactory,
            IGridService<TGrid, TViewControl> gridService)
        {
            this.RailsIndexViewFactory = railsIndexViewFactory;
            this.RailsEditViewModelFactory = railsEditViewModelFactory;
            this.GridService = gridService;
            this.RailsBindCreator = this.RailsEditViewModelFactory.RailsBindCreator;
        }

        #region Properties

        public IRailsBindCreator<TBinding> RailsBindCreator { get; }
        protected IRailsEditViewModelFactory<TBinding> RailsEditViewModelFactory { get; }
        protected IGridService<TGrid, TViewControl> GridService { get; }
        protected IRailsIndexViewFactory<TViewControl> RailsIndexViewFactory { get; }

        #endregion

        #region Public Methods

        public virtual void Edit(IRailsEditViewModel viewModel)
        {
            var subModel = viewModel.Model;
            var type = viewModel.Type;
            if (!type.IsClass)
            {
                return;
            }

            if (this.ShowEditView(subModel, type, out object editInstance) != true)
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

        public virtual bool? ShowEditWindow(Type type, out object editedModel)
        {
            return this.ShowEditView(null, type, out editedModel);
        }

        public virtual TViewControl CreateEditView(object model, Type type = null)
        {
            type = type ?? model.GetType();

            var viewModel = this.RailsEditViewModelFactory.Create(model);
            var grid = this.GridService.Create(viewModel);
            this.GridService.AddAutoColumnDefinition(grid);
            this.GridService.AddStarColumnDefinition(grid);

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
                        this.GridService.AddStarColumnDefinition(grid);
                    }
                    else
                    {
                        this.GridService.AddAutoRowDefinition(grid);
                    }

                    var label = this.CreateLabelControl(property);
                    this.GridService.AddChild(grid, label, gridRow, 0);
                    this.GridService.AddChild(grid, uiElement, gridRow, 1);
                    gridRow++;
                }
            }

            return grid;
        }

        public abstract bool? ShowEditView(object initialValueModel, Type type, out object editedModel);


        #endregion

        #region Protected Methods

        protected virtual TViewControl CreatePropertyEditControl(object model, PropertyInfo property, RailsBindAttribute railsBind)
        {
            TViewControl uiElement = default;
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

        protected abstract TViewControl CreateLabelControl(PropertyInfo property);

        protected abstract TViewControl CreateEditDoubleControl(TBinding valueBinding);

        protected abstract TViewControl CreateEditIntegerControl(TBinding valueBinding);

        protected abstract TViewControl CreateEditBooleanControl(TBinding valueBinding);

        protected abstract TViewControl CreateEditStringControl(TBinding valueBinding);

        protected abstract TViewControl CreateEditClassControl(object propertyValue);

        protected abstract TViewControl CreateEditEnumControl(Type enumType, TBinding valueBinding);

        protected virtual TViewControl CreateEditListClassControl(object propertyValue, RailsListBindAttribute railsListBindAttribute)
        {
            return this.RailsIndexViewFactory.CreateIndexView(propertyValue as IList, railsListBindAttribute.ElementType);
        }
        
        #endregion
    }
}