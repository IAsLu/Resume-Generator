using System;
using System.Web.Mvc;
using FINAL.Models;
using FINAL.Repositories;

namespace FINAL.Controllers
{
    public class WorkExperienceController : Controller
    {
        private readonly WorkExperienceRepository _workExperienceRepository;

        public WorkExperienceController()
        {
            _workExperienceRepository = new WorkExperienceRepository();
        }

        public ActionResult Index()
        {
            try
            {
                var workExperiences = _workExperienceRepository.GetAllWorkExperiences();
                return View(workExperiences);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex); // Uncomment and implement logging
                return View("Error", new HandleErrorInfo(ex, "WorkExperience", "Index"));
            }
        }

        public ActionResult Details(int resumeId)
        {
            try
            {
                var workExperience = _workExperienceRepository.GetWorkExperienceByResumeId(resumeId);
                if (workExperience == null)
                {
                    return HttpNotFound();
                }
                ViewData["ResumeId"] = resumeId;
                return View(workExperience);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                return View("Error", new HandleErrorInfo(ex, "WorkExperience", "Details"));
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
                return View("Error", new HandleErrorInfo(ex, "WorkExperience", "Create"));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WorkExperience workExperience, int resumeId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    workExperience.ResumeId = resumeId;
                    _workExperienceRepository.CreateWorkExperience(workExperience);
                    return RedirectToAction("Create", "Education", new { resumeId = resumeId });
                }
                ViewBag.ResumeId = resumeId;
                return View(workExperience);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                ViewBag.ResumeId = resumeId;
                return View("Error", new HandleErrorInfo(ex, "WorkExperience", "Create"));
            }
        }

        public ActionResult Edit(int resumeId)
        {
            try
            {
                var workExperience = _workExperienceRepository.GetWorkExperienceByResumeId(resumeId);
                if (workExperience == null)
                {
                    return HttpNotFound();
                }
                ViewData["ResumeId"] = resumeId;
                return View(workExperience);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                return View("Error", new HandleErrorInfo(ex, "WorkExperience", "Edit"));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WorkExperience workExperience, int resumeId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _workExperienceRepository.UpdateWorkExperience(workExperience);
                    return RedirectToAction("Edit", "Education", new { resumeId = resumeId });
                }
                ViewData["ResumeId"] = resumeId;
                return View(workExperience);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                ViewData["ResumeId"] = resumeId;
                return View("Error", new HandleErrorInfo(ex, "WorkExperience", "Edit"));
            }
        }

        public ActionResult Delete(int resumeId)
        {
            try
            {
                var workExperience = _workExperienceRepository.GetWorkExperienceByResumeId(resumeId);
                if (workExperience == null)
                {
                    return HttpNotFound();
                }
                ViewData["ResumeId"] = resumeId;
                return View(workExperience);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                return View("Error", new HandleErrorInfo(ex, "WorkExperience", "Delete"));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int resumeId)
        {
            try
            {
                var workExperience = _workExperienceRepository.GetWorkExperienceByResumeId(resumeId);
                if (workExperience == null)
                {
                    return HttpNotFound();
                }
                _workExperienceRepository.DeleteWorkExperience(workExperience.Id);
                return RedirectToAction("Index", new { resumeId = resumeId });
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                return View("Error", new HandleErrorInfo(ex, "WorkExperience", "DeleteConfirmed"));
            }
        }
    }
}
