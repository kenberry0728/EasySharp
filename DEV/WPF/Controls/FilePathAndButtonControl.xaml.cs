﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EasySharpWpf.Controls
{
    /// <summary>
    /// Interaction logic for FilePathAndButtonControl.xaml
    /// </summary>
    public partial class FilePathAndButtonControl : UserControl
    {
        public FilePathAndButtonControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                this.FilePathTextBox.Text = openFileDialog.FileName;
            }
        }
    }
}
