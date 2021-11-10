using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace issBlueMetal.Models
{
    public class AccountGroup
    {
        public int id { get; set; }
        public string accountGrouop { get; set; }
        public string parentGroup { get; set; }
        public int companyId { get; set; }
    }
}