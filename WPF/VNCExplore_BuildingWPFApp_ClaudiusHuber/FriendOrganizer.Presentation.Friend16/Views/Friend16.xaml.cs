using System.Windows;
using System.Windows.Controls;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend16.Views
{
    public partial class Friend16: UserControl, IFriend16
    {
        private static int _instanceCountV = 0;

        public Friend16()
        {
            _instanceCountV++;
            InitializeComponent();
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
