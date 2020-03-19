using AutoMapper;
using SaleECS.Common;
using SaleECS.Models;
using SaleECS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaptchaMvc.HtmlHelpers;

namespace SaleECS.Areas.Admin.Controllers
{
    public class RegisterController : Controller
    {
        private ECSDBEntities db = new ECSDBEntities();
        // GET: Admin/Register
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!this.IsCaptchaValid(""))
            {
                ViewBag.error = "Invalid Captcha";
                return View("Register");
            }
            else
            {

            
            Employee emp = Mapper.Map<RegisterViewModel, Employee>(model);
            if (emp.LoginName == null)
            {
                ViewBag.RegisterStatus = "Enter Login Name";
            }
            else
            {
                if (emp.Password == null)
                {
                    ViewBag.RegisterStatus = "Enter Password";
                }
                else
                {
                    var encrypttedPas = Encrytor.MD5Hash(emp.Password);
                    emp.Password = encrypttedPas;
                    if (emp.Email == null)
                    {
                        ViewBag.RegisterStatus = "Enter Email";
                    }
                    else
                    {
                        emp.FirstName = null;
                        emp.LastName = null;
                        emp.Birthday = null;
                        emp.Gender = null;
                        emp.Address = null;
                        emp.Avatar = "~/Content/Avatar/default.jpg";
                        emp.Phone = null;
                        emp.Status = false;
                        emp.DepartmentId = 5;
                        db.Employees.Add(emp);
                        db.SaveChanges();
                  
                        return RedirectToAction("Index", "Login");
                    }
                 
                }
               
            }
            }
            return View("Register");   
        }

    }
}