using FriendOrganizer.DomainServices.Lookups;
using FriendOrganizer.DomainServices.Repositories;

using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNCExplore_FriendOrganizer.Core;
using VNCExplore_FriendOrganizer.Core.DomainServices;
using VNCExplore_FriendOrganizer.Core.Services;

namespace FriendOrganizer.Presentation.Friend11
{
    public class Friend11Module : IModule
    {
        private readonly IRegionManager _regionManager;
        public static IContainerProvider _containerProvider;

        //public static Friend11Detail friend10Detail;

        // 01

        public Friend11Module(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ViewModels.IFriend11DetailViewModel, ViewModels.Friend11DetailViewModel>();
            containerRegistry.RegisterSingleton<Views.IFriend11Detail, Views.Friend11Detail>();

            containerRegistry.Register<IFriendRepository10, FriendRepository10>();

            containerRegistry.RegisterSingleton<ViewModels.IFriend11ViewModel, ViewModels.Friend11ViewModel>();
            containerRegistry.RegisterSingleton<Views.IFriend11, Views.Friend11>();

            containerRegistry.Register<IFriendLookupDataService10, LookupDataService10>();

            containerRegistry.Register<Views.Friend11Main>();

            containerRegistry.Register<IMessageDialogService, MessageDialogService>();
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _containerProvider = containerProvider;

            //friend10Detail = (Friend11Detail)Friend11Module._containerProvider.Resolve(typeof(IFriend11Detail));

            // Put FriendOrganizerMain on MainWindow

            _regionManager.RegisterViewWithRegion(RegionNames.MainRegion11, typeof(Views.Friend11Main));

            // Put Friend and FriendDetail on FriendOrganizerMain (this assembly)

            _regionManager.RegisterViewWithRegion(RegionNames.Region11, typeof(Views.Friend11));
            _regionManager.RegisterViewWithRegion(RegionNames.RegionDetail11, typeof(Views.Friend11Detail));
        }
    }
}