using ModuleInterfaces;
using System.Windows.Controls;
using VNC.Core.Mvvm;

namespace ModuleViewBasedNavigationA
{
    /// <summary>
    /// Interaction logic for EmailView.xaml
    /// </summary>
    public partial class Email : UserControl, IView
    {
        public Email(IEmailViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
        }

        public IViewModel ViewModel
        {
            get { return (IViewModel)DataContext; }
            set { DataContext = value; }
        }
    }
}