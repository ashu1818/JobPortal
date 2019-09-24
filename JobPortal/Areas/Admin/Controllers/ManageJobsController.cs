using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobPortal.Models;
using static JobPortal.FilterConfig;

namespace JobPortal.Areas.Admin.Controllers
{
    [AdminLoginFilter]
    public class ManageJobsController : Controller
    {
        private dbjobportalEntities1 db = new dbjobportalEntities1();

        // GET: Admin/ManageJobs
        public ActionResult Index()
        {
           
            var manageJobs = db.ManageJobs.Include(m => m.CityMaster).Include(m => m.Department);
            return View(manageJobs.ToList());
        }

        // GET: Admin/ManageJobs/Details/5
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

        // GET: Admin/ManageJobs/Create
        public ActionResult Create()
        {

            ViewBag.countryname = new SelectList(db.CountryMasters.ToList(), "CountryId", "CountryName");
            ViewBag.statename = new List<SelectListItem>(){
                new SelectListItem(){ Text = "", Value="Select State"}
            }; ViewBag.RefCityId = new List<SelectListItem>() {
                new SelectListItem(){Text="", Value="Select City"}
            };
            ViewBag.RefDepartmentId = new SelectList(db.Departments, "DepId", "DepName");
            return View();
        }

        public JsonResult GetStatesByCountryId(int id)
        {
            return Json(db.StateMasters.Where(a => a.RefCountryId == id).Select(b => new { b.StateName, b.StateId }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCitiesByStateId(int id)

        {
            return Json(db.CityMasters.Where(a => a.RefStateId == id).Select(b => new { b.CityName, b.CityId }).ToList(), JsonRequestBehavior.AllowGet);
        }

        // POST: Admin/ManageJobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobId,JobName,JobSkills,JobExpirience,JobIsActive,JobDocuments,JobCreatedBy,JobModifiedBy,JobCreatedDate,JobModifiedDate,RefDepartmentId,RefCityId")] ManageJob manageJob, HttpPostedFileBase[] JobDocuments)
        {
            if (ModelState.IsValid)
            {
               
                    var JobCreate = Convert.ToString(Session["name"]);
                    manageJob.JobCreatedBy = JobCreate;
                    manageJob.JobCreatedDate = DateTime.Now;

                string allfl = "";
                foreach(HttpPostedFileBase file in JobDocuments)
                {
                    if(file != null)
                    {
                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/Content/Admin/UploadedFiles/") + InputFileName);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        //assigning file uploaded status to ViewBag for showing message to user.  
                        ViewBag.UploadStatus = JobDocuments.Count().ToString() + " files uploaded successfully.";
                        allfl += InputFileName + ",";
                    }
                }
                manageJob.JobDocuments = allfl.Remove(allfl.Length - 1, 1);
                    db.ManageJobs.Add(manageJob);
                    db.SaveChanges();
                    return RedirectToAction("Index");
               
            }

           // ViewBag.RefCityId = new SelectList(db.CityMasters, "CityId", "CityName", manageJob.RefCityId);
            ViewBag.RefDepartmentId = new SelectList(db.Departments, "DepId", "DepName", manageJob.RefDepartmentId);
            return View(manageJob);
        }

        // GET: Admin/ManageJobs/Edit/5
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
            ViewBag.countryname = new SelectList(db.CountryMasters.ToList(), "CountryId", "CountryName");
            ViewBag.statename = new List<SelectListItem>(){
                new SelectListItem(){ Text = "", Value="Select State"}
            }; ViewBag.RefCityId = new List<SelectListItem>() {
                new SelectListItem(){Text="", Value="Select City"}
            };
           // ViewBag.RefCityId = new SelectList(db.CityMasters, "CityId", "CityName", manageJob.RefCityId);
            ViewBag.RefDepartmentId = new SelectList(db.Departments, "DepId", "DepName", manageJob.RefDepartmentId);
            return View(manageJob);
        }

        // POST: Admin/ManageJobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobId,JobName,JobSkills,JobExpirience,JobIsActive,JobDocuments,JobCreatedBy,JobModifiedBy,JobCreatedDate,JobModifiedDate,RefDepartmentId,RefCityId")] ManageJob manageJob)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var ModifiedName = Convert.ToString(Session["name"]);
                    manageJob.JobModifiedBy = ModifiedName;
                    manageJob.JobModifiedDate = DateTime.Now;
                    db.Entry(manageJob).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                    TempData["err"] = ex.Message;
                }
            }
            ViewBag.RefCityId = new SelectList(db.CityMasters, "CityId", "CityName", manageJob.RefCityId);
            ViewBag.RefDepartmentId = new SelectList(db.Departments, "DepId", "DepName", manageJob.RefDepartmentId);
            return View(manageJob);
        }

        // GET: Admin/ManageJobs/Delete/5
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

        // POST: Admin/ManageJobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                ManageJob manageJob = db.ManageJobs.Find(id);
                db.ManageJobs.Remove(manageJob);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                TempData["err"] = ex.Message;
            }
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
