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

namespace issBlueMetal.Controllers
{
    public class AcLedgersController : Controller
    {
        private issModel db = new issModel();
        private errorLog errorLog = new errorLog();
        // GET: AcLedgers
        public async Task<ActionResult> Index()
        {
            try
            {
                var acLedger = db.AcLedger.Include(a => a.accountGroup);
                return View(await acLedger.OrderByDescending(x => x.id).ToListAsync());
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "AcLedger";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "index";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View(); 
        }

        // GET: AcLedgers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AcLedger acLedger = await db.AcLedger.FindAsync(id);
                if (acLedger == null)
                {
                    return HttpNotFound();
                }
                return View(acLedger);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "AcLedger";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Details";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
           
        }

        // GET: AcLedgers/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.accountGroupID = new SelectList(db.AccountGroups, "id", "accountGrouop");
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "AcLedger";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Create View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            
            return View();
        }

        // POST: AcLedgers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,accountledger,accountGroupID,openingBal,type,companyId")] AcLedger acLedger)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.AcLedger.Add(acLedger);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                ViewBag.accountGroupID = new SelectList(db.AccountGroups, "id", "accountGrouop", acLedger.accountGroupID);
                return View(acLedger);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "AcLedger";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Create";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
           
        }

        // GET: AcLedgers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AcLedger acLedger = await db.AcLedger.FindAsync(id);
                if (acLedger == null)
                {
                    return HttpNotFound();
                }
                ViewBag.accountGroupID = new SelectList(db.AccountGroups, "id", "accountGrouop", acLedger.accountGroupID);
                return View(acLedger);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "AcLedger";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Edit View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
           
        }

        // POST: AcLedgers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,accountledger,accountGroupID,openingBal,type,companyId")] AcLedger acLedger)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(acLedger).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                ViewBag.accountGroupID = new SelectList(db.AccountGroups, "id", "accountGrouop", acLedger.accountGroupID);
                return View(acLedger);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "AcLedger";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Edit";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
           
        }

        // GET: AcLedgers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AcLedger acLedger = await db.AcLedger.FindAsync(id);
                if (acLedger == null)
                {
                    return HttpNotFound();
                }
                return View(acLedger);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "AcLedger";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Delete View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
        }

        // POST: AcLedgers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                AcLedger acLedger = await db.AcLedger.FindAsync(id);
                db.AcLedger.Remove(acLedger);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "AcLedger";
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
