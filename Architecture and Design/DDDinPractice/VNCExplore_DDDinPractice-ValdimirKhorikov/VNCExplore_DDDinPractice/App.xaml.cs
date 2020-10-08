using System;
using System.Windows;

using FriendOrganizer.DomainServices;

using FriendOrganizer.Presentation.Friend;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

using VNCExplore_FriendOrganizer.Core.DomainServices;

namespace VNCExplore_FriendOrganizer
{
    public partial class App : PrismApplication
    {
        // 01

        protected override void ConfigureViewModelLocator()
        {
            base.ConfigureViewModelLocator();
        }

        // 02

        protected override IContainerExtension CreateContainerExtension()
        {
            return base.CreateContainerExtension();
        }

        // 03 - Create the catalog of Modules

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return base.CreateModuleCatalog();
        }

        // 04

        protected override void RegisterRequiredTypes(IContainerRegistry containerRegistry)
        {
            base.RegisterRequiredTypes(containerRegistry);
        }

        // 05

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // These are used by MainWindowDxLayout

            //containerRegistry.Register<IFriendDataService05, FriendDataService05>();
            //containerRegistry.Register<IFriendDataService04, FriendDataServiceMock04>();
        }

        // 06

        protected override void ConfigureServiceLocator()
        {
            base.ConfigureServiceLocator();
        }

        // 07 - Configure the catalog of modules
        // Modules are loaded at Startup and must be a project reference

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule(typeof(FriendModule));
        }

        // 08

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
            base.ConfigureRegionAdapterMappings(regionAdapterMappings);
        }

        // 09

        protected override void ConfigureDefaultRegionBehaviors(IRegionBehaviorFactory regionBehaviors)
        {
            base.ConfigureDefaultRegionBehaviors(regionBehaviors);
        }

        // 10

        protected override void RegisterFrameworkExceptionTypes()
        {
            base.RegisterFrameworkExceptionTypes();
        }

        // 11 

        protected override Window CreateShell()
        {
            return Container.Resolve<Views.MainWindowDxLayout>();
        }

        // 12

        protected override void InitializeShell(Window shell)
        {
            base.InitializeShell(shell);
        }

        // 13 

        protected override void InitializeModules()
        {
            base.InitializeModules();
        }

        private void Application_DispatcherUnhandledException(object sender,
            System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Unexpected error occurred. Please inform the admin."
              + Environment.NewLine + e.Exception.Message, "Unexpected error");

            e.Handled = true;
        }
    }
}
