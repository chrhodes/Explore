using System;
using System.Web.Mvc;
using MvcControllers.Models;

namespace MvcControllers.Controllers
{
    public class HomeController : Controller
    {
        //[OutputCache(Duration = 60)]
        public ActionResult Index()
        {
            ViewData["Message"] = "Hello from Index()";
            var model = DateTime.Now;
            return View(model);
        }


        
        public ActionResult Results()
        {
            return HttpNotFound();
        }


        [ChildActionOnly]
        //[OutputCache(Duration=10)]
        public PartialViewResult CurrentTime()
        {
            ViewBag.Message = "Hello from CurrentTime()";

            var model = DateTime.Now;
            return PartialView(model);
        } 
        
        [HttpGet]
        public ActionResult Edit()
        {
            var instructor = new Instructor();
            return View(instructor);
        }


        [HttpPost]        
        public ActionResult Edit(Instructor instructor)
        {
            return View(instructor);
        }
    }
}
