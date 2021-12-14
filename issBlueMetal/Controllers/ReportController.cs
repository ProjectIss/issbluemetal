using issBlueMetal.Custom;
using issBlueMetal.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace issBlueMetal.Controllers
{
    [CustomAuthorize(Roles = "Admin,Manager")]
    public class ReportController : Controller
    {
        private issModel db = new issModel();
        private errorLog errorLog = new errorLog();

        // GET: Report
        public ActionResult Index(int? id)
        {
            try
            {
                ViewBag.Id = id;
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "Receipt";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "index";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }

            return View();
        }

        public ActionResult SalesReport()
        {
            try
            {
                List<SelectListItem> customer = new List<SelectListItem>();
                customer.Add(new SelectListItem { Text = "All", Value = "0" });
                foreach (var item in db.Sales.GroupBy(x => x.customer.name).Select(g => g.FirstOrDefault()).ToList())
                {
                    customer.Add(new SelectListItem { Text = item.customer.name, Value = item.customer.name });

                }
                ViewBag.customers = customer;
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "SalesReport";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "index";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View(db.Sales.Where(x => x.Id == 0).ToList());
        }
        public ActionResult PaymentReport()
        {
            try
            {

            }
            catch (Exception ex)
            {

                errorLog.controllerName = "PaymentReport";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "index";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View(db.PaymentEntries.Where(x => x.Id == 0).ToList());
        }
        public ActionResult ReciptReport()
        {
            try
            {

            }
            catch (Exception ex)
            {

                errorLog.controllerName = "ReciptReport";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "index";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View(db.ReciptEntries.Where(x => x.Id == 0).ToList());
        }

        public ActionResult MaterialPurchase()
        {

            try
            {

                List<SelectListItem> supplier = new List<SelectListItem>();
                supplier.Add(new SelectListItem { Text = "All", Value = "0" });
                foreach (var item in db.RawMateriyalPurchases.GroupBy(x => x.Supplier).Select(g => g.FirstOrDefault()).ToList())
                {
                    supplier.Add(new SelectListItem { Text = item.Supplier.name, Value = item.Supplier.name });

                }
                ViewBag.customers = supplier;

            }
            catch (Exception ex)
            {

                errorLog.controllerName = "MaterialPurchase";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "index";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View(db.RawMateriyalPurchases.Where(x => x.id == 0).ToList());

        }
        [HttpPost]
        public ActionResult MaterialPurchase(string fromDate, string toDate, string id)
        {
            try
            {
                ViewBag.gTotalPaid = 0;
                ViewBag.Total = 0;
                List<SelectListItem> supplier = new List<SelectListItem>();
                supplier.Add(new SelectListItem { Text = "All", Value = "" });
                foreach (var item in db.RawMateriyalPurchases.GroupBy(x => x.Supplier).Select(g => g.FirstOrDefault()).ToList())
                {
                    supplier.Add(new SelectListItem { Text = item.Supplier.name, Value = item.Supplier.name });

                }
                ViewBag.customers = supplier;
                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                {
                    DateTime fDate = Convert.ToDateTime(fromDate);
                    DateTime tDate = Convert.ToDateTime(toDate);
                    if (!string.IsNullOrEmpty(id))
                    {

                        var data = db.RawMateriyalPurchases.Where(x => x.dateTime >= fDate && x.dateTime <= tDate && x.Supplier.name == id).ToList();
                        ViewBag.gTotalPaid = data.Sum(x => x.totalAmount);
                        ViewBag.Total = data.Sum(x => x.netAmount);
                        return View(data);
                    }
                    else if (string.IsNullOrEmpty(id))
                    {
                        var data = db.RawMateriyalPurchases.Where(x => x.dateTime >= fDate && x.dateTime <= tDate).ToList();
                        ViewBag.gTotalPaid = data.Sum(x => x.totalAmount);
                        ViewBag.Total = data.Sum(x => x.netAmount);
                        return View(data);
                    }
                }

                else return View(new RawMateriyalPurchase());

            }

            catch (Exception ex)
            {
            }
            return View(new RawMateriyalPurchase());

        }
        [HttpPost]
        public ActionResult MaterialReport(string fromDate, string toDate, int id)
        {
            try
            {

                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate) && id > 0)
                {
                    DateTime fDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                    DateTime tDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
                    var data = db.RawMateriyalPurchases.Where(x => x.dateTime >= fDate && x.dateTime <= tDate && x.id == id).ToList();

                    return View(data);
                }
                else return View(db.RawMateriyalPurchases.Where(x => x.id == 0).ToList());
            }

            catch (Exception ex)
            {
                return View(db.RawMateriyalPurchases.Where(x => x.id == 0).ToList());
            }

        }

        private object DataTableToJSON(object dtResult)
        {
            throw new NotImplementedException();
        }

        public ActionResult RawMaterial(int? id)
        {
            ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        public JsonResult getRawData(int id)
        {
            var RawData = db.RawMateriyalPurchases.Where(x => x.id == id).FirstOrDefault();

            return Json(RawData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SalesReport(string fromDate, string toDate, string id)
        {
            try
            {

                ViewBag.netAmount = 0;
                ViewBag.paidAmount = 0;
                ViewBag.balanceAmount = 0;
                List<SelectListItem> customer = new List<SelectListItem>();
                customer.Add(new SelectListItem { Text = "All", Value = "" });
                foreach (var item in db.Sales.GroupBy(x => x.customer.name).Select(g => g.FirstOrDefault()).ToList())
                {
                    customer.Add(new SelectListItem { Text = item.customer.name, Value = item.customer.name });

                }
                ViewBag.customers = customer;
                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                {
                    DateTime fDate = Convert.ToDateTime(fromDate);
                    DateTime tDate = Convert.ToDateTime(toDate);
                    if (!string.IsNullOrEmpty(id))
                    {
                        var data = db.Sales.Where(x => x.Date >= fDate && x.Date <= tDate && x.customer.name == id).ToList();
                        ViewBag.netAmount = data.Sum(x => x.netAmount);
                        ViewBag.paidAmount = data.Sum(x => x.paidAmount);
                        ViewBag.balanceAmount = data.Sum(x => x.balanceAmount);
                        return View(data);
                    }
                    else if (string.IsNullOrEmpty(id))
                    {
                        var data = db.Sales.Where(x => x.Date >= fDate && x.Date <= tDate).ToList();
                        ViewBag.netAmount = data.Sum(x => x.netAmount);
                        ViewBag.paidAmount = data.Sum(x => x.paidAmount);
                        ViewBag.balanceAmount = data.Sum(x => x.balanceAmount);
                        return View(data);
                    }
                }

                else return View(new Sales());

            }

            catch (Exception ex)
            {
            }
            return View(new Sales());

        }
        [HttpPost]
        public ActionResult PaymentReport(string fromDate, string toDate)
        {


            try
            {
                ViewBag.Total = 0;
                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                {
                    DateTime fDate = Convert.ToDateTime(fromDate);
                    DateTime tDate = Convert.ToDateTime(toDate);


                    var data = db.PaymentEntries.Where(x => x.Date >= fDate && x.Date <= tDate).ToList();
                    ViewBag.Total = data.Sum(x => x.Amount);
                    return View(data);

                }

                else return View(new PaymentEntry());

            }

            catch (Exception ex)
            {
            }
            return View(new PaymentEntry());



            //try
            //{
            //    if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            //    {
            //        DateTime fDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
            //        DateTime tDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
            //        var data = db.PaymentEntries.Where(x => x.Date >= fDate && x.Date <= tDate).ToList();
            //        return Json(data, JsonRequestBehavior.AllowGet);
            //    }
            //    else return Json("Faild", JsonRequestBehavior.AllowGet);
            //}

            //catch (Exception ex)
            //{
            //    return Json("Faild", JsonRequestBehavior.AllowGet);
            //}

        }
        [HttpPost]
        public ActionResult ReciptReport(string fromDate, string toDate)
        {

            try
            {
                ViewBag.Total = 0;
                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                {
                    DateTime fDate = Convert.ToDateTime(fromDate);
                    DateTime tDate = Convert.ToDateTime(toDate);


                    var data = db.ReciptEntries.Where(x => x.Date >= fDate && x.Date <= tDate).ToList();
                    ViewBag.Total = data.Sum(x => x.Amount);
                    return View(data);

                }

                else return View(new ReciptEntry());

            }

            catch (Exception ex)
            {
            }
            return View(new ReciptEntry());
            //try
            //{
            //    if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            //    {
            //        DateTime fDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
            //        DateTime tDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
            //        var data = db.ReciptEntries.Where(x => x.Date >= fDate && x.Date <= tDate).ToList();
            //        return Json(data, JsonRequestBehavior.AllowGet);
            //    }
            //    else return Json("Faild", JsonRequestBehavior.AllowGet);
            //}

            //catch (Exception ex)
            //{
            //    return Json("Faild", JsonRequestBehavior.AllowGet);
            //}


        }
        [HttpPost]
        public JsonResult getReceipt(int id)
        {
            var RawData = db.PaymentEntries.Where(x => x.Id == id).FirstOrDefault();

            return Json(RawData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SupplierLedger()
        {

            try
            {

                List<SelectListItem> supplier = new List<SelectListItem>();
                //supplier.Add(new SelectListItem { Text = "", Value = "0" });
                foreach (var item in db.supplierLedgers.GroupBy(x => x.SupplierLedger.name).Select(g => g.FirstOrDefault()).ToList())
                {
                    supplier.Add(new SelectListItem { Text = item.SupplierLedger.name, Value = item.SupplierLedger.name });

                }
                ViewBag.customers = supplier;

            }
            catch (Exception ex)
            {

                errorLog.controllerName = "MaterialPurchase";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "index";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View(db.supplierLedgers.Where(x => x.Id == 0).ToList());

        }

        [HttpPost]
        public ActionResult SupplierLedger(string fromDate, string toDate, string id)
        {

            try
            {

                List<SelectListItem> supplier = new List<SelectListItem>();
                //supplier.Add(new SelectListItem { Text = "", Value = "0" });
                foreach (var item in db.supplierLedgers.GroupBy(x => x.SupplierLedger.name).Select(g => g.FirstOrDefault()).ToList())
                {
                    supplier.Add(new SelectListItem { Text = item.SupplierLedger.name, Value = item.SupplierLedger.name });

                }
                ViewBag.customers = supplier;
                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                {
                    DateTime fDate = Convert.ToDateTime(fromDate);
                    DateTime tDate = Convert.ToDateTime(toDate);
                    tDate = tDate.AddDays(-1);
                    if (!string.IsNullOrEmpty(id))
                    {

                        var data = db.supplierLedgers.Where(x => x.dateOfPurchages >= fDate && x.dateOfPurchages <= tDate && x.SupplierLedger.name == id).ToList();
                        var gTotalPaid = db.supplierLedgers.Where(x => x.dateOfPurchages >= fDate && x.dateOfPurchages <= tDate && x.SupplierLedger.name == id).Sum((x => (decimal?)x.credit));
                        var Total = db.supplierLedgers.Where(x => x.dateOfPurchages >= fDate && x.dateOfPurchages <= tDate && x.SupplierLedger.name == id).Sum(x => (decimal?)x.debit);

                        var openingBalance = db.Suppliers.Where(x => x.name == id).FirstOrDefault();
                        decimal tCredit = 0;
                        decimal tDebit = 0;
                        if (gTotalPaid != null && gTotalPaid > 0) { tCredit = Convert.ToDecimal(gTotalPaid); }
                        if (Total != null && Total > 0) { tDebit = Convert.ToDecimal(Total); }
                        decimal resOpeningBalance = 0;
                        if (openingBalance.openingBalance != null)
                        {
                            resOpeningBalance = (tCredit - tDebit) + Convert.ToDecimal(openingBalance.openingBalance);
                        }
                        else resOpeningBalance = (tCredit - tDebit);
                        ViewBag.gTotalPaid = gTotalPaid;
                        ViewBag.Total = Total;
                        ViewBag.resOpeningBalance = resOpeningBalance;

                        var closingBalance = ((gTotalPaid) + (resOpeningBalance)) - (Total);
                        ViewBag.closingBalance = closingBalance;
                        return View(data);
                    }
                    else if (string.IsNullOrEmpty(id))
                    {
                        var data = db.supplierLedgers.Where(x => x.dateOfPurchages >= fDate && x.dateOfPurchages <= tDate).ToList();
                        var gTotalPaid = db.supplierLedgers.Where(x => x.dateOfPurchages < fDate && x.SupplierLedger.name == id).Sum((x => (decimal?)x.credit));
                        var Total = db.supplierLedgers.Where(x => x.dateOfPurchages < fDate && x.SupplierLedger.name == id).Sum(x => (decimal?)x.debit);

                        var openingBalance = db.Suppliers.Where(x => x.name == id).FirstOrDefault();
                        decimal tCredit = 0;
                        decimal tDebit = 0;
                        if (gTotalPaid != null && gTotalPaid > 0) { tCredit = Convert.ToDecimal(gTotalPaid); }
                        if (Total != null && Total > 0) { tDebit = Convert.ToDecimal(Total); }
                        decimal resOpeningBalance = 0;
                        if (openingBalance.openingBalance != null)
                        {
                            resOpeningBalance = (tCredit - tDebit) + Convert.ToDecimal(openingBalance.openingBalance);
                        }
                        else resOpeningBalance = (tCredit - tDebit);
                        ViewBag.gTotalPaid = gTotalPaid;
                        ViewBag.Total = Total;
                        ViewBag.resOpeningBalance = resOpeningBalance;
                        var closingBalance = ((gTotalPaid) + (resOpeningBalance)) - (Total);
                        ViewBag.closingBalance = closingBalance;
                        return View(data);
                    }
                }

                else return View(new supplierLedger());

            }

            catch (Exception ex)
            {
            }
            return View(new supplierLedger());
            //try
            //{
            //    if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate) && name > 0)
            //    {
            //        DateTime fDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
            //        DateTime tDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
            //        tDate = tDate.AddDays(1);
            //        var supplier = db.supplierLedgers.Where(x => x.dateOfPurchages >= fDate && x.dateOfPurchages <= tDate && x.supplierId == name).ToList();
            //        var totalCredit = db.supplierLedgers.Where(x => x.dateOfPurchages < fDate && x.supplierId == name).Sum((x => (decimal?)x.credit));
            //        var totalDebit = db.supplierLedgers.Where(x => x.dateOfPurchages < fDate && x.supplierId == name).Sum(x => (decimal?)x.debit);
            //        var openingBalance = db.Suppliers.Where(x => x.id == name).FirstOrDefault();
            //        // decimal oBalance = 0;
            //        decimal tCredit = 0;
            //        decimal tDebit = 0;
            //        if (totalCredit != null && totalCredit > 0) { tCredit = Convert.ToDecimal(totalCredit); }
            //        if (totalDebit != null && totalDebit > 0) { tDebit = Convert.ToDecimal(totalDebit); }
            //        decimal resOpeningBalance = 0;
            //        if (openingBalance.openingBalance != null)
            //        {
            //            resOpeningBalance = (tCredit - tDebit) + Convert.ToDecimal(openingBalance.openingBalance);
            //        }
            //        else resOpeningBalance = (tCredit - tDebit);
            //        var data = new { supplier, resOpeningBalance };
            //        return Json(data, JsonRequestBehavior.AllowGet);
            //    }
            //    else return Json("Faild", JsonRequestBehavior.AllowGet);
            //}

            //catch (Exception ex)
            //{
            //    return Json("Faild", JsonRequestBehavior.AllowGet);
            //}


        }
        public ActionResult CustomerLedger()
        {

            try
            {

                List<SelectListItem> supplier = new List<SelectListItem>();
                //supplier.Add(new SelectListItem { Text = "", Value = "0" });
                foreach (var item in db.customerLedgers.GroupBy(x => x.Customer.name).Select(g => g.FirstOrDefault()).ToList())
                {
                    supplier.Add(new SelectListItem { Text = item.Customer.name, Value = item.Customer.name });

                }
                ViewBag.customers = supplier;

            }
            catch (Exception ex)
            {

                errorLog.controllerName = "MaterialPurchase";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "index";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View(db.customerLedgers.Where(x => x.Id == 0).ToList());


        }



        [HttpPost]
        public ActionResult CustomerLedger(string fromDate, string toDate, string id)
        {

            try
            {

                List<SelectListItem> supplier = new List<SelectListItem>();
                //supplier.Add(new SelectListItem { Text = "", Value = "0" });
                foreach (var item in db.customerLedgers.GroupBy(x => x.Customer.name).Select(g => g.FirstOrDefault()).ToList())
                {
                    supplier.Add(new SelectListItem { Text = item.Customer.name, Value = item.Customer.name });

                }
                ViewBag.customers = supplier;
                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                {
                    DateTime fDate = Convert.ToDateTime(fromDate);
                    DateTime tDate = Convert.ToDateTime(toDate);
                    tDate = tDate.AddDays(-1);
                    if (!string.IsNullOrEmpty(id))
                    {

                        var data = db.customerLedgers.Where(x => x.dateOfPurchages >= fDate && x.dateOfPurchages <= tDate && x.Customer.name == id).ToList();
                        var gTotalPaid = db.customerLedgers.Where(x => x.dateOfPurchages >= fDate && x.dateOfPurchages <= tDate && x.Customer.name == id).Sum((x => (decimal?)x.credit));
                        var Total = db.customerLedgers.Where(x => x.dateOfPurchages >= fDate && x.dateOfPurchages <= tDate && x.Customer.name == id).Sum(x => (decimal?)x.debit);

                        var openingBalance = db.Customers.Where(x => x.name == id).FirstOrDefault();
                        decimal tCredit = 0;
                        decimal tDebit = 0;
                        if (gTotalPaid != null && gTotalPaid > 0) { tCredit = Convert.ToDecimal(gTotalPaid); }
                        if (Total != null && Total > 0) { tDebit = Convert.ToDecimal(Total); }
                        decimal resOpeningBalance = 0;
                        if (openingBalance.openingBalance != null)
                        {
                            resOpeningBalance = (tCredit - tDebit) + Convert.ToDecimal(openingBalance.openingBalance);
                        }
                        else resOpeningBalance = (tCredit - tDebit);
                        ViewBag.gTotalPaid = gTotalPaid;
                        ViewBag.Total = Total;
                        ViewBag.resOpeningBalance = resOpeningBalance;

                        var closingBalance = ((gTotalPaid) + (resOpeningBalance)) - (Total);
                        ViewBag.closingBalance = closingBalance;
                        return View(data);
                    }
                    else if (string.IsNullOrEmpty(id))
                    {
                        var data = db.customerLedgers.Where(x => x.dateOfPurchages >= fDate && x.dateOfPurchages <= tDate).ToList();
                        var gTotalPaid = db.customerLedgers.Where(x => x.dateOfPurchages < fDate && x.Customer.name == id).Sum((x => (decimal?)x.credit));
                        var Total = db.customerLedgers.Where(x => x.dateOfPurchages < fDate && x.Customer.name == id).Sum(x => (decimal?)x.debit);

                        var openingBalance = db.Customers.Where(x => x.name == id).FirstOrDefault();
                        decimal tCredit = 0;
                        decimal tDebit = 0;
                        if (gTotalPaid != null && gTotalPaid > 0) { tCredit = Convert.ToDecimal(gTotalPaid); }
                        if (Total != null && Total > 0) { tDebit = Convert.ToDecimal(Total); }
                        decimal resOpeningBalance = 0;
                        if (openingBalance.openingBalance != null)
                        {
                            resOpeningBalance = (tCredit - tDebit) + Convert.ToDecimal(openingBalance.openingBalance);
                        }
                        else resOpeningBalance = (tCredit - tDebit);
                        ViewBag.gTotalPaid = gTotalPaid;
                        ViewBag.Total = Total;
                        ViewBag.resOpeningBalance = resOpeningBalance;
                        var closingBalance = ((gTotalPaid) + (resOpeningBalance)) - (Total);
                        ViewBag.closingBalance = closingBalance;
                        return View(data);
                    }
                }

                else return View(new customerLedger());

            }

            catch (Exception ex)
            {
            }
            return View(new customerLedger());

        }
        public ActionResult DayBook()
        {
            return View();
        }
        public ActionResult HotelsBook()
        {
            return View();
        }

        [HttpPost]
        public JsonResult DayBookReport(string Date)
        {
            List<dayBook> dayBooks = new List<dayBook>();
            DateTime todayDate = Convert.ToDateTime(Date);
            decimal totalIncome = 0, totalPaid = 0, openingBalance = 0, todayIncome = 0, todayExpenses = 0, closingBalance = 0;
            var rsales = db.Sales.Where(x => x.Date == todayDate && x.paidAmount > 0).ToList();
            var Rrecipt = db.ReciptEntries.Where(x => x.Date == todayDate && x.Amount > 0).ToList();
            var rpayment = db.PaymentEntries.Where(x => x.Date == todayDate && x.Amount > 0).ToList();
            var rpaid = db.RawMateriyalPurchases.Where(x => x.dateTime == todayDate).ToList();

            var sales = db.Sales.Where(x => x.Date < todayDate).ToList();
            var recipt = db.ReciptEntries.Where(x => x.Date < todayDate).ToList();
            var payment = db.PaymentEntries.Where(x => x.Date < todayDate).ToList();
            var paid = db.RawMateriyalPurchases.Where(x => x.dateTime < todayDate).ToList();

            foreach (var item in sales)
            {
                totalIncome += Convert.ToDecimal(item.paidAmount);
            }
            foreach (var sa in recipt)
            {
                totalIncome += Convert.ToDecimal(sa.Amount);
            }
            //foreach (var pa in paid)
            //{
            //    totalPaid += Convert.ToDecimal(pa.netAmount);
            //}
            foreach (var pay in payment)
            {
                totalPaid += Convert.ToDecimal(pay.Amount);
            }

            int i = 1;
            foreach (var item in rsales)
            {
                dayBook newbook = new dayBook();
                newbook.Date = todayDate;
                newbook.BillID = item.billNo;
                if (item.Item != null)
                {
                    newbook.Description = item.Item.name;
                }
                newbook.Expenace = "0.00";
                newbook.Type = "sales";
                newbook.Income = item.paidAmount.ToString();
                todayIncome += Convert.ToDecimal(item.paidAmount);
                dayBooks.Add(newbook);
                i++;
            }
            foreach (var sa in Rrecipt)
            {
                dayBook newbook = new dayBook();
                newbook.Date = todayDate;
                newbook.BillID = sa.Id;
                if (sa.AcLedger != null)
                {
                    newbook.Description = sa.AcLedger.accountledger;
                }
                newbook.Expenace = "0.00";
                newbook.Type = "Recipt";
                newbook.Income = sa.Amount.ToString();
                todayIncome += Convert.ToDecimal(sa.Amount);
                dayBooks.Add(newbook);
                i++;
            }
            //foreach (var pa in rpaid)
            //{
            //    dayBook newbook = new dayBook();
            //    newbook.Date = todayDate;
            //    newbook.BillID = pa.id;
            //    newbook.Description = pa.materialName.ToString();
            //    newbook.Expenace = pa.netAmount.ToString();
            //    newbook.Type = "Paid";
            //    newbook.Income = "0.00";
            //    todayExpenses += Convert.ToDecimal(pa.netAmount);
            //    dayBooks.Add(newbook);
            //    i++;
            //}
            foreach (var pay in rpayment)
            {
                dayBook newbook = new dayBook();
                newbook.Date = todayDate;
                newbook.BillID = pay.Id;
                if (pay.Supplier != null)
                {
                    newbook.Description = pay.Supplier.name;
                }
                newbook.Expenace = pay.Amount.ToString();
                newbook.Type = "Paid";
                newbook.Income = "0.00";
                todayExpenses += Convert.ToDecimal(pay.Amount);
                dayBooks.Add(newbook);
                i++;
            }
            openingBalance = totalIncome - totalPaid;
            closingBalance = (openingBalance + todayIncome) - todayExpenses;
            var data = new { dayBooks, openingBalance, closingBalance };
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult HotelBookReport(string Date)
        {


            try
            {

                if (!string.IsNullOrEmpty(Date))
                {
                    DateTime todayDate = Convert.ToDateTime(Date);

                    var dayBooks = db.Hotels.Where(x => x.date == todayDate).ToList();

                    todayDate = todayDate.AddDays(-1);
                    decimal totalIncome = 0, totalPaid = 0, openingBalance = 0, closingBalance = 0, todayIncome = 0, todayExpenses = 0;
                    var paid = db.Hotels.Where(x => x.date <= todayDate && x.mode == "Cash").ToList();
                    foreach (var item in paid)
                    {
                        totalIncome += item.income;
                        totalPaid += item.expence;

                    }
                    DateTime todayd = Convert.ToDateTime(Date);
                    var today = db.Hotels.Where(x => x.date == todayd && x.mode == "Cash").ToList();
                    foreach (var day in today)
                    {
                        todayIncome += day.income;
                        todayExpenses += day.expence;
                    }
                    openingBalance = totalIncome - totalPaid;
                    closingBalance = (openingBalance + todayIncome) - todayExpenses;

                    openingBalance = totalIncome - totalPaid;
                    var data = new { dayBooks, openingBalance, closingBalance };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }


            }

            catch (Exception ex)
            {
            }
            return Json(JsonRequestBehavior.AllowGet);



        }
        public ActionResult TblGRC()
        {
            try
            {

            }
            catch (Exception ex)
            {

                errorLog.controllerName = "TblGRC";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "index";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View(db.TblGRCs.Where(x => x.id == 0).ToList());
        }
        [HttpPost]
        public ActionResult TblGRC(string fromDate, string toDate)
        {
            try
            {

                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                {
                    DateTime fDate = Convert.ToDateTime(fromDate);
                    DateTime tDate = Convert.ToDateTime(toDate);


                    var data = db.TblGRCs.Where(x => x.grcDate >= fDate && x.grcDate <= tDate).ToList();
                    //ViewBag.Total = data.Sum(x => x.Amount);
                    return View(data);

                }

                else return View(new TblGRC());

            }

            catch (Exception ex)
            {
            }
            return View(new TblGRC());


            //DateTime fDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
            //DateTime tDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
            //var TblGRC = db.TblGRCs.Where(x => x.grcDate >= fDate && x.grcDate <= tDate).ToList();

            //return Json(TblGRC, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RoomStatus()
        {
            return View();
        }
        [HttpPost]
        public JsonResult RoomStatuss()
        {
            try
            {
                DateTime todayDate = DateTime.Today;

                var dayBooks = db.TblGRCs.Where(x => x.status == "Pending").ToList();





                return Json(dayBooks, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                errorLog.controllerName = "Home";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "getOBHotels";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();

            }
            return Json("Invalid", JsonRequestBehavior.AllowGet);
        }
        public ActionResult CustomerConsolidate()
        {

            try
            {


            }
            catch (Exception ex)
            {

                errorLog.controllerName = "MaterialPurchase";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "index";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View(db.Sales.Where(x => x.Id == 0).ToList());

        }
        [HttpPost]
        public JsonResult CustomerConsolidate(string fromDate, string toDate)

        {
            try
            {
                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                {
                    DateTime fDate = Convert.ToDateTime(fromDate);
                    DateTime tDate = Convert.ToDateTime(toDate);
                    var data = db.Sales.Where(x => x.Date >= fDate && x.Date <= tDate).ToList();
                    int amount = 0;
                    List<Sales> lstSales = new List<Sales>();
                    foreach (var item in data)
                    {
                        Sales sales = new Sales();
                        sales.netAmount = data.Where(x => x.customer.name == item.customer.name).Sum(x => x.netAmount);
                        sales.customer = item.customer;
                        sales.Item = item.Item;
                        //var ll = lstSales.Where(x => x.customer.name == item.customer.name).ToList();
                        //if (ll.Count > 0)
                        //{
                        //    lstSales.Add()
                        //}
                        lstSales.Add(sales);
                    }
                    var responesData = lstSales.GroupBy(x => x.customer.name).ToList();
                   
                    //for (int i = 0; i < data.Count; i++)
                    //{
                    //    Sales sales = new Sales();

                    //    amount = data[i].netAmount != null ? Convert.ToInt32(data[i].netAmount) : 0; 
                    //    if (i > 0)
                    //    {
                    //        if (data[i].customer.name == data[i - 1].customer.name)
                    //        {
                    //           int a = data[i].netAmount != null ? Convert.ToInt32(data[i].netAmount) : 0;
                    //            amount += a;
                    //            if (lstSales[i-1].customer.name==data[i].customer.name)
                    //            {
                    //                var sl = lstSales.Where(x => x.customer.name==)
                    //            }
                    //        }
                    //        else
                    //        {
                    //            amount = 0;
                    //        }
                    //    }
                    //    sales.netAmount = amount;
                    //    sales.customer = data[i].customer;
                    //    sales.Item = data[i].Item;
                    //    lstSales.Add(sales);
                    //}

                    return Json(responesData, JsonRequestBehavior.AllowGet);
                }
                else return Json("Faild", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Faild", JsonRequestBehavior.AllowGet);
            }

        }



        public ActionResult SupplierConsolidate()
        {
            try
            {


            }
            catch (Exception ex)
            {

                errorLog.controllerName = "MaterialPurchase";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "index";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View(db.RawMateriyalPurchases.Where(x => x.id == 0).ToList());

        }
        [HttpPost]
        public ActionResult SupplierConsolidate(string fromDate, string toDate)
        {
            try
            {
                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                {
                    DateTime fDate = Convert.ToDateTime(fromDate);
                    DateTime tDate = Convert.ToDateTime(toDate);
                    var data = db.RawMateriyalPurchases.Where(x => x.dateTime >= fDate && x.dateTime <= tDate).ToList();
                  
                    List<RawMateriyalPurchase> lstRawMateriyalPurchase = new List<RawMateriyalPurchase>();
                    foreach (var item in data)
                    {
                        RawMateriyalPurchase RawMateriyalPurchase = new RawMateriyalPurchase();
                        RawMateriyalPurchase.netAmount = data.Where(x => x.Supplier.name == item.Supplier.name).Sum(x => x.netAmount);
                        RawMateriyalPurchase.Supplier = item.Supplier;
                        RawMateriyalPurchase.materialName = item.materialName;
                       
                        lstRawMateriyalPurchase.Add(RawMateriyalPurchase);
                    }
                    var responesData = lstRawMateriyalPurchase.GroupBy(x => x.Supplier.name).ToList();


                    return Json(responesData, JsonRequestBehavior.AllowGet);
                }
                else return Json("Faild", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json("Faild", JsonRequestBehavior.AllowGet);
            }




        }

    }
    public class dayBook
    {
        public DateTime Date { get; set; }
        public int BillID { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Expenace { get; set; }
        public string Income { get; set; }

    }
}