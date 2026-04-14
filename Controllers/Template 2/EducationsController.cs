using FINAL.Models.Template_2;
using FINAL.Repositories.Template_2;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

public class EducationsController : Controller
{
    private readonly EducationsRepository _educationRepository;

    public EducationsController()
    {
        _educationRepository = new EducationsRepository("YourConnectionStringName");
    }

    // GET: Education
    public ActionResult Index(int resumeId)
    {
        try
        {
            var educations = _educationRepository.GetByResumeId(resumeId);
            return View(educations);
        }
        catch (Exception ex)
        {
            // Log the exception (you can use a logging framework)
            ViewBag.ErrorMessage = "An error occurred while retrieving the education records.";
            return View("Error");
        }
    }

    // GET: Education/Create
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
            ViewBag.ErrorMessage = "An error occurred while preparing to create an education record.";
            return View("Error");
        }
    }

    // POST: Education/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Educations education)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _educationRepository.Insert(education);
                return RedirectToAction("Create", "Skills", new { resumeId = education.ResumeId });
            }
            return View(education);
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while creating the education record.";
            return View("Error");
        }
    }

    // GET: Education/Edit/5
    public ActionResult Edit(int resumeId)
    {
        try
        {
            var education = _educationRepository.GetByResumeId(resumeId); // Adjust based on your method
            if (education == null)
            {
                return HttpNotFound();
            }
            return View(education);
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while retrieving the education record for editing.";
            return View("Error");
        }
    }

    // POST: Education/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Educations education)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _educationRepository.Update(education);
                return RedirectToAction("Edit", "Skills", new { resumeId = education.ResumeId });
            }
            return View(education);
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while editing the education record.";
            return View("Error");
        }
    }

    // GET: Education/Delete/5
    public ActionResult Delete(int id)
    {
        try
        {
            var education = _educationRepository.GetByResumeId(id); // Adjust based on your method
            if (education == null)
            {
                return HttpNotFound();
            }
            return View(education);
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while retrieving the education record for deletion.";
            return View("Error");
        }
    }

    // POST: Education/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        try
        {
            _educationRepository.Delete(id);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while deleting the education record.";
            return View("Error");
        }
    }

    // GET: Education/Details/5
    public ActionResult Details(int id, int resumeId)
    {
        try
        {
            var education = _educationRepository.GetByResumeId(id);
            if (education == null)
            {
                return HttpNotFound();
            }

            ViewBag.ResumeId = resumeId; // Pass resumeId to the view
            return View(education);
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while retrieving the education details.";
            return View("Error");
        }
    }
}
