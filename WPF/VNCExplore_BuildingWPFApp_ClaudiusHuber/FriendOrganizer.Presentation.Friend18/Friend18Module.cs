using FriendOrganizer.DomainServices.Lookups;
using FriendOrganizer.DomainServices.Repositories;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNCExplore_FriendOrganizer.Core;
using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend18
{
    public class Friend18Module : IModule
    {
        private readonly IRegionManager _regionManager;
        public static IContainerProvider _containerProvider;

        // 01

        public Friend18Module(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ViewModels.IFriend18DetailViewModel, ViewModels.Friend18DetailViewModel>();
            containerRegistry.RegisterSingleton<Views.IFriend18Detail, Views.Friend18Detail>();

            containerRegistry.Register<ViewModels.IMeeting18DetailViewModel, ViewModels.Meeting18DetailViewModel>();
            containerRegistry.RegisterSingleton<Views.IMeeting18Detail, Views.Meeting18Detail>();

            containerRegistry.Register<ViewModels.IProgrammingLanguage18DetailViewModel, ViewModels.ProgrammingLanguage18DetailViewModel>();
            containerRegistry.RegisterSingleton<Views.IProgrammingLanguage18Detail, Views.ProgrammingLanguage18Detail>();

            containerRegistry.RegisterSingleton<ViewModels.INavigation18ViewModel, ViewModels.Navigation18ViewModel>();
            //containerRegistry.RegisterSingleton<ViewModels.IFriend18ViewModel, ViewModels.Friend18ViewModel>();
            containerRegistry.RegisterSingleton<Views.INavigation18, Views.Navigation18>();
            //containerRegistry.RegisterSingleton<Views.IFriend18, Views.Friend18>();

            containerRegistry.Register<Views.Friend18Main>();

            // TODO(crhodes)
            // Learn if Unity can do AsImplementedInterfaces like AutoFac

            containerRegistry.Register<IFriendLookupDataService10, LookupDataService15>();
            containerRegistry.Register<IProgrammingLanguageLookupDataService12, LookupDataService15>();
            containerRegistry.Register<IMeetingLookupDataService15, LookupDataService15>();

            containerRegistry.Register<IFriendRepository16, FriendRepository16>();
            containerRegistry.Register<IMeetingRepository16, MeetingRepository16>();
            containerRegistry.Register<IProgrammingLanguageRepository18, ProgrammingLanguageRepository18>();

            containerRegistry.Register<IMessageDialogService, MessageDialogService>();
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _containerProvider = containerProvider;

            // Put FriendOrganizerMain on MainWindow

            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion18, typeof(Views.Friend18Main));

            // Put Friend and FriendDetail on FriendOrganizerMain (this assembly)

            _regionManager.RegisterViewWithRegion(RegionNames.Region18, typeof(Views.Navigation18));
            //_regionManager.RegisterViewWithRegion(RegionNames.Region18, typeof(Views.Friend18));

            //_regionManager.RegisterViewWithRegion(RegionNames.RegionDetail18, typeof(Views.Friend18Detail));
        }
    }
}