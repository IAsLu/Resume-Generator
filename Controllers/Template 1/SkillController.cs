using System;
using System.Web.Mvc;
using FINAL.Models;
using FINAL.Repositories;

namespace USERRES.Controllers
{
    public class SkillController : Controller
    {
        private readonly SkillRepository _skillRepository;

        public SkillController()
        {
            _skillRepository = new SkillRepository();
        }

        public ActionResult Index()
        {
            try
            {
                var skills = _skillRepository.GetAllSkills();
                return View(skills);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex); // Uncomment and implement logging
                return View("Error", new HandleErrorInfo(ex, "Skill", "Index"));
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                var skill = _skillRepository.GetSkillById(id);
                if (skill == null)
                {
                    return HttpNotFound();
                }
                return View(skill);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                return View("Error", new HandleErrorInfo(ex, "Skill", "Details"));
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
                return View("Error", new HandleErrorInfo(ex, "Skill", "Create"));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Skill skill, int resumeId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    skill.ResumeId = resumeId; // Ensure the resumeId is set in the skill object
                    _skillRepository.CreateSkill(skill);
                    return RedirectToAction("Create", "Certification", new { resumeId = resumeId });
                }
                ViewBag.ResumeId = resumeId; // Pass the resumeId back to the view in case of an error
                return View(skill);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                ViewBag.ResumeId = resumeId;
                return View("Error", new HandleErrorInfo(ex, "Skill", "Create"));
            }
        }

        public ActionResult Edit(int resumeId)
        {
            try
            {
                var skill = _skillRepository.GetSkillById(resumeId);
                if (skill == null)
                {
                    return HttpNotFound();
                }

                ViewData["ResumeId"] = resumeId;
                return View(skill);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                return View("Error", new HandleErrorInfo(ex, "Skill", "Edit"));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Skill skill, int resumeId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _skillRepository.UpdateSkill(skill);
                    return RedirectToAction("Edit", "Certification", new { resumeId = resumeId });
                }

                ViewData["ResumeId"] = resumeId;
                return View(skill);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                ViewData["ResumeId"] = resumeId;
                return View("Error", new HandleErrorInfo(ex, "Skill", "Edit"));
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var skill = _skillRepository.GetSkillById(id);
                if (skill == null)
                {
                    return HttpNotFound();
                }
                return View(skill);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                return View("Error", new HandleErrorInfo(ex, "Skill", "Delete"));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _skillRepository.DeleteSkill(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log the exception and return an error view
                // LogException(ex);
                return View("Error", new HandleErrorInfo(ex, "Skill", "DeleteConfirmed"));
            }
        }
    }
}
