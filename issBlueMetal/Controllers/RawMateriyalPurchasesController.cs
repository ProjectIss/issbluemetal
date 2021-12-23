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
    public class RawMateriyalPurchasesController : Controller
    {
        private issModel db = new issModel();
        private errorLog errorLog = new errorLog();
        // GET: RawMateriyalPurchases
        public ActionResult Index()
        {
            try
            {
                var rawMateriyalPurchases = db.RawMateriyalPurchases.Include(r => r.ArrivalPlace).Include(r => r.DeparturePlace).Include(r => r.materialName).Include(r => r.Staff).Include(r => r.Supplier).Include(r => r.Vehicle);
                return View(rawMateriyalPurchases.Where(x => x.companyId == Display.companyId).OrderByDescending(x => x.id).ToList());
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "RawMateriyalPurchases";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "index";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
        }

        // GET: RawMateriyalPurchases/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RawMateriyalPurchase rawMateriyalPurchase = db.RawMateriyalPurchases.Find(id);
                if (rawMateriyalPurchase == null)
                {
                    return HttpNotFound();
                }
                return View(rawMateriyalPurchase);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "RawMateriyalPurchases";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Details";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
        }

        // GET: RawMateriyalPurchases/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.arrivalPlaceId = new SelectList(db.Locations.OrderBy(x=>x.name), "id", "name");
                ViewBag.departurePlaceId = new SelectList(db.Locations.OrderBy(x => x.name), "id", "name");
                ViewBag.materialNameId = new SelectList(db.RawMeteriyals.OrderBy(x => x.name), "id", "name");
                ViewBag.staffId = new SelectList(db.Staffs.OrderBy(x => x.name), "id", "name");
                ViewBag.supplierId = new SelectList(db.Suppliers.OrderBy(x => x.name), "id", "name");
                ViewBag.vehicleId = new SelectList(db.Vehicles.OrderBy(x => x.vehicleNo), "id", "vehicleNo");
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "RawMateriyalPurchases";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Create View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
           
            return View();
        }

        // POST: RawMateriyalPurchases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,dateTime,vehicleId,staffId,type,departurePlaceId,supplierId,arrivalPlaceId,materialNameId,Weight,totalUnit,rawMaterialDept,salary,totalAmount,JcbSalary,netAmount,noofLoad,productionDept,salaryPerLoad,ratePerUnit,driverSalary,vehicleRent")] RawMateriyalPurchase rawMateriyalPurchase)
        {
            try
            {
                var company = db.Companies.Where(x => x.name == Custom.Display.company).FirstOrDefault();
                //var id = db.RawMateriyalPurchases.Where(x => x. == rawMateriyalPurchase.materialName).Select(x => x.id);
                if (ModelState.IsValid)
                {
                    rawMateriyalPurchase.companyId = Display.companyId;
                    db.RawMateriyalPurchases.Add(rawMateriyalPurchase);
                    db.SaveChanges();
                    supplierLedger supplierLedger = new supplierLedger();
                    supplierLedger.Company = company;
                    supplierLedger.companyId = company.id;
                    supplierLedger.credit = rawMateriyalPurchase.netAmount;
                    supplierLedger.supplierId = rawMateriyalPurchase.supplierId;
                    supplierLedger.dateOfPurchages = Convert.ToDateTime(rawMateriyalPurchase.dateTime);
                    db.supplierLedgers.Add(supplierLedger);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.arrivalPlaceId = new SelectList(db.Locations, "id", "name", rawMateriyalPurchase.arrivalPlaceId);
                ViewBag.departurePlaceId = new SelectList(db.Locations, "id", "name", rawMateriyalPurchase.departurePlaceId);
                ViewBag.materialNameId = new SelectList(db.RawMeteriyals, "id", "name", rawMateriyalPurchase.materialNameId);
                ViewBag.staffId = new SelectList(db.Staffs, "id", "name", rawMateriyalPurchase.staffId);
                ViewBag.supplierId = new SelectList(db.Suppliers, "id", "name", rawMateriyalPurchase.supplierId);
                ViewBag.vehicleId = new SelectList(db.Vehicles, "id", "vehicleNo", rawMateriyalPurchase.vehicleId);
                return View(rawMateriyalPurchase);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "RawMateriyalPurchases";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Create";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
        }

        // GET: RawMateriyalPurchases/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RawMateriyalPurchase rawMateriyalPurchase = db.RawMateriyalPurchases.Find(id);
                if (rawMateriyalPurchase == null)
                {
                    return HttpNotFound();
                }
                ViewBag.arrivalPlaceId = new SelectList(db.Locations.OrderBy(x => x.name), "id", "name", rawMateriyalPurchase.arrivalPlaceId);
                ViewBag.departurePlaceId = new SelectList(db.Locations.OrderBy(x => x.name), "id", "name", rawMateriyalPurchase.departurePlaceId);
                ViewBag.materialNameId = new SelectList(db.RawMeteriyals.OrderBy(x => x.name), "id", "name", rawMateriyalPurchase.materialNameId);
                ViewBag.staffId = new SelectList(db.Staffs.OrderBy(x => x.name), "id", "name", rawMateriyalPurchase.staffId);
                ViewBag.supplierId = new SelectList(db.Suppliers.OrderBy(x => x.name), "id", "name", rawMateriyalPurchase.supplierId);
                ViewBag.vehicleId = new SelectList(db.Vehicles.OrderBy(x => x.vehicleNo), "id", "vehicleNo", rawMateriyalPurchase.vehicleId);
                return View(rawMateriyalPurchase);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "RawMateriyalPurchases";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Edit View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
        }

        // POST: RawMateriyalPurchases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,dateTime,vehicleId,staffId,type,departurePlaceId,supplierId,arrivalPlaceId,materialNameId,Weight,totalUnit,rawMaterialDept,salary,totalAmount,JcbSalary,netAmount,noofLoad,productionDept,salaryPerLoad,ratePerUnit,driverSalary,vehicleRent")] RawMateriyalPurchase rawMateriyalPurchase)
        {
            try
            {
                var company = db.Companies.Where(x => x.name == Custom.Display.company).FirstOrDefault();
                if (ModelState.IsValid)
                {
                    db.Entry(rawMateriyalPurchase).State = EntityState.Modified;
                    db.SaveChanges();
                    supplierLedger supplierLedger = new supplierLedger();
                    supplierLedger.Company = company;
                    supplierLedger.companyId = company.id;
                    supplierLedger.credit = rawMateriyalPurchase.netAmount;
                    supplierLedger.supplierId = rawMateriyalPurchase.supplierId;
                    supplierLedger.dateOfPurchages = DateTime.Now;
                    db.supplierLedgers.Add(supplierLedger);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.arrivalPlaceId = new SelectList(db.Locations, "id", "name", rawMateriyalPurchase.arrivalPlaceId);
                ViewBag.departurePlaceId = new SelectList(db.Locations, "id", "name", rawMateriyalPurchase.departurePlaceId);
                ViewBag.materialNameId = new SelectList(db.RawMeteriyals, "id", "name", rawMateriyalPurchase.materialNameId);
                ViewBag.staffId = new SelectList(db.Staffs, "id", "name", rawMateriyalPurchase.staffId);
                ViewBag.supplierId = new SelectList(db.Suppliers, "id", "name", rawMateriyalPurchase.supplierId);
                ViewBag.vehicleId = new SelectList(db.Vehicles, "id", "vehicleNo", rawMateriyalPurchase.vehicleId);
                return View(rawMateriyalPurchase);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "RawMateriyalPurchases";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Edit";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
        }

        // GET: RawMateriyalPurchases/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RawMateriyalPurchase rawMateriyalPurchase = db.RawMateriyalPurchases.Find(id);
                if (rawMateriyalPurchase == null)
                {
                    return HttpNotFound();
                }
                return View(rawMateriyalPurchase);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "RawMateriyalPurchases";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Delete View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
        }

        // POST: RawMateriyalPurchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                RawMateriyalPurchase rawMateriyalPurchase = db.RawMateriyalPurchases.Find(id);
                db.RawMateriyalPurchases.Remove(rawMateriyalPurchase);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "RawMateriyalPurchases";
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
    }
}
