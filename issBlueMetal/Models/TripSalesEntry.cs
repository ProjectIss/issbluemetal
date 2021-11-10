using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace issBlueMetal.Models
{
    public class TripSalesEntry
    {
        public int companyId { get; set; }
        public int id { get; set; }
        public DateTime? date { get; set; }
        public int vehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public int staffId { get; set; }
        public virtual Staff Staff { get; set; }
        public string type { get; set; }
        public int materialNameId { get; set; }
        public virtual RawMeteriyal materialName { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public string address { get; set; }
        public string phoneNo { get; set; }
        public int deliveryPlaceId { get; set; }
        public virtual Location deliveryPlace { get; set; }
        public decimal noofUnit { get; set; }
        public decimal driverSalary { get; set; }
        public decimal netAmount { get; set; }
        public decimal paidAmount { get; set; }
        public decimal rateperUnit { get; set; }
        public decimal rentAmount { get; set; }
        public decimal advanceAmt { get; set; }
        public decimal balanceAmt { get; set; }
    }
}