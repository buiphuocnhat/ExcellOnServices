using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaleECS.Models;

namespace SaleECS.Areas.Admin.Controllers
{
    public class ChartsController : Controller
    {
        private ECSDBEntities db = new ECSDBEntities();
        // GET: Admin/Charts
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetData()
        {
            int inbound = db.OrderServiceDetails.Where(x => x.Service.ServiceName == "In-bound Services").Count();
            int outbound = db.OrderServiceDetails.Where(x => x.Service.ServiceName == "Out-bound Services").Count();
            int tele = db.OrderServiceDetails.Where(x => x.Service.ServiceName == "Tele Marketing Services").Count();
            Ratio obj = new Ratio();
            obj.Inbound = inbound;
            obj.Outbound = outbound;
            obj.Tele = tele;

            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public class Ratio
        {
            public int Inbound { get; set; }
            public int Outbound { get; set; }
            public int Tele { get; set; }
        }
    }
}