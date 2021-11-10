using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace issBlueMetal.Models
{
    public class Vehicle
    {
        public int companyId { get; set; }
        public int id { get; set; }
        public string vehicleNo { get; set; }
        public string shortNo { get; set; }
        public string vehicleName { get; set; }
        public string registrationAddress { get; set; }
        public DateTime? mfdYear { get; set; }
        public DateTime? insuranceExDate { get; set; }
        public DateTime? taxExDate { get; set; }
        public DateTime? permitExDate { get; set; }
        public DateTime? fcExDate { get; set; }
        public DateTime? oillService { get; set; }
        public DateTime? greecePacking { get; set; }
        public DateTime? coolentOill { get; set; }
        public DateTime? gearBoxOill { get; set; }
        public DateTime? grownOill { get; set; }
        public string salary { get; set; }
        public string vehicleDetail { get; set; }
        public string noofUnit { get; set; }
    }
}