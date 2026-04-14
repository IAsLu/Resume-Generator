
using FINAL.Attributes;
using FINAL.Controllers;
using FINAL.Models;
using FINAL.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RESUMEGENERATOR.Controllers
{
    public class SignInController :Controller
    {
       
        private readonly SignInRepository repository = new SignInRepository();

        // GET: SignIn
        public ActionResult Index()
        {
            return View();
        }

        // POST: SignIn
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(UserRegistration objUser)
        {
            UserRegistrationRepository userRepo = new UserRegistrationRepository();

            // Retrieve the user by email
            var user = userRepo.GetUserByEmail(objUser.Email);

            // Check if the user exists and the password matches
            if (user != null && user.Password == objUser.Password && objUser.Password != null)
            {
                // Redirect based on the user's role
                switch (user.Role)
                {
                    case "Admin":
                        Session["Name"] = user.FirstName;

                        Session["Aemail"] = user.Email; // Set role in session

                        Session["Role"] = "Admin"; // Set role in session
                        return RedirectToAction("Index", "Admin");
                    case "User":
                        Session["Name"] = user.FirstName;

                        Session["Uemail"] = user.Email; // Set role in session

                        Session["Role"] = "User"; // Set role in session
                        return RedirectToAction("temp", "Logout");
                    default:
                        return RedirectToAction("Index", "SignIn");
                }
            }

            // If authentication fails, add an error message
            ModelState.AddModelError("", "Invalid email or password");
            return View(objUser);
        }
    }
}
