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

namespace JobPortal.Areas.User.Controllers
{
    public class UserMastersController : Controller
    {
        private dbjobportalEntities1 db = new dbjobportalEntities1();

        // GET: User/UserMasters
        public ActionResult Index()
        {
            var userMasters = db.UserMasters.Include(u => u.CityMaster).Include(u => u.Department);
            return View(userMasters.ToList());
        }

        // GET: User/UserMasters/Details/5
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

        // GET: User/UserMasters/Create
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
        // POST: User/UserMasters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,UserName,UserPassword,UserAddress1,UserAddress2,UserGender,UserDOB,UserContact,UserEmail,UserSkills,UserExperience,UserDoc,UCreatedBy,UModifiedBy,UCreatedDate,UModifiedDate,RefDepartmentId,RefCityId")] UserMaster userMaster, HttpPostedFileBase[] UserDoc)
        {
            if (ModelState.IsValid)
            {
                var UserCreate = Convert.ToString(Session["UserName"]);
                userMaster.UCreatedBy = UserCreate;
                userMaster.UCreatedDate = DateTime.Now;
                string allfl = "";
                foreach (HttpPostedFileBase file in UserDoc)
                {
                    if (file != null)
                    {
                        var InputFileName = Path.GetFileName(file.FileName);
                        var ServerSavePath = Path.Combine(Server.MapPath("~/Content/Admin/UploadedFiles/") + InputFileName);
                        //Save file to server folder  
                        file.SaveAs(ServerSavePath);
                        //assigning file uploaded status to ViewBag for showing message to user.  
                        ViewBag.UploadStatus = UserDoc.Count().ToString() + " files uploaded successfully.";
                        allfl += InputFileName + ",";
                    }
                }
                userMaster.UserDoc = allfl.Remove(allfl.Length - 1, 1);

                db.UserMasters.Add(userMaster);
                    db.SaveChanges();
                    return RedirectToAction("Login", "UserLogin");
                
               
                
            }
            ViewBag.countryname = new SelectList(db.CountryMasters.ToList(), "CountryId", "CountryName");
            ViewBag.statename = new List<SelectListItem>(){
                new SelectListItem(){ Text = "", Value="Select State"}
            }; ViewBag.RefCityId = new List<SelectListItem>() {
                new SelectListItem(){Text="", Value="Select City"}
            };
            //ViewBag.RefCityId = new SelectList(db.CityMasters, "CityId", "CityName", userMaster.RefCityId);
            ViewBag.RefDepartmentId = new SelectList(db.Departments, "DepId", "DepName", userMaster.RefDepartmentId);
            return View(userMaster);
        }

        // GET: User/UserMasters/Edit/5
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

        // POST: User/UserMasters/Edit/5
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

        // GET: User/UserMasters/Delete/5
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

        // POST: User/UserMasters/Delete/5
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
