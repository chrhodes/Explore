﻿using ModuleInterfaces;
using Prism.Regions;
using System.Windows.Controls;
using VNC.Core.Mvvm;

namespace ModuleViewBasedNavigationA
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>

    public partial class ViewA1 : UserControl, IView, INavigationAware
    {
        public ViewA1(IContentAVBNViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
        }

        public IViewModel ViewModel
        {
            get { return (IViewModel)DataContext; }
            set { DataContext = value; }
        }


        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {

        }
    }
}
