using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FINAL.Models.Template_2
{
    public class Educations
    {
        public int EducationId { get; set; }
        public int ResumeId { get; set; }
        public string Degree { get; set; }
        public string Major { get; set; }
        public string SchoolName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int GraduationYear { get; set; }
    }
}