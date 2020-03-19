using SaleECS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaleECS.Controllers
{
    public class NewController : Controller
    {
        private ECSDBEntities db = new ECSDBEntities();
        public List<News> ListNewService(int top)
        {
            return db.News.OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }
        // GET: New
        public ActionResult Index()
        {

            ViewBag.NewServices = ListNewService(8);
            return View();
        }
        public ActionResult Detail()
        {
            ViewBag.NewServices = ListNewService(1);
            return View();
        }

    }
}