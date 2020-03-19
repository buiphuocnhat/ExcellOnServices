using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SaleECS.Models;
using SaleECS.ViewModels;

namespace SaleECS.Areas.Admin.Controllers
{
    public class OrderServicesController : Controller
    {
        private ECSDBEntities db = new ECSDBEntities();
        [Authorize(Roles = "Admin,Service")]

        // GET: Admin/OrderServices
        public ActionResult Index()
        {
          
            return View();
        }
     
        public ActionResult GetList()
        {
            db.Configuration.ProxyCreationEnabled = false;
            //var orderServices = db.OrderServices.ToList<OrderService>();
            List<OrderServiceViewModel> OrderLst = db.OrderServices.Select(x => new OrderServiceViewModel
            {
                Id = x.Id,
                Code = x.Code,
                PaymentDate = x.PaymentDate,
                PaymentLate = x.PaymentLate,
                TotalPrice = x.TotalPrice,
                Tax = x.Tax,
                Description = x.Description,
                Status = x.Status,
                CompanyName = x.Company.CompanyName,
                EmployeeName = x.Employee.FirstName,
            }).ToList();
            return Json(new { data = OrderLst }, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/OrderServices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderService orderService = db.OrderServices.Find(id);
            if (orderService == null)
            {
                return HttpNotFound();
            }
            return View(orderService);
        }

        // GET: Admin/OrderServices/Create
        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(db.Companys, "Id", "CompanyName");
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "LoginName");
            return View();
        }

        // POST: Admin/OrderServices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,PaymentDate,PaymentLate,TotalPrice,Tax,Description,Status,CompanyId,EmployeeId")] OrderService orderService)
        {
            if (ModelState.IsValid)
            {
                db.OrderServices.Add(orderService);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(db.Companys, "Id", "CompanyName", orderService.CompanyId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "LoginName", orderService.EmployeeId);
            return View(orderService);
        }

        // GET: Admin/OrderServices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderService orderService = db.OrderServices.Find(id);
            if (orderService == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companys, "Id", "CompanyName", orderService.CompanyId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "LoginName", orderService.EmployeeId);
            return View(orderService);
        }

        // POST: Admin/OrderServices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,PaymentDate,PaymentLate,TotalPrice,Tax,Description,Status,CompanyId,EmployeeId")] OrderService orderService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderService).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Update successfully", "success");
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Companys, "Id", "CompanyName", orderService.CompanyId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "LoginName", orderService.EmployeeId);
            return View(orderService);
        }


        // POST: Admin/OrderServices/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            OrderService orderService = db.OrderServices.Where(x => x.Id == id).FirstOrDefault<OrderService>();
            db.OrderServices.Remove(orderService);
            db.SaveChanges();
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
        protected void SetAlert(string message, string type)
        {
            TempData["AlertMessage"] = message;
            if (type == "success")
            {
                TempData["AlertType"] = "alert-success";
            }
            else if (type == "Warning")
            {
                TempData["AlertType"] = "alert-warning";
            }
            else if (type == "error")
            {
                TempData["AlertType"] = "alert-danger";
            }
        }
    }
}
