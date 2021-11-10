using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace issBlueMetal.Models
{
    public class RoomStatus
    {
        public int id { get; set; }
        public int booking { get; set; }
        public int vacancy { get; set; }
        public int housKeeping { get; set; }
        public DateTime? Date { get; set; }
    }
}