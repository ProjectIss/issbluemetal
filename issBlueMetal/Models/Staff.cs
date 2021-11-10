using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace issBlueMetal.Models
{
    public class Staff
    {
        public int companyId { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string cellNo { get; set; }
        public string licenceNo { get; set; }
        public string badgeno { get; set; }
        public DateTime? licenceExDate { get; set; }
        public string salary { get; set; }
        public string salaryType { get; set; }

    }
}