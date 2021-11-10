using issBlueMetal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace issBlueMetal.Controllers
{

    public class HomeController : Controller
    {
        private issModel db = new issModel();
        private errorLog errorLog = new errorLog();
        public ActionResult Index()
        {
            try
            {

            }
            catch (Exception ex)
            {

                errorLog.controllerName = "HomeController";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "index";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }
            return View();
        }

        public ActionResult About()
        {
            try
            {
                ViewBag.Message = "Your application description page.";
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "HomeController";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "About";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }


            return View();
        }

        public ActionResult Contact()
        {
            try
            {
                ViewBag.Message = "Your contact page.";
            }
            catch (Exception ex)
            {

                errorLog.controllerName = "HomeController";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "Contact";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();
            }


            return View();
        }
        [HttpGet]
        public JsonResult getRecivedAmount()
        {

            decimal totalRecivedAmount = 0;
            var todayDate = DateTime.Now;
            var recievedAmount = db.ReciptEntries.Where(x => x.Date == todayDate.Date && x.companyId == Custom.Display.companyId).ToList();
            var paidAmount = db.Sales.Where(x => x.Date == todayDate.Date).ToList();
            if (recievedAmount.Count > 0)
            {
                foreach (var item in recievedAmount)
                {
                    if (item.Amount != null && item.Amount > 0)
                    {
                        totalRecivedAmount += item.Amount;
                    }

                }
            }
            if (paidAmount.Count > 0)
            {
                foreach (var item in paidAmount)
                {
                    if (item.paidAmount != null && item.paidAmount > 0)
                    {
                        totalRecivedAmount += Convert.ToDecimal(item.paidAmount);
                    }

                }
            }
            return Json(totalRecivedAmount, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult getPaidAmount()
        {
            decimal totalPaidAmount = 0;
            var todayDate = DateTime.Now;
            int cid = Custom.Display.companyId;
            var recievedAmount = db.PaymentEntries.Where(x => x.Date == todayDate.Date && x.companyId == cid).ToList();
            if (recievedAmount.Count > 0)
            {
                foreach (var item in recievedAmount)
                {
                    if (item.Amount != null && item.Amount > 0)
                    {
                        totalPaidAmount += item.Amount;
                    }
                }
            }
            return Json(totalPaidAmount, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getOB()
        {
            try
            {
                string date = DateTime.Now.AddDays(-1).ToShortDateString();
                DateTime todayDate = Convert.ToDateTime(date);
                //todayDate = todayDate.AddDays(-1);
                decimal totalIncome = 0, totalPaid = 0, closingBalance = 0, openingBalance = 0, todayIncome = 0, TodayPaid = 0;
                var sales = db.Sales.Where(x => x.Date <= todayDate).ToList();
                var recipt = db.ReciptEntries.Where(x => x.Date <= todayDate.Date).ToList();
                var payment = db.PaymentEntries.Where(x => x.Date <= todayDate.Date).ToList();
                //var paid = db.RawMateriyalPurchases.Where(x => x.dateTime <= todayDate).ToList();

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
                openingBalance = totalIncome - totalPaid;
                var currentDate = DateTime.Now.ToShortDateString();
                DateTime d = Convert.ToDateTime(currentDate);
                var tsales = db.Sales.Where(x => x.Date == d).ToList();
                var trecipt = db.ReciptEntries.Where(x => x.Date == d).ToList();
                var tpayment = db.PaymentEntries.Where(x => x.Date == d).ToList();
                //var tpaid = db.RawMateriyalPurchases.Where(x => x.dateTime == d).ToList();
                foreach (var item in tsales)
                {
                    todayIncome += Convert.ToDecimal(item.paidAmount);
                }
                foreach (var sa in trecipt)
                {
                    todayIncome += Convert.ToDecimal(sa.Amount);
                }
                //foreach (var pa in tpaid)
                //{
                //    TodayPaid += Convert.ToDecimal(pa.netAmount);
                //}
                foreach (var pay in tpayment)
                {
                    TodayPaid += Convert.ToDecimal(pay.Amount);
                }
                closingBalance = (openingBalance + todayIncome) - TodayPaid;
                var response = new { openingBalance, todayIncome, closingBalance, TodayPaid };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                errorLog.controllerName = "Home";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "getOB";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();

            }
            return Json("Invalid", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getOBHotel(string tdate)
        {
            try
            {
                string date = DateTime.Now.AddDays(-1).ToShortDateString();
                DateTime todayDate = Convert.ToDateTime(date);
                decimal totalIncome = 0, totalPaid = 0, openingBalance = 0, closingBalance = 0, todayIncome = 0, todayExpenses = 0;
                var paid = db.Hotels.Where(x => x.date <= todayDate && x.mode == "Cash").ToList();
                foreach (var item in paid)
                {
                    totalIncome += item.income;
                    totalPaid += item.expence;

                }
                DateTime todayd = Convert.ToDateTime(tdate);
                var today = db.Hotels.Where(x => x.date == todayd && x.mode == "Cash").ToList();
                foreach (var day in today)
                {
                    todayIncome += day.income;
                    todayExpenses += day.expence;
                }
                openingBalance = totalIncome - totalPaid;
                closingBalance = (openingBalance + todayIncome) - todayExpenses;
                var response = new { openingBalance, todayIncome, todayExpenses, closingBalance };
                return Json(response, JsonRequestBehavior.AllowGet);
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
        [HttpGet]
        public JsonResult RoomStatus()
        {
            try
            {
                DateTime todayDate = DateTime.Today;
                decimal booking = 0, vacancy = 0, housKeeping = 0;
                var paid = db.RoomStatus.FirstOrDefault();
                booking = paid.booking; vacancy = paid.vacancy; housKeeping = paid.housKeeping;
                var response = new { booking, vacancy, housKeeping };
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                errorLog.controllerName = "Home";
                errorLog.ErrorDate = DateTime.Now;
                errorLog.MethodName = "RoomStatus";
                errorLog.ErrorMessage = ex.Message;
                db.errorLogs.Add(errorLog);
                db.SaveChanges();

            }
            return Json("Invalid", JsonRequestBehavior.AllowGet);
        }

    }
}
