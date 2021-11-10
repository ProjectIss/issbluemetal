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
    public class VehiclesController : Controller
    {
        private issModel db = new issModel();
        private errorLog errorLog = new errorLog();
        // GET: Vehicles
        public async Task<ActionResult> Index()
        {
            try
            {
                return View(await db.Vehicles.ToListAsync());

            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Vehicles";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "index";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
        }

        // GET: Vehicles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Vehicle vehicle = await db.Vehicles.FindAsync(id);
                if (vehicle == null)
                {
                    return HttpNotFound();
                }
                return View(vehicle);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Vehicles";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Details";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
           
        }

        // GET: Vehicles/Create
        public ActionResult Create()
        {
            try
            {

            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Vehicles";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Create View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,vehicleNo,shortNo,vehicleName,registrationAddress,mfdYear,insuranceExDate,taxExDate,permitExDate,fcExDate,oillService,greecePacking,coolentOill,gearBoxOill,grownOill,salary,vehicleDetail,noofUnit")] Vehicle vehicle)
        {
            try
            {
                var no = db.Vehicles.Where(x => x.vehicleNo == vehicle.vehicleNo).FirstOrDefault();
                if (ModelState.IsValid && no == null)
                {

                    db.Vehicles.Add(vehicle);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                return View(vehicle);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Vehicles";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Create";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
            
        }

        // GET: Vehicles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Vehicle vehicle = await db.Vehicles.FindAsync(id);
                if (vehicle == null)
                {
                    return HttpNotFound();
                }
                return View(vehicle);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Vehicles";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Edit View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
           
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,vehicleNo,shortNo,vehicleName,registrationAddress,mfdYear,insuranceExDate,taxExDate,permitExDate,fcExDate,oillService,greecePacking,coolentOill,gearBoxOill,grownOill,salary,vehicleDetail,noofUnit")] Vehicle vehicle)
        {
            try
            {
                //var no = db.Vehicles.Where(x => x.vehicleNo == vehicle.vehicleNo).FirstOrDefault();
                if (ModelState.IsValid)
                {
                    db.Entry(vehicle).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(vehicle);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Vehicles";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Edit";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
           
        }

        // GET: Vehicles/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Vehicle vehicle = await db.Vehicles.FindAsync(id);
                if (vehicle == null)
                {
                    return HttpNotFound();
                }
                return View(vehicle);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Vehicles";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Delete View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
           
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Vehicle vehicle = await db.Vehicles.FindAsync(id);
                db.Vehicles.Remove(vehicle);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Vehicles";
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
