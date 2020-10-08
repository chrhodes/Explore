using FriendOrganizer.DomainServices.Lookups;
using FriendOrganizer.DomainServices.Repositories;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNCExplore_FriendOrganizer.Core;
using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend17
{
    public class Friend17Module : IModule
    {
        private readonly IRegionManager _regionManager;
        public static IContainerProvider _containerProvider;

        // 01

        public Friend17Module(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ViewModels.IFriend17DetailViewModel, ViewModels.Friend17DetailViewModel>();
            containerRegistry.RegisterSingleton<Views.IFriend17Detail, Views.Friend17Detail>();

            containerRegistry.Register<ViewModels.IMeeting17DetailViewModel, ViewModels.Meeting17DetailViewModel>();
            containerRegistry.RegisterSingleton<Views.IMeeting17Detail, Views.Meeting17Detail>();

            containerRegistry.RegisterSingleton<ViewModels.INavigation17ViewModel, ViewModels.Navigation17ViewModel>();
            //containerRegistry.RegisterSingleton<ViewModels.IFriend17ViewModel, ViewModels.Friend17ViewModel>();
            containerRegistry.RegisterSingleton<Views.INavigation17, Views.Navigation17>();
            //containerRegistry.RegisterSingleton<Views.IFriend17, Views.Friend17>();

            containerRegistry.Register<Views.Friend17Main>();

            // TODO(crhodes)
            // Learn if Unity can do AsImplementedInterfaces like AutoFac

            containerRegistry.Register<IFriendLookupDataService10, LookupDataService15>();
            containerRegistry.Register<IProgrammingLanguageLookupDataService12, LookupDataService15>();
            containerRegistry.Register<IMeetingLookupDataService15, LookupDataService15>();

            containerRegistry.Register<IFriendRepository16, FriendRepository16>();
            containerRegistry.Register<IMeetingRepository16, MeetingRepository16>();

            containerRegistry.Register<IMessageDialogService, MessageDialogService>();
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _containerProvider = containerProvider;

            // Put FriendOrganizerMain on MainWindow

            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion17, typeof(Views.Friend17Main));

            // Put Friend and FriendDetail on FriendOrganizerMain (this assembly)

            _regionManager.RegisterViewWithRegion(RegionNames.Region17, typeof(Views.Navigation17));
            //_regionManager.RegisterViewWithRegion(RegionNames.Region17, typeof(Views.Friend17));

            //_regionManager.RegisterViewWithRegion(RegionNames.RegionDetail17, typeof(Views.Friend17Detail));
        }
    }
}