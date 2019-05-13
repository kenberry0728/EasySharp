using EasySharpStandard.DiskIO.Serializers;
using EasySharpStandard.SafeCodes.Core;
using EasySharpWpf.Models.Rails.Core;
using EasySharpWpf.Sample.Models.AutoLayout;
using EasySharpWpf.Views.EasyViews.Core;
using EasySharpWpf.Views.Rails.Core.Edit;
using EasySharpWpf.Views.Rails.Core.Index;
using System.Windows;

namespace EasySharpWpf.Sample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string SaveFilePath = "books.json";
        private readonly BookShelf bookShelf;

        public MainWindow()
           : this(null)
        {
        }

        public MainWindow(
            IRailsEditViewFactory railsEditViewFactory)
        {
            railsEditViewFactory = railsEditViewFactory.Resolve();

            InitializeComponent();

            if (!Try.To(() => SaveFilePath.DeserializeFromJson<BookShelf>(), out this.bookShelf))
            {
                this.bookShelf = new BookShelf();
                this.bookShelf.Books = new RailsList<Book>()
                {
                    new Book() { Title = "Kafka On The Shore", Author = "Haruki Murakami", Publisher = new Publisher(){ Name="Kodansha" } },
                    new Book() { Title = "Norwegian Wood", Author = "Haruki Murakami", Publisher = new Publisher(){ Name="Kodansha" } }
                };
            }

            this.IndexGrid.Children.Add(railsEditViewFactory.CreateEditView(bookShelf, typeof(BookShelf)));
        }

        private void SaveClicked(object sender, RoutedEventArgs e)
        {
            this.bookShelf.SerializeAsJson(SaveFilePath);
        }

        private void ShowCustomEditView(object sender, RoutedEventArgs e)
        {
            var customLayoutWindow = new CustomLayoutWindow();
            customLayoutWindow.Show();
        }
    }
}
