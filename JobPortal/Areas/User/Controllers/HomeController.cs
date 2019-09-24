using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static JobPortal.FilterConfig;

namespace JobPortal.Areas.User.Controllers
{
    [UserLoginFilter]
    public class HomeController : Controller
    {
        // GET: User/Home
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Home", "Home");
        }
    }
}