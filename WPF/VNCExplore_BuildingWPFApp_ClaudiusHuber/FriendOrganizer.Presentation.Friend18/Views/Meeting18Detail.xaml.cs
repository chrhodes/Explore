using System.Windows.Controls;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend18.Views
{
    public partial class Meeting18Detail : UserControl, IMeeting18Detail
    {
        public static int _instanceCountDV = 0;

        public Meeting18Detail()
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
