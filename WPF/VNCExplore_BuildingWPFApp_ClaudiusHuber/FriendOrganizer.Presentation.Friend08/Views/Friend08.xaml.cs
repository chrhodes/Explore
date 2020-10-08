using System.Windows;
using System.Windows.Controls;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend08.Views
{
    public partial class Friend08 : UserControl, IFriend08
    {

        public Friend08(ViewModels.IFriend08ViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;
            Loaded += Friend08_Loaded;
        }

        private async void Friend08_Loaded(object sender, RoutedEventArgs e)
        {
            await ((ViewModels.IFriend08ViewModel)ViewModel).LoadAsync();
        }

        public IViewModel ViewModel
        {
            get { return (IViewModel)DataContext; }
            set { DataContext = value; }
        }
    }
}
