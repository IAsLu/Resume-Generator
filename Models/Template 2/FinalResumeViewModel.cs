using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FINAL.Models.Template_2
{
    public class FinalResumeViewModel
    {
        public Resumes Resume { get; set; }
        public List<WorkExperiences> WorkExperiences { get; set; }
        public List<Educations> Educations { get; set; }
        public List<Skills> Skills { get; set; }
        public List<Certifications> Certifications { get; set; }
        public List<Projects> Projects { get; set; }
        public List<Languages> Languages { get; set; }
        public List<Hobbies> Hobbies { get; set; }
    }
}