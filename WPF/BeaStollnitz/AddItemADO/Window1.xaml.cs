using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Data;

namespace AddItemADO
{
	public partial class Window1
	{
		public Window1()
		{
			this.InitializeComponent();
            this.DataContext = new MyDataTable();
        }

        private void AddItem(object sender, RoutedEventArgs e)
        {
            ((MyDataTable)(this.DataContext)).Rows.Add("Joanna", 23);
        }
	}

    public class MyDataTable : DataTable
    {
        public MyDataTable()
        {
            this.Columns.Add("Name");
            this.Columns.Add("Age");
            this.Rows.Add("John", 20);
            this.Rows.Add("Mary", 25);
            this.Rows.Add("Marc", 27);
        }
    }
}