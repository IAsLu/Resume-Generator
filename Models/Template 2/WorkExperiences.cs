using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FINAL.Models.Template_2
{
    public class WorkExperiences
    {
        public int ExperienceId { get; set; }
        public int ResumeId { get; set; }
        public string JobTitle { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Responsibilities { get; set; }
    }
}