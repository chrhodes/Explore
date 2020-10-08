
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Unity;

namespace ModuleA
{
    public class ModuleAModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _manager;

        public ModuleAModule(IUnityContainer container, IRegionManager manager)
        {
            _container = container;
            _manager = manager;
        }

        public void Initialize()
        {
            //_manager.RegisterViewWithRegion("ContentRegion", typeof (ModuleAView));

            var view = _container.Resolve<ModuleAView>();
            _manager.Regions["ContentRegionA"].Add(view);
        }

        public void OnInitialized(IContainerProvider containerProvider)
        { 
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegionA", typeof(ModuleAView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}
