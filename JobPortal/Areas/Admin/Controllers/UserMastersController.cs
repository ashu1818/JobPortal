using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobPortal.Models;
using static JobPortal.FilterConfig;

namespace JobPortal.Areas.Admin.Controllers
{
    [AdminLoginFilter]
    public class UserMastersController : Controller
    {
        private dbjobportalEntities1 db = new dbjobportalEntities1();

        // GET: Admin/UserMasters
        public ActionResult Index()
        {
            var userMasters = db.UserMasters.Include(u => u.CityMaster).Include(u => u.Department);
            return View(userMasters.ToList());
        }

        // GET: Admin/UserMasters/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserMaster userMaster = db.UserMasters.Find(id);
            if (userMaster == null)
            {
                return HttpNotFound();
            }
            return View(userMaster);
        }

        // GET: Admin/UserMasters/Create
        public ActionResult Create()
        {
            ViewBag.RefCityId = new SelectList(db.CityMasters, "CityId", "CityName");
            ViewBag.RefDepartmentId = new SelectList(db.Departments, "DepId", "DepName");
            return View();
        }

        // POST: Admin/UserMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,UserName,UserPassword,UserAddress1,UserAddress2,UserGender,UserDOB,UserContact,UserEmail,UserSkills,UserExperience,UserDoc,UCreatedBy,UModifiedBy,UCreatedDate,UModifiedDate,RefDepartmentId,RefCityId")] UserMaster userMaster)
        {
            if (ModelState.IsValid)
            {
                db.UserMasters.Add(userMaster);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RefCityId = new SelectList(db.CityMasters, "CityId", "CityName", userMaster.RefCityId);
            ViewBag.RefDepartmentId = new SelectList(db.Departments, "DepId", "DepName", userMaster.RefDepartmentId);
            return View(userMaster);
        }

        // GET: Admin/UserMasters/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserMaster userMaster = db.UserMasters.Find(id);
            if (userMaster == null)
            {
                return HttpNotFound();
            }
            ViewBag.RefCityId = new SelectList(db.CityMasters, "CityId", "CityName", userMaster.RefCityId);
            ViewBag.RefDepartmentId = new SelectList(db.Departments, "DepId", "DepName", userMaster.RefDepartmentId);
            return View(userMaster);
        }

        // POST: Admin/UserMasters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,UserName,UserPassword,UserAddress1,UserAddress2,UserGender,UserDOB,UserContact,UserEmail,UserSkills,UserExperience,UserDoc,UCreatedBy,UModifiedBy,UCreatedDate,UModifiedDate,RefDepartmentId,RefCityId")] UserMaster userMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userMaster).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RefCityId = new SelectList(db.CityMasters, "CityId", "CityName", userMaster.RefCityId);
            ViewBag.RefDepartmentId = new SelectList(db.Departments, "DepId", "DepName", userMaster.RefDepartmentId);
            return View(userMaster);
        }

        // GET: Admin/UserMasters/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserMaster userMaster = db.UserMasters.Find(id);
            if (userMaster == null)
            {
                return HttpNotFound();
            }
            return View(userMaster);
        }

        // POST: Admin/UserMasters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserMaster userMaster = db.UserMasters.Find(id);
            db.UserMasters.Remove(userMaster);
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
