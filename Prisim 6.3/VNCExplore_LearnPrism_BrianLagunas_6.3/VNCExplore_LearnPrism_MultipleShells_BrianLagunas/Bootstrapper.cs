using System.Windows;
using Prism.Modularity;
using Microsoft.Practices.Unity;
using Prism.Unity;
using System.Windows.Controls;
using Prism.Regions;
using System;

using VNC.Core.Mvvm.Prism;

using ModuleMultipleShells;

using VNC.Core.Mvvm;

namespace VNCExplore_LearnPrism_MultipleShells_BrianLagunas
{
    class Bootstrapper : UnityBootstrapper
    {
        // Step 1a - Create the catalog of Modules

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }

        // Step 1b - Configure the catalog of modules
        // Modules are loaded at Startup and must be a project reference

        protected override void ConfigureModuleCatalog()
        {
            var moduleCatalog = (ModuleCatalog)ModuleCatalog;
            moduleCatalog.AddModule(typeof(ModuleMultipleShellsModule));
        }

        // Step 2 - Configure the container

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            // Create a Singleton ShellService (DialogService)
            Container.RegisterType<IShellService, ShellService>(new ContainerControlledLifetimeManager());
        }

        // Step 3 - Configure the RegionAdapters if any custom ones have been created

        // Step 4 - Create the Shell that will hold the modules in designated regions.

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<Views.MainWindow>();
        }

        // Step 5 - Show the MainWindow

        protected override void InitializeShell()
        {
            var regionManager = RegionManager.GetRegionManager(Shell);
            RegionManagerAware.SetRegionManagerAware(Shell, regionManager);

            Application.Current.MainWindow.Show();        
        }
    }
}
