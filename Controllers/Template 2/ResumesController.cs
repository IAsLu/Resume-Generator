using FINAL.Models.Template_2;
using FINAL.Repositories.Template_2;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

public class ResumesController : Controller
{
    private readonly ResumesRepository _resumeRepository;

    public ResumesController()
    {
        _resumeRepository = new ResumesRepository("YourConnectionStringName");
    }

    public ActionResult FinalResume(int resumeId)
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            var viewModel = new FinalResumeViewModel
            {
                Resume = null,
                WorkExperiences = new List<WorkExperiences>(),
                Educations = new List<Educations>(),
                Skills = new List<Skills>(),
                Certifications = new List<Certifications>(),
                Projects = new List<Projects>(),
                Languages = new List<Languages>(),
                Hobbies = new List<Hobbies>()
            };

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Fetch Resume data
                using (SqlCommand cmd = new SqlCommand("GetResumeById2", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ResumeId", resumeId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            viewModel.Resume = new Resumes
                            {
                                ResumeId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Email = reader.GetString(2),
                                Phone = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Address = reader.IsDBNull(4) ? null : reader.GetString(4),
                                ProfessionalSummary = reader.IsDBNull(5) ? null : reader.GetString(5)
                            };
                        }
                    }
                }

                // Fetch Work Experiences
                using (SqlCommand cmd = new SqlCommand("GetWorkExperiencesByResumeId2", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ResumeId", resumeId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            viewModel.WorkExperiences.Add(new WorkExperiences
                            {
                                ExperienceId = reader.GetInt32(0),
                                JobTitle = reader.GetString(2),
                                CompanyName = reader.GetString(3),
                                City = reader.GetString(4),
                                State = reader.GetString(5),
                                StartDate = reader.GetString(6),
                                EndDate = reader.GetString(7),
                                Responsibilities = reader.GetString(8)
                            });
                        }
                    }
                }

                // Fetch Educations
                using (SqlCommand cmd = new SqlCommand("GetEducationsByResumeId2", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ResumeId", resumeId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            viewModel.Educations.Add(new Educations
                            {
                                EducationId = reader.GetInt32(0),
                                Degree = reader.GetString(2),
                                Major = reader.GetString(3),
                                SchoolName = reader.GetString(4),
                                City = reader.GetString(5),
                                State = reader.GetString(6),
                                GraduationYear = reader.GetInt32(7)
                            });
                        }
                    }
                }

                // Fetch Skills
                using (SqlCommand cmd = new SqlCommand("GetSkillsByResumeId2", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ResumeId", resumeId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            viewModel.Skills.Add(new Skills
                            {
                                SkillId = reader.GetInt32(0),
                                SkillName = reader.GetString(2)
                            });
                        }
                    }
                }

                // Fetch Certifications
                using (SqlCommand cmd = new SqlCommand("GetCertificationsByResumeId2", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ResumeId", resumeId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            viewModel.Certifications.Add(new Certifications
                            {
                                CertificationId = reader.GetInt32(0),
                                CertificationName = reader.GetString(2),
                                IssuingOrganization = reader.GetString(3),
                                Years = reader.GetInt32(4)
                            });
                        }
                    }
                }

                // Fetch Projects
                using (SqlCommand cmd = new SqlCommand("GetProjectsByResumeId2", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ResumeId", resumeId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            viewModel.Projects.Add(new Projects
                            {
                                ProjectId = reader.GetInt32(0),
                                ProjectTitle = reader.GetString(2),
                                Description = reader.GetString(3),
                                Role = reader.GetString(4),
                                Outcome = reader.GetString(5)
                            });
                        }
                    }
                }

                // Fetch Languages
                using (SqlCommand cmd = new SqlCommand("GetLanguagesByResumeId2", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ResumeId", resumeId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            viewModel.Languages.Add(new Languages
                            {
                                LanguageId = reader.GetInt32(0),
                                LanguageName = reader.GetString(2)
                            });
                        }
                    }
                }

                // Fetch Hobbies
                using (SqlCommand cmd = new SqlCommand("GetHobbiesByResumeId2", con))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ResumeId", resumeId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            viewModel.Hobbies.Add(new Hobbies
                            {
                                HobbyId = reader.GetInt32(0),
                                HobbyName = reader.GetString(2)
                            });
                        }
                    }
                }
            }

            return View(viewModel);
        }
        catch (SqlException ex)
        {
            // Log the exception
            // Optionally, show a user-friendly error message
            return new HttpStatusCodeResult(500, "Internal server error: " + ex.Message);
        }
        catch (Exception ex)
        {
            // Log the exception
            // Optionally, show a user-friendly error message
            return new HttpStatusCodeResult(500, "An error occurred: " + ex.Message);
        }
    }

    // GET: Resume
    public ActionResult Index()
    {
        try
        {
            var resumes = _resumeRepository.GetAll();
            return View(resumes);
        }
        catch (Exception ex)
        {
            // Log the exception
            return new HttpStatusCodeResult(500, "An error occurred while fetching resumes: " + ex.Message);
        }
    }

    // GET: Resume/Details/5
    public ActionResult Details(int id)
    {
        try
        {
            var resume = _resumeRepository.GetById(id);
            if (resume == null)
            {
                return HttpNotFound();
            }
            return View(resume);
        }
        catch (Exception ex)
        {
            // Log the exception
            return new HttpStatusCodeResult(500, "An error occurred while fetching the resume details: " + ex.Message);
        }
    }

    public ActionResult Detview()
    {
        return View();
    }

    // GET: Resume/Create
    public ActionResult Create()
    {
        return View();
    }

    // POST: Resume/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(Resumes resume)
    {
        try
        {
            if (ModelState.IsValid)
            {
                int newResumeId = _resumeRepository.Insert(resume);
                return RedirectToAction("Create", "WorkExperiences", new { resumeId = newResumeId });
            }
            return View(resume);
        }
        catch (Exception ex)
        {
            // Log the exception
            return new HttpStatusCodeResult(500, "An error occurred while creating the resume: " + ex.Message);
        }
    }

    // GET: Resume/Edit/5
    public ActionResult Edit(int id)
    {
        try
        {
            var resume = _resumeRepository.GetById(id);
            if (resume == null)
            {
                return HttpNotFound();
            }
            return View(resume);
        }
        catch (Exception ex)
        {
            // Log the exception
            return new HttpStatusCodeResult(500, "An error occurred while fetching the resume for editing: " + ex.Message);
        }
    }

    // POST: Resume/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit(Resumes resume)
    {
        try
        {
            if (ModelState.IsValid)
            {
                _resumeRepository.Update(resume);
                return RedirectToAction("Edit", "WorkExperiences", new { resumeId = resume.ResumeId });
            }
            return View(resume);
        }
        catch (Exception ex)
        {
            // Log the exception
            return new HttpStatusCodeResult(500, "An error occurred while updating the resume: " + ex.Message);
        }
    }

    // GET: Resume/Delete/5
    public ActionResult Delete(int id)
    {
        try
        {
            var resume = _resumeRepository.GetById(id);
            if (resume == null)
            {
                return HttpNotFound();
            }
            return View(resume);
        }
        catch (Exception ex)
        {
            // Log the exception
            return new HttpStatusCodeResult(500, "An error occurred while fetching the resume for deletion: " + ex.Message);
        }
    }

    // POST: Resume/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        try
        {
            _resumeRepository.Delete(id);
            return RedirectToAction("Index");
        }
        catch (Exception ex)
        {
            // Log the exception
            return new HttpStatusCodeResult(500, "An error occurred while deleting the resume: " + ex.Message);
        }
    }
}
