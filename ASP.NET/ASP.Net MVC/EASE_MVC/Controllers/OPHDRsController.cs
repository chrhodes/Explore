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
    public class OPHDRsController : Controller
    {
        private RoutingEntities db = new RoutingEntities();

        // GET: OPHDRs
        public ActionResult Index()
        {
            return View(db.OPHDRs.ToList());
        }

        // GET: OPHDRs/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OPHDR oPHDR = db.OPHDRs.Find(id);
            if (oPHDR == null)
            {
                return HttpNotFound();
            }
            return View(oPHDR);
        }

        // GET: OPHDRs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OPHDRs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,RECTYPE,SEQ,OPNO,OPSEQ,OPDESC,WORKCENT,MAXBATCH,DATAREC,PFDCYC,PFDSETUP,REALCYC,REALSETUP,ACOSTRATE,ACYCTIME,ASETUPTIME,MATRECNO,MACHRECNO,EffectiveFrom,ISMETRIC,MISCFLAG1,PCNNO,MISCFLAG2,USERDEF1,USERDEF2,USERDEF3,TOOLCOSTRUN,TOOLCOSTSU,NSL,PROCESSFLAG,NOMEN,BALANCEFLAG,ALTFLAG,COSTKEY,RULEID,STATIONTIME,Engineer,ReleasedFlag,InUseFlag,CRITOPTIME,CRITOP,VA,NVA,ESSNVA,OpRev,CheckOut_Engineer,OpRelDate,EffectiveTo,SPARE1,SPARE2,SPARE3,SHAREDOPID,BasicRunTime,BasicSetupTime,KVICODE")] OPHDR oPHDR)
        {
            if (ModelState.IsValid)
            {
                db.OPHDRs.Add(oPHDR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(oPHDR);
        }

        // GET: OPHDRs/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OPHDR oPHDR = db.OPHDRs.Find(id);
            if (oPHDR == null)
            {
                return HttpNotFound();
            }
            return View(oPHDR);
        }

        // POST: OPHDRs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,RECTYPE,SEQ,OPNO,OPSEQ,OPDESC,WORKCENT,MAXBATCH,DATAREC,PFDCYC,PFDSETUP,REALCYC,REALSETUP,ACOSTRATE,ACYCTIME,ASETUPTIME,MATRECNO,MACHRECNO,EffectiveFrom,ISMETRIC,MISCFLAG1,PCNNO,MISCFLAG2,USERDEF1,USERDEF2,USERDEF3,TOOLCOSTRUN,TOOLCOSTSU,NSL,PROCESSFLAG,NOMEN,BALANCEFLAG,ALTFLAG,COSTKEY,RULEID,STATIONTIME,Engineer,ReleasedFlag,InUseFlag,CRITOPTIME,CRITOP,VA,NVA,ESSNVA,OpRev,CheckOut_Engineer,OpRelDate,EffectiveTo,SPARE1,SPARE2,SPARE3,SHAREDOPID,BasicRunTime,BasicSetupTime,KVICODE")] OPHDR oPHDR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oPHDR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(oPHDR);
        }

        // GET: OPHDRs/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OPHDR oPHDR = db.OPHDRs.Find(id);
            if (oPHDR == null)
            {
                return HttpNotFound();
            }
            return View(oPHDR);
        }

        // POST: OPHDRs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            OPHDR oPHDR = db.OPHDRs.Find(id);
            db.OPHDRs.Remove(oPHDR);
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
