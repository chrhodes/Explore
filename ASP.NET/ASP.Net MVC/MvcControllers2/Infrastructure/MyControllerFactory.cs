using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace MvcControllers2.Infrastructure
{
    public class MyControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return ObjectFactory.GetInstance(controllerType) as IController;

            // This only works for controllers with Logger parameter

            //return Activator.CreateInstance(controllerType,
            //            new SqlServerLogger()) as IController;
        }
    }
}
