using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FINAL.Models.Template_2
{
    public class Certifications
    {
        public int CertificationId { get; set; }
        public int ResumeId { get; set; }
        public string CertificationName { get; set; }
        public string IssuingOrganization { get; set; }
        public int Years{ get; set; }
    }
}