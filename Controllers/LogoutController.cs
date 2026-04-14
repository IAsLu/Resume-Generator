using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FINAL.Controllers
{
    public class LogoutController : BaseController
    {

        [CustomAuthorize("User")]
        public ActionResult temp()
        {
            ViewBag.Message = "Your temp page.";

            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Index", "SignIn");
        }
    }
}
