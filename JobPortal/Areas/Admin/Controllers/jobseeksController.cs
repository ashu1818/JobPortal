using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using iTextSharp.text.html.simpleparser;
using JobPortal.Models;
using static JobPortal.FilterConfig;

namespace JobPortal.Areas.Admin.Controllers
{
    [AdminLoginFilter]
    public class jobseeksController : Controller
    {
        private dbjobportalEntities1 db = new dbjobportalEntities1();

        // GET: Admin/jobseeks
        public ActionResult Index()
        {
            var jobseeks = db.jobseeks.Include(j => j.ManageJob).Include(j => j.UserMaster);
            return View(jobseeks.ToList());
        }

        // GET: Admin/jobseeks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jobseek jobseek = db.jobseeks.Find(id);
            if (jobseek == null)
            {
                return HttpNotFound();
            }
            return View(jobseek);
        }

        // GET: Admin/jobseeks/Create
        public ActionResult Create()
        {
            ViewBag.RefJobId = new SelectList(db.ManageJobs, "JobId", "JobName");
            ViewBag.RefUserId = new SelectList(db.UserMasters, "UserId", "UserName");
            return View();
        }

        // POST: Admin/jobseeks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JobseekId,JobCreatedDate,RefUserId,RefJobId")] jobseek jobseek)
        {
            if (ModelState.IsValid)
            {
                db.jobseeks.Add(jobseek);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RefJobId = new SelectList(db.ManageJobs, "JobId", "JobName", jobseek.RefJobId);
            ViewBag.RefUserId = new SelectList(db.UserMasters, "UserId", "UserName", jobseek.RefUserId);
            return View(jobseek);
        }

        // GET: Admin/jobseeks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jobseek jobseek = db.jobseeks.Find(id);
            if (jobseek == null)
            {
                return HttpNotFound();
            }
            ViewBag.RefJobId = new SelectList(db.ManageJobs, "JobId", "JobName", jobseek.RefJobId);
            ViewBag.RefUserId = new SelectList(db.UserMasters, "UserId", "UserName", jobseek.RefUserId);
            return View(jobseek);
        }

        // POST: Admin/jobseeks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobseekId,JobCreatedDate,RefUserId,RefJobId")] jobseek jobseek)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobseek).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RefJobId = new SelectList(db.ManageJobs, "JobId", "JobName", jobseek.RefJobId);
            ViewBag.RefUserId = new SelectList(db.UserMasters, "UserId", "UserName", jobseek.RefUserId);
            return View(jobseek);
        }

        // GET: Admin/jobseeks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jobseek jobseek = db.jobseeks.Find(id);
            if (jobseek == null)
            {
                return HttpNotFound();
            }
            return View(jobseek);
        }

        // POST: Admin/jobseeks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            jobseek jobseek = db.jobseeks.Find(id);
            db.jobseeks.Remove(jobseek);
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


        [HttpPost]
        [ValidateInput(false)]
        public FileResult Export(string GridHtml)
        {
            using (MemoryStream stream = new System.IO.MemoryStream())
            {
                StringReader sr = new StringReader(GridHtml);
                Document pdfDoc = new Document(PageSize.A2);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "Grid.pdf");
            }
        }
    }



    
}
