using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaleECS.Models;

namespace SaleECS.Controllers
{
    public class ServiceController : Controller
    {
        ECSDBEntities db = new ECSDBEntities();
        public List<Service> ListNewService( )
        {
            return db.Services.Where(x => x.Status == true).ToList();
        }
        // GET: Service
        public ActionResult Index()
        {
            ViewBag.Services = ListNewService();
            return View();
        }
    }
}