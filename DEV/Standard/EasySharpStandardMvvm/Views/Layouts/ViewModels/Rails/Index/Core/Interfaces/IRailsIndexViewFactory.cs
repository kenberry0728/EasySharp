using System;
using System.Collections;

namespace EasySharpStandardMvvm.ViewModels.Rails.Index.Core.Interfaces
{
    public interface IRailsIndexViewFactory<TViewControl>
    {
        TViewControl CreateIndexView(IList model, Type type);
    }
}