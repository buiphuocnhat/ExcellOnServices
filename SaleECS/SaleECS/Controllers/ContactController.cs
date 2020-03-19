using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaleECS.Common;
using SaleECS.Models;

namespace SaleECS.Controllers
{
    public class ContactController : Controller
    {
        private ECSDBEntities db = new ECSDBEntities(); 
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        public int InsertFeedBack(FeedBack fb)
        {
            db.FeedBacks.Add(fb);
            db.SaveChanges();
            return fb.Id;

        }
        [HttpPost]
        public JsonResult Submit(string fullname, string service, string email, string content)
        {
            decimal total = 0;
            string  contents = System.IO.File.ReadAllText(Server.MapPath("~/assets/Client/template/Feedback.html"));
            if (fullname != "" && service !="" && email != "" && content != "")
            {
            contents = contents.Replace("{{FullName}}", fullname);
            contents = contents.Replace("{{Service}}", service);
            contents = contents.Replace("{{Email}}", email);
            contents = contents.Replace("{{Content}}", content);
            contents = contents.Replace("{{Total}}", total.ToString("N0"));
            var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
            new MailHelper().SendMail(toEmail, "FeedBack Service",contents);
            }

            var fb = new FeedBack();
            fb.FullName = fullname;
            fb.Service = service;
            fb.Email = email;
            fb.Content = content;
            fb.CreateDate = DateTime.Now;

            var id = InsertFeedBack(fb);
            if (id > 0)
            {
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }
    }
}