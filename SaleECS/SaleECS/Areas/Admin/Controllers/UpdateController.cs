using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using SaleECS.Common;
using SaleECS.Models;
using SaleECS.ViewModels;

namespace SaleECS.Areas.Admin.Controllers
{
    public class UpdateController : Controller
    {
        private ECSDBEntities db = new ECSDBEntities();
        // GET: Admin/Update
        public ActionResult Update(int? id)
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include = "Id,LoginName,Password,FirstName,LastName,Birthday,Gender,Address,Avatar,Email,Phone,Status,DepartmentId,ImageFile")] Employee employee)
        {
            if (employee.ImageFile != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(employee.ImageFile.FileName);

                string extension = Path.GetExtension(employee.ImageFile.FileName);
                fileName += extension;
                employee.Avatar = "~/Content/Avatar/" + fileName;
                fileName = Path.Combine(Server.MapPath("~/Content/Avatar/"), fileName);
                employee.ImageFile.SaveAs(fileName);
            }
            
            if (ModelState.IsValid)
            {
                employee.LoginName = employee.LoginName;
                employee.Password = employee.Password;
                employee.Status = employee.Status;
                employee.DepartmentId = employee.DepartmentId;
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
              
          
                return RedirectToAction("Index", "Home");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "DepartmentName", employee.DepartmentId);
            return View(employee);
        }


    }
}
