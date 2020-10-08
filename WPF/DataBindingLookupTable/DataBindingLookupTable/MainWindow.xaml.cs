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

namespace DataBindingLookupTable
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

            DataBindingLookupTable.DataSet1 dataSet1 = ((DataBindingLookupTable.DataSet1)(this.FindResource("dataSet1")));
            // Load data into the table Customers. You can modify this code as needed.
            DataBindingLookupTable.DataSet1TableAdapters.CustomersTableAdapter dataSet1CustomersTableAdapter = new DataBindingLookupTable.DataSet1TableAdapters.CustomersTableAdapter();
            dataSet1CustomersTableAdapter.Fill(dataSet1.Customers);
            System.Windows.Data.CollectionViewSource customersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("customersViewSource")));
            customersViewSource.View.MoveCurrentToFirst();
            // Load data into the table Orders. You can modify this code as needed.
            DataBindingLookupTable.DataSet1TableAdapters.OrdersTableAdapter dataSet1OrdersTableAdapter = new DataBindingLookupTable.DataSet1TableAdapters.OrdersTableAdapter();
            dataSet1OrdersTableAdapter.Fill(dataSet1.Orders);
            System.Windows.Data.CollectionViewSource customersOrdersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("customersOrdersViewSource")));
            customersOrdersViewSource.View.MoveCurrentToFirst();
        }
    }
}
