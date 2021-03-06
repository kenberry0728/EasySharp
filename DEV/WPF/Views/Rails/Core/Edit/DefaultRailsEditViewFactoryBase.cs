﻿using EasySharpStandardMvvm.Views.Rails.Edit.Core;
using EasySharpWpf.Views.Layouts.Core;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using EasySharpStandardMvvm.Views.Layouts.ViewModels.Rails.Edit.Core;
using EasySharpStandardMvvm.Views.Layouts.ViewModels.Rails.Index.Core.Interfaces;
using EasySharpWpf.Views.Rails.Core.Edit.Interfaces;

namespace EasySharpWpf.Views.Rails.Core.Edit
{
    public abstract class DefaultRailsEditViewFactoryBase
        : DefaultRailsEditViewFactoryBase<Binding, UIElement, Grid>, IRailsEditViewFactory
    {
        public DefaultRailsEditViewFactoryBase(
            IRailsIndexViewFactory<UIElement> railsIndexViewFactory,
            IRailsEditViewModelFactory<Binding> railsEditViewModelFactory,
            IGridService gridService)
            : base(railsIndexViewFactory, railsEditViewModelFactory, gridService)
        {
        }
    }
}
