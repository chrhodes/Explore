﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
//using Microsoft.Practices.Prism.Regions;
using VNCExplore_LearnPrism_BrianLagunas.Infrastructure;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleC
{


    public class ModuleCModule : IModule
    {
        IUnityContainer _container;
        IRegionManager _regionManager;

        public ModuleCModule(IUnityContainer container, IRegionManager regionManager)
        {
            _container = container;
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.ToolBarRegionC, typeof(ToolBarView));
            _regionManager.RegisterViewWithRegion(RegionNames.ContentRegionC, typeof(ContentView));
        }
    }
}
