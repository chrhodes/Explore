//using Microsoft.Practices.Unity;
using Prism.Regions;
using Unity;
using VNC.Core.Mvvm;
using VNC.Core.Mvvm.Prism;
using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;

namespace VNCExplore_LearnPrism_MultipleShells_BrianLagunas
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

        public void ShowShell(string uri)
        {
            // TODO(crhodes)
            // Don't think we want to hard code MainWindow here.

            //var shell = _container.Resolve<MainWindow>();
            var shell = _container.Resolve<Views.MainWindow>();

            var scopedRegion = _regionManager.CreateRegionManager();
            RegionManager.SetRegionManager(shell, scopedRegion);

            RegionManagerAware.SetRegionManagerAware(shell, scopedRegion);

            scopedRegion.RequestNavigate(RegionNames.ContentRegionS_MS, uri);

            shell.Show();
        }

        public void ShowShell()
        {
            throw new System.NotImplementedException();
        }
    }
}
