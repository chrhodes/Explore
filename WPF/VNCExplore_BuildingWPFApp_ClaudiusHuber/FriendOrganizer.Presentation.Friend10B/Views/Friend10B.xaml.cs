using System.Windows;
using System.Windows.Controls;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend10B.Views
{
    public partial class Friend10B : UserControl, IFriend10B
    {
        //IFriend10BDetail _friend10BDetail;
        private static int _instanceCountV = 0;

        public Friend10B()
        {
            _instanceCountV++;
            InitializeComponent();
        }

        //public Friend10B(ViewModels.IFriend10BViewModel viewModel,
        //    Views.IFriend10BDetail friend10BDetail)
        //{
        //    _instanceCountV++;
        //    InitializeComponent();
        //    ViewModel = viewModel;

        //    //_friend10BDetail = friend10BDetail;
        //    ((ViewModels.Friend10BViewModel)ViewModel).Friend10BDetail = friend10BDetail;

        //    Loaded += Friend10B_Loaded;
        //}

        //private async void Friend10B_Loaded(object sender, RoutedEventArgs e)
        //{
        //    await ((ViewModels.IFriend10BViewModel)ViewModel).LoadAsync();
        //}

        //public IFriend10BDetail Friend10BDetail
        //{
        //    get { return _friend10BDetail; }
        //    set { _friend10BDetail = value; }
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
