using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;


namespace RecurringRelations
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>

    public partial class Window1 : Window
    {

        public Window1()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            BindingListCollectionView blcv = CollectionViewSource.GetDefaultView(tv.ItemsSource) as BindingListCollectionView;
            blcv.CustomFilter = "ParentID IS NULL";
        }
    }

    public class MyTable : DataTable
    {
        public MyTable()
        {
            this.Columns.Add("ID");
            this.Columns.Add("ParentID");
            this.Columns.Add("Name");

            this.Rows.Add(1, null, "Node1");
            this.Rows.Add(2, 1, "Node2");
            this.Rows.Add(3, 1, "Node3");
            this.Rows.Add(4, 3, "Node4");
        }
    }

    public class MyDataSet : DataSet
    {
        public MyDataSet()
        {
            MyTable table1 = new MyTable();
            this.Tables.Add(table1);
            this.Relations.Add(new DataRelation("IDParentID", table1.Columns["ID"], table1.Columns["ParentID"]));
        }
    }
}