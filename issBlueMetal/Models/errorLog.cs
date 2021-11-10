using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace issBlueMetal.Models
{
    public class errorLog
    {
        public int id { get; set; }
        public DateTime ErrorDate { get; set; }
        public string controllerName { get; set; }
        public string MethodName { get; set; }
        [Column(TypeName = "varchar(MAX)")]
        public string ErrorMessage { get; set; }
    }
}