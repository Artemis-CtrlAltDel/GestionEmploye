using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace GestionEmploye.Helpers
{
    public class AuthorizationFilter : ActionFilterAttribute
{

    private readonly string _role;
    public AuthorizationFilter(string role = "Employee") => _role = role;

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
}