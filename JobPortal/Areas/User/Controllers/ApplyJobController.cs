using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobPortal.Models;
using static JobPortal.FilterConfig;

namespace JobPortal.Areas.User.Controllers
{
    [UserLoginFilter]
    public class ApplyJobController : Controller
    {
        dbjobportalEntities1 db = new dbjobportalEntities1();
        // GET: User/ApplyJob
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Index(jobseek apply)
        {
            try
            {
                var Logindata = db.jobseeks.SingleOrDefault(a => a.RefUserId == apply.RefUserId && a.RefJobId == apply.RefJobId);
                if (Logindata != null)
                {
                    Session["UserId"] = Logindata.RefUserId;
                    Session["JobId"] = Logindata.RefJobId;

                    return RedirectToAction("Index", "ApplyJob");
                }
                else
                {
                    TempData["err"] = "Already Applied for this Job!!";

                }
            }
            catch (Exception ex)
            {
                TempData["err"] = ex.Message;
            }
            return View();
        }

        public ActionResult apply(int id)
        {

                jobseek j = new jobseek();
            
                j.RefUserId = int.Parse(Session["UserId"].ToString());
                j.JobCreatedDate = System.DateTime.Now;
                j.RefJobId = id;
                db.jobseeks.Add(j);
                db.SaveChanges();
            

            return RedirectToAction("Index", "ManageJobs");
        }
    }
}