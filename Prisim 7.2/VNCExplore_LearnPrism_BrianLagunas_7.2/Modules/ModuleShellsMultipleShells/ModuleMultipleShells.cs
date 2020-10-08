using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

namespace ModuleMultipleShells
{
    public class ModuleMultipleShellsModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _regionManager;

        public ModuleMultipleShellsModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            //_regionManager.RegisterViewWithRegion(RegionNames.ChildRegion, typeof(ViewB));

            //IRegion region = _regionManager.Regions[RegionNames.ContentRegionS_SS];

            //var view1 = _container.Resolve<ViewA>();
            //region.Add(view1);
            //region.Activate(view1);

            _container.RegisterType(typeof(object), typeof(ViewA), "ViewA");
            _container.RegisterType(typeof(object), typeof(ViewB), "ViewB");
            //_container.RegisterType(typeof(object), typeof(ViewC), "ViewC");
            //_container.RegisterType(typeof(object), typeof(ViewD), "ViewD");
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
