using System;
using System.Reflection;
using System.Windows.Data;
using EasySharpStandardMvvm.ViewModels.Rails.Edit.Core;
using EasySharpWpf.ViewModels.Rails.Edit.Core;

namespace EasySharpWpf.ViewModels.Rails.Edit.Implementation
{
    internal class RailsEditViewModel : RailsEditViewModelBase<Binding>
    {
        #region Fields

        private readonly IRailsBindCreator railsBindCreator;

        #endregion

        public RailsEditViewModel(
            object model,
            IRailsBindCreator railsBindCreator,
            Type type = null) :
            base(model, type)
        {
            this.railsBindCreator = railsBindCreator;
        }

        #region Properties

        #endregion

        #region Public Methods

        public override Binding CreateRailsBinding(PropertyInfo propertyInfo)
        {
            return this.railsBindCreator.CreateRailsBinding(propertyInfo);
        }

        #endregion

        #region Private Methods

        #endregion
    }
}
