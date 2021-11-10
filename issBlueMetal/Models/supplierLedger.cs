using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace issBlueMetal.Models
{
    public class supplierLedger
    {

        public int Id { get; set; }
        public DateTime dateOfPurchages { get; set; }
        public int supplierId { get; set; }
        public virtual Supplier SupplierLedger { get; set; }
        public decimal debit { get; set; } = 0;
        public decimal credit { get; set; }
        public string type { get; set; } = "Purchages";
        public int companyId { get; set; }
        public virtual Company Company { get; set; }
    }
}