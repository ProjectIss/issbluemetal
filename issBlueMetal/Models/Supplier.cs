using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace issBlueMetal.Models
{
    public class Supplier
    {
        public int companyId { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string cellNo { get; set; }
        public string openingBalance { get; set; }
        public string cstNo { get; set; }

    }
}