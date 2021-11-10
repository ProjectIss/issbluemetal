using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace issBlueMetal.Models
{
    public class TblGRC
    {
        public int id { get; set; }
        public int grcNo { get; set; } 
        public DateTime grcDate { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public string age { get; set; }
        public string adult { get; set; }
        public string child { get; set; }
        public DateTime arrtime { get; set; }
        public string roomType { get; set; }
        public string roomOption { get; set; }
        public string roomNo { get; set; }
        public string tariff { get; set; }
        public int billNo { get; set; }
        public int noofDay { get; set; }
        public int total { get; set; }
        public DateTime chkdate { get; set; }
        public DateTime chkTime { get; set; }
        public string taxAmt { get; set; }
        public string status { get; set; }

    }
}