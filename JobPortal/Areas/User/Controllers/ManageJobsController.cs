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

namespace JobPortal.Areas.User.Controllers
{
    [UserLoginFilter]
    public class ManageJobsController : Controller
    {
        private dbjobportalEntities1 db = new dbjobportalEntities1();

        // GET: User/ManageJobs
        public ActionResult Index()
        {
            var manageJobs = db.ManageJobs.Include(m => m.CityMaster).Include(m => m.Department);
            return View(manageJobs.ToList());
        }

        // GET: User/ManageJobs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManageJob manageJob = db.ManageJobs.Find(id);
            if (manageJob == null)
            {
                return HttpNotFound();
            }
            return View(manageJob);
        }

        // GET: User/ManageJobs/Create
        public ActionResult Create()
        {
            ViewBag.RefCityId = new SelectList(db.CityMasters, "CityId", "CityName");
            ViewBag.RefDepartmentId = new SelectList(db.Departments, "DepId", "DepName");
            return View();
        }

        // POST: User/ManageJobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobId,JobName,JobSkills,JobExpirience,JobIsActive,JobDocuments,JobCreatedBy,JobModifiedBy,JobCreatedDate,JobModifiedDate,RefDepartmentId,RefCityId")] ManageJob manageJob)
        {
            if (ModelState.IsValid)
            {
                db.ManageJobs.Add(manageJob);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RefCityId = new SelectList(db.CityMasters, "CityId", "CityName", manageJob.RefCityId);
            ViewBag.RefDepartmentId = new SelectList(db.Departments, "DepId", "DepName", manageJob.RefDepartmentId);
            return View(manageJob);
        }

        // GET: User/ManageJobs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManageJob manageJob = db.ManageJobs.Find(id);
            if (manageJob == null)
            {
                return HttpNotFound();
            }
            ViewBag.RefCityId = new SelectList(db.CityMasters, "CityId", "CityName", manageJob.RefCityId);
            ViewBag.RefDepartmentId = new SelectList(db.Departments, "DepId", "DepName", manageJob.RefDepartmentId);
            return View(manageJob);
        }

        // POST: User/ManageJobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobId,JobName,JobSkills,JobExpirience,JobIsActive,JobDocuments,JobCreatedBy,JobModifiedBy,JobCreatedDate,JobModifiedDate,RefDepartmentId,RefCityId")] ManageJob manageJob)
        {
            if (ModelState.IsValid)
            {
                db.Entry(manageJob).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RefCityId = new SelectList(db.CityMasters, "CityId", "CityName", manageJob.RefCityId);
            ViewBag.RefDepartmentId = new SelectList(db.Departments, "DepId", "DepName", manageJob.RefDepartmentId);
            return View(manageJob);
        }

        // GET: User/ManageJobs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ManageJob manageJob = db.ManageJobs.Find(id);
            if (manageJob == null)
            {
                return HttpNotFound();
            }
            return View(manageJob);
        }

        // POST: User/ManageJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ManageJob manageJob = db.ManageJobs.Find(id);
            db.ManageJobs.Remove(manageJob);
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
