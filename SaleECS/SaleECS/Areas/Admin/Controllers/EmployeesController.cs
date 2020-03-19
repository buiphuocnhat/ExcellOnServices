using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using SaleECS.Common;
using SaleECS.Models;
using SaleECS.ViewModels;

namespace SaleECS.Areas.Admin.Controllers
{
        [Authorize(Roles = "Admin,HR")]
    public class EmployeesController : Controller
    {
        private readonly ECSDBEntities db = new ECSDBEntities();

        // GET: Admin/Employees
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetList()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<EmployeeViewModel> empLst = db.Employees.Select(x => new EmployeeViewModel
            {
                Id = x.Id,
                LoginName = x.LoginName,
                Password = x.Password,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Birthday = x.Birthday,
                Gender = x.Gender,
                Address = x.Address,
                Avatar = x.Avatar,
                Email = x.Email,
                Phone = x.Phone,
                Status = x.Status,
                DepartmentName = x.Department.DepartmentName
            }).ToList();
            return Json(new { data = empLst }, JsonRequestBehavior.AllowGet);
        }
        // GET: Admin/Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Admin/Employees/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName");
            return View();
        }

        // POST: Admin/Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,LoginName,Password,FirstName,LastName,Birthday,Gender,Address,Avatar,Email,Phone,Status,DepartmentId,ImageFile")] Employee employee)
        {

            string fileName = Path.GetFileNameWithoutExtension(employee.ImageFile.FileName);
            string extension = Path.GetExtension(employee.ImageFile.FileName);
            fileName += extension;
            employee.Avatar = "~/Content/Avatar/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Content/Avatar/"), fileName);
            employee.ImageFile.SaveAs(fileName);
            if (ModelState.IsValid)
            {
                var encrypttedPas = Encrytor.MD5Hash(employee.Password);
                employee.Password = encrypttedPas;
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName", employee.DepartmentId);
            return View(employee);
        }

        // GET: Admin/Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName", employee.DepartmentId);
            return View(employee);
        }

        // POST: Admin/Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,LoginName,Password,FirstName,LastName,Birthday,Gender,Address,Avatar,Email,Phone,Status,DepartmentId,ImageFile")] Employee employee)
        {
            if (ModelState.IsValid)
                {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                SetAlert("Update successfully", "success");
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName", employee.DepartmentId);
            return View(employee);
        }

       

        // GET: Admin/Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            Employee emp = db.Employees.Where(x => x.Id == id).FirstOrDefault<Employee>();
            db.Employees.Remove(emp);
            db.SaveChanges();
            //SetAlert("Delete successfully", "success");
            return RedirectToAction("Index");
        }

      

        public ActionResult ChangePassword(int? id)
        {
            Employee emp = db.Employees.Find(id);
            ChangePasswordViewModel empData = Mapper.Map<Employee, ChangePasswordViewModel>(emp);
            return View(empData);
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel empData)
        {
            Employee emp = db.Employees.Find(empData.Id);
            Mapper.Map(empData, emp);
            var encrypttedPas = Encrytor.MD5Hash(emp.Password);
            emp.Password = encrypttedPas;
            db.SaveChanges();
            ViewBag.passStatus = "Pass error !!";
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
