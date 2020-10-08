
using ModuleInterfaces;
using Prism.Regions;
using System.Windows.Controls;
using VNC.Core.Mvvm;

namespace ModuleViewBasedNavigationBPassingParameters
{
    public partial class ViewB1 : UserControl, IView
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
    }
}
