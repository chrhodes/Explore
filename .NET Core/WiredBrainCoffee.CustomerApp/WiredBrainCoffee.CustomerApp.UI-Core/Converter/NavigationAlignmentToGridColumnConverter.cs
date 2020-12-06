using System;
using System.Globalization;
using System.Windows.Data;
using WiredBrainCoffee.CustomerApp.UI.ViewModel;

namespace WiredBrainCoffee.CustomerApp.UI.Converter
{
  public class NavigationAlignmentToGridColumnConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return (NavigationAlignment)value == NavigationAlignment.Left
        ? 0
        : 2;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
