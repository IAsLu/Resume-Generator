using FINAL.Repositories.Template_2;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using FINAL.Models.Template_2;

public class HobbyController : Controller
{
    private readonly HobbyRepository _hobbyRepository;

    public HobbyController()
    {
        _hobbyRepository = new HobbyRepository("YourConnectionStringName");
    }

    // GET: Hobby
    public ActionResult Index(int resumeId)
    {
        try
        {
            var hobby = _hobbyRepository.GetByResumeId(resumeId);
            return View(hobby);
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while retrieving hobbies.";
            return View("Error");
        }
    }

    // GET: Hobby/Create
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
            ViewBag.ErrorMessage = "An error occurred while preparing to create a hobby.";
            return View("Error");
        }
    }

    // POST: Hobby/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Hobbies hobby)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _hobbyRepository.Insert(hobby);
                return RedirectToAction("FinalResume", "Resumes", new { resumeId = hobby.ResumeId });
            }

            return View(hobby);
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while creating the hobby.";
            return View("Error");
        }
    }

    // GET: Hobby/Edit/5
    public ActionResult Edit(int resumeId)
    {
        try
        {
            var hobby = _hobbyRepository.GetByResumeId(resumeId); // Adjust based on your method
            if (hobby == null)
            {
                return HttpNotFound();
            }
            return View(hobby);
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while retrieving the hobby for editing.";
            return View("Error");
        }
    }

    // POST: Hobby/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Hobbies hobby)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _hobbyRepository.Update(hobby);
                return RedirectToAction("FinalResume","Resumes", new { resumeId = hobby.ResumeId });
            }

            return View(hobby);
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while editing the hobby.";
            return View("Error");
        }
    }

    // GET: Hobby/Delete/5
    public ActionResult Delete(int id)
    {
        try
        {
            var hobby = _hobbyRepository.GetByResumeId(id); // Adjust based on your method
            if (hobby == null)
            {
                return HttpNotFound();
            }
            return View(hobby);
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while retrieving the hobby for deletion.";
            return View("Error");
        }
    }

    // POST: Hobby/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        try
        {
            _hobbyRepository.Delete(id);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while deleting the hobby.";
            return View("Error");
        }
    }

    // GET: Hobby/Details/5
    public ActionResult Details(int id, int resumeId)
    {
        try
        {
            var hobby = _hobbyRepository.GetByResumeId(id);
            if (hobby == null)
            {
                return HttpNotFound();
            }

            ViewBag.ResumeId = resumeId; // Pass resumeId to the view
            return View(hobby);
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while retrieving the hobby details.";
            return View("Error");
        }
    }
}
