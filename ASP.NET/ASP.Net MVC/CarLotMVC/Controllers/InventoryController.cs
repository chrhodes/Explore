using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoLotDAL.EF;
using AutoLotDAL.Models;
using AutoLotDAL.Repositories;

namespace CarLotMVC.Controllers
{
    public class InventoryController : Controller
    {
        // This was created by wizzard
        //private AutoLotEntities db = new AutoLotEntities();

        // Use repository instead
        private readonly InventoryRepository _repo = new InventoryRepository();

        // GET: Inventory
        public ActionResult Index()
        {
            // This uses a layout page, e.g. _ViewStart.cshtml -> _Layout.cshtml
            return View(_repo.GetAll());
            //return View(db.Inventory.ToList());
        }

        public ActionResult IndexNoLayout()
        {
            // This does not use the default layout page (but one can be specified)
            return PartialView(_repo.GetAll());
            // This uses a layout page, e.g. _ViewStart.cshtml -> _Layout.cshtml
            //return View(_repo.GetAll());
            //return View(db.Inventory.ToList());
        }

        // GET: Inventory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Inventory inventory = _repo.GetOne(id);
            //Inventory inventory = db.Inventory.Find(id);

            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // Read about Post-Redirect-Get (PRG) Pattern

        // GET: Inventory/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inventory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Make,Color,PetName")] Inventory inventory)
        {
            if (!ModelState.IsValid) return View(inventory);
            try
            {
                _repo.Add(inventory);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $@"Unable to create record: {ex.Message}");
                return View(inventory);
            }
            return RedirectToAction("Index");
        }
        //public ActionResult Create([Bind(Include = "Id,Make,Color,PetName,Timestamp")] Inventory inventory)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Inventory.Add(inventory);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(inventory);
        //}

        //        For implicit model binding, you use the desired type as the parameter for the method. The model
        //        binding engine does the same operation with the parameter as it did with TryUpdateModel() in the previous
        //        example.
        //public ActionResult Create(Inventory inventory)
        //        {
        //            if (ModelState.IsValid)
        //            {
        //                //Save the data;
        //            }
        //        }

        // GET: Inventory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = _repo.GetOne(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // POST: Inventory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Make,Color,PetName,Timestamp")] Inventory inventory)
        {
            if (!ModelState.IsValid) return View(inventory);
            try
            {
                _repo.Save(inventory);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ModelState.AddModelError(string.Empty,
                    $@"Unable to save the record. Another user has updated it. {ex.Message}");
                return View(inventory);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $@"Unable to save the record. {ex.Message}");
                return View(inventory);
            }
            return RedirectToAction("Index");
        }

        // GET: Inventory/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Inventory inventory = db.Inventory.Find(id);
        //    if (inventory == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(inventory);
        //}

//        The Post Version
//        This version is executed when a user has clicked the submit button on the Delete form.The autogenerated
//version of this method takes only the id as a parameter, meaning it has the same signature as the HttpGet
//version of the method.Since you can’t have two methods of the same name with the same signature, the
//wizard named this method DeleteConfirmed() and added the [ActionName("Delete")] attribute. Update
//the signature to the following (you can delete the ActionName attribute as well as long as you rename the
//method to Delete()):
        //// POST: Inventory/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Make,Color,PetName,Timestamp")] Inventory inventory)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(inventory).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(inventory);
        //}

        // GET: Inventory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = _repo.GetOne(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // POST: Inventory/Delete/5
        //[HttpPost, ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "Id,Timestamp")] Inventory inventory)
        {
            try
            {
                _repo.Delete(inventory);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ModelState.AddModelError(string.Empty, $@"Unable to delete record. Another user updated the record. {ex.Message}");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $@"Unable to create record: {ex.Message}");
            }

            return RedirectToAction("Index");
        }

        // GET: Inventory/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Inventory inventory = db.Inventory.Find(id);
        //    if (inventory == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(inventory);
        //}

        //// POST: Inventory/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Inventory inventory = db.Inventory.Find(id);
        //    db.Inventory.Remove(inventory);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _repo.Dispose();
                //db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
