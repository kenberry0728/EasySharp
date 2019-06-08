﻿using EasySharpStandardMvvm.ViewModels.Rails.Edit.Core;
using EasySharpWpf.ViewModels.Rails.Edit.Core;
using System;
using System.Windows.Data;

namespace EasySharpWpf.ViewModels.Rails.Edit.Implementation
{
    internal class RailsEditViewModelFactory : IRailsEditViewModelFactory
    {
        public IRailsBindCreator<Binding> RailsBindCreator { get; } = new RailsBindCreator();

        public IRailsEditViewModel Create(object model, Type type = null)
        {
            return new RailsEditViewModel(model, type);
        }
    }
}
