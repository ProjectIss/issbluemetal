using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace issBlueMetal.Models
{
    public class Sales
    {
        public int companyId { get; set; }
        public int Id { get; set; }
        public int billNo { get; set; }
        public DateTime? Date { get; set; }
        public string vehicle { get; set; }
       
        public string driver { get; set; }
      
        public string type { get; set; }
        public int itemId { get; set; }
        public virtual Item Item { get; set; }
       
        public int customerId { get; set; }
        public virtual Customer customer { get; set; }
        public string address { get; set; }
        public string phoneNo { get; set; }
        
        public string deliveryPlace { get; set; }
       
        public decimal ? noOfUnit { get; set; }
        public decimal? ratePerUnit { get; set; }
        public decimal? driverSalary { get; set; }
        public decimal? rentAmount { get; set; }
        public decimal? netAmount { get; set; }
        public decimal? advanceAmount { get; set; }
        public decimal? paidAmount { get; set; }
        public decimal? balanceAmount { get; set; }

    }
}