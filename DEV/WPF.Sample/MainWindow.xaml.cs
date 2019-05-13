using EasySharpStandard.DiskIO.Serializers;
using EasySharpStandard.SafeCodes.Core;
using EasySharpWpf.Sample.Models.AutoLayout;
using EasySharpWpf.Views.EasyViews.Core;
using EasySharpWpf.Views.Rails.Core.Index;
using EasySharpWpf.Views.Rails.Core.Index.Interfaces;
using System.Collections.Generic;
using System.Windows;

namespace EasySharpWpf.Sample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string SaveFilePath = "books.json";
        private readonly List<Book> books;

        public MainWindow()
           : this(null)
        {
        }

        public MainWindow(
            IRailsIndexViewFactory railsIndexViewFactory)
        {
            railsIndexViewFactory = railsIndexViewFactory.Resolve();

            InitializeComponent();
            if (!Try.To(() => SaveFilePath.DeserializeFromJson<List<Book>>(), out this.books))
            {
                this.books = new List<Book>()
                {
                    new Book() { Title = "Kafka On The Shore", Author = "Haruki Murakami", Publisher = new Publisher(){ Name="Kodansha" } },
                    new Book() { Title = "Norwegian Wood", Author = "Haruki Murakami", Publisher = new Publisher(){ Name="Kodansha" } }
                };
            }

            this.IndexGrid.Children.Add(railsIndexViewFactory.CreateIndexView(books, typeof(Book)));
        }

        private void SaveClicked(object sender, RoutedEventArgs e)
        {
            this.books.SerializeAsJson(SaveFilePath);
        }

        private void ShowCustomEditView(object sender, RoutedEventArgs e)
        {
            var customLayoutWindow = new CustomLayoutWindow();
            customLayoutWindow.Show();
        }
    }
}
