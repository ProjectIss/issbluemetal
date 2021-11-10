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
    public class RawMeteriyalsController : Controller
    {
        private issModel db = new issModel();
        private errorLog errorLog = new errorLog();
        // GET: RawMeteriyals
        public async Task<ActionResult> Index()
        {
            try
            {
                return View(await db.RawMeteriyals.Where(x => x.companyId == Display.companyId).ToListAsync());
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "RawMeteriyals";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "index";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
        }

        // GET: RawMeteriyals/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RawMeteriyal rawMeteriyal = await db.RawMeteriyals.FindAsync(id);
                if (rawMeteriyal == null)
                {
                    return HttpNotFound();
                }
                return View(rawMeteriyal);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "RawMeteriyals";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Details";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
           
        }

        // GET: RawMeteriyals/Create
        public ActionResult Create()
        {
            try
            {

            }
            catch (Exception ex)
            {

                errorLog.controllerName = "RawMeteriyals";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Create View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
        }

        // POST: RawMeteriyals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,name,uom,discription")] RawMeteriyal rawMeteriyal)
        {
            try
            {
                var id = db.RawMeteriyals.Where(x => x.name == rawMeteriyal.name).FirstOrDefault();
                if (ModelState.IsValid && id == null)
                {
                    rawMeteriyal.companyId = Display.companyId;
                    db.RawMeteriyals.Add(rawMeteriyal);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                return View(rawMeteriyal);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "RawMeteriyals";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Create ";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
           
        }

        // GET: RawMeteriyals/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RawMeteriyal rawMeteriyal = await db.RawMeteriyals.FindAsync(id);
                if (rawMeteriyal == null)
                {
                    return HttpNotFound();
                }
                return View(rawMeteriyal);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "RawMeteriyals";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Edit Viwe";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
           
        }

        // POST: RawMeteriyals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,name,uom,discription")] RawMeteriyal rawMeteriyal)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(rawMeteriyal).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                return View(rawMeteriyal);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "RawMeteriyals";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Edit";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
           
        }

        // GET: RawMeteriyals/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                RawMeteriyal rawMeteriyal = await db.RawMeteriyals.FindAsync(id);
                if (rawMeteriyal == null)
                {
                    return HttpNotFound();
                }
                return View(rawMeteriyal);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "RawMeteriyals";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Delete View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
           
        }

        // POST: RawMeteriyals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                RawMeteriyal rawMeteriyal = await db.RawMeteriyals.FindAsync(id);
                db.RawMeteriyals.Remove(rawMeteriyal);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "RawMeteriyals";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Delete View";
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
