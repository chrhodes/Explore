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

namespace ExploreDataBinding
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

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            ExploreDataBinding.SQLMonitorDBDataSet sQLMonitorDBDataSet = ((ExploreDataBinding.SQLMonitorDBDataSet)(this.FindResource("sQLMonitorDBDataSet")));

            // Load data into the table Servers. You can modify this code as needed.
            ExploreDataBinding.SQLMonitorDBDataSetTableAdapters.ServersTableAdapter sQLMonitorDBDataSetServersTableAdapter = new ExploreDataBinding.SQLMonitorDBDataSetTableAdapters.ServersTableAdapter();
            sQLMonitorDBDataSetServersTableAdapter.Fill(sQLMonitorDBDataSet.Servers);
            System.Windows.Data.CollectionViewSource serversViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("serversViewSource")));
            serversViewSource.View.MoveCurrentToFirst();

            // Load data into the table Instances. You can modify this code as needed.
            ExploreDataBinding.SQLMonitorDBDataSetTableAdapters.InstancesTableAdapter sQLMonitorDBDataSetInstancesTableAdapter = new ExploreDataBinding.SQLMonitorDBDataSetTableAdapters.InstancesTableAdapter();
            sQLMonitorDBDataSetInstancesTableAdapter.Fill(sQLMonitorDBDataSet.Instances);
            System.Windows.Data.CollectionViewSource instancesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("instancesViewSource")));
            instancesViewSource.View.MoveCurrentToFirst();

            // Load data into the table Databases. You can modify this code as needed.
            ExploreDataBinding.SQLMonitorDBDataSetTableAdapters.DatabasesTableAdapter sQLMonitorDBDataSetDatabasesTableAdapter = new ExploreDataBinding.SQLMonitorDBDataSetTableAdapters.DatabasesTableAdapter();
            sQLMonitorDBDataSetDatabasesTableAdapter.Fill(sQLMonitorDBDataSet.Databases);
            System.Windows.Data.CollectionViewSource databasesViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("databasesViewSource")));
            databasesViewSource.View.MoveCurrentToFirst();
        }
    }
}
