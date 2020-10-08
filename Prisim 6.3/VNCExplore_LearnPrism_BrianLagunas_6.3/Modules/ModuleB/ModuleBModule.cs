
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;

namespace ModuleB
{
    // Can put attributes on Modules
    [Module(ModuleName = "ModuleB", OnDemand = true)]
    [ModuleDependency("ModuleC")]
    public class ModuleBModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _regionManager;

        public ModuleBModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.ToolBarRegionB, typeof(ToolBarView));
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegionB, typeof(ContentView));
        }
    }
}
