using System;
using System.Web.Mvc;
using FINAL.Models;
using FINAL.Repositories;

namespace FINAL.Controllers
{
    public class EducationController : Controller
    {
        private readonly EducationRepository _educationRepository;

        public EducationController()
        {
            _educationRepository = new EducationRepository();
        }

        public ActionResult Index()
        {
            try
            {
                var educations = _educationRepository.GetAllEducations();
                return View(educations);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex); // Uncomment and implement logging
                return View("Error", new HandleErrorInfo(ex, "Education", "Index"));
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                var education = _educationRepository.GetEducationById(id);
                if (education == null)
                {
                    return HttpNotFound();
                }
                return View(education);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                return View("Error", new HandleErrorInfo(ex, "Education", "Details"));
            }
        }

        public ActionResult Create(int resumeId)
        {
            try
            {
                ViewBag.ResumeId = resumeId;
                return View();
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                return View("Error", new HandleErrorInfo(ex, "Education", "Create"));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Education education, int resumeId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    education.ResumeId = resumeId;
                    _educationRepository.CreateEducation(education);
                    return RedirectToAction("Create", "Skill", new { resumeId = resumeId });
                }
                ViewBag.ResumeId = resumeId;
                return View(education);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                ViewBag.ResumeId = resumeId;
                return View("Error", new HandleErrorInfo(ex, "Education", "Create"));
            }
        }

        public ActionResult Edit(int resumeId)
        {
            try
            {
                var education = _educationRepository.GetEducationById(resumeId);
                if (education == null)
                {
                    return HttpNotFound();
                }
                ViewData["ResumeId"] = resumeId;
                return View(education);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                return View("Error", new HandleErrorInfo(ex, "Education", "Edit"));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Education education, int resumeId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _educationRepository.UpdateEducation(education);
                    return RedirectToAction("Edit", "Skill", new { resumeId = resumeId });
                }
                ViewData["ResumeId"] = resumeId;
                return View(education);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                ViewData["ResumeId"] = resumeId;
                return View("Error", new HandleErrorInfo(ex, "Education", "Edit"));
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var education = _educationRepository.GetEducationById(id);
                if (education == null)
                {
                    return HttpNotFound();
                }
                return View(education);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                return View("Error", new HandleErrorInfo(ex, "Education", "Delete"));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _educationRepository.DeleteEducation(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                return View("Error", new HandleErrorInfo(ex, "Education", "DeleteConfirmed"));
            }
        }
    }
}
