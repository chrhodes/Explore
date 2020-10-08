using FriendOrganizer.DomainServices.Lookups;
using FriendOrganizer.DomainServices.Repositories;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNCExplore_FriendOrganizer.Core;
using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend19
{
    public class Friend19Module : IModule
    {
        private readonly IRegionManager _regionManager;
        public static IContainerProvider _containerProvider;

        // 01

        public Friend19Module(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ViewModels.IFriend19DetailViewModel, ViewModels.Friend19DetailViewModel>();
            containerRegistry.RegisterSingleton<Views.IFriend19Detail, Views.Friend19Detail>();

            containerRegistry.Register<ViewModels.IMeeting19DetailViewModel, ViewModels.Meeting19DetailViewModel>();
            containerRegistry.RegisterSingleton<Views.IMeeting19Detail, Views.Meeting19Detail>();

            containerRegistry.Register<ViewModels.IProgrammingLanguage19DetailViewModel, ViewModels.ProgrammingLanguage19DetailViewModel>();
            containerRegistry.RegisterSingleton<Views.IProgrammingLanguage19Detail, Views.ProgrammingLanguage19Detail>();

            containerRegistry.RegisterSingleton<ViewModels.INavigation19ViewModel, ViewModels.Navigation19ViewModel>();
            containerRegistry.RegisterSingleton<Views.INavigation19, Views.Navigation19>();

            containerRegistry.Register<Views.Friend19Main>();

            // TODO(crhodes)
            // Learn if Unity can do AsImplementedInterfaces like AutoFac

            containerRegistry.Register<IFriendLookupDataService10, LookupDataService19>();
            containerRegistry.Register<IProgrammingLanguageLookupDataService12, LookupDataService19>();
            containerRegistry.Register<IMeetingLookupDataService15, LookupDataService19>();

            containerRegistry.Register<IFriendRepository19, FriendRepository19>();
            containerRegistry.Register<IMeetingRepository19, MeetingRepository19>();
            containerRegistry.Register<IProgrammingLanguageRepository18, ProgrammingLanguageRepository18>();

            containerRegistry.Register<IMessageDialogService, MessageDialogService>();
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _containerProvider = containerProvider;

            // Put FriendOrganizerMain on MainWindow

            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion19, typeof(Views.Friend19Main));

            // Put Friend and FriendDetail on FriendOrganizerMain (this assembly)

            _regionManager.RegisterViewWithRegion(RegionNames.Region19, typeof(Views.Navigation19));
            //_regionManager.RegisterViewWithRegion(RegionNames.Region19, typeof(Views.Friend19));

            //_regionManager.RegisterViewWithRegion(RegionNames.RegionDetail19, typeof(Views.Friend19Detail));
        }
    }
}