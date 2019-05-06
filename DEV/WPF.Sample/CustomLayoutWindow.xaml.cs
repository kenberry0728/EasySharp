using EasySharpWpf.Sample.Models;
using EasySharpWpf.Views.Rails.Core.Edit;
using System.Windows;

namespace EasySharpWpf.Sample
{
    /// <summary>
    /// Interaction logic for CustomLayoutWindow.xaml
    /// </summary>
    public partial class CustomLayoutWindow : Window
    {
        private readonly IRailsEditViewBinder<CustomLayoutBook> viewBinder;

        public CustomLayoutWindow(IRailsEditViewBinder<CustomLayoutBook> viewBinder = null)
        {
            InitializeComponent();

            this.Loaded += OnLoaded;
            this.viewBinder = viewBinder.Resolve();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var editModel = new CustomLayoutBook()
            {
                Title = "となりのトトロ"
            };

            viewBinder.ApplyRailsBinding(this, editModel);
        }
    }
}
