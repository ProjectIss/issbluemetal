using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using issBlueMetal.Models;
using issBlueMetal.Custom;

namespace issBlueMetal.Controllers
{
    [CustomAuthorize(Roles = "Admin,Manager")]
    public class TripSalesEntriesController : Controller
    {
        private issModel db = new issModel();
        private errorLog errorLog = new errorLog();
        // GET: TripSalesEntries
        public async Task<ActionResult> Index()
        {
            try
            {
                return View(await db.TripSalesEntries.Where(x => x.companyId == Display.companyId).ToListAsync());
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "TripSalesEntries";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "index";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
        }

        // GET: TripSalesEntries/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TripSalesEntry tripSalesEntry = await db.TripSalesEntries.FindAsync(id);
                if (tripSalesEntry == null)
                {
                    return HttpNotFound();
                }
                return View(tripSalesEntry);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "TripSalesEntries";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Details";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
          
        }

        // GET: TripSalesEntries/Create
        public ActionResult Create()
        {
            try
            {

            }
            catch (Exception ex)
            {

                errorLog.controllerName = "TripSalesEntries";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Create View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
        }

        // POST: TripSalesEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,date,vehicleNo,driverName,type,materialName,customerName,address,phoneNo,deliveryPlace,noofUnit,driverSalary,netAmount,paidAmount,rateperUnit,rentAmount,advanceAmt,balanceAmt")] TripSalesEntry tripSalesEntry)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    tripSalesEntry.companyId = Display.companyId;
                    db.TripSalesEntries.Add(tripSalesEntry);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                return View(tripSalesEntry);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "TripSalesEntries";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Create";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
          
        }

        // GET: TripSalesEntries/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TripSalesEntry tripSalesEntry = await db.TripSalesEntries.FindAsync(id);
                if (tripSalesEntry == null)
                {
                    return HttpNotFound();
                }
                return View(tripSalesEntry);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "TripSalesEntries";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Edit View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
           
        }

        // POST: TripSalesEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,date,vehicleNo,driverName,type,materialName,customerName,address,phoneNo,deliveryPlace,noofUnit,driverSalary,netAmount,paidAmount,rateperUnit,rentAmount,advanceAmt,balanceAmt")] TripSalesEntry tripSalesEntry)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(tripSalesEntry).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(tripSalesEntry);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "TripSalesEntries";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Edit";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
           
        }

        // GET: TripSalesEntries/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                TripSalesEntry tripSalesEntry = await db.TripSalesEntries.FindAsync(id);
                if (tripSalesEntry == null)
                {
                    return HttpNotFound();
                }
                return View(tripSalesEntry);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "TripSalesEntries";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Delete View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
          
        }

        // POST: TripSalesEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                TripSalesEntry tripSalesEntry = await db.TripSalesEntries.FindAsync(id);
                db.TripSalesEntries.Remove(tripSalesEntry);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "TripSalesEntries";
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
