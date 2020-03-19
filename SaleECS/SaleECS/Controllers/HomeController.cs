using SaleECS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaleECS.Controllers
{
    public class HomeController : Controller
    {
        private ECSDBEntities db = new ECSDBEntities();
        public List<FeedBack> ListNewService(int top)
        {
            return db.FeedBacks.OrderByDescending(x => x.Status == true).Take(top).ToList();
        }
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.FeedBServices = ListNewService(3);
            return View();
        }
    }
}