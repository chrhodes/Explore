using Microsoft.Practices.Unity;
using Prism.Regions;
using VNC.Core.Mvvm;
using VNC.Core.Mvvm.Prism;
using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;
using VNCExplore_LearnPrism_BrianLagunas.Views;

namespace VNCExplore_LearnPrism_BrianLagunas
{
    public class ShellService : IShellService
    {
        IUnityContainer _container;
        IRegionManager _regionManager;

        public ShellService(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void ShowShell()
        {
            // TODO(crhodes)
            // Don't think we want to hard code MainWindow here.

            //var shell = _container.Resolve<MainWindow>();
            var shell = _container.Resolve<MainWindow>();

            var scopedRegion = _regionManager.CreateRegionManager();

            RegionManager.SetRegionManager(shell, scopedRegion);

            shell.Show();
        }

        public void ShowShell(string uri)
        {
            // TODO(crhodes)
            // Don't think we want to hard code MainWindow here.

            //var shell = _container.Resolve<MainWindow>();
            var shell = _container.Resolve<MainWindow>();

            var scopedRegion = _regionManager.CreateRegionManager();

            RegionManagerAware.SetRegionManagerAware(shell, scopedRegion);

            scopedRegion.RequestNavigate(RegionNames.ContentRegionS_DS, uri);

            shell.Show();
        }
    }
}
