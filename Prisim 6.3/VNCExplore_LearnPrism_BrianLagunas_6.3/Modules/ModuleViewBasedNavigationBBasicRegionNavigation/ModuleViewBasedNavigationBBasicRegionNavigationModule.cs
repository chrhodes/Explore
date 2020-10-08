using Microsoft.Practices.Unity;

using Prism.Regions;
using Prism.Unity;

using VNC.Core.Mvvm.Prism;
using VNC.Core.Unity;

using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;


namespace ModuleViewBasedNavigationBBasicRegionNavigation
{
    public class ModuleViewBasedNavigationBBasicRegionNavigationModule : ModuleBase
    {
        public ModuleViewBasedNavigationBBasicRegionNavigationModule(IUnityContainer container, IRegionManager regionManager)
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
        }

        protected override void InitializeModule()
        {
            RegionManager.RegisterViewWithRegion(RegionNames.ToolBarRegionN_VB_BRN, typeof(ViewB1Button));;
        }


    }
}
