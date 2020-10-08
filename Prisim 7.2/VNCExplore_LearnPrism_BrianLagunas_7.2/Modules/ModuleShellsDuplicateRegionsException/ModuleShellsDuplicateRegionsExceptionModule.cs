using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;

namespace ModuleShellsDuplicateRegionsException
{
    public class ModuleShellsDuplicateRegionsExceptionModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _regionManager;

        public ModuleShellsDuplicateRegionsExceptionModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.ChildRegion, typeof(ViewB));

            IRegion region = _regionManager.Regions[RegionNames.ContentRegionS_DR];

            var view1 = _container.Resolve<ViewA>();
            region.Add(view1);
            region.Activate(view1);

            // Adding the lines below will cause a duplicate region exception because
            // RegionNames.ChildRegion is now registered twice

            //var view2 = _container.Resolve<ViewA>();
            //region.Add(view2);
            //region.Activate(view2);
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {

        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}
