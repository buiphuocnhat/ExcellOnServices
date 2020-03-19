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
    public class DealersController : Controller
    {
        private ECSDBEntities db = new ECSDBEntities();

    [Authorize(Roles = "Admin,Service,HR,Auditors")]
        // GET: Admin/Dealers
        public ActionResult Index()
        {
            var dealers = db.Dealers.Include(d => d.Company);
            return View(dealers.ToList());
        }
        public ActionResult GetList()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var del = db.Dealers.ToList<Dealer>();
            return Json(new { data = del }, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/Dealers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dealer dealer = db.Dealers.Find(id);
            if (dealer == null)
            {
                return HttpNotFound();
            }
            return View(dealer);
        }

        // GET: Admin/Dealers/Create
        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(db.Companys, "Id", "CompanyName");
            return View();
        }

        // POST: Admin/Dealers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DealerName,Phone,Email,Address,CompanyId")] Dealer dealer)
        {
            if (ModelState.IsValid)
            {
                db.Dealers.Add(dealer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(db.Companys, "Id", "CompanyName", dealer.CompanyId);
            return View(dealer);
        }

        // GET: Admin/Dealers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dealer dealer = db.Dealers.Find(id);
            if (dealer == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companys, "Id", "CompanyName", dealer.CompanyId);
            return View(dealer);
        }

        // POST: Admin/Dealers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DealerName,Phone,Email,Address,CompanyId")] Dealer dealer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dealer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Companys, "Id", "CompanyName", dealer.CompanyId);
            return View(dealer);
        }

        // GET: Admin/Dealers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dealer dealer = db.Dealers.Find(id);
            if (dealer == null)
            {
                return HttpNotFound();
            }
            return View(dealer);
        }

        // POST: Admin/Dealers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dealer dealer = db.Dealers.Find(id);
            db.Dealers.Remove(dealer);
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
    }
}
