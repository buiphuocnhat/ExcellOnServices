using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaleECS.Models;

namespace SaleECS.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Service,HR,Auditors")]
    public class HomeController : Controller
    {
        ECSDBEntities db = new ECSDBEntities();
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
       

    }
}