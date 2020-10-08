using System.Windows;
using System.Windows.Controls;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend09.Views
{
    public partial class Friend09 : UserControl, IFriend09
    {
        private static int instanceCountV= 0;

        public Friend09(ViewModels.IFriend09ViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;
            Loaded += Friend09_Loaded;

            instanceCountV++;
        }

        private async void Friend09_Loaded(object sender, RoutedEventArgs e)
        {
            await ((ViewModels.IFriend09ViewModel)ViewModel).LoadAsync();
        }

        public IViewModel ViewModel
        {
            get { return (IViewModel)DataContext; }
            set { DataContext = value; }
        }
    }
}
