using issBlueMetal.Custom;
using issBlueMetal.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace issBlueMetal.Controllers
{
    public class AccountGroupsController : Controller
    {
        private issModel db = new issModel();
        private errorLog errorLog = new errorLog();
        // GET: AccountGroups
        public async Task<ActionResult> Index()
        {
            try
            {
                return View(await db.AccountGroups.OrderByDescending(x => x.id).ToListAsync());
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "AccountGroup";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "index";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
           
        }

        // GET: AccountGroups/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AccountGroup accountGroup = await db.AccountGroups.FindAsync(id);
                if (accountGroup == null)
                {
                    return HttpNotFound();
                }
                return View(accountGroup);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "AccountGroup";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Details";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
           
        }

        // GET: AccountGroups/Create
        public ActionResult Create()
        {
            try
            {

            }
            catch (Exception ex)
            {

                errorLog.controllerName = "AccountGroup";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Create View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
        }

        // POST: AccountGroups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,accountGrouop,parentGroup")] AccountGroup accountGroup)
        {
            try
            {
                var id = db.AccountGroups.Where(x => x.accountGrouop == accountGroup.accountGrouop).Select(x => x.accountGrouop).FirstOrDefault();
                if (ModelState.IsValid && id == null)
                {

                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "AccountGroup";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Create";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
           

            return View(accountGroup);
        }

        // GET: AccountGroups/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AccountGroup accountGroup = await db.AccountGroups.FindAsync(id);
                if (accountGroup == null)
                {
                    return HttpNotFound();
                }
                return View(accountGroup);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "AccountGroup";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Edit View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();

        }

        // POST: AccountGroups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,accountGrouop,parentGroup")] AccountGroup accountGroup)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    accountGroup.companyId = Display.companyId;
                    db.Entry(accountGroup).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "AccountGroup";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Edit";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
           
            return View(accountGroup);
        }

        // GET: AccountGroups/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AccountGroup accountGroup = await db.AccountGroups.FindAsync(id);
                if (accountGroup == null)
                {
                    return HttpNotFound();
                }
                return View(accountGroup);
            }
            catch (Exception ex)
            {
                errorLog.controllerName = "AccountGroup";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Delete View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();

            }
            return View();

        }

        // POST: AccountGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                AccountGroup accountGroup = await db.AccountGroups.FindAsync(id);
                db.AccountGroups.Remove(accountGroup);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                errorLog.controllerName = "AccountGroup";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Delete";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
           
            return RedirectToAction("Index");
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
