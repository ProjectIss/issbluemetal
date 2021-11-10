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
    public class SuppliersController : Controller
    {
        private issModel db = new issModel();
        private errorLog errorLog = new errorLog();
        // GET: Suppliers
        public async Task<ActionResult> Index()
        {
            try
            {
                return View(await db.Suppliers.ToListAsync());
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Suppliers";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "index";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
        }

        // GET: Suppliers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Supplier supplier = await db.Suppliers.FindAsync(id);
                if (supplier == null)
                {
                    return HttpNotFound();
                }
                return View(supplier);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Suppliers";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Details";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
          
        }

        // GET: Suppliers/Create
        public ActionResult Create()
        {
            try
            {

            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Suppliers";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Create View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
        }

        // POST: Suppliers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,name,address,cellNo,openingBalance,cstNo")] Supplier supplier)
        {
            try
            {
                var id = db.Suppliers.Where(x => x.name == supplier.name).Select(x => x.name).FirstOrDefault();
                if (ModelState.IsValid && id == null)
                {

                    db.Suppliers.Add(supplier);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                return View(supplier);
            }
            catch (Exception ex)
            {
                errorLog.controllerName = "Suppliers";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Create";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
           
        }

        // GET: Suppliers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Supplier supplier = await db.Suppliers.FindAsync(id);
                if (supplier == null)
                {
                    return HttpNotFound();
                }
                return View(supplier);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Suppliers";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Edit View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
           
        }

        // POST: Suppliers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,name,address,cellNo,openingBalance,cstNo")] Supplier supplier)
        {
            try
            {
                //var id = db.Suppliers.Where(x => x.name == supplier.name).Select(x => x.name).FirstOrDefault();
                if (ModelState.IsValid)
                {
                    db.Entry(supplier).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(supplier);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Suppliers";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Edit";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
            
        }

        // GET: Suppliers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Supplier supplier = await db.Suppliers.FindAsync(id);
                if (supplier == null)
                {
                    return HttpNotFound();
                }
                return View(supplier);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Suppliers";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Delete View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
           
        }

        // POST: Suppliers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                Supplier supplier = await db.Suppliers.FindAsync(id);
                db.Suppliers.Remove(supplier);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Suppliers";
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
