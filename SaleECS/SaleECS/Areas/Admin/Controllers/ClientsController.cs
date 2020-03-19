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
    public class ClientsController : Controller
    {
        private ECSDBEntities db = new ECSDBEntities();

        [Authorize(Roles = "Admin,Service,HR,Auditors")]
        // GET: Admin/Clients
        public ActionResult Index()
        {
            List<Company> companyLst = db.Companys.ToList();
            ViewBag.ListOfCompany = new SelectList(companyLst, "CompanyId", "CompanyName");
            //var clients = db.Clients.Include(c => c.Company);
            return View();
        }
        public ActionResult GetList()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<ClientViewModel> clientLst = db.Clients.Select(x => new ClientViewModel
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                Phone = x.Phone,
                Gender = x.Gender,
                Address = x.Address,
                Status = x.Status,
                CompanyName = x.Company.CompanyName
            }).ToList();
            return Json(new { data = clientLst }, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/Clients/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Admin/Clients/Create
        public ActionResult Create()
        {
            ViewBag.CompanyId = new SelectList(db.Companys, "Id", "CompanyName");
            return View();
        }

        // POST: Admin/Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FullName,Email,Phone,Gender,Address,CompanyId")] Client client)
        {

            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                db.SaveChanges();
                SetAlert("Create successfully", "success");
                return RedirectToAction("Index");
            }

            ViewBag.CompanyId = new SelectList(db.Companys, "Id", "CompanyName", client.CompanyId);
            return View(client);
        }

        // GET: Admin/Clients/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            ViewBag.CompanyId = new SelectList(db.Companys, "Id", "CompanyName", client.CompanyId);
            return View(client);
        }

        // POST: Admin/Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FullName,Email,Phone,Gender,Address,CompanyId")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Update successfully", "success");
                return RedirectToAction("Index");
            }
            ViewBag.CompanyId = new SelectList(db.Companys, "Id", "CompanyName", client.CompanyId);
            return View(client);
        }

        //// GET: Admin/Clients/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Client client = db.Clients.Find(id);
        //    if (client == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(client);
        //}

        //// POST: Admin/Clients/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Client client = db.Clients.Where(x => x.Id == id).FirstOrDefault<Client>();
            db.Clients.Remove(client);
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
