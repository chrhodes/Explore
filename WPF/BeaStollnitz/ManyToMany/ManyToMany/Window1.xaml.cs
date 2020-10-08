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
using System.Data.OleDb;


namespace ManyToMany
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>

	public partial class Window1 : System.Windows.Window
	{
		DataSet ds;
        DataView viewJunction;
        DataView viewBorrowers;

		public Window1()
		{
			InitializeComponent();
			ds = GetData();
			sp.DataContext = ds.Tables["Books"];
            viewJunction = new DataView(ds.Tables["Junction"]);
            viewBorrowers = new DataView(ds.Tables["Borrowers"]);
        }

        private void UpdateBorrowers(object sender, RoutedEventArgs e)
        {
            ListBox booksListBox = (ListBox)sender;
            DataRowView selectedRow = (DataRowView)booksListBox.SelectedItem;
            string bookID = selectedRow["Book_ID"] as string;

            viewJunction.RowFilter = "Book_ID = '" + bookID + "'";

            bool firstTimeInLoop = true;
            StringBuilder filterStr = new StringBuilder();
            foreach (DataRowView row in viewJunction)
            {
                if (firstTimeInLoop)
                {
                    firstTimeInLoop = false;
                    filterStr.Append("Borrower_ID = '" + row["Borrower_ID"] + "'");
                }
                filterStr.Append(" OR Borrower_ID = '" + row["Borrower_ID"] + "'");
            }
            viewBorrowers.RowFilter = filterStr.ToString();
            lbBorrowers.ItemsSource = viewBorrowers;
        }

		private DataSet GetData()
		{
			DataSet dataSet = new DataSet();

			DataTable books = new DataTable("Books");
			books.Columns.Add(new DataColumn("Book_ID"));
			books.Columns.Add(new DataColumn("Title"));

			DataTable borrowers = new DataTable("Borrowers");
			borrowers.Columns.Add(new DataColumn("Borrower_ID"));
			borrowers.Columns.Add(new DataColumn("Borrower_Name"));

			DataTable junction = new DataTable("Junction");
			junction.Columns.Add(new DataColumn("Book_ID"));
			junction.Columns.Add(new DataColumn("Borrower_ID"));

			dataSet.Tables.Add(books);
			dataSet.Tables.Add(borrowers);
			dataSet.Tables.Add(junction);

			dataSet.Relations.Add(new DataRelation("BooksJunction", books.Columns["Book_ID"],
				junction.Columns["Book_ID"]));
			dataSet.Relations.Add(new DataRelation("BorrowersJunction", borrowers.Columns["Borrower_ID"], 
				junction.Columns["Borrower_ID"]));

			DataRow rowBooks1 = books.NewRow();
			rowBooks1[0] = 1;
			rowBooks1[1] = "Programming Windows Presentation Foundation";
			books.Rows.Add(rowBooks1);

			DataRow rowBooks2 = books.NewRow();
			rowBooks2[0] = 2;
			rowBooks2[1] = "Applications = Code + Markup: A Guide to the Microsoft Windows Presentation Foundation (Pro - Developer)";
			books.Rows.Add(rowBooks2);

			DataRow rowBooks3 = books.NewRow();
			rowBooks3[0] = 3;
			rowBooks3[1] = "Windows Presentation Foundation Unleashed";
			books.Rows.Add(rowBooks3);

			DataRow rowBooks4 = books.NewRow();
			rowBooks4[0] = 4;
			rowBooks4[1] = "C# Programming Language, The (2nd Edition) (Microsoft .NET Development Series)";
			books.Rows.Add(rowBooks4);

			DataRow rowBooks5 = books.NewRow();
			rowBooks5[0] = 5;
			rowBooks5[1] = "Inside C#, Second Edition";
			books.Rows.Add(rowBooks5);

			DataRow rowBooks6 = books.NewRow();
			rowBooks6[0] = 6;
			rowBooks6[1] = "Microsoft ADO.NET (Core Reference)";
			books.Rows.Add(rowBooks6);

			DataRow rowBorrowers1 = borrowers.NewRow();
			rowBorrowers1[0] = 1;
			rowBorrowers1[1] = "Beatriz";
			borrowers.Rows.Add(rowBorrowers1);

			DataRow rowBorrowers2 = borrowers.NewRow();
			rowBorrowers2[0] = 2;
			rowBorrowers2[1] = "John";
			borrowers.Rows.Add(rowBorrowers2);

			DataRow rowBorrowers3 = borrowers.NewRow();
			rowBorrowers3[0] = 3;
			rowBorrowers3[1] = "Mark";
			borrowers.Rows.Add(rowBorrowers3);

			DataRow rowBorrowers4 = borrowers.NewRow();
			rowBorrowers4[0] = 4;
			rowBorrowers4[1] = "Andrea";
			borrowers.Rows.Add(rowBorrowers4);

			DataRow rowBorrowers5 = borrowers.NewRow();
			rowBorrowers5[0] = 5;
			rowBorrowers5[1] = "Lucy";
			borrowers.Rows.Add(rowBorrowers5);

			DataRow rowJunction1 = junction.NewRow();
			rowJunction1[0] = 1;
			rowJunction1[1] = 1;
			junction.Rows.Add(rowJunction1);

			DataRow rowJunction2 = junction.NewRow();
			rowJunction2[0] = 1;
			rowJunction2[1] = 2;
			junction.Rows.Add(rowJunction2);

			DataRow rowJunction3 = junction.NewRow();
			rowJunction3[0] = 2;
			rowJunction3[1] = 1;
			junction.Rows.Add(rowJunction3);

			DataRow rowJunction4 = junction.NewRow();
			rowJunction4[0] = 2;
			rowJunction4[1] = 4;
			junction.Rows.Add(rowJunction4);

			DataRow rowJunction5 = junction.NewRow();
			rowJunction5[0] = 2;
			rowJunction5[1] = 5;
			junction.Rows.Add(rowJunction5);

			DataRow rowJunction6 = junction.NewRow();
			rowJunction6[0] = 3;
			rowJunction6[1] = 5;
			junction.Rows.Add(rowJunction6);

			DataRow rowJunction7 = junction.NewRow();
			rowJunction7[0] = 4;
			rowJunction7[1] = 1;
			junction.Rows.Add(rowJunction7);

			DataRow rowJunction8 = junction.NewRow();
			rowJunction8[0] = 5;
			rowJunction8[1] = 1;
			junction.Rows.Add(rowJunction8);

			DataRow rowJunction9 = junction.NewRow();
			rowJunction9[0] = 6;
			rowJunction9[1] = 4;
			junction.Rows.Add(rowJunction9);

			return dataSet;
		}
	}
}