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
    public class PaymentEntriesController : Controller
    {
        private issModel db = new issModel();
        private errorLog errorLog = new errorLog();
        // GET: PaymentEntries
        public async Task<ActionResult> Index()
        {
            var paymentEntries = db.PaymentEntries.Include(p => p.AcLedger).Include(p => p.Supplier).OrderByDescending(x=>x.Id).ToListAsync();
            return View(await paymentEntries);
        }

        // GET: PaymentEntries/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PaymentEntry paymentEntry = await db.PaymentEntries.FindAsync(id);
                if (paymentEntry == null)
                {
                    return HttpNotFound();
                }
                return View(paymentEntry);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "PaymentEntries";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "index";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
        }


        // GET: PaymentEntries/Create
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
                ViewBag.supplierId = new SelectList(db.Suppliers.OrderBy(x => x.name), "id", "name");
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "PaymentEntries";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Create View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }

            return View();
        }

        // POST: PaymentEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Date,PaymentType,acLedgerId,description,Amount,supplierId,companyId")] PaymentEntry paymentEntry)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    paymentEntry.companyId = Display.companyId;
                    db.PaymentEntries.Add(paymentEntry);
                    var company = db.Companies.Where(x => x.name == Custom.Display.company).FirstOrDefault();
                    supplierLedger supplierLedger = new supplierLedger();
                    supplierLedger.Company = company;
                    supplierLedger.companyId = company.id;
                    supplierLedger.debit = paymentEntry.Amount;
                    supplierLedger.supplierId = paymentEntry.supplierId;
                    supplierLedger.SupplierLedger = paymentEntry.Supplier;
                    supplierLedger.type = "Payment";
                    supplierLedger.dateOfPurchages = (DateTime)paymentEntry.Date;
                    db.supplierLedgers.Add(supplierLedger);
                    db.SaveChanges();
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
                ViewBag.acLedgerId = new SelectList(db.AcLedger, "id", "accountledger", paymentEntry.acLedgerId);
                ViewBag.supplierId = new SelectList(db.Suppliers, "id", "name", paymentEntry.supplierId);
                return View(paymentEntry);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "PaymentEntries";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Create";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
        }


        // GET: PaymentEntries/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PaymentEntry paymentEntry = await db.PaymentEntries.FindAsync(id);
                if (paymentEntry == null)
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
                ViewBag.acLedgerId = new SelectList(db.AcLedger.OrderBy(x => x.accountledger), "id", "accountledger", paymentEntry.acLedgerId);
                ViewBag.supplierId = new SelectList(db.Suppliers.OrderBy(x => x.name), "id", "name", paymentEntry.supplierId);
                return View(paymentEntry);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "PaymentEntries";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Edit View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
        }

        // POST: PaymentEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Date,PaymentType,acLedgerId,description,Amount,supplierId,companyId")] PaymentEntry paymentEntry)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    paymentEntry.companyId = Display.companyId;
                    db.Entry(paymentEntry).State = EntityState.Modified;
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
                ViewBag.acLedgerId = new SelectList(db.AcLedger, "id", "accountledger", paymentEntry.acLedgerId);
                ViewBag.supplierId = new SelectList(db.Suppliers, "id", "name", paymentEntry.supplierId);
                return View(paymentEntry);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "PaymentEntries";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Edit";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
        }

        // GET: PaymentEntries/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                PaymentEntry paymentEntry = await db.PaymentEntries.FindAsync(id);
                if (paymentEntry == null)
                {
                    return HttpNotFound();
                }
                return View(paymentEntry);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "PaymentEntries";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Delete View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
        }

        // POST: PaymentEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                PaymentEntry paymentEntry = await db.PaymentEntries.FindAsync(id);
                db.PaymentEntries.Remove(paymentEntry);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "PaymentEntries";
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
