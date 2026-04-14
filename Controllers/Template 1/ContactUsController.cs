using System.Web.Mvc;
using FINAL.Models;
using FINAL.Repositories;

namespace FINAL.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly ContactUsRepository _repository;

        public ContactUsController()
        {

            _repository = new ContactUsRepository();
        }

        // GET: ContactUs
        public ActionResult Contact()
        {
            return View();
        }

        // POST: ContactUs
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactUsViewModel model)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveContactUsMessage(model);
                return RedirectToAction("ContactSuccess");
            }
            return View(model);
        }

        public ActionResult ContactSuccess()
        {
            return View();
        }
    }
}
