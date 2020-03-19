using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SaleECS.Models;
using SaleECS.ViewModels;

namespace SaleECS.Areas.Admin.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ECSDBEntities db = new ECSDBEntities();
        

    [Authorize(Roles = "Admin,Service,HR,Auditors")]
        // GET: Admin/Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Client);
            return View(products.ToList());
        }
        public ActionResult GetList()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<ProductViewModel> productLst = db.Products.Select(x => new ProductViewModel
            {
                Id = x.Id,
                ProductName = x.ProductName,
                Code = x.Code,
                Image = x.Image,
                Price = x.Price,
                CreateDate = x.CreateDate,
                Description = x.Description,
                Status = x.Status,
                ClientName = x.Client.FullName
            }).ToList();
            return Json(new { data = productLst }, JsonRequestBehavior.AllowGet);
        }

        // GET: Admin/Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Admin/Products/Create
        public ActionResult Create()
        {
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FullName");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProductName,Code,Image,Price,Description,ClientId")] Product product)
        {
            
            //string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
            //string extension = Path.GetExtension(product.ImageFile.FileName);
            //fileName += extension;
            //product.Image = "~/Content/Products/" + fileName;
            //fileName = Path.Combine(Server.MapPath("~/Content/Products/"), fileName);
            //product.ImageFile.SaveAs(fileName);
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                SetAlert("Create successfully", "success");
                return RedirectToAction("Index", "Products");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FullName", product.ClientId);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FullName", product.ClientId);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProductName,Code,Image,Price,Description,ClientId")] Product product)
        {
            //if(product.ImageFile!=null)
            //{
            //    string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);

            //    string extension = Path.GetExtension(product.ImageFile.FileName);
            //    fileName += extension;
            //    product.Image = "~/Content/Products/" + fileName;
            //    fileName = Path.Combine(Server.MapPath("~/Content/Products/"), fileName);
            //    product.ImageFile.SaveAs(fileName);
            //}
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Update successfully", "success");
                return RedirectToAction("Index");
            }
            ViewBag.ClientId = new SelectList(db.Clients, "Id", "FullName", product.ClientId);
            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Product product = db.Products.Where(x => x.Id == id).FirstOrDefault<Product>();
            db.Products.Remove(product);
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
