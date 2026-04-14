using FINAL.Models.Template_2;
using FINAL.Repositories.Template_2;
using System;
using System.Web.Mvc;

namespace FINAL.Controllers
{
    public class WorkExperiencesController : Controller
    {
        private readonly WorkExperiencesRepository _workExperienceRepository;

        public WorkExperiencesController()
        {
            _workExperienceRepository = new WorkExperiencesRepository("DefaultConnection");
        }

        // GET: WorkExperiences/Index
        public ActionResult Index(int resumeId)
        {
            try
            {
                var workExperiences = _workExperienceRepository.GetByResumeId(resumeId);
                ViewBag.ResumeId = resumeId; // Pass resumeId to the view
                return View(workExperiences);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return new HttpStatusCodeResult(500, "An error occurred while fetching work experiences: " + ex.Message);
            }
        }

        // GET: WorkExperiences/Create
        public ActionResult Create(int resumeId)
        {
            try
            {
                ViewBag.ResumeId = resumeId; // Pass resumeId to the view
                return View();
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return new HttpStatusCodeResult(500, "An error occurred while preparing the Create view: " + ex.Message);
            }
        }

        // POST: WorkExperiences/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WorkExperiences workExperiences)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _workExperienceRepository.Insert(workExperiences);
                    return RedirectToAction("Create", "Educations", new { resumeId = workExperiences.ResumeId });
                }

                ViewBag.ResumeId = workExperiences.ResumeId;
                return View(workExperiences);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return new HttpStatusCodeResult(500, "An error occurred while creating the work experience: " + ex.Message);
            }
        }

        // GET: WorkExperiences/Edit/5
        public ActionResult Edit(int resumeId)
        {
            try
            {
                var workExperience = _workExperienceRepository.GetById(resumeId);
                if (workExperience == null)
                {
                    return HttpNotFound();
                }

                ViewBag.ResumeId = resumeId; // Pass resumeId to the view
                return View(workExperience);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return new HttpStatusCodeResult(500, "An error occurred while fetching the work experience for editing: " + ex.Message);
            }
        }

        // POST: WorkExperiences/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(WorkExperiences workExperience)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _workExperienceRepository.Update(workExperience);
                    return RedirectToAction("Edit", "Educations", new { resumeId = workExperience.ResumeId });
                }

                ViewBag.ResumeId = workExperience.ResumeId; // Ensure resumeId is passed back to the view
                return View(workExperience);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return new HttpStatusCodeResult(500, "An error occurred while updating the work experience: " + ex.Message);
            }
        }

        // GET: WorkExperiences/Delete/5
        public ActionResult Delete(int resumeId)
        {
            try
            {
                var workExperience = _workExperienceRepository.GetById(resumeId);
                if (workExperience == null)
                {
                    return HttpNotFound();
                }

                ViewBag.ResumeId = resumeId; // Pass resumeId to the view
                return View(workExperience);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return new HttpStatusCodeResult(500, "An error occurred while fetching the work experience for deletion: " + ex.Message);
            }
        }

        // POST: WorkExperiences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int resumeId)
        {
            try
            {
                var workExperience = _workExperienceRepository.GetById(resumeId);
                if (workExperience != null)
                {
                    _workExperienceRepository.Delete(resumeId);
                }

                return RedirectToAction("Index", new { resumeId = resumeId });
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return new HttpStatusCodeResult(500, "An error occurred while deleting the work experience: " + ex.Message);
            }
        }

        // GET: WorkExperiences/Details/5
        public ActionResult Details(int id, int resumeId)
        {
            try
            {
                var workExperience = _workExperienceRepository.GetById(id);
                if (workExperience == null)
                {
                    return HttpNotFound();
                }

                ViewBag.ResumeId = resumeId; // Pass resumeId to the view
                return View(workExperience);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return new HttpStatusCodeResult(500, "An error occurred while fetching work experience details: " + ex.Message);
            }
        }
    }
}
