using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FINAL.Models
{
    public class Resume
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string LinkedIn { get; set; }
        public string GitHub { get; set; }
        public string ProfessionalSummary { get; set; }

        public string PdfData { get; set; }
    }

    // WorkExperience Model
    public class WorkExperience
    {
        public int Id { get; set; }
        public int ResumeId { get; set; }
        public string JobTitle { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public string Years { get; set; }
    }

    // Education Model
    public class Education
    {
        public int Id { get; set; }
        public int ResumeId { get; set; }
        public string Degree { get; set; }
        public string Institution { get; set; }
        public string GraduationYear { get; set; }
    }

    // Skill Model
    public class Skill
    {
        public int Id { get; set; }
        public int ResumeId { get; set; }
        public string SkillName { get; set; }
    }

    // Certification Model
    public class Certification
    {
        public int Id { get; set; }
        public int ResumeId { get; set; }
        public string CertificationName { get; set; }
        public string IssuedBy { get; set; }
        public string Year { get; set; }
    }

    // Project Model
    public class Project
    {
        public int Id { get; set; }
        public int ResumeId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
    }

}