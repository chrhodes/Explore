using System;
using Microsoft.Practices.Unity;
using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;
using Prism.Regions;
using Prism.Modularity;
using ModuleInterfaces;

namespace ModuleStatusBar
{
    public class ModuleStatusBarModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public ModuleStatusBarModule(IUnityContainer container, IRegionManager regionManager)
        {
            this._container = container;
            this._regionManager = regionManager;
        }

        public void Initialize()
        {
            RegisterViewsAndServices();

            //var vm = _container.Resolve<IStatusBarViewModel>();

            var vmDC = _container.Resolve<IStatusBarViewModel>();
            var vmCC = _container.Resolve<IStatusBarViewModel>();
            var vmEA = _container.Resolve<IStatusBarViewModel>();
            var vmSS = _container.Resolve<IStatusBarViewModel>();
            var vmRC = _container.Resolve<IStatusBarViewModel>();

            _regionManager.Regions[RegionNames.StatusBarRegionC_DC].Add(vmDC.View);
            _regionManager.Regions[RegionNames.StatusBarRegionC_CC].Add(vmCC.View);
            _regionManager.Regions[RegionNames.StatusBarRegionC_EA].Add(vmEA.View);
            _regionManager.Regions[RegionNames.StatusBarRegionC_SS].Add(vmSS.View);
            _regionManager.Regions[RegionNames.StatusBarRegionC_RC].Add(vmRC.View);
        }

        protected void RegisterViewsAndServices()
        {
            _container.RegisterType<IStatusBarViewModel, StatusBarViewModel>();
            _container.RegisterType<IStatusBar, StatusBar>();
        }
    }
}
