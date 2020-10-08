
using ModuleInterfaces;
using Prism.Regions;
using System.Windows.Controls;
using VNC.Core.Mvvm;

namespace ModuleViewBasedNavigationBNavigationParticipation
{
    public partial class ViewB1 : UserControl, IView, INavigationAware, IRegionMemberLifetime
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

        public int PropertyName { get; set; }
        public bool KeepAlive
        {
            get { return false; }   // Always get new instance
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
