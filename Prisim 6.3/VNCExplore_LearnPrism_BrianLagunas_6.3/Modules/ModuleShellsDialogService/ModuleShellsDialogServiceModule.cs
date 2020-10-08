using Microsoft.Practices.Unity;

using Prism.Modularity;
using Prism.Regions;

namespace ModuleShellsDialogService
{
    public class ModuleShellsDialogServiceModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _regionManager;

        public ModuleShellsDialogServiceModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            //_regionManager.RegisterViewWithRegion(RegionNames.ContentRegionS_DS, typeof(ViewA));
            //_regionManager.RegisterViewWithRegion(RegionNames.ContentRegionS_DS, typeof(ViewB));
            _container.RegisterType(typeof(object), typeof(ViewA), "ViewA");
            _container.RegisterType(typeof(object), typeof(ViewB), "ViewB");
        }
    }
}
