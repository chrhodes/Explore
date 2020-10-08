using System.Windows.Controls;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend10A.Views
{
    public partial class Friend10ADetail : UserControl, IFriend10ADetail
    {
        public static int _instanceCountDV = 0;

        public Friend10ADetail()
        {
            _instanceCountDV++;
            InitializeComponent();
            //ViewModel = viewModel;

            //Friend10AModule.friend10Detail = this;
        }
        //public Friend10ADetail(ViewModels.IFriend10ADetailViewModel viewModel)
        //{
        //    _instanceCountDV++;
        //    InitializeComponent();
        //    ViewModel = viewModel;

        //    //Friend10AModule.friend10Detail = this;
        //}

        public IViewModel ViewModel
        {
            get { return (IViewModel)DataContext; }
            set { DataContext = value; }
        }

        public int InstanceCountDV
        {
            get { return _instanceCountDV; }
            set { _instanceCountDV = value; }
        }
    }
}
