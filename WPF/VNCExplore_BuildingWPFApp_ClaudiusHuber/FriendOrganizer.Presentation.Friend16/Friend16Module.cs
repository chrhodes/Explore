using FriendOrganizer.DomainServices.Lookups;
using FriendOrganizer.DomainServices.Repositories;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNCExplore_FriendOrganizer.Core;
using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend16
{
    public class Friend16Module : IModule
    {
        private readonly IRegionManager _regionManager;
        public static IContainerProvider _containerProvider;

        // 01

        public Friend16Module(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ViewModels.IFriend16DetailViewModel, ViewModels.Friend16DetailViewModel>();
            containerRegistry.RegisterSingleton<Views.IFriend16Detail, Views.Friend16Detail>();

            containerRegistry.Register<ViewModels.IMeeting16DetailViewModel, ViewModels.Meeting16DetailViewModel>();
            containerRegistry.RegisterSingleton<Views.IMeeting16Detail, Views.Meeting16Detail>();

            containerRegistry.RegisterSingleton<ViewModels.INavigation16ViewModel, ViewModels.Navigation16ViewModel>();
            //containerRegistry.RegisterSingleton<ViewModels.IFriend16ViewModel, ViewModels.Friend16ViewModel>();
            containerRegistry.RegisterSingleton<Views.INavigation16, Views.Navigation16>();
            //containerRegistry.RegisterSingleton<Views.IFriend16, Views.Friend16>();

            containerRegistry.Register<Views.Friend16Main>();

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

            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion16, typeof(Views.Friend16Main));

            // Put Friend and FriendDetail on FriendOrganizerMain (this assembly)

            _regionManager.RegisterViewWithRegion(RegionNames.Region16, typeof(Views.Navigation16));
            //_regionManager.RegisterViewWithRegion(RegionNames.Region16, typeof(Views.Friend16));

            //_regionManager.RegisterViewWithRegion(RegionNames.RegionDetail16, typeof(Views.Friend16Detail));
        }
    }
}