using ModuleInterfaces;
using Prism.Regions;
using System.Windows.Controls;
using VNC.Core.Mvvm;

namespace ModuleViewBasedNavigationB
{
    public partial class ViewB1 : UserControl, IView, INavigationAware
    {
        public ViewB1(IContentBVBNViewModel viewModel)
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
