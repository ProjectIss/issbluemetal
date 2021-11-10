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
    public class UnitConvertionsController : Controller
    {
        private issModel db = new issModel();
        private errorLog errorLog = new errorLog();
        // GET: UnitConvertions
        public async Task<ActionResult> Index()
        {
            try
            {
                return View(await db.UnitConvertions.ToListAsync());
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "UnitConvertions";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "index";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
        }

        // GET: UnitConvertions/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                UnitConvertion unitConvertion = await db.UnitConvertions.FindAsync(id);
                if (unitConvertion == null)
                {
                    return HttpNotFound();
                }
                return View(unitConvertion);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "UnitConvertions";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Details";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
          
        }

        // GET: UnitConvertions/Create
        public ActionResult Create()
        {
            try
            {

            }
            catch (Exception ex)
            {

                errorLog.controllerName = "UnitConvertions";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Create View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
        }

        // POST: UnitConvertions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,mainUnitName,subUnitName,factor")] UnitConvertion unitConvertion)
        {
            try
            {
                var id = db.UnitConvertions.Where(x => x.mainUnitName == unitConvertion.mainUnitName).Select(x => x.mainUnitName).FirstOrDefault();
                if (ModelState.IsValid && id == null)
                {

                    db.UnitConvertions.Add(unitConvertion);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                return View(unitConvertion);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "UnitConvertions";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Create";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
           
        }

        // GET: UnitConvertions/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                UnitConvertion unitConvertion = await db.UnitConvertions.FindAsync(id);
                if (unitConvertion == null)
                {
                    return HttpNotFound();
                }
                return View(unitConvertion);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "UnitConvertions";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Edit View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
           
        }

        // POST: UnitConvertions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,mainUnitName,subUnitName,factor")] UnitConvertion unitConvertion)
        {
            try
            {
                //var id = db.UnitConvertions.Where(x => x.mainUnitName == unitConvertion.mainUnitName).Select(x => x.mainUnitName).FirstOrDefault();
                if (ModelState.IsValid)
                {
                    db.Entry(unitConvertion).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(unitConvertion);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "UnitConvertions";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Edit";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
           
        }

        // GET: UnitConvertions/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                UnitConvertion unitConvertion = await db.UnitConvertions.FindAsync(id);
                if (unitConvertion == null)
                {
                    return HttpNotFound();
                }
                return View(unitConvertion);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "UnitConvertions";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Delete View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
            
        }

        // POST: UnitConvertions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                UnitConvertion unitConvertion = await db.UnitConvertions.FindAsync(id);
                db.UnitConvertions.Remove(unitConvertion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "UnitConvertions";
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
