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
    public class UsersController : Controller
    {
        private issModel db = new issModel();
        private errorLog errorLog = new errorLog();
        // GET: Users
        public async Task<ActionResult> Index()
        {
            try
            {
                return View(await db.Users.ToListAsync());
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Users";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "index";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = await db.Users.FindAsync(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Users";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Details";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
            
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            try
            {
                ViewBag.companyId = new SelectList(db.Companies.OrderBy(x => x.name), "id", "name");

            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Users";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Create View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,name,username,password,role,status,lastLogin,companyId")] User user)
        {
            try
            {
                // var id = db.Users.Where(x => x.user == user.user).Select(x => x.user).FirstOrDefault();
                if (ModelState.IsValid)
                {

                    db.Users.Add(user);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                ViewBag.companyId = new SelectList(db.Companies, "id", "name", user.companyId);
                return View(user);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Users";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Create";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
           
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = await db.Users.FindAsync(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                ViewBag.companyId = new SelectList(db.Companies, "id", "name", user.companyId);
                return View(user);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Users";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Edit View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
           
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,name,username,password,role,status,lastLogin,companyId")] User user)
        {
            try
            {
                //  var id = db.Users.Where(x => x.user == user.user).Select(x => x.user).FirstOrDefault();
                if (ModelState.IsValid)
                {
                    db.Entry(user).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                ViewBag.companyId = new SelectList(db.Companies, "id", "name", user.companyId);
                return View(user);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Users";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Edit";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
          
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                User user = await db.Users.FindAsync(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                return View(user);
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Users";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Delete View";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
           
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                User user = await db.Users.FindAsync(id);
                db.Users.Remove(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Users";
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
