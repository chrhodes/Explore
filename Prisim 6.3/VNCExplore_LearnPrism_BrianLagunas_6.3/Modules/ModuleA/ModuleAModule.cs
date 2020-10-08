
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;

namespace ModuleA
{
    public class ModuleAModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _regionManager;

        public ModuleAModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            // Magic strings

            //_regionManager.RegisterViewWithRegion("ToolBarRegion", typeof(ToolBarView));
            //_regionManager.RegisterViewWithRegion("ContentRegion", typeof(ContentView));

            // No more Magic Strings

            //_regionManager.RegisterViewWithRegion(RegionNames.ToolBarRegion, typeof(ToolBarView));
            //_regionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ContentView));

            // Multiple ToolBar Regions

            IRegion region = _regionManager.Regions[RegionNames.ToolBarRegionA];

            region.Add(_container.Resolve<ToolBarView>());
            region.Add(_container.Resolve<ToolBarView>());
            region.Add(_container.Resolve<ToolBarView>());
            region.Add(_container.Resolve<ToolBarView>());
            region.Add(_container.Resolve<ToolBarView>());

            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegionA, typeof(ContentView));
        }
    }
}
