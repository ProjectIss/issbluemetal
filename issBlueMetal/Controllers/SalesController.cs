using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using issBlueMetal.Custom;
using issBlueMetal.Models;

namespace issBlueMetal.Controllers
{
    [CustomAuthorize(Roles = "Admin,Manager")]
    public class SalesController : Controller
    {
        private issModel db = new issModel();
        private errorLog errorLog = new errorLog();
        // GET: Sales
        public ActionResult Index()
        {
            try
            {
                var sales = db.Sales;
                return View(sales.Where(x => x.companyId == Display.companyId).OrderByDescending(x => x.Id).ToList());

            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Sales";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "index";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Sales sales = db.Sales.Find(id);
                if (sales == null)
                {
                    return HttpNotFound();
                }
                return View(sales);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Sales";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Details";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();

        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.itemId = new SelectList(db.GetItems, "id", "name");
                ViewBag.customerId = new SelectList(db.Customers, "id", "name");

            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Sales";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Create View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }


            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,billNo,Date,vehicle,driver,type,itemId,customerId,address,phoneNo,deliveryPlace,noOfUnit,ratePerUnit,driverSalary,rentAmount,netAmount,advanceAmount,paidAmount,balanceAmount")] Sales sales)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var length = db.Sales.ToList();

                    if (length.Count > 0)
                    {
                        var Billno = db.Sales.Max(x => x.billNo);
                        sales.billNo = Billno + 1;
                    }
                    else
                    {
                        sales.billNo = 1;
                    }
                    db.Sales.Add(sales);
                    db.SaveChanges();

                    var company = db.Companies.Where(x => x.name == Custom.Display.company).FirstOrDefault();
                    if (sales.balanceAmount > 0)
                    {
                        sales.companyId = Display.companyId;
                        customerLedger customerLedger = new customerLedger();
                        customerLedger.Company = company;
                        customerLedger.companyId = company.id;
                        customerLedger.debit = Convert.ToDecimal(sales.netAmount);
                        customerLedger.credit = 0;
                        customerLedger.customerId = sales.customerId;
                        customerLedger.dateOfPurchages = DateTime.Now;
                        db.customerLedgers.Add(customerLedger);
                        db.SaveChanges();
                        
                    }
                    return RedirectToAction("Index");
                }



                ViewBag.itemId = new SelectList(db.GetItems, "id", "name", sales.itemId);
                ViewBag.customerId = new SelectList(db.Customers, "id", "name", sales.customerId);


                return View(sales);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Sales";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Create";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();

        }

        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Sales sales = db.Sales.Find(id);
                if (sales == null)
                {
                    return HttpNotFound();
                }



                ViewBag.itemId = new SelectList(db.GetItems, "id", "name", sales.itemId);
                ViewBag.customerId = new SelectList(db.Customers, "id", "name", sales.customerId);
                return View(sales);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Sales";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Edit View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();

        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,billNo,Date,vehicle,driver,type,itemId,customerId,address,phoneNo,deliveryPlace,noOfUnit,ratePerUnit,driverSalary,rentAmount,netAmount,advanceAmount,paidAmount,balanceAmount")] Sales sales)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    sales.companyId = Display.companyId;
                    db.Entry(sales).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }


                ViewBag.itemId = new SelectList(db.GetItems, "id", "name", sales.itemId);
                ViewBag.customerId = new SelectList(db.Customers, "id", "name", sales.customerId);
                return View(sales);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Sales";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Edit";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();

        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Sales sales = db.Sales.Find(id);
                if (sales == null)
                {
                    return HttpNotFound();
                }
                return View(sales);
            }
            catch (Exception ex)
            {
                errorLog.controllerName = "Sales";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Delete View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();

        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Sales sales = db.Sales.Find(id);
                db.Sales.Remove(sales);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Sales";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Delete";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpPost]
        public JsonResult getCustomerDetails(int? id)
        {
            var result = db.Customers.Where(x => x.id == id).FirstOrDefault();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
