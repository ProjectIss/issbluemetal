using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace issBlueMetal.Models
{
    public class AcLedger
    {
        public int id { get; set; }
        public string accountledger { get; set; }
        public int accountGroupID { get; set; }
        public virtual AccountGroup accountGroup { get; set; }
        public string openingBal { get; set; }
        public string type { get; set; }
        public int companyId { get; set; }
    }
}