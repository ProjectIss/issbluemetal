using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace issBlueMetal.Models
{
    public class ReciptEntry
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string PaymentType { get; set; }
        public int acLedgerId { get; set; }
        public virtual AcLedger AcLedger { get; set; }
       
        public string description { get; set; }
        public decimal Amount { get; set; }
        public int companyId { get; set; }
        public int customerId { get; set; }
        public virtual Customer customer { get; set; }
    }
}