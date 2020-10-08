
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Unity;

namespace ModuleB
{
    public class ModuleBModule : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _manager;

        public ModuleBModule(IUnityContainer container, IRegionManager manager)
        {
            _container = container;
            _manager = manager;
        }

        public void Initialize()
        {
            //_manager.RegisterViewWithRegion("ContentRegion", typeof (ModuleAView));

            var view = _container.Resolve<ModuleBView>();
            _manager.Regions["ContentRegionB"].Add(view);
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegionB", typeof(ModuleBView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
           
        }
    }
}
