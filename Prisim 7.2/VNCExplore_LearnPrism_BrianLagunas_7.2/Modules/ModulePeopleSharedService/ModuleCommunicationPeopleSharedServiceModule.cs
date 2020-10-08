using ModuleInterfaces;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;
using VNC;

using PrismDemo.DomainServices.Repositories;

namespace ModuleCommunicationPeopleSharedService
{
    public class ModuleCommunicationPeopleSharedServiceModule : IModule
    {
        // 01
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            long startTicks = Log.Trace("Enter", Common.LOG_APPNAME, 0);
            containerRegistry.RegisterSingleton<IPersonRepository, PersonRepository>();
            containerRegistry.Register<IPersonViewModel, PersonViewModel>();
            containerRegistry.Register<IPerson, Person>();

            //// TODO(crhodes)
            //// Where was this done in he 6.3 Prism stuff  In Services Module


            Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }

        // 02
        public void OnInitialized(IContainerProvider containerProvider)
        {
            long startTicks = Log.Trace("Enter", Common.LOG_APPNAME, 0);

            var regionManager = containerProvider.Resolve<IRegionManager>();
            // CompositeCommand Example
            // This is for TabControl multiple view 

            IRegion region = regionManager.Regions[RegionNames.ContentRegionC_SS];

            var vm = containerProvider.Resolve<IPersonViewModel>();
            vm.CreatePerson("Bob", "Smith");

            region.Add(vm.View);
            region.Activate(vm.View);

            var vm2 = containerProvider.Resolve<IPersonViewModel>();
            vm2.CreatePerson("Karl", "Sums");
            region.Add(vm2.View);

            var vm3 = containerProvider.Resolve<IPersonViewModel>();
            vm3.CreatePerson("Jeff", "Lock");
            region.Add(vm3.View);

            Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }
    }
}