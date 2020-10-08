using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

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

        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
