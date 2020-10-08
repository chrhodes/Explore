using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CarLotMVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // This supports http://<site>/Contact

            routes.MapRoute("Contact", "Contact", new { controller = "Home", action = "Contact" });

            // This supports http://<site>/About

            routes.MapRoute("About", "About/{*pathinfo}", new { controller = "Home", action = "About" });

            // This supports http://<site>/Home/Contact and http://<site>/Home/About
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
