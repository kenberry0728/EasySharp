using System;
using System.Collections;
using Xamarin.Forms;

namespace EasySharpXamarinForms.Views.Rails.Core.Index.Interfaces
{
    public interface IRailsIndexViewFactory
    {
        View CreateIndexView(IList model, Type type);
    }
}