using System;
using System.Collections.Generic;
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

namespace WpfApplication1
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            WpfApplication1.DataSet1 dataSet1 = ((WpfApplication1.DataSet1)(this.FindResource("dataSet1")));
            // Load data into the table Employees. You can modify this code as needed.
            WpfApplication1.DataSet1TableAdapters.EmployeesTableAdapter dataSet1EmployeesTableAdapter = new WpfApplication1.DataSet1TableAdapters.EmployeesTableAdapter();
            dataSet1EmployeesTableAdapter.Fill(dataSet1.Employees);
            System.Windows.Data.CollectionViewSource employeesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("employeesViewSource")));
            employeesViewSource.View.MoveCurrentToFirst();
        }
    }
}
