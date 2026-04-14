using FINAL.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace FINAL.Controllers {


    [CustomAuthorize("Admin")]
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin/Logout
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            // Set cache control headers
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetNoStore();
            return RedirectToAction("Index", "SignIn");
        }

        public ActionResult ViewTemplate()
        {
            return View();
        }
    }
}