using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace issBlueMetal.Models
{
    public class Hotel
    {
        public int Id { get; set; }
        public int tranNo { get; set; }
        public int income { get; set; }
        public int expence { get; set; }
        public int flag { get; set; }
        public DateTime date { get; set; }
        public string roomNo { get; set; }
        public string particular { get; set; }
        public string status { get; set; }
        public string user { get; set; }
        public string mode { get; set; }
        public string invNo { get; set; }
    }
}