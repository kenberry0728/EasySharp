using EasySharpStandardMvvm.Views.Layouts.ViewModels.Core;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace EasySharpWpf.ViewModels.Rails.Edit.Implementation
{
    internal class DependentCandidateContainer
    {
        private readonly object model;
        private readonly PropertyInfo dependentPropertyInfo;
        private readonly IDictionary<string, List<ValueAndDisplayValue<string>>> selectableItems;

        public DependentCandidateContainer(
            object model,
            PropertyInfo dependentPropertyInfo,
            IDictionary<string, List<ValueAndDisplayValue<string>>> selectableItems)
        {
            this.model = model;
            this.dependentPropertyInfo = dependentPropertyInfo;
            this.selectableItems = selectableItems;
        }

        public IEnumerable<ValueAndDisplayValue<string>> SelectableItems
        {
            get
            {
                var key = this.dependentPropertyInfo.GetValue(model).ToString();
                if (selectableItems.TryGetValue(key, out var values))
                {
                    return values;
                }

                return Array.Empty<ValueAndDisplayValue<string>>();
            }
        }
    }
}
