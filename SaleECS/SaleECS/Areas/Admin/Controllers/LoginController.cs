using SaleECS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaleECS.Models;
using System.Web.Security;
using System.Configuration;
using SaleECS.Common;

namespace SaleECS.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private ECSDBEntities db = new ECSDBEntities();
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel loginView)
        {
            if (ModelState.IsValid)
            {
                Employee emp = db.Employees.FirstOrDefault(e => e.LoginName == loginView.LoginName);
                if (emp != null)
                {
                    if (emp.Password == Encrytor.MD5Hash(loginView.Password))
                    {
                        if (emp.Status == true)
                        {
                        FormsAuthentication.SetAuthCookie(emp.LoginName, false);
                            string urlAvatar = ConfigurationManager.AppSettings["CusAvatar"] + emp.Avatar;
                            urlAvatar = urlAvatar.Substring(urlAvatar.IndexOf("/"));
                            Session["Login"] = emp.LoginName;
                            Session["Avatar"] = urlAvatar;
                            Session["Id"] = emp.Id;
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewBag.LoginStatus = "user locked";
                        }
                    }
                    else
                    {
                       
                        ViewBag.LoginStatus = "Password  is error";
                    }
                }
                else
                {
                    ViewBag.LoginStatus = "Login Name  is error";
                }
            }
            return View("Index");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
        public ActionResult ForgotPassword()
        {
            return View();
        }
    }
}