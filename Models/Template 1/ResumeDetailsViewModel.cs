using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FINAL.Models
{
    public class ResumeDetailsViewModel
    {
        public Resume Resume { get; set; }
        public List<WorkExperience> WorkExperiences { get; set; }
        public List<Education> Educations { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Certification> Certifications { get; set; }
        public List<Project> Projects { get; set; }
    }
}