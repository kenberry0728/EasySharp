﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace EasySharpStandardMvvm.Views.Layouts.ViewModels.Core
{
    public class ObservableModelLinkedCollection<TViewModel> : ObservableCollection<TViewModel>
        where TViewModel : IViewModelWithModel
    {
        private readonly IList model;

        public ObservableModelLinkedCollection(IEnumerable<TViewModel> viewModel, IList model)
            : base(viewModel)
        {
            this.model = model;
        }

        protected override void ClearItems()
        {
            base.ClearItems();
            this.model.Clear();
        }

        protected override void InsertItem(int index, TViewModel item)
        {
            base.InsertItem(index, item);
            this.model.Insert(index, item.Model);
        }

        protected override void MoveItem(int oldIndex, int newIndex)
        {
            base.MoveItem(oldIndex, newIndex);

            var item = this.model[oldIndex];
            this.RemoveAt(oldIndex);
            this.model.Insert(newIndex, item);
        }

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
            this.model.RemoveAt(index);
        }

        protected override void SetItem(int index, TViewModel item)
        {
            base.SetItem(index, item);
            this.model[index] = item.Model;
        }
    }
}
