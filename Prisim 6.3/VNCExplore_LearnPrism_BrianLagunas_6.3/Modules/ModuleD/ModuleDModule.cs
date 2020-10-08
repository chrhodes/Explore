
using Microsoft.Practices.Unity;
using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleD
{

    public class ModuleDModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _regionManager;

        public ModuleDModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.ToolBarRegionD, typeof(ToolBarView));
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegionD, typeof(ContentView));
        }
    }
}
