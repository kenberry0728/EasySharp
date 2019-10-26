using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace EasySharpStandardMvvm.Collections
{
    public class SizeRestrictedObservableCollection<T> : ObservableCollection<T>
    {
        private readonly int max;

        public SizeRestrictedObservableCollection(int max = 1000)
        {
            this.CollectionChanged += OnCollectionChanged;
            this.max = max;
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add 
                && this.max < this.Count)
            {
                for (int i = 0; i < this.Count - this.max; i++)
                {
                    this.RemoveAt(0);
                }
            }
        }
    }
}
