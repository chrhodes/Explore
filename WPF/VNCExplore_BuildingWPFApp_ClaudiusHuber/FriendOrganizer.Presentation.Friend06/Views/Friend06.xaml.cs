using System.Windows;
using System.Windows.Controls;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend06.Views
{
    public partial class Friend06 : UserControl, IFriend06
    {

        public Friend06(ViewModels.IFriend06ViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;
            Loaded += Friend06Navigation_Loaded;
        }

        private async void Friend06Navigation_Loaded(object sender, RoutedEventArgs e)
        {
            await ((ViewModels.IFriend06ViewModel)ViewModel).LoadAsync();
        }

        public IViewModel ViewModel
        {
            get { return (IViewModel)DataContext; }
            set { DataContext = value; }
        }
    }
}
