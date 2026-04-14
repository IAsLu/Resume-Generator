using FINAL.Models.Template_2;
using FINAL.Repositories;
using FINAL.Repositories.Template_2;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

public class CertificationsController : Controller
{
    private readonly CertificationsRepository _certificationRepository;

    public CertificationsController()
    {
        _certificationRepository = new CertificationsRepository("YourConnectionStringName");
    }

    // GET: Certification
    public ActionResult Index()
    {
        try
        {
            var certifications = _certificationRepository.GetAllCertifications();
            return View(certifications);
        }
        catch (Exception ex)
        {
            // Log the exception (you can use a logging framework)
            ViewBag.ErrorMessage = "An error occurred while retrieving the certifications.";
            return View("Error");
        }
    }

    // GET: Certification/Create
    public ActionResult Create(int resumeId)
    {
        try
        {
            ViewBag.ResumeId = resumeId; // Pass resumeId to the view
            return View();
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while preparing to create a certification.";
            return View("Error");
        }
    }

    // POST: Certification/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Certifications certification)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _certificationRepository.Insert(certification);
                return RedirectToAction("Create", "Projects", new { resumeId = certification.ResumeId });
            }
            // Reinitialize ResumeId if returning the view
            ViewBag.ResumeId = certification.ResumeId;
            return View(certification);
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while creating the certification.";
            return View("Error");
        }
    }

    // GET: Certification/Edit/5
    public ActionResult Edit(int resumeId)
    {
        try
        {
            var certification = _certificationRepository.GetById(resumeId); // Adjust based on your method
            if (certification == null)
            {
                return HttpNotFound();
            }
            return View(certification);
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while retrieving the certification for editing.";
            return View("Error");
        }
    }

    // POST: Certification/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Certifications certification)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _certificationRepository.Update(certification);
                return RedirectToAction("Edit", "Projects", new { resumeId = certification.ResumeId });
            }
            return View(certification);
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while editing the certification.";
            return View("Error");
        }
    }

    // GET: Certification/Delete/5
    public ActionResult Delete(int id)
    {
        try
        {
            var certification = _certificationRepository.GetById(id); // Adjust based on your method
            if (certification == null)
            {
                return HttpNotFound();
            }
            return View(certification);
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while retrieving the certification for deletion.";
            return View("Error");
        }
    }

    // POST: Certification/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        try
        {
            _certificationRepository.Delete(id);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while deleting the certification.";
            return View("Error");
        }
    }

    // GET: Certification/Details/5
    public ActionResult Details(int id)
    {
        try
        {
            var certification = _certificationRepository.GetById(id); // Adjust based on your method
            if (certification == null)
            {
                return HttpNotFound();
            }
            return View(certification);
        }
        catch (Exception ex)
        {
            // Log the exception
            ViewBag.ErrorMessage = "An error occurred while retrieving the certification details.";
            return View("Error");
        }
    }
}
