using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobPortal.Models;

namespace JobPortal.Areas.User.Controllers
{
    public class UserLoginController : Controller
    {
        dbjobportalEntities1 db = new dbjobportalEntities1();
        // GET: User/UserLogin
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserMaster login)
        {
            try
            {

                var Logindata = db.UserMasters.SingleOrDefault(a => a.UserName == login.UserName && a.UserPassword == login.UserPassword);
                if (Logindata != null)
                {
                    Session["UserId"] = Logindata.UserId;
                    Session["UserName"] = Logindata.UserName;
                    TempData["msg"] = "Login Done!";

                    return RedirectToAction("Home", "Home");
                }
                else
                {
                    TempData["err"] = "Username or Password is Wrong!!";

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
