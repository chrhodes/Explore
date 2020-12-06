using System.Windows;

namespace WiredBrainCoffee.CustomerApp.UI.Dialogs
{
  public interface IMessageBoxService
  {
    void ShowInfoBox(string text);
    MessageBoxResult ShowYesNoCancelBox(string text, string title);
    MessageBoxResult ShowOkCancelBox(string text, string title);
  }

  public class MessageBoxService : IMessageBoxService
  {
    public MessageBoxResult ShowYesNoCancelBox(string text, string title)
    {
      return MessageBox.Show(text, title, MessageBoxButton.YesNoCancel);
    }

    public MessageBoxResult ShowOkCancelBox(string text, string title)
    {
      return MessageBox.Show(text, title, MessageBoxButton.OKCancel);
    }

    public void ShowInfoBox(string text)
    {
      MessageBox.Show(text, "Info");
    }
  }
}
