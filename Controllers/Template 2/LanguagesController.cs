using FINAL.Models.Template_2;
using FINAL.Repositories.Template_2;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

public class LanguagesController : Controller
{
    private readonly LanguageRepository _languageRepository;

    public LanguagesController()
    {
        _languageRepository = new LanguageRepository("YourConnectionStringName");
    }

    // GET: Language
    public ActionResult Index(int resumeId)
    {
        try
        {
            var languages = _languageRepository.GetByResumeId(resumeId);
            return View(languages);
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while retrieving the languages.";
            return View("Error");
        }
    }

    // GET: Language/Create
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
            ViewBag.ErrorMessage = "An error occurred while preparing to create a language.";
            return View("Error");
        }
    }

    // POST: Language/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Languages language)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _languageRepository.Insert(language);
                return RedirectToAction("Create", "Hobby", new { resumeId = language.ResumeId });
            }

            return View(language);
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while creating the language.";
            return View("Error");
        }
    }

    // GET: Language/Edit/5
    public ActionResult Edit(int resumeId)
    {
        try
        {
            var language = _languageRepository.GetByResumeId(resumeId); // Adjust based on your method
            if (language == null)
            {
                return HttpNotFound();
            }
            return View(language);
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while retrieving the language for editing.";
            return View("Error");
        }
    }

    // POST: Language/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Languages language)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _languageRepository.Update(language);
                return RedirectToAction("Edit", "Hobby", new { resumeId = language.ResumeId });
            }

            return View(language);
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while editing the language.";
            return View("Error");
        }
    }

    // GET: Language/Delete/5
    public ActionResult Delete(int id)
    {
        try
        {
            var language = _languageRepository.GetByResumeId(id); // Adjust based on your method
            if (language == null)
            {
                return HttpNotFound();
            }
            return View(language);
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while retrieving the language for deletion.";
            return View("Error");
        }
    }

    // POST: Language/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        try
        {
            _languageRepository.Delete(id);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while deleting the language.";
            return View("Error");
        }
    }

    // GET: Language/Details/5
    public ActionResult Details(int id, int resumeId)
    {
        try
        {
            var language = _languageRepository.GetByResumeId(id);
            if (language == null)
            {
                return HttpNotFound();
            }

            ViewBag.ResumeId = resumeId; // Pass resumeId to the view
            return View(language);
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while retrieving the language details.";
            return View("Error");
        }
    }
}
