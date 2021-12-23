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
    public class ReciptEntriesController : Controller
    {
        private issModel db = new issModel();
        private errorLog errorLog = new errorLog();
        // GET: ReciptEntries
        public async Task<ActionResult> Index()
        {
          
            return View(await db.ReciptEntries.Include(p => p.AcLedger).Include(p => p.customer).OrderByDescending(x=>x.Id).OrderByDescending(x => x.Id).ToListAsync());
        }

        // GET: ReciptEntries/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ReciptEntry reciptEntry = await db.ReciptEntries.FindAsync(id);
                if (reciptEntry == null)
                {
                    return HttpNotFound();
                }
                return View(reciptEntry);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "ReciptEntries";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Details";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();

        }


        // GET: ReciptEntries/Create
        public ActionResult Create()
        {
            try
            {
                var accountledger = db.AcLedger;
                List<SelectListItem> accountLedger = new List<SelectListItem>();
                foreach (var item in db.AcLedger)
                {
                    if (item.accountGroupID == 34 || item.accountGroupID == 31)
                    {
                        accountLedger.Add(new SelectListItem { Text = item.accountledger, Value = item.accountledger });
                    }
                }
                ViewBag.PaymentType = accountLedger;
                ViewBag.acLedgerId = new SelectList(db.AcLedger.OrderBy(x => x.accountledger), "id", "accountledger");
                ViewBag.customerId = new SelectList(db.Customers.OrderBy(x => x.name), "id", "name");
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "ReciptEntries";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Create View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();


        }

        // POST: ReciptEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Date,PaymentType,acLedgerId,description,Amount,customerId")] ReciptEntry reciptEntry)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    reciptEntry.companyId = Display.companyId;
                    db.ReciptEntries.Add(reciptEntry);
                    var company = db.Companies.Where(x => x.name == Custom.Display.company).FirstOrDefault();
                    customerLedger customerLedger = new customerLedger();
                    customerLedger.Company = company;
                    customerLedger.companyId = company.id;
                    customerLedger.credit = Convert.ToDecimal(reciptEntry.Amount);
                    customerLedger.customerId = reciptEntry.customerId;
                    customerLedger.Customer = reciptEntry.customer;
                    customerLedger.type = "Received";
                    customerLedger.dateOfPurchages = DateTime.Now;
                    db.customerLedgers.Add(customerLedger);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                List<SelectListItem> accountLedger = new List<SelectListItem>();
                foreach (var item in db.AcLedger)
                {
                    if (item.accountGroupID == 34 || item.accountGroupID == 31)
                    {
                        accountLedger.Add(new SelectListItem { Text = item.accountledger, Value = item.accountledger });
                    }
                }
                ViewBag.PaymentType = accountLedger;
                ViewBag.acLedgerId = new SelectList(db.AcLedger, "id", "accountledger", reciptEntry.acLedgerId);
                ViewBag.customerId = new SelectList(db.Customers, "id", "name", reciptEntry.customerId);
                return View(reciptEntry);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "ReciptEntries";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Create";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();

        }

        // GET: ReciptEntries/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ReciptEntry reciptEntry = await db.ReciptEntries.FindAsync(id);
                if (reciptEntry == null)
                {
                    return HttpNotFound();
                }
                var accountledger = db.AcLedger;
                List<SelectListItem> accountLedger = new List<SelectListItem>();
                foreach (var item in db.AcLedger)
                {
                    if (item.accountGroupID == 34 || item.accountGroupID == 31)
                    {
                        accountLedger.Add(new SelectListItem { Text = item.accountledger, Value = item.accountledger });
                    }
                }
                ViewBag.PaymentType = accountLedger;
                ViewBag.acLedgerId = new SelectList(db.AcLedger.OrderBy(x => x.accountledger), "id", "accountledger", reciptEntry.acLedgerId);
                ViewBag.customerId = new SelectList(db.Customers.OrderBy(x => x.name), "id", "name", reciptEntry.customerId);
                return View(reciptEntry);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "ReciptEntries";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Edit View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();


        }

        // POST: ReciptEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Date,PaymentType,acLedgerId,description,Amount,customerId")] ReciptEntry reciptEntry)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    reciptEntry.companyId = Display.companyId;
                    db.Entry(reciptEntry).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                List<SelectListItem> accountLedger = new List<SelectListItem>();
                foreach (var item in db.AcLedger)
                {
                    if (item.accountGroupID == 34 || item.accountGroupID == 31)
                    {
                        accountLedger.Add(new SelectListItem { Text = item.accountledger, Value = item.accountledger });
                    }
                }
                ViewBag.acLedgerId = new SelectList(db.AcLedger, "id", "accountledger", reciptEntry.acLedgerId);
                ViewBag.customerId = new SelectList(db.Customers, "id", "name", reciptEntry.customerId);
                return View(reciptEntry);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "ReciptEntries";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Edit";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();

        }


        // GET: ReciptEntries/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ReciptEntry reciptEntry = await db.ReciptEntries.FindAsync(id);
                if (reciptEntry == null)
                {
                    return HttpNotFound();
                }
                return View(reciptEntry);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "ReciptEntries";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Delete View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();

        }

        // POST: ReciptEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                ReciptEntry reciptEntry = await db.ReciptEntries.FindAsync(id);
                db.ReciptEntries.Remove(reciptEntry);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "ReciptEntries";
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
