using FINAL.Models.Template_2;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FINAL.Repositories.Template_2;

public class ProjectsController : Controller
{
    private readonly ProjectsRepository _projectRepository;

    public ProjectsController()
    {
        _projectRepository = new ProjectsRepository("YourConnectionStringName");
    }

    // GET: Project
    public ActionResult Index(int resumeId)
    {
        try
        {
            var projects = _projectRepository.GetByResumeId(resumeId);
            return View(projects);
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while retrieving the projects.";
            return View("Error");
        }
    }

    // GET: Project/Create
    public ActionResult Create(int resumeId)
    {
        try
        {
            ViewBag.ResumeId = resumeId;
            return View();
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while preparing to create a project.";
            return View("Error");
        }
    }

    // POST: Project/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Projects project)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _projectRepository.Insert(project);
                return RedirectToAction("Create", "Languages", new { resumeId = project.ResumeId });
            }

            return View(project);
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while creating the project.";
            return View("Error");
        }
    }

    // GET: Project/Edit/5
    public ActionResult Edit(int resumeId)
    {
        try
        {
            var project = _projectRepository.GetByResumeId(resumeId); // Adjust based on your method
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while retrieving the project for editing.";
            return View("Error");
        }
    }

    // POST: Project/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Projects project)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _projectRepository.Update(project);
                return RedirectToAction("Edit", "Languages", new { resumeId = project.ResumeId });
            }

            return View(project);
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while editing the project.";
            return View("Error");
        }
    }

    // GET: Project/Delete/5
    public ActionResult Delete(int id)
    {
        try
        {
            var project = _projectRepository.GetByResumeId(id); // Adjust based on your method
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while retrieving the project for deletion.";
            return View("Error");
        }
    }

    // POST: Project/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        try
        {
            _projectRepository.Delete(id);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while deleting the project.";
            return View("Error");
        }
    }

    // GET: Project/Details/5
    public ActionResult Details(int id, int resumeId)
    {
        try
        {
            var project = _projectRepository.GetByResumeId(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            ViewBag.ResumeId = resumeId; // Pass resumeId to the view
            return View(project);
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while retrieving the project details.";
            return View("Error");
        }
    }
}
