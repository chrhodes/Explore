using Microsoft.Practices.Unity;

using Prism.Modularity;
using Prism.Regions;
using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;

namespace ModuleShellsScopedRegions
{
    public class ModuleShellsScopedRegionsModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _regionManager;

        public ModuleShellsScopedRegionsModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.ChildRegion, typeof(ViewB));

            IRegion region = _regionManager.Regions[RegionNames.ContentRegionS_SR];

            var view1 = _container.Resolve<ViewA>();

            // Third argument is to create RegionManagerScope for added view
            // instead of using current region manager (which is typically global manager)
            region.Add(view1, null, true);
            region.Activate(view1);

            // Adding the lines below will no longer cause a duplicate region exception because
            // because each view has its own Region Manager

            var view2 = _container.Resolve<ViewA>();
            region.Add(view2, null, true);
            region.Activate(view2);

            var view3 = _container.Resolve<ViewA>();
            region.Add(view3, null, true);
            region.Activate(view3);
        }
    }
}
