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
    public class ROUTEHDRsController : Controller
    {
        private RoutingEntities db = new RoutingEntities();

        // GET: ROUTEHDRs
        public ActionResult Index()
        {
            return View(db.ROUTEHDRs.ToList());
        }

        // GET: ROUTEHDRs/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROUTEHDR rOUTEHDR = db.ROUTEHDRs.Find(id);
            if (rOUTEHDR == null)
            {
                return HttpNotFound();
            }
            return View(rOUTEHDR);
        }

        // GET: ROUTEHDRs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ROUTEHDRs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RECTYPE,SEQ,LOTSIZE,AUTHREL,ENG,RELDATE,USER0,USER1,USER2,USER3,MATCOST,EXPUNIT,EXPLOT,CCOST,SCOST,CHANGE,TCOST,TOOLCOSTS,NOP,PARTDESC,NET,USERID,SETUPHRS,RUNHRS,FLAG1,FLAG2,FLAG3,MASTERPART,TAKTTIME,NetDate,PLANTID,PARTREV,LOCKPLAN,ContinousFlow,SHLevel")] ROUTEHDR rOUTEHDR)
        {
            if (ModelState.IsValid)
            {
                db.ROUTEHDRs.Add(rOUTEHDR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rOUTEHDR);
        }

        // GET: ROUTEHDRs/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROUTEHDR rOUTEHDR = db.ROUTEHDRs.Find(id);
            if (rOUTEHDR == null)
            {
                return HttpNotFound();
            }
            return View(rOUTEHDR);
        }

        // POST: ROUTEHDRs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RECTYPE,SEQ,LOTSIZE,AUTHREL,ENG,RELDATE,USER0,USER1,USER2,USER3,MATCOST,EXPUNIT,EXPLOT,CCOST,SCOST,CHANGE,TCOST,TOOLCOSTS,NOP,PARTDESC,NET,USERID,SETUPHRS,RUNHRS,FLAG1,FLAG2,FLAG3,MASTERPART,TAKTTIME,NetDate,PLANTID,PARTREV,LOCKPLAN,ContinousFlow,SHLevel")] ROUTEHDR rOUTEHDR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rOUTEHDR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rOUTEHDR);
        }

        // GET: ROUTEHDRs/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ROUTEHDR rOUTEHDR = db.ROUTEHDRs.Find(id);
            if (rOUTEHDR == null)
            {
                return HttpNotFound();
            }
            return View(rOUTEHDR);
        }

        // POST: ROUTEHDRs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            ROUTEHDR rOUTEHDR = db.ROUTEHDRs.Find(id);
            db.ROUTEHDRs.Remove(rOUTEHDR);
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
