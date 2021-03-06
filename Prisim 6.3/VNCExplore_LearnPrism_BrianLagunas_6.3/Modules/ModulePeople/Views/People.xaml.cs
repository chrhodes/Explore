﻿using ModuleInterfaces;
using System.Windows.Controls;
using VNC.Core.Mvvm;

namespace ModulePeopleEventAggregation
{
    public partial class People : UserControl, IPeople
    {
        public People()
        {
            InitializeComponent();
        }

        public IViewModel ViewModel
        {
            get { return (IViewModel)DataContext; }
            set { DataContext = value; }
        }
    }
}
