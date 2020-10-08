using FriendOrganizer.DomainServices.Lookups;
using FriendOrganizer.DomainServices.Repositories;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNCExplore_FriendOrganizer.Core;
using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend13
{
    public class Friend13Module : IModule
    {
        private readonly IRegionManager _regionManager;
        public static IContainerProvider _containerProvider;

        //public static Friend13Detail friend10Detail;

        // 01

        public Friend13Module(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ViewModels.IFriend13DetailViewModel, ViewModels.Friend13DetailViewModel>();
            containerRegistry.RegisterSingleton<Views.IFriend13Detail, Views.Friend13Detail>();

            containerRegistry.RegisterSingleton<ViewModels.IFriend13ViewModel, ViewModels.Friend13ViewModel>();
            containerRegistry.RegisterSingleton<Views.IFriend13, Views.Friend13>();

            containerRegistry.Register<Views.Friend13Main>();

            // TODO(crhodes)
            // Learn if Unity can do AsImplementedInterfaces like AutoFac

            containerRegistry.Register<IFriendLookupDataService10, LookupDataService13>();
            containerRegistry.Register<IProgrammingLanguageLookupDataService12, LookupDataService12>();
            containerRegistry.Register<IFriendRepository13, FriendRepository13>();

            containerRegistry.Register<IMessageDialogService, MessageDialogService>();
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _containerProvider = containerProvider;

            // Put FriendOrganizerMain on MainWindow

            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion13, typeof(Views.Friend13Main));

            // Put Friend and FriendDetail on FriendOrganizerMain (this assembly)

            _regionManager.RegisterViewWithRegion(RegionNames.Region13, typeof(Views.Friend13));
            _regionManager.RegisterViewWithRegion(RegionNames.RegionDetail13, typeof(Views.Friend13Detail));
        }
    }
}