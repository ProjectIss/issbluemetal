using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace issBlueMetal.Models
{
    public class Item
    {
       
        public int id { get; set; }
        public string name { get; set; }
        public string uom { get; set; }
        public string purchasePrize { get; set; }
        public string salcePrize { get; set; }
        public int companyId { get; set; }
    }
}