using System.Windows;
using System.Windows.Controls;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend07.Views
{
    public partial class Friend07 : UserControl, IFriend07
    {

        public Friend07(ViewModels.IFriend07ViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;
            Loaded += Friend07_Loaded;
        }

        private async void Friend07_Loaded(object sender, RoutedEventArgs e)
        {
            await ((ViewModels.IFriend07ViewModel)ViewModel).LoadAsync();
        }

        public IViewModel ViewModel
        {
            get { return (IViewModel)DataContext; }
            set { DataContext = value; }
        }
    }
}
