using System.Web.Mvc;
using System.Web;
using System;

public class BaseController : Controller
{
    // Refactor the cache control and session check logic into a separate method
    public void PerformCacheAndSessionCheck(ActionExecutingContext filterContext)
    {
        // Set cache control headers
        HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
        HttpContext.Response.Cache.SetNoStore();

        // Check if either UserId or AdminId is present in the session
        if (Session["Uemail"] == null && Session["Aemail"] == null)
        {
            // Redirect to SignIn if no user or admin is authenticated
            filterContext.Result = new RedirectToRouteResult(
                new System.Web.Routing.RouteValueDictionary(
                    new { controller = "SignIn", action = "Index" }
                )
            );
        }
    }

    protected override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        PerformCacheAndSessionCheck(filterContext);
        base.OnActionExecuting(filterContext);
    }
}