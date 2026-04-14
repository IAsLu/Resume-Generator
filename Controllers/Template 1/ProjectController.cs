using System;
using System.Web.Mvc;
using FINAL.Models;
using FINAL.Repositories;

namespace FINAL.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ProjectRepository _projectRepository;

        public ProjectController()
        {
            _projectRepository = new ProjectRepository();
        }

        public ActionResult Index()
        {
            try
            {
                var projects = _projectRepository.GetAllProjects();
                return View(projects);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex); // Uncomment and implement logging
                return View("Error", new HandleErrorInfo(ex, "Project", "Index"));
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                var project = _projectRepository.GetProjectById(id);
                if (project == null)
                {
                    return HttpNotFound();
                }
                return View(project);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                return View("Error", new HandleErrorInfo(ex, "Project", "Details"));
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
                return View("Error", new HandleErrorInfo(ex, "Project", "Create"));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project project, int resumeId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    project.ResumeId = resumeId; // Ensure the resumeId is set in the project object
                    _projectRepository.CreateProject(project);
                    return RedirectToAction("Final", "Resume", new { resumeId = resumeId });
                }
                ViewBag.ResumeId = resumeId; // Pass the resumeId back to the view in case of an error
                return View(project);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                ViewBag.ResumeId = resumeId;
                return View("Error", new HandleErrorInfo(ex, "Project", "Create"));
            }
        }

        public ActionResult Edit(int resumeId)
        {
            try
            {
                var project = _projectRepository.GetProjectById(resumeId);
                if (project == null)
                {
                    return HttpNotFound();
                }

                ViewData["ResumeId"] = resumeId;
                return View(project);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                return View("Error", new HandleErrorInfo(ex, "Project", "Edit"));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Project project, int resumeId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _projectRepository.UpdateProject(project);
                    return RedirectToAction("Final", "Resume", new { resumeId = resumeId });
                }

                ViewData["ResumeId"] = resumeId;
                return View(project);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                ViewData["ResumeId"] = resumeId;
                return View("Error", new HandleErrorInfo(ex, "Project", "Edit"));
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var project = _projectRepository.GetProjectById(id);
                if (project == null)
                {
                    return HttpNotFound();
                }
                return View(project);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                return View("Error", new HandleErrorInfo(ex, "Project", "Delete"));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _projectRepository.DeleteProject(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                return View("Error", new HandleErrorInfo(ex, "Project", "DeleteConfirmed"));
            }
        }
    }
}
