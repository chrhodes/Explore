﻿using System.Windows.Controls;

using VNC.Core.Mvvm;

namespace FriendOrganizer.Presentation.Friend15.Views
{
    public partial class Meeting15Detail : UserControl, IMeeting15Detail
    {
        public static int _instanceCountDV = 0;

        public Meeting15Detail()
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
