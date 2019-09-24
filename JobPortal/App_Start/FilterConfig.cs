using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace JobPortal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public class AdminLoginFilter : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {

               if(HttpContext.Current.Session["adminloginId"] == null)
                {
                    FormsAuthentication.SignOut();
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                    {
                        {"action","Login" },
                        {"controller","AdminLogin" },
                        {"returnUrl", filterContext.HttpContext.Request.RawUrl }
                    });
                    return;
                }
            }
        }

        public class UserLoginFilter : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {

                if (HttpContext.Current.Session["UserId"] == null)
                {
                    FormsAuthentication.SignOut();
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                    {
                        {"action","Login" },
                        {"controller","UserLogin" },
                        {"returnUrl", filterContext.HttpContext.Request.RawUrl }
                    });
                    return;
                }
            }
        }
    }
}
