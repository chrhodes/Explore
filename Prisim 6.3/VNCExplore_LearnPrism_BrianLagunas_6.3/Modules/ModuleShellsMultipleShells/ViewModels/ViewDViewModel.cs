using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using VNC.Core.Mvvm.Prism;
using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;

namespace ModuleMultipleShells.ViewModels
{
    public class ViewDViewModel : BindableBase, IRegionManagerAware
    {
        public DelegateCommand NavigateCommand { get; set; }

        public ViewDViewModel()
        {
            NavigateCommand = new DelegateCommand(Navigate);
        }

        void Navigate()
        {
            RegionManager.RequestNavigate(RegionNames.ContentRegion, "ViewB");
        }

        public IRegionManager RegionManager { get; set; }
    }
}
