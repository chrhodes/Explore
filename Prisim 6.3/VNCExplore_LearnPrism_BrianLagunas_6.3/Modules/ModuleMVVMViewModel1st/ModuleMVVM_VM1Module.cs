using System;
using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;
using System.Windows;
using ModuleInterfaces;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace ModuleMVVM_VM1
{
    public class ModuleMVVM_VM1Module : IModule
    {
        IUnityContainer _container;
        IRegionManager _regionManager;

        // Need a container so we can register our views
        // and a RegionManager so we can compose views and perform View discovery.
        // Prism will pass in when this module is created.

        public ModuleMVVM_VM1Module(IUnityContainer container, IRegionManager manager)
        {
            _container = container;
            _regionManager = manager;
        }

        public void Initialize()
        {
            // 1. Register Views and ViewModels

            // ViewModel first approach.

            // ViewModel is responsible for instantiating the View
            // When container creates ViewModel is sees a View is required in constructor.
            // Container resolves the View and passes it into the ViewModel's constructor.

            // Register a View as type IContentA_VM1_View.  Container will return ContentA_VM1
            // because ContentA_VM1 implements IContentA_VM1_View
            // Do same for IContentA_VM1_ViewViewModel, ContentA_VM1_ViewViewModel

            _container.RegisterType<ToolBarA>();
            _container.RegisterType<IContentA, ContentA_VM1>();
            _container.RegisterType<IContentAViewModel, ContentA_VM1_ViewModel>();

            // 2. Compose Application views using registered Views and ViewModels

            // Enable view discovery for ToolBar

            _regionManager.RegisterViewWithRegion(RegionNames.ToolBarRegionV_VM1, typeof(ToolBarA));

            // Enable view discovery for content

            // This creates the View directly without resolving the ViewModel
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegionV_VM1, typeof(ContentA_VM1));

            // But if we resolve the ViewModel first, it will create the view (Constructor needs)
            var vm = _container.Resolve<IContentAViewModel>();

            //Now injection (Regions[].Add() the view the ViewModel created. (17)
            _regionManager.Regions[RegionNames.ContentRegionV_VM1].Add(vm.View);
        }
    }
}
