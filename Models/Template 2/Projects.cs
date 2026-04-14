using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FINAL.Models.Template_2
{
    public class Projects
    {
        public int ProjectId { get; set; }
        public int ResumeId { get; set; }
        public string ProjectTitle { get; set; }
        public string Description { get; set; }
        public string Role { get; set; }
        public string Outcome { get; set; }
    }
}