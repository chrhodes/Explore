using System.Windows;
using System.Windows.Controls;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend19.Views
{
    public partial class Friend19: UserControl, IFriend19
    {
        private static int _instanceCountV = 0;

        public Friend19()
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
