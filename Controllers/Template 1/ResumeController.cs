using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Web.Mvc;
using FINAL.Repositories;
using FINAL.Models;
using FINAL.Attributes;

namespace FINAL.Controllers
{
    public class ResumeController : Controller
    {
        private readonly ResumeRepository _resumeRepository;

        public ResumeController()
        {
            _resumeRepository = new ResumeRepository();
        }

        // Begin

        public ActionResult Final(int resumeId)
        {
            var viewModel = new ResumeDetailsViewModel();

            try
            {
                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    connection.Open();

                    // Fetch Resume
                    using (var command = new SqlCommand("GetResume", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Id", resumeId);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                viewModel.Resume = new Resume
                                {
                                    Id = (int)reader["Id"],
                                    Name = reader["Name"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    Phone = reader["Phone"].ToString(),
                                    LinkedIn = reader["LinkedIn"].ToString(),
                                    GitHub = reader["GitHub"].ToString(),
                                    ProfessionalSummary = reader["ProfessionalSummary"].ToString()
                                };
                            }
                        }
                    }

                    // Fetch Work Experience
                    using (var command = new SqlCommand("GetWorkExperience", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ResumeId", resumeId);
                        using (var reader = command.ExecuteReader())
                        {
                            viewModel.WorkExperiences = new List<WorkExperience>();
                            while (reader.Read())
                            {
                                viewModel.WorkExperiences.Add(new WorkExperience
                                {
                                    Id = (int)reader["Id"],
                                    ResumeId = (int)reader["ResumeId"],
                                    JobTitle = reader["JobTitle"].ToString(),
                                    Company = reader["Company"].ToString(),
                                    Location = reader["Location"].ToString(),
                                    Years = reader["Years"].ToString()
                                });
                            }
                        }
                    }

                    // Fetch Education
                    using (var command = new SqlCommand("GetEducation", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ResumeId", resumeId);
                        using (var reader = command.ExecuteReader())
                        {
                            viewModel.Educations = new List<Education>();
                            while (reader.Read())
                            {
                                viewModel.Educations.Add(new Education
                                {
                                    Id = (int)reader["Id"],
                                    ResumeId = (int)reader["ResumeId"],
                                    Degree = reader["Degree"].ToString(),
                                    Institution = reader["Institution"].ToString(),
                                    GraduationYear = reader["GraduationYear"].ToString()
                                });
                            }
                        }
                    }

                    // Fetch Skills
                    using (var command = new SqlCommand("GetSkills", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ResumeId", resumeId);
                        using (var reader = command.ExecuteReader())
                        {
                            viewModel.Skills = new List<Skill>();
                            while (reader.Read())
                            {
                                viewModel.Skills.Add(new Skill
                                {
                                    Id = (int)reader["Id"],
                                    ResumeId = (int)reader["ResumeId"],
                                    SkillName = reader["SkillName"].ToString()
                                });
                            }
                        }
                    }

                    // Fetch Certifications
                    using (var command = new SqlCommand("GetCertifications", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ResumeId", resumeId);
                        using (var reader = command.ExecuteReader())
                        {
                            viewModel.Certifications = new List<Certification>();
                            while (reader.Read())
                            {
                                viewModel.Certifications.Add(new Certification
                                {
                                    Id = (int)reader["Id"],
                                    ResumeId = (int)reader["ResumeId"],
                                    CertificationName = reader["CertificationName"].ToString(),
                                    IssuedBy = reader["IssuedBy"].ToString(),
                                    Year = reader["Year"].ToString()
                                });
                            }
                        }
                    }

                    // Fetch Projects
                    using (var command = new SqlCommand("GetProjects", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ResumeId", resumeId);
                        using (var reader = command.ExecuteReader())
                        {
                            viewModel.Projects = new List<Project>();
                            while (reader.Read())
                            {
                                viewModel.Projects.Add(new Project
                                {
                                    Id = (int)reader["Id"],
                                    ResumeId = (int)reader["ResumeId"],
                                    ProjectName = reader["ProjectName"].ToString(),
                                    ProjectDescription = reader["ProjectDescription"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception and handle it as needed
                // For example, you might want to log the exception and show a friendly error message
                // LogException(ex);
                return new HttpStatusCodeResult(500, "Internal server error. Please try again later.");
            }

            return View(viewModel);
        }

        public ActionResult Index()
        {
            try
            {
                var resumes = _resumeRepository.GetAllResumes();
                return View(resumes);
            }
            catch (Exception ex)
            {
                // Log the exception and handle it as needed
                // LogException(ex);
                return new HttpStatusCodeResult(500, "Internal server error. Please try again later.");
            }
        }

        public ActionResult Details(int id)
        {
            try
            {
                var resume = _resumeRepository.GetResumeById(id);
                if (resume == null)
                {
                    return HttpNotFound();
                }
                return View(resume);
            }
            catch (Exception ex)
            {
                // Log the exception and handle it as needed
                // LogException(ex);
                return new HttpStatusCodeResult(500, "Internal server error. Please try again later.");
            }
        }

        public ActionResult Det()
        {
            try
            {
                var resumes = _resumeRepository.GetAllResumes();
                return View(resumes);
            }
            catch (Exception ex)
            {
                // Log the exception and handle it as needed
                // LogException(ex);
                return new HttpStatusCodeResult(500, "Internal server error. Please try again later.");
            }
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Resume resume)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    int newResumeId = _resumeRepository.CreateResume(resume);
                    return RedirectToAction("Create", "WorkExperience", new { resumeId = newResumeId });
                }
                return View(resume);
            }
            catch (Exception ex)
            {
                // Log the exception and handle it as needed
                // LogException(ex);
                return new HttpStatusCodeResult(500, "Internal server error. Please try again later.");
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var resume = _resumeRepository.GetResumeById(id);
                if (resume == null)
                {
                    return HttpNotFound();
                }
                return View(resume);
            }
            catch (Exception ex)
            {
                // Log the exception and handle it as needed
                // LogException(ex);
                return new HttpStatusCodeResult(500, "Internal server error. Please try again later.");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Resume resume)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _resumeRepository.UpdateResume(resume);
                    return RedirectToAction("Edit", "WorkExperience", new { id = resume.Id, resumeId = resume.Id });
                }
                return View(resume);
            }
            catch (Exception ex)
            {
                // Log the exception and handle it as needed
                // LogException(ex);
                return new HttpStatusCodeResult(500, "Internal server error. Please try again later.");
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                var resume = _resumeRepository.GetResumeById(id);
                if (resume == null)
                {
                    return HttpNotFound();
                }
                return View(resume);
            }
            catch (Exception ex)
            {
                // Log the exception and handle it as needed
                // LogException(ex);
                return new HttpStatusCodeResult(500, "Internal server error. Please try again later.");
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _resumeRepository.DeleteResume(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log the exception and handle it as needed
                // LogException(ex);
                return new HttpStatusCodeResult(500, "Internal server error. Please try again later.");
            }
        }
    }
}
