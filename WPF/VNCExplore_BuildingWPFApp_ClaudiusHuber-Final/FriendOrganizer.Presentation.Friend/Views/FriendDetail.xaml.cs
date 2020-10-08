using System.Windows.Controls;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend.Views
{
    public partial class FriendDetail : UserControl, IFriendDetail
    {
        public static int _instanceCountDV = 0;

        public FriendDetail()
        {
            _instanceCountDV++;
            InitializeComponent();
        }

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
