using Microsoft.Practices.Unity;

using Prism.Modularity;
using Prism.Regions;

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
    }
}
