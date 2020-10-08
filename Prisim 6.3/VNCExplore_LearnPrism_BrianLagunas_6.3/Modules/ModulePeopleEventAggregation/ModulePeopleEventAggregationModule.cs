using Prism.Modularity;
using Prism.Regions;
using Microsoft.Practices.Unity;
using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;
using ModuleInterfaces;

namespace ModulePeopleEventAggregation
{
    public class ModulePeopleEventAggregationModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public ModulePeopleEventAggregationModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            RegisterViewsAndServices();

            // CompositeCommand Example
            // This is for TabControl multiple view 

            IRegion region = _regionManager.Regions[RegionNames.ContentRegionC_EA];

            var vm = _container.Resolve<IPersonViewModel>();
            vm.CreatePerson("Bob", "Smith");

            region.Add(vm.View);
            region.Activate(vm.View);

            var vm2 = _container.Resolve<IPersonViewModel>();
            vm2.CreatePerson("Karl", "Sums");
            region.Add(vm2.View);

            var vm3 = _container.Resolve<IPersonViewModel>();
            vm3.CreatePerson("Jeff", "Lock");
            region.Add(vm3.View);
        }

        protected void RegisterViewsAndServices()
        {
            _container.RegisterType<IPersonViewModel, PersonViewModel>();
            _container.RegisterType<IPerson, Person>();
        }
    }
}