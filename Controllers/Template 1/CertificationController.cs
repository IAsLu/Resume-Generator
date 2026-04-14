using System;
using System.Web.Mvc;
using FINAL.Models;
using FINAL.Repositories;

namespace FINAL.Controllers
{
    public class CertificationController : Controller
    {
        private readonly CertificationRepository _certificationRepository;

        public CertificationController()
        {
            _certificationRepository = new CertificationRepository();
        }

        public ActionResult Index()
        {
            try
            {
                var certifications = _certificationRepository.GetAllCertifications();
                return View(certifications);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex); // Uncomment and implement logging
                return View("Error", new HandleErrorInfo(ex, "Certification", "Index"));
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                var certification = _certificationRepository.GetCertificationById(id);
                if (certification == null)
                {
                    return HttpNotFound();
                }
                return View(certification);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                return View("Error", new HandleErrorInfo(ex, "Certification", "Details"));
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
                return View("Error", new HandleErrorInfo(ex, "Certification", "Create"));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Certification certification, int resumeId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    certification.ResumeId = resumeId; // Ensure the resumeId is set in the certification object
                    _certificationRepository.CreateCertification(certification);
                    return RedirectToAction("Create", "Project", new { resumeId = resumeId });
                }
                ViewBag.ResumeId = resumeId; // Pass the resumeId back to the view in case of an error
                return View(certification);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                ViewBag.ResumeId = resumeId;
                return View("Error", new HandleErrorInfo(ex, "Certification", "Create"));
            }
        }

        public ActionResult Edit(int resumeId)
        {
            try
            {
                var certification = _certificationRepository.GetCertificationById(resumeId);
                if (certification == null)
                {
                    return HttpNotFound();
                }

                ViewData["ResumeId"] = resumeId;
                return View(certification);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                return View("Error", new HandleErrorInfo(ex, "Certification", "Edit"));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Certification certification, int resumeId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _certificationRepository.UpdateCertification(certification);
                    return RedirectToAction("Edit", "Project", new { resumeId = resumeId });
                }

                ViewData["ResumeId"] = resumeId;
                return View(certification);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                ViewData["ResumeId"] = resumeId;
                return View("Error", new HandleErrorInfo(ex, "Certification", "Edit"));
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var certification = _certificationRepository.GetCertificationById(id);
                if (certification == null)
                {
                    return HttpNotFound();
                }
                return View(certification);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                return View("Error", new HandleErrorInfo(ex, "Certification", "Delete"));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _certificationRepository.DeleteCertification(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                return View("Error", new HandleErrorInfo(ex, "Certification", "DeleteConfirmed"));
            }
        }
    }
}
