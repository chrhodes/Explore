using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using WiredBrainCoffee.ToDoList.Model;

namespace WiredBrainCoffee.ToDoList.DataProvider
{
  public class TodoItemDataProvider
  {
    private readonly string _fileName;

    public TodoItemDataProvider()
    {
      _fileName = ConfigurationManager.AppSettings["JsonFileName"];
    }

    public IEnumerable<TodoItem> LoadItems()
    {
      IEnumerable<TodoItem> todoItems = null;
      if (File.Exists(_fileName))
      {
        var json = File.ReadAllText(_fileName);
        todoItems = JsonConvert.DeserializeObject<List<TodoItem>>(json);
      }

      return todoItems ?? Enumerable.Empty<TodoItem>();
    }

    public void SaveItems(IEnumerable<TodoItem> items)
    {
      var listOfItems = items.ToList();

      var json = JsonConvert.SerializeObject(listOfItems);
      File.WriteAllText(_fileName, json);
    }
  }
}
