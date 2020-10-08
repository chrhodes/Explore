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
using System.ComponentModel;
using System.Collections.ObjectModel;


namespace ClearTable
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>

	public partial class Window1 : System.Windows.Window
	{

		public Window1()
		{
			InitializeComponent();
			this.DataContext = new PlacesDataTable();
		}

		private void ClearRepopulate(object sender, RoutedEventArgs e)
		{
			PlacesDataTable src = this.DataContext as PlacesDataTable;
			src.Clear();
			src.Rows.Add("New name", "New state");
			CollectionViewSource.GetDefaultView(this.DataContext).MoveCurrentToFirst(); 
		}
	}

	public class Places : ObservableCollection<Place>
	{
		public Places()
		{
			Add(new Place("Seattle", "WA"));
			Add(new Place("Redmond", "WA"));
			Add(new Place("Bellevue", "WA"));
			Add(new Place("Kirkland", "WA"));
			Add(new Place("Portland", "OR"));
			Add(new Place("San Francisco", "CA"));
			Add(new Place("Los Angeles", "CA"));
			Add(new Place("San Diego", "CA"));
			Add(new Place("San Jose", "CA"));
			Add(new Place("Santa Ana", "CA"));
			Add(new Place("Bellingham", "WA"));
		}
	}

	public class Place : INotifyPropertyChanged
	{
		private string name;

		private string state;

		public string Name
		{
			get { return name; }
			set
			{
				name = value;
				RaisePropertyChangedEvent("Name");
			}
		}

		public string State
		{
			get { return state; }
			set
			{
				state = value;
				RaisePropertyChangedEvent("State");
			}
		}

		public Place()
		{
			this.name = "";
			this.state = "";
		}

		public Place(string name, string state)
		{
			this.name = name;
			this.state = state;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChangedEvent(string name)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(name));
			}
		}
	}

	public class PlacesDataTable : DataTable
	{
		public PlacesDataTable()
		{
			Places places = new Places();
			this.TableName = "Places";
			this.Columns.Add("Name");
			this.Columns.Add("State");
			this.Rows.Add(places[0].Name, places[0].State);
			this.Rows.Add(places[1].Name, places[1].State);
			this.Rows.Add(places[2].Name, places[2].State);
			this.Rows.Add(places[3].Name, places[3].State);
			this.Rows.Add(places[4].Name, places[4].State);
			this.Rows.Add(places[5].Name, places[5].State);
			this.Rows.Add(places[6].Name, places[6].State);
			this.Rows.Add(places[7].Name, places[7].State);
			this.Rows.Add(places[8].Name, places[8].State);
			this.Rows.Add(places[9].Name, places[9].State);
			this.Rows.Add(places[10].Name, places[10].State);
		}
	}
}