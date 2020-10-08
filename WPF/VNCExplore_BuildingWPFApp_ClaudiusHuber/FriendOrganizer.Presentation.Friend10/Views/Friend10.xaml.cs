using System.Windows;
using System.Windows.Controls;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend10.Views
{
    public partial class Friend10 : UserControl, IFriend10
    {
        private static int _instanceCountV = 0;

        public Friend10(ViewModels.IFriend10ViewModel viewModel)
        {
            _instanceCountV++;
            InitializeComponent();
            ViewModel = viewModel;

            Loaded += Friend10_Loaded;
        }

        private async void Friend10_Loaded(object sender, RoutedEventArgs e)
        {
            await ((ViewModels.IFriend10ViewModel)ViewModel).LoadAsync();
        }

        public IViewModel ViewModel
        {
            get { return (IViewModel)DataContext; }
            set { DataContext = value; }
        }

        public int InstanceCountV
        {
            get { return _instanceCountV; }
            set { _instanceCountV = value; }
        }
    }
}
