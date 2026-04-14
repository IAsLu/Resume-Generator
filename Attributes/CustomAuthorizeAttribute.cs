using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FINAL.Attributes
{
    public class CustomAuthorizeAttribute: AuthorizeAttribute
    {
        private readonly string _role;

        public CustomAuthorizeAttribute(string role)
        {
            _role = role;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var sessionRole = httpContext.Session["Role"] as string;
            return !string.IsNullOrEmpty(sessionRole) && sessionRole.Equals(_role, StringComparison.OrdinalIgnoreCase);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["Role"] == null)
            {
                filterContext.Result = new RedirectResult("/Account/SignIn");
            }
            else
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}