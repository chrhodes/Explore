using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;
using Microsoft.Practices.Unity;
using ModuleInterfaces;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleToolBar
{
    public class ModuleToolBarModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public ModuleToolBarModule(IUnityContainer container, IRegionManager regionManager)
        {
            this._container = container;
            this._regionManager = regionManager;
        }

        public void Initialize()
        {
            RegisterViewsAndServices();

            var vm = _container.Resolve<IToolBarViewModel>();

            var vmCC = _container.Resolve<IToolBarViewModel>();
            var vmEA = _container.Resolve<IToolBarViewModel>();
            var vmSS = _container.Resolve<IToolBarViewModel>();

            _regionManager.Regions[RegionNames.ToolBarRegionC_CC].Add(vmCC.View);
            _regionManager.Regions[RegionNames.ToolBarRegionC_EA].Add(vmEA.View);
            _regionManager.Regions[RegionNames.ToolBarRegionC_SS].Add(vmSS.View);
        }

        protected void RegisterViewsAndServices()
        {
            _container.RegisterType<IToolBarViewModel, ToolBarViewModel>();
            _container.RegisterType<IToolBar, ToolBar>();
        }
    }
}
