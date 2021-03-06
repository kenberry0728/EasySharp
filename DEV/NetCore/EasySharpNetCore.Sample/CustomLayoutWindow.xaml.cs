﻿using EasySharp.Sample.Models.UserDefinedLayout;
using EasySharpWpf.Views.Rails.Core.Edit;
using EasySharpWpf.Views.Rails.Core.Edit.Interfaces;
using System.Windows;

namespace EasySharpNetCore.Sample
{
    /// <summary>
    /// Interaction logic for CustomLayoutWindow.xaml
    /// </summary>
    public partial class CustomLayoutWindow : Window
    {
        private readonly IRailsEditViewBinder viewBinder;

        public CustomLayoutWindow(IRailsEditViewBinder viewBinder = null)
        {
            InitializeComponent();

            this.Loaded += OnLoaded;
            this.viewBinder = viewBinder.Resolve();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var editModel = new CustomLayoutBook
            {
                Title = "Kafka On The Shore",
                Author = "Haruki Murakami",
                Publisher = "Kodansha"
            };

            viewBinder.ApplyRailsBinding(this, editModel, typeof(CustomLayoutBook));
        }
    }
}
