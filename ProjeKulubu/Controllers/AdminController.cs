using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjeKulubu.Models;
using System.IO;
using System.Net;
using System.Web.Providers.Entities;
using PagedList;

namespace ProjeKulubu.Controllers
{
    public class AdminController : Controller
    {

        db2299D218BEEntities10 db = new db2299D218BEEntities10();

        [UserAuthorize]
        public ActionResult Index(int? page)
        {
            var kayitlar = from x in db.LoginList select x;
            kayitlar = kayitlar.OrderByDescending(LoginList => LoginList.ID);
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.HtmlStr = kayitlar.Count();
            return View(kayitlar.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginControl(string UserName, string Password)
        {
            var adminDataList = db.Admin.Where(x=>x.UserName !=null && x.UserPassword !=null).ToList();

            if (adminDataList.AsQueryable().Any(x => x.UserName == UserName && x.UserPassword == Password))
            {
                HttpCookie cookie = new HttpCookie("UserName", UserName.ToString());
                Response.Cookies.Add(cookie);
                HttpCookie cookie2 = new HttpCookie("PassWord", Password);
                Response.Cookies.Add(cookie2);
                var context = System.Web.HttpContext.Current;
                string ip = string.Empty;

                if (context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    ip = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                }
                else if (!String.IsNullOrWhiteSpace(context.Request.UserHostAddress))
                {
                    ip = context.Request.UserHostAddress;
                }

                if (ip == "::1")
                {
                    ip = "127.0.0.1";
                }

                LoginList addLogin = new LoginList();
                string browser_name = Request.Browser.Browser;
                addLogin.IPAdress = ip;
                addLogin.LoginDate = DateTime.Now;
                addLogin.SoftwareType = browser_name;
                db.LoginList.Add(addLogin);
                db.SaveChanges();
                return RedirectToAction("Index","Admin");
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }
        }
        [UserAuthorize]
        public ActionResult HelpDetail()
        {
            return View();
        } 
        public ActionResult LogOut()
        {
            Response.Cookies["UserName"].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies["PassWord"].Expires = DateTime.Now.AddDays(-1);
            return RedirectToAction("Index","Home");
        }
        [UserAuthorize]
        public ActionResult Settings()
        {
            //Doldurulcak
            return View();
        }
        public ActionResult LoginListMultipleDelete(IEnumerable<int> idler)
        {
            db.LoginList.Where(x => idler.Contains(x.ID)).ToList().ForEach(y => db.LoginList.Remove(y));
            db.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }
        public ActionResult AllDelete()
        {
            var alldata = db.LoginList.ToList();
            foreach (var item in alldata)
            {
                var delete_data = db.LoginList.Find(item.ID);
                db.LoginList.Remove(delete_data);
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Admin");
        }

        [UserAuthorize]
        public ActionResult Error()
        {
            return View();
        }

        public ActionResult MainUpdate(string facebook,string twitter,string google,string linkedin,string youtube,string phone,string mail,string working1,string working2,string working3,string fax,int office1,int office2,int office3,string contactmessage,string contactmainoffice,string workingcomment)
        {
            var data = db.MainContents.Where(x => x.ID == 0).FirstOrDefault();
            data.facebookURL = facebook;
            data.twitterURL = twitter;
            data.googleURL = google;
            data.LinkedinURL = linkedin;
            data.youtubeURL = youtube;
            data.phoneNumber = phone;
            data.Mail = mail;
            data.Working1 = working1;
            data.Working2 = working2;
            data.Working3 = working3;
            data.fax = fax;
            data.mainofficeID1 = office1;
            data.mainofficeID2 = office2;
            data.mainofficeID3 = office3;
            data.ContactMessage = contactmessage;
            data.contanctmainoffice = contactmainoffice;
            data.workingcomment = workingcomment;
            db.SaveChanges();
            return View();
        }




    }
}
