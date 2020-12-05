using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WiredBrainCoffee.ToDoList.DataProvider;
using WiredBrainCoffee.ToDoList.Model;

namespace WiredBrainCoffee.ToDoList
{
  public partial class MainWindow : Window
  {
    private readonly TodoItemDataProvider _dataProvider;

    public MainWindow()
    {
      InitializeComponent();
      _dataProvider = new TodoItemDataProvider();
      Loaded += MainWindow_Loaded;
    }

    private void MainWindow_Loaded(object sender, RoutedEventArgs e)
    {
      Load();
    }

    private void Load()
    {
      todoItemsControl.Items.Clear();

      var todoItems = _dataProvider.LoadItems();

      foreach (var todoItem in todoItems)
      {
        todoItem.PropertyChanged += TodoItem_PropertyChanged;
        todoItemsControl.Items.Add(todoItem);
      }
    }

    private void TodoItem_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      Save();
    }

    private void ButtonCreateTodoItem_Click(object sender, RoutedEventArgs e)
    {
      CreateTodoItem();
    }

    private void CreateTodoItem()
    {
      var todoItem = new TodoItem(txtTitle.Text);

      todoItem.PropertyChanged += TodoItem_PropertyChanged;

      todoItemsControl.Items.Insert(0, todoItem);

      Save();

      txtTitle.Text = string.Empty;
    }

    private void Save(bool sort = false)
    {
      var todoItemsEnumerable = todoItemsControl.Items.Cast<TodoItem>();
      if (sort)
      {
        todoItemsEnumerable = todoItemsEnumerable.OrderBy(todoItem => todoItem.IsDone);
      }

      _dataProvider.SaveItems(todoItemsEnumerable.ToList());
    }

    private void TextBoxTitle_TextChanged(object sender, TextChangedEventArgs e)
    {
      btnCreateTodoItem.IsEnabled = CanCreateTodoItem;
      txtTitlePlaceholder.Visibility = CanCreateTodoItem
        ? Visibility.Collapsed
        : Visibility.Visible;
    }

    private void TextBoxTitle_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.Key == Key.Enter && CanCreateTodoItem)
      {
        CreateTodoItem();
      }
    }

    private bool CanCreateTodoItem => !string.IsNullOrWhiteSpace(txtTitle.Text);

    private void ButtonSortItems_Click(object sender, RoutedEventArgs e)
    {
      Save(sort: true);
      Load();
    }

    private void ButtonDeleteTodoItem_Click(object sender, RoutedEventArgs e)
    {
      var todoItem = ((Button)sender).Tag as TodoItem;
      if (todoItem != null)
      {
        todoItem.PropertyChanged -= TodoItem_PropertyChanged;
        todoItemsControl.Items.Remove(todoItem);
        Save();
      }
    }
  }
}
