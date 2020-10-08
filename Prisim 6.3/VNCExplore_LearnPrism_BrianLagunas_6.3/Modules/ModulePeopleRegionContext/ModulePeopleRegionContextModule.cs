using Prism.Modularity;
using Prism.Regions;
using Microsoft.Practices.Unity;
using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;
using ModuleInterfaces;
using PrismDemo.Business;

namespace ModulePeopleRegionContext
{
    public class ModulePeopleRegionContextModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public ModulePeopleRegionContextModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            RegisterViewsAndServices();

            var vm = this._container.Resolve<IPeopleViewModel>();
            _regionManager.Regions[RegionNames.ContentRegionC_RC].Add(vm.View);

            _regionManager.RegisterViewWithRegion(RegionNames.PersonDetailsRegionC_RC, typeof(PersonDetails));
        }

        protected void RegisterViewsAndServices()
        {
            _container.RegisterType<IPeopleViewModel, PeopleViewModel>();
            _container.RegisterType<IPeople, People>();
            _container.RegisterType<IPersonDetails, PersonDetails>();
            _container.RegisterType<IPersonDetailsViewModel, PersonDetailsViewModel>();
        }
    }
}