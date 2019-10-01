using JobPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static JobPortal.FilterConfig;

namespace JobPortal.Areas.Admin.Controllers
{
    public class AdminLoginController : Controller
    {
        dbjobportalEntities1 db = new dbjobportalEntities1();
        // GET: Admin/AdminLogin
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(adminlog data)
        {
            try
            {
                var LoginData = db.adminlogs.SingleOrDefault(a => a.adminName == data.adminName && a.adminPassword == data.adminPassword);
                if(LoginData != null)
                {
                    Session["id"] = LoginData.adminloginId;
                    Session["name"] = LoginData.adminName;
                    TempData["msg"] = "LoginDone!!";
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    TempData["err"] = "UserName or Password is wrong!!";
                }
            }
            catch (Exception ex)
            {

                TempData["err"] = ex.Message;
            }
            return View();
        }
       
    }
    
}