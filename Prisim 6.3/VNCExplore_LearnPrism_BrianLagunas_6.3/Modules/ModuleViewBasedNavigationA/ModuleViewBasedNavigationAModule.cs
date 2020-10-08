﻿using Microsoft.Practices.Unity;
using ModuleInterfaces;
using Prism.Regions;
using Prism.Unity;
using VNC.Core.Mvvm.Prism;
using VNC.Core.Unity;
using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;

namespace ModuleViewBasedNavigationA
{
    public class ModuleViewBasedNavigationAModule : ModuleBase
    {
        public ModuleViewBasedNavigationAModule(IUnityContainer container, IRegionManager regionManager)
            : base(container, regionManager)
        {
        }

        protected override void RegisterTypes()
        {
            // Cannot do this - Shows up as System.Object
            //Container.RegisterType<ViewA1>();

            // When using Navigation have to register as Object.
            //Container.RegisterType<object, ViewA1>(typeof(ViewA1).FullName);

            // This hides the complexity.  Sigh.  There is a RegisterTypeForNavigation in Prism.Unity, too.
            // Be careful with using statements.  Need VNC.Core.Unity
            Container.RegisterTypeForNavigation<ViewA1>();

            Container.RegisterType<IContentAVBNViewModel, ViewA1ViewModel>();

            // This is for the parameters demo

            Container.RegisterType<IEmailViewModel, EmailViewModel>();
            Container.RegisterTypeForNavigation<Email>();
        }

        protected override void InitializeModule()
        {
            RegionManager.RegisterViewWithRegion(RegionNames.ToolBarRegionN_VB, typeof(ViewA1Button));

            // This is for the navigation demo
            //ApplicationCommands.NavigateCommand.Execute(typeof(ViewA1));
        }


    }
}
