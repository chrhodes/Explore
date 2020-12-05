using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace WiredBrainCoffee.CustomerApp.UI.ViewModel.Base
{
  public class ViewModelBase : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
    {
      PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
