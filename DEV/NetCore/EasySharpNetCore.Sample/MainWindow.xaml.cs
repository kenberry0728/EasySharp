﻿using EasySharpWpf.Views.Rails.Core.Edit;
using System.Windows;
using EasySharpWpf.Views.Rails.Core.Edit.Interfaces;
using EasySharp.Sample.Models.AutoLayout;
using EasySharp.Runtime.Serialization.Json;
using EasySharp;

namespace EasySharpNetCore.Sample
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

            var result = Try.To(() => SaveFilePath.DeserializeFromJson<BookShelf>());
            if (result.Ok)
            {
                this.bookShelf = result.Value;
            }
            else
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

            if (saveResult.Ok)
            {
                MessageBox.Show("保存しました", "保存", MessageBoxButton.OKCancel);
            }
            else
            {
                MessageBox.Show($"保存に失敗しました\r\n{saveResult.Exception.Message}", "保存", MessageBoxButton.OKCancel);
            }
        }

        private void ShowCustomEditView(object sender, RoutedEventArgs e)
        {
            var customLayoutWindow = new CustomLayoutWindow();
            customLayoutWindow.Show();
        }
    }
}
