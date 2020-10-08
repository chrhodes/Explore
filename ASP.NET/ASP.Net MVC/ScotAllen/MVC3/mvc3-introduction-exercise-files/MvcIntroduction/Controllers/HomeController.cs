using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcIntroduction.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to the ASP.NET MVC course!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
      
        public ActionResult Hello(string name)
        {
            return Content(String.Format("Hello, {0}!", name));
        }
    }
}
