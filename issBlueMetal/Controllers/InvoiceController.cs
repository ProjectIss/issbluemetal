using issBlueMetal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace issBlueMetal.Controllers
{
    public class InvoiceController : Controller
    {
        private issModel db = new issModel();

        // GET: Invoice
        public ActionResult Index(int? id)
        {
            ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        public JsonResult getSalesData(int id)
        {
            var RawData = db.Sales.Where(x => x.Id == id).FirstOrDefault();
           
            
            return Json(RawData, JsonRequestBehavior.AllowGet);
        }
    }
}