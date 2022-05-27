using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GestionEmploye.Helpers
{
    public class AdminOnlyFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.GetInt32("Admin") != 1)
            {

                filterContext.Result = new RedirectToRouteResult(
                       new RouteValueDictionary((new { controller = "Auth", action = "Login" })));
            }
            base.OnActionExecuting(filterContext);
        }
    }
    
    public class LoggedInFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.GetInt32("PersonId") == null)
            {

                filterContext.Result = new RedirectToRouteResult(
                       new RouteValueDictionary((new { controller = "Auth", action = "Login" })));
            }
            base.OnActionExecuting(filterContext);
        }
    }
    public class LoggedOutFilter : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session.GetInt32("PersonId") != null)
            {
                var isAdmin = filterContext.HttpContext.Session.GetInt32("Admin") == 1;
                filterContext.Result = new RedirectToRouteResult(
                       new RouteValueDictionary( isAdmin ? 
                       (new { controller = "Admin", action ="Index" })
                       :(new { controller = "Employees", action ="Details",id = filterContext.HttpContext.Session.GetInt32("EmployeId") })));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}