using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;

namespace ModuleShellsViewComposition
{
    public class ModuleShellsViewCompositionModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _regionManager;

        public ModuleShellsViewCompositionModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {

            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegionS_VC, typeof(ViewA));
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegionS_VC, typeof(ViewB));

            //IRegion region = _regionManager.Regions[RegionNames.ContentRegionS_SS];

            //var view1 = _container.Resolve<ViewA>();
            //region.Add(view1);
            //region.Activate(view1);

            //_container.RegisterType(typeof(object), typeof(ViewA), "ViewA");
            //_container.RegisterType(typeof(object), typeof(ViewB), "ViewB");
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
