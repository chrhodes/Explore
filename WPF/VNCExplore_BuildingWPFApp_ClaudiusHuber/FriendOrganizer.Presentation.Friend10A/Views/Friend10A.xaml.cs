using System.Windows;
using System.Windows.Controls;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend10A.Views
{
    public partial class Friend10A : UserControl, IFriend10A
    {
        //IFriend10ADetail _friend10ADetail;
        private static int _instanceCountV = 0;

        public Friend10A()
        {
            _instanceCountV++;
            InitializeComponent();
        }

        //public Friend10A(ViewModels.IFriend10AViewModel viewModel,
        //    Views.IFriend10ADetail friend10ADetail)
        //{
        //    _instanceCountV++;
        //    InitializeComponent();
        //    ViewModel = viewModel;

        //    //_friend10ADetail = friend10ADetail;
        //    ((ViewModels.Friend10AViewModel)ViewModel).Friend10ADetail = friend10ADetail;

        //    Loaded += Friend10A_Loaded;
        //}

        //private async void Friend10A_Loaded(object sender, RoutedEventArgs e)
        //{
        //    await ((ViewModels.IFriend10AViewModel)ViewModel).LoadAsync();
        //}

        //public IFriend10ADetail Friend10ADetail
        //{
        //    get { return _friend10ADetail; }
        //    set { _friend10ADetail = value; }
        //}

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
