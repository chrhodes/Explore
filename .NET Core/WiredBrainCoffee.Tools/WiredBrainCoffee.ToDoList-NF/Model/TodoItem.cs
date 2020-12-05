using System;

namespace WiredBrainCoffee.ToDoList.Model
{
  public class TodoItem : Observable
  {
    private string _title;
    private bool _isDone;

    public TodoItem(string title)
    {
      _title = title;
    }

    public string Title
    {
      get { return _title; }
      set
      {
        _title = value;
        OnPropertyChanged();
      }
    }

    public bool IsDone
    {
      get { return _isDone; }
      set
      {
        _isDone = value;
        OnPropertyChanged();
      }
    }
  }
}
