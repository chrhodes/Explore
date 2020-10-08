using MvcControllers2.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using StructureMap;

namespace MvcControllers2
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ControllerBuilder.Current.SetControllerFactory(new MyControllerFactory());
            ConfigureContainer();
        }
        void ConfigureContainer()
        {
            // Configure the StructureMap container
            //StructureMapConfiguration
            //   .ForRequestedType<ILogger>()
            //   .TheDefaultIsConcreteType<SqlServerLogger>();

            ObjectFactory.Initialize(x => x.For<ILogger>().Use<SqlServerLogger>());
        }
    }
}
