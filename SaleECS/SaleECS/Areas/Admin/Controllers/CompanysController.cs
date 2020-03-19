using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SaleECS.Models;

namespace SaleECS.Areas.Admin.Controllers
{
    public class CompanysController : Controller
    {
        private ECSDBEntities db = new ECSDBEntities();
        [Authorize(Roles = "Admin,Service,HR,Auditors")]
        // GET: Admin/Company
        public ActionResult Index()
        {
            return View(db.Companys.ToList());
        }
        public ActionResult GetList()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var companies = db.Companys.ToList<Company>();
            return Json(new { data = companies }, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/Companys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companys.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // GET: Admin/Companys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Companys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CompanyName,Description,Phone,Address")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Companys.Add(company);
                db.SaveChanges();
                SetAlert("Create successfully", "success");
                return RedirectToAction("Index");
            }

            return View(company);
        }

        // GET: Admin/Companys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Company company = db.Companys.Find(id);
            if (company == null)
            {
                return HttpNotFound();
            }
            return View(company);
        }

        // POST: Admin/Companys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CompanyName,Description,Phone,Address")] Company company)
        {
            if (ModelState.IsValid)
            {
                db.Entry(company).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Update successfully", "success");
                return RedirectToAction("Index");
            }
            return View(company);
        }

        // GET: Admin/Companys/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Company company = db.Companys.Find(id);
        //    if (company == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(company);
        //}

        //// POST: Admin/Companys/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]

        public ActionResult Delete(int id)
        {
            Company company = db.Companys.Where(x => x.Id == id).FirstOrDefault<Company>();
            db.Companys.Remove(company);
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
