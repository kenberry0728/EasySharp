using System.IO;
using System.Reflection;
using EasySharp.Sample.Models.AutoLayout;
using EasySharpStandard.DiskIO.Serializers;
using EasySharpStandard.SafeCodes.Core;
using EasySharpWpf.Views.Rails.Core.Edit;
using System.Windows;
using AppInstaller.Core;
using AppInstaller.Core.Arguments;
using AppInstaller.Core.Results;
using EasySharpStandard.Processes;
using EasySharpWpf.Views.Rails.Core.Edit.Interfaces;

namespace EasySharpWpf.Sample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string SaveFilePath = "bookShelf.json";
        private BookShelf bookShelf;

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
                CreateSampleData();
            }

            this.BookShelfGrid.Children.Add(railsEditViewFactory.CreateEditView(bookShelf, typeof(BookShelf)));
        }

        private void CreateSampleData()
        {
            this.bookShelf = new BookShelf();

            this.bookShelf.Books.Add(
                new Book
                {
                    Title = "Kafka On The Shore",
                    Author = "Haruki Murakami",
                    Price = 300,
                    Publisher = new Publisher
                    {
                        Name = "Kodansha",
                        PublisherType = PublisherType.Company
                    }
                });
            this.bookShelf.Books.Add(
                new Book
                {
                    Title = "Norwegian Wood",
                    Author = "Haruki Murakami",
                    Price = 500,
                    Publisher = new Publisher
                    {
                        Name = "Kodansha",
                        PublisherType = PublisherType.Company
                    }
                });
        }

        private void SaveClicked(object sender, RoutedEventArgs e)
        {
            var saveResult = Try.To(() =>
            {
                this.bookShelf.SerializeAsJson(SaveFilePath);
            });

            if (saveResult)
            {
                MessageBox.Show("保存しました", "保存", MessageBoxButton.OKCancel);
            }
        }

        private void ShowCustomEditView(object sender, RoutedEventArgs e)
        {
            var customLayoutWindow = new CustomLayoutWindow();
            customLayoutWindow.Show();
        }

        private void CheckUpdate(object sender, RoutedEventArgs e)
        {
            var dirInfo = new DirectoryInfo(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)));
            if (dirInfo.Parent == null)
            {
                return;
            }

            var sourceDir = Path.Combine(dirInfo.Parent.FullName, "Release");
            var arg = new AppInstallerArgument(RunMode.CheckUpdate)
            {
                InstallDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                SourceDir = sourceDir,
                OriginalAppPath = Assembly.GetExecutingAssembly().Location,
            };

            var result = AppInstallerConstants
                .AppFilePath
                .RunProcessAndGetStandardOutput(arg.ToCommandLineString())
                .AppInstallerResultFromString();
            if (result.Updated)
            {
                arg.RunMode = RunMode.DownloadItemsToTemp;
                AppInstallerConstants.AppFilePath.RunProcess(arg.ToCommandLineString());
                this.Close();
            }
            else
            {
                MessageBox.Show("This is the latest.");
            }
        }
    }
}
