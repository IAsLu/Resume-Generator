using System;
using System.Web;
using System.Web.Mvc;

public class CustomAuthorizeAttribute : AuthorizeAttribute
{
    private readonly string[] _roles;

    public CustomAuthorizeAttribute(params string[] roles)
    {
        _roles = roles;
    }

    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
        var user = httpContext.Session["Role"] as string;
        return user != null && Array.Exists(_roles, role => role.Equals(user, StringComparison.OrdinalIgnoreCase));
    }

    protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
    {
        if (filterContext.HttpContext.Session["Role"] == null)
        {
            filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary
            {
                { "Controller", "SignIn" },
                { "Action", "Index" }
            });
        }
        else
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}
