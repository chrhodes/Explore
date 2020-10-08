using ModuleInterfaces;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;
using VNC;

namespace ModuleCommunicationPeopleRegionContext
{
    public class ModuleCommunicationPeopleRegionContextModule : IModule
    {
        // 01
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            long startTicks = Log.Trace("Enter", Common.LOG_APPNAME, 0);

            containerRegistry.Register<IPeopleViewModel, PeopleViewModel>();
            containerRegistry.Register<IPeople, People>();
            containerRegistry.Register<IPersonDetails, PersonDetails>();
            containerRegistry.Register<IPersonDetailsViewModel, PersonDetailsViewModel>();

            Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }

        // 02
        public void OnInitialized(IContainerProvider containerProvider)
        {
            long startTicks = Log.Trace("Enter", Common.LOG_APPNAME, 0);

            var regionManager = containerProvider.Resolve<IRegionManager>();

            var vm = containerProvider.Resolve<IPeopleViewModel>();
            regionManager.Regions[RegionNames.ContentRegionC_RC].Add(vm.View);

            regionManager.RegisterViewWithRegion(RegionNames.PersonDetailsRegionC_RC, typeof(PersonDetails));

            Log.Trace("Exit", Common.LOG_APPNAME, 0, startTicks);
        }


    }
}