using Microsoft.Practices.Unity;
using ModuleInterfaces;
using Prism.Regions;
using Prism.Unity;
using VNC.Core.Mvvm.Prism;
using VNC.Core.Unity;
using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;


namespace ModuleViewBasedNavigationB
{
    public class ModuleViewBasedNavigationBModule : ModuleBase
    {
        public ModuleViewBasedNavigationBModule(IUnityContainer container, IRegionManager regionManager)
            : base(container, regionManager)
        {
        }

        protected override void RegisterTypes()
        {
            // Cannot do this - Shows up as System.Object
            //Container.RegisterType<ViewB1>();

            // When using Navigation have to register as Object.
            //Container.RegisterType<object, ViewB1>(typeof(ViewB1).FullName);

            // This hides the complexity.  Sigh.  There is a RegisterTypeForNavigation in Prism.Unity, too.
            // Be careful with using statements.  Need VNC.Core.Unity

            //ExtensionMethods.RegisterTypeForNavigation<ViewB1>(Container);

            Container.RegisterTypeForNavigation<ViewB1>();

            Container.RegisterType<IContentBVBNViewModel, ViewB1ViewModel>();

            // This is for the parameters demo

            Container.RegisterType<IEmailViewModel, EmailViewModel>();
            Container.RegisterTypeForNavigation<Email>();
        }

        protected override void InitializeModule()
        {
            RegionManager.RegisterViewWithRegion(RegionNames.ToolBarRegionN_VB, typeof(ViewB1Button));

            // This is for the navigation demo
            //ApplicationCommands.NavigateCommand.Execute(typeof(ViewB1));
        }


    }
}
