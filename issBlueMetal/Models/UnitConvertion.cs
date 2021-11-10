using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace issBlueMetal.Models
{
    public class UnitConvertion
    {
        public int companyId { get; set; }
        public int Id { get; set; }
        public string mainUnitName { get; set; }
        public string subUnitName { get; set; }
        public int factor { get; set; }
    }
}