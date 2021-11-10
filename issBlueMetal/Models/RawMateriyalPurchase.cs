using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace issBlueMetal.Models
{
    public class RawMateriyalPurchase
    {
        public int companyId { get; set; }
        public int id { get; set; }
        public DateTime? dateTime { get; set; }
        public int vehicleId { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public int staffId { get; set; }
        public virtual Staff Staff { get; set; }
        public string type { get; set; }

        public int departurePlaceId { get; set; }
        public virtual Location DeparturePlace { get; set; }
       
        public int supplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
        public int arrivalPlaceId { get; set; }
        public virtual Location ArrivalPlace { get; set; }
       
        public int materialNameId { get; set; }
        public virtual RawMeteriyal materialName { get; set; }
        
        public decimal Weight { get; set; }
        public decimal totalUnit { get; set; }
        public decimal rawMaterialDept { get; set; }
        public decimal salary { get; set; }
        public decimal totalAmount { get; set; }
        public decimal JcbSalary { get; set; }
        public decimal netAmount { get; set; }
        public int noofLoad { get; set; }
        public decimal productionDept { get; set; }
        public decimal salaryPerLoad { get; set; }
        public decimal ratePerUnit { get; set; }
        public decimal driverSalary { get; set; }
        public decimal vehicleRent { get; set; }


    }
}