using FriendOrganizer.DomainServices.Lookups;
using FriendOrganizer.DomainServices.Repositories;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNCExplore_FriendOrganizer.Core;
using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend15
{
    public class Friend15Module : IModule
    {
        private readonly IRegionManager _regionManager;
        public static IContainerProvider _containerProvider;

        // 01

        public Friend15Module(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ViewModels.IFriend15DetailViewModel, ViewModels.Friend15DetailViewModel>();
            containerRegistry.RegisterSingleton<Views.IFriend15Detail, Views.Friend15Detail>();

            containerRegistry.Register<ViewModels.IMeeting15DetailViewModel, ViewModels.Meeting15DetailViewModel>();
            containerRegistry.RegisterSingleton<Views.IMeeting15Detail, Views.Meeting15Detail>();

            containerRegistry.RegisterSingleton<ViewModels.INavigation15ViewModel, ViewModels.Navigation15ViewModel>();
            //containerRegistry.RegisterSingleton<ViewModels.IFriend15ViewModel, ViewModels.Friend15ViewModel>();
            containerRegistry.RegisterSingleton<Views.INavigation15, Views.Navigation15>();
            //containerRegistry.RegisterSingleton<Views.IFriend15, Views.Friend15>();

            containerRegistry.Register<Views.Friend15Main>();

            // TODO(crhodes)
            // Learn if Unity can do AsImplementedInterfaces like AutoFac

            containerRegistry.Register<IFriendLookupDataService10, LookupDataService15>();
            containerRegistry.Register<IProgrammingLanguageLookupDataService12, LookupDataService15>();
            containerRegistry.Register<IMeetingLookupDataService15, LookupDataService15>();

            containerRegistry.Register<IFriendRepository15, FriendRepository15>();
            containerRegistry.Register<IMeetingRepository15, MeetingRepository15>();

            containerRegistry.Register<IMessageDialogService, MessageDialogService>();
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _containerProvider = containerProvider;

            // Put FriendOrganizerMain on MainWindow

            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion15, typeof(Views.Friend15Main));

            // Put Friend and FriendDetail on FriendOrganizerMain (this assembly)

            _regionManager.RegisterViewWithRegion(RegionNames.Region15, typeof(Views.Navigation15));
            //_regionManager.RegisterViewWithRegion(RegionNames.Region15, typeof(Views.Friend15));

            //_regionManager.RegisterViewWithRegion(RegionNames.RegionDetail15, typeof(Views.Friend15Detail));
        }
    }
}