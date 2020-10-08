using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace Mvc4VS2012Demo.Controllers
{
    public class ProcessController : Controller
    {
        //
        // GET: /Process/

        [Authorize]
        public ViewResult  List()
        {
            var processList = from p in Process.GetProcesses()
                              select p;

            ViewData.Model = processList.ToList();

            return View();
        }

        //
        // GET: /Process/Details/5

        public ActionResult Details(int id)
        {
            var process = (from p in Process.GetProcesses()
                               where p.Id == id
                               select p).Single();

            return View(process);
        }

        //
        // GET: /Process/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Process/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Process/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Process/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Process/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Process/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
