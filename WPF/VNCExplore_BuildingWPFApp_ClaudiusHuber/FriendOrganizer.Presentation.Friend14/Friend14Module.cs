using FriendOrganizer.DomainServices.Lookups;
using FriendOrganizer.DomainServices.Repositories;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNCExplore_FriendOrganizer.Core;
using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend14
{
    public class Friend14Module : IModule
    {
        private readonly IRegionManager _regionManager;
        public static IContainerProvider _containerProvider;

        // 01

        public Friend14Module(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ViewModels.IFriend14DetailViewModel, ViewModels.Friend14DetailViewModel>();
            containerRegistry.RegisterSingleton<Views.IFriend14Detail, Views.Friend14Detail>();

            containerRegistry.RegisterSingleton<ViewModels.IFriend14ViewModel, ViewModels.Friend14ViewModel>();
            containerRegistry.RegisterSingleton<Views.IFriend14, Views.Friend14>();

            containerRegistry.Register<Views.Friend14Main>();

            // TODO(crhodes)
            // Learn if Unity can do AsImplementedInterfaces like AutoFac

            containerRegistry.Register<IFriendLookupDataService10, LookupDataService12>();
            containerRegistry.Register<IProgrammingLanguageLookupDataService12, LookupDataService12>();

            containerRegistry.Register<IFriendRepository13, FriendRepository13>();

            containerRegistry.Register<IMessageDialogService, MessageDialogService>();
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _containerProvider = containerProvider;

            // Put FriendOrganizerMain on MainWindow

            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion14, typeof(Views.Friend14Main));

            // Put Friend and FriendDetail on FriendOrganizerMain (this assembly)

            _regionManager.RegisterViewWithRegion(RegionNames.Region14, typeof(Views.Friend14));
            _regionManager.RegisterViewWithRegion(RegionNames.RegionDetail14, typeof(Views.Friend14Detail));
        }
    }
}