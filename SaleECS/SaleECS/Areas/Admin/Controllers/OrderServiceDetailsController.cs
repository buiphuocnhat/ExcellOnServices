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
    public class OrderServiceDetailsController : Controller
    {
        private ECSDBEntities db = new ECSDBEntities();
        [Authorize(Roles = "Admin,Service")]

        // GET: Admin/OrderServiceDetails
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetList()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<OrderServiceDetailViewModel> DetailLst = db.OrderServiceDetails.Select(x => new OrderServiceDetailViewModel
            {
                Id = x.Id,
                Price = x.Price,
                Quantity = x.Quantity,
                Code = x.OrderService.Code,
                ServiceName = x.Service.ServiceName,
                Status = x.Status
            }).ToList();
            return Json(new { data = DetailLst }, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/OrderServiceDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderServiceDetail orderServiceDetail = db.OrderServiceDetails.Find(id);
            if (orderServiceDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderServiceDetail);
        }

        // GET: Admin/OrderServiceDetails/Create
        public ActionResult Create()
        {
            ViewBag.OrderServiceId = new SelectList(db.OrderServices, "Id", "Code");
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "ServiceName");
            return View();
        }

        // POST: Admin/OrderServiceDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Price,Quantity,OrderServiceId,ServiceId,Status")] OrderServiceDetail orderServiceDetail)
        {
            if (ModelState.IsValid)
            {
                db.OrderServiceDetails.Add(orderServiceDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderServiceId = new SelectList(db.OrderServices, "Id", "Code", orderServiceDetail.OrderServiceId);
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "ServiceName", orderServiceDetail.ServiceId);
            return View(orderServiceDetail);
        }

        // GET: Admin/OrderServiceDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderServiceDetail orderServiceDetail = db.OrderServiceDetails.Find(id);
            if (orderServiceDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderServiceId = new SelectList(db.OrderServices, "Id", "Code", orderServiceDetail.OrderServiceId);
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "ServiceName", orderServiceDetail.ServiceId);
            return View(orderServiceDetail);
        }

        // POST: Admin/OrderServiceDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Price,Quantity,OrderServiceId,ServiceId,Status")] OrderServiceDetail orderServiceDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderServiceDetail).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Update successfully", "success");
                return RedirectToAction("Index");
            }
            ViewBag.OrderServiceId = new SelectList(db.OrderServices, "Id", "Code", orderServiceDetail.OrderServiceId);
            ViewBag.ServiceId = new SelectList(db.Services, "Id", "ServiceName", orderServiceDetail.ServiceId);
            return View(orderServiceDetail);
        }

        // GET: Admin/OrderServiceDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderServiceDetail orderServiceDetail = db.OrderServiceDetails.Find(id);
            if (orderServiceDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderServiceDetail);
        }

        // POST: Admin/OrderServiceDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderServiceDetail orderServiceDetail = db.OrderServiceDetails.Find(id);
            db.OrderServiceDetails.Remove(orderServiceDetail);
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
