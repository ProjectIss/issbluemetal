using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace issBlueMetal.Models
{
    public class customerLedger
    {
        public int Id { get; set; }
        public DateTime dateOfPurchages { get; set; }
        public int customerId { get; set; }
       public virtual Customer Customer { get; set; }
        public decimal debit { get; set; } = 0;
        public decimal credit { get; set; }
        public string type { get; set; } = "Sales";
        public int companyId { get; set; }
        public virtual Company Company { get; set; }
    }
}