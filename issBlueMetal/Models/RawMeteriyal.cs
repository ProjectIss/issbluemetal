using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace issBlueMetal.Models
{
    public class RawMeteriyal
    {
        public int id { get; set; }
        public string name { get; set; }
        public string uom { get; set; }
        public string discription { get; set; }
        public int companyId { get; set; }
    }
}