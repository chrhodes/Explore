using System;
using System.Web.Mvc;
using Models.Models;

namespace Models.Controllers
{
    public class HomeController : Controller
    {
        private readonly TimeCardRepository _timeCards;

        public HomeController(TimeCardRepository timeCards)
        {
            _timeCards = timeCards;
        }
        
        public JsonResult CheckUsername(string username)
        {
            var result = false;

            if (username == "sallen")
            {
                result = true;
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            var model = _timeCards.GetAll();
            return View(model);
        }

        public ActionResult Details(Guid id)
        {
            var model = _timeCards.GetById(id);
            return View(model);
        }

        public ActionResult Create()
        {
            return View(new TimeCard());
        } 

        [HttpPost]
        public ActionResult Create(TimeCard newCard)
        {
            if (ModelState.IsValid)
            {
                newCard.Id = Guid.NewGuid();
                _timeCards.Add(newCard);
                return RedirectToAction("Index");
            }
            else
            {
                return View(newCard);
            }
        }
        
        public ActionResult Edit(Guid id)
        {
            var model = _timeCards.GetById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(TimeCard editedCard)
        {
            if (ModelState.IsValid)
            {               
                return RedirectToAction("Index");
            }
            else
            {
                return View(editedCard);
            }
        }

        public ActionResult Delete(Guid id)
        {
            var model = _timeCards.GetById(id);
            return View();
        }

        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            _timeCards.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
