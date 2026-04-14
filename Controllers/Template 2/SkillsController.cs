using FINAL.Models.Template_2;
using FINAL.Repositories.Template_2;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

public class SkillsController : Controller
{
    private readonly SkillsRepository _skillRepository;

    public SkillsController()
    {
        _skillRepository = new SkillsRepository("YourConnectionStringName");
    }

    // GET: Skill
    public ActionResult Index()
    {
        try
        {
            var skills = _skillRepository.GetAllSkills();
            return View(skills);
        }
        catch (Exception ex)
        {
            // Log the exception (optional)
            return new HttpStatusCodeResult(500, "An error occurred while fetching skills: " + ex.Message);
        }
    }

    // GET: Skill/Create
    public ActionResult Create(int resumeId)
    {
        try
        {
            ViewBag.ResumeId = resumeId;
            return View();
        }
        catch (Exception ex)
        {
            // Log the exception (optional)
            return new HttpStatusCodeResult(500, "An error occurred while preparing the Create view: " + ex.Message);
        }
    }

    // POST: Skill/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Skills skill, int resumeId)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _skillRepository.Insert(skill);
                return RedirectToAction("Create", "Certifications", new { resumeId = skill.ResumeId });
            }
            return View(skill);
        }
        catch (Exception ex)
        {
            // Log the exception (optional)
            return new HttpStatusCodeResult(500, "An error occurred while creating the skill: " + ex.Message);
        }
    }

    // GET: Skill/Edit/5
    public ActionResult Edit(int resumeId)
    {
        try
        {
            var skill = _skillRepository.GetById(resumeId); // Adjust based on your method
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }
        catch (Exception ex)
        {
            // Log the exception (optional)
            return new HttpStatusCodeResult(500, "An error occurred while fetching the skill for editing: " + ex.Message);
        }
    }

    // POST: Skill/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Skills skill)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _skillRepository.Update(skill);
                return RedirectToAction("Edit", "Certifications", new { resumeId = skill.ResumeId });
            }
            return View(skill);
        }
        catch (Exception ex)
        {
            // Log the exception (optional)
            return new HttpStatusCodeResult(500, "An error occurred while updating the skill: " + ex.Message);
        }
    }

    // GET: Skill/Delete/5
    public ActionResult Delete(int id)
    {
        try
        {
            var skill = _skillRepository.GetById(id); // Adjust based on your method
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }
        catch (Exception ex)
        {
            // Log the exception (optional)
            return new HttpStatusCodeResult(500, "An error occurred while fetching the skill for deletion: " + ex.Message);
        }
    }

    // POST: Skill/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        try
        {
            _skillRepository.Delete(id);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            // Log the exception (optional)
            return new HttpStatusCodeResult(500, "An error occurred while deleting the skill: " + ex.Message);
        }
    }

    // GET: Skill/Details/5
    public ActionResult Details(int id, int resumeId)
    {
        try
        {
            var skill = _skillRepository.GetById(id);
            if (skill == null)
            {
                return HttpNotFound();
            }

            ViewBag.ResumeId = resumeId; // Pass resumeId to the view
            return View(skill);
        }
        catch (Exception ex)
        {
            // Log the exception (optional)
            return new HttpStatusCodeResult(500, "An error occurred while fetching skill details: " + ex.Message);
        }
    }
}
