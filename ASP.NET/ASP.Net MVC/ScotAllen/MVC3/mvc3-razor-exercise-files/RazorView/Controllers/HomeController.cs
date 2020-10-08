using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;

namespace RazorView.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            ViewData["Message"] = "Hello, Razor!";            
            return View(_instructors);
        }


        private List<Instructor> _instructors = new List<Instructor>
        {
            new Instructor
                {
                    Name="Scott",
                    TwitterHandle = "@OdeToCode",
                    HtmlDescription = "This is <em>markup</em>!"
                },
            new Instructor
                {
                    Name="David",
                    TwitterHandle = "@ElegantCoder",
                    HtmlDescription = "This is <em>markup</em> too!"
                },
        };
    }
}
