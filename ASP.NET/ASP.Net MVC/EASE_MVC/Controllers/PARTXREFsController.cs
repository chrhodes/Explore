using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EaseEFDAL.EF;
using EaseEFDAL.Models;

namespace EASE_MVC.Controllers
{
    public class PARTXREFsController : Controller
    {
        private RoutingEntities db = new RoutingEntities();

        // GET: PARTXREFs
        public ActionResult Index()
        {
            return View(db.PARTXREFs.ToList());
        }

        // GET: PARTXREFs/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PARTXREF pARTXREF = db.PARTXREFs.Find(id);
            if (pARTXREF == null)
            {
                return HttpNotFound();
            }
            return View(pARTXREF);
        }

        // GET: PARTXREFs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PARTXREFs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,PARTNO,PlantID")] PARTXREF pARTXREF)
        {
            if (ModelState.IsValid)
            {
                db.PARTXREFs.Add(pARTXREF);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pARTXREF);
        }

        // GET: PARTXREFs/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PARTXREF pARTXREF = db.PARTXREFs.Find(id);
            if (pARTXREF == null)
            {
                return HttpNotFound();
            }
            return View(pARTXREF);
        }

        // POST: PARTXREFs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,PARTNO,PlantID")] PARTXREF pARTXREF)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pARTXREF).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pARTXREF);
        }

        // GET: PARTXREFs/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PARTXREF pARTXREF = db.PARTXREFs.Find(id);
            if (pARTXREF == null)
            {
                return HttpNotFound();
            }
            return View(pARTXREF);
        }

        // POST: PARTXREFs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            PARTXREF pARTXREF = db.PARTXREFs.Find(id);
            db.PARTXREFs.Remove(pARTXREF);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
