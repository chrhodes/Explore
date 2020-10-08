using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FunWithSystemIO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void cbFileName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cbi = ((sender as ComboBox).SelectedItem as ComboBoxItem);

            string fileName = cbi.Content.ToString();

            FileInfo myFile = new FileInfo(fileName);

            txtFullName.Text = myFile.FullName;
            txtDirectoryName.Text = myFile.DirectoryName;
            txtName.Text = myFile.Name;
            txtExtension.Text = myFile.Extension;      
        }
    }
}
