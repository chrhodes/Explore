using Prism.Regions;
using Prism.Unity;

using Unity;

using VNC.Core.Mvvm.Prism;
using VNC.Core.Unity;

using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;

namespace ModuleViewBasedNavigationABasicRegionNavigation
{
    public class ModuleViewBasedNavigationABasicRegionNavigationModule : ModuleBase
    {
        public ModuleViewBasedNavigationABasicRegionNavigationModule(IUnityContainer container, IRegionManager regionManager)
            : base(container, regionManager)
        {
        }

        protected override void RegisterTypes()
        {
            // Cannot do this - Shows up as System.Object
            //Container.RegisterType<ViewA1>();

            // When using Navigation have to register as Object.
            //Container.RegisterType<object, ViewA1>(typeof(ViewA1).FullName);

            // This hides the complexity.  
            // Sigh.  There is a RegisterTypeForNavigation in Prism.Unity, too.
            // Be careful with using statements.  Need VNC.Core.Unity
            Container.RegisterTypeForNavigation<ViewA1>();
        }

        protected override void InitializeModule()
        {
            RegionManager.RegisterViewWithRegion(RegionNames.ToolBarRegionN_VB_BRN, typeof(ViewA1Button));
        }
    }
}
