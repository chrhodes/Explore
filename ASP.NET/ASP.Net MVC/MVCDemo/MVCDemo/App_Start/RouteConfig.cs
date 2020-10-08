using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcDemo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // CHR Added

            // http://localhost/Process

            routes.MapRoute(
                "Process",
                "Process/{action}/{id}",
                new { controller = "Process", action = "List", id = "" }
            );

            // End CHR Added
            
            // Default route.  Typically put last

            // Handles
            // http://localhost
            // http://localhost/home
            // http://localhost/home/about
            // http://localhost/process/list

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
