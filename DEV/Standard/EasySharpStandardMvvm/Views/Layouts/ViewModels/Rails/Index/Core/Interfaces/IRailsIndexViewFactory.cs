using System;
using System.Collections;

namespace EasySharpStandardMvvm.Views.Layouts.ViewModels.Rails.Index.Core.Interfaces
{
    public interface IRailsIndexViewFactory<TViewControl>
    {
        TViewControl CreateIndexView(IList model, Type type);
    }
}