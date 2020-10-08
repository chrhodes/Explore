using FriendOrganizer.DomainServices;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

using Unity;

using VNCExplore_FriendOrganizer.Core;
using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace FriendOrganizer.Presentation.Friend09
{
    public class Friend09Module : IModule
    {
        private readonly IRegionManager _regionManager;
        //private readonly IUnityContainer _container;

        // 01

        public Friend09Module(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        //public Friend09Module(IUnityContainer container, IRegionManager regionManager)
        //{
        //    _container = container;
        //    _regionManager = regionManager;
        //}

        // 02

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ViewModels.IFriend09DetailViewModel, ViewModels.Friend09DetailViewModel>();
            containerRegistry.Register<Views.IFriend09Detail, Views.Friend09Detail>();

            containerRegistry.Register<IFriendDataService08, FriendDataService08>();

            containerRegistry.RegisterSingleton<ViewModels.IFriend09ViewModel, ViewModels.Friend09ViewModel>();
            containerRegistry.RegisterSingleton<Views.IFriend09, Views.Friend09>();

            containerRegistry.Register<IFriendLookupDataService06, FriendLookupDataService06 >();
            
            //_container.RegisterType<ViewModels.IFriend09DetailViewModel, ViewModels.Friend09DetailViewModel>();
            //_container.RegisterType<Views.IFriend09Detail, Views.Friend09Detail>();

            //_container.RegisterType<IFriend08DataService, Friend08DataService>();

            //_container.RegisterType<ViewModels.IFriend09ViewModel, ViewModels.Friend09ViewModel>();
            //_container.RegisterType<Views.IFriend09, Views.Friend09>();

            //_container.RegisterType<IFriend06LookupDataService, Friend06LookupDataService>();
        }

        // 03

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion(RegionNames.Region09, typeof(Views.Friend09));
            _regionManager.RegisterViewWithRegion(RegionNames.Region09Detail, typeof(Views.Friend09Detail));
        }
    }
}