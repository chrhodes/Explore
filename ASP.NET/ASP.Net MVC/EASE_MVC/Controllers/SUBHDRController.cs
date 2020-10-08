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
    public class SUBHDRController : Controller
    {
        private RoutingEntities db = new RoutingEntities();

        // GET: SUBHDRs
        public ActionResult Index()
        {
            return View(db.SUBHDRs.ToList());
        }

        // GET: SUBHDRs/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUBHDR sUBHDR = db.SUBHDRs.Find(id);
            if (sUBHDR == null)
            {
                return HttpNotFound();
            }
            return View(sUBHDR);
        }

        // GET: SUBHDRs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SUBHDRs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SHID,ID,RECTYPE,SEQ,OPNO,SHSEQ,DESCX,CHANGEFLAG,MODEL1,MODEL2,DESTINATION1,DESTINATION2,OPTION1,OPTION2,EXCLUDEFROMPRINT,SETUPTIME,CYCLETIME,GANTTSTARTFROM,LBSTICKY,MISCFLAG1,MISCFLAG2,MISCFLAG3,MISCFLAG4,SHType,LaborType,NumMen,DESC2,PLATTENID")] SUBHDR sUBHDR)
        {
            if (ModelState.IsValid)
            {
                db.SUBHDRs.Add(sUBHDR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sUBHDR);
        }

        // GET: SUBHDRs/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUBHDR sUBHDR = db.SUBHDRs.Find(id);
            if (sUBHDR == null)
            {
                return HttpNotFound();
            }
            return View(sUBHDR);
        }

        // POST: SUBHDRs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SHID,ID,RECTYPE,SEQ,OPNO,SHSEQ,DESCX,CHANGEFLAG,MODEL1,MODEL2,DESTINATION1,DESTINATION2,OPTION1,OPTION2,EXCLUDEFROMPRINT,SETUPTIME,CYCLETIME,GANTTSTARTFROM,LBSTICKY,MISCFLAG1,MISCFLAG2,MISCFLAG3,MISCFLAG4,SHType,LaborType,NumMen,DESC2,PLATTENID")] SUBHDR sUBHDR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sUBHDR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sUBHDR);
        }

        // GET: SUBHDRs/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SUBHDR sUBHDR = db.SUBHDRs.Find(id);
            if (sUBHDR == null)
            {
                return HttpNotFound();
            }
            return View(sUBHDR);
        }

        // POST: SUBHDRs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            SUBHDR sUBHDR = db.SUBHDRs.Find(id);
            db.SUBHDRs.Remove(sUBHDR);
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
