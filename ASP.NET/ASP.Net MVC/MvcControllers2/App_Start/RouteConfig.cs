using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcControllers2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Register a new route.  Note only the controller is being specified
            // Since this controller derived from IController, it called Execute() and did not try
            // to find an action(method) to invoke.

            routes.MapRoute(
                "SayHello",
                "Hello",
                new { controller = "Hello" });

            // NB.  This controller derived from Controller, the RouteData is evaluated
            // for the name of an Action(method) to invoke,
            // so have to specify a default Action(method) name.

            //routes.MapRoute(
            //    "SayHello2",
            //    "Hello2",
            //    new { controller = "Hello2", action = "SayHello" });

            // SayHello(...) take id parameter so need to provide default.
            routes.MapRoute(
                "SayHello2",
                "Hello2",
                new { controller = "Hello2", action = "SayHello", id = 42 });

            // Default route

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
