using MvcControllers2.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcControllers2.Controllers
{
    [Log]
    public class Hello2Controller : Controller
    {
        ILogger _logger = null;

        public Hello2Controller(ILogger logger)
        {
            _logger = logger;
        }

        public ActionResult SayHello(int id)
        {
            return View("Hello");
            //return View(id);
        }

        public ContentResult SayHello2(int id)
        {
            var routeData = this.RouteData;
            return Content("SayHello2(int id) from my new Controller that derives from Controller" + id.ToString());
        }

        // id can come from form, querystring, MapRoute default

        public string SayHello3(int id)
        {
            var routeData = this.RouteData;
            return "SayHello3(int id) from my new Controller that derives from Controller" + id.ToString();
        }

        public string SayHello4(int? id)
        {
            var routeData = this.RouteData;
            // Need to check if id is null
            return "SayHello4(int? id) from my new Controller that derives from Controller" + id.ToString();
        }

        public string SayHello5()
        {
            var routeData = this.RouteData;
            return "SayHello5() from my new Controller that derives from Controller";
        }

        // Abstract base class provides this now

        //public void Execute(RequestContext requestContext)
        //{
        //    requestContext.HttpContext.Response.Write("Hello from my Controller that derives from Controller");
        //    _logger.Log("Log Message");
        //}
    }
}