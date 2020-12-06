using System.Windows;
using System.Windows.Controls;

namespace WiredBrainCoffee.WpfControls
{
  public partial class ApplicationHeader : UserControl
  {
    public static readonly DependencyProperty ApplicationTitleProperty;
    public static readonly DependencyProperty VersionProperty;

    static ApplicationHeader()
    {
      ApplicationTitleProperty = DependencyProperty.Register(nameof(ApplicationTitle), typeof(string), typeof(ApplicationHeader), new PropertyMetadata(null));
      VersionProperty = DependencyProperty.Register(nameof(Version), typeof(string), typeof(ApplicationHeader), new PropertyMetadata(null));
    }

    public ApplicationHeader()
    {
      InitializeComponent();
    }

    public string ApplicationTitle
    {
      get { return (string)GetValue(ApplicationTitleProperty); }
      set { SetValue(ApplicationTitleProperty, value); }
    }

    public string Version
    {
      get { return (string)GetValue(VersionProperty); }
      set { SetValue(VersionProperty, value); }
    }
  }
}
