using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

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
