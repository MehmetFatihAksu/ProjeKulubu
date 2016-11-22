using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjeKulubu.Models;
using System.IO;
using System.Net;
using System.Web.Providers.Entities;

namespace ProjeKulubu.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        db2299D218BEEntities9 db = new db2299D218BEEntities9();

        [UserAuthorize]
        public ActionResult Index()
        {
            return View();
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
                return RedirectToAction("Index","Admin");
            }
            else
            {
                return RedirectToAction("Login", "Admin");
                ViewBag.Error = "Kullanıcı Adı veya Şifre Hatalı";
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

        #endregion

        #region Login
        //#region Login
        //public ActionResult Login()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult Login(Admin Model)
        //{
        //    var kullanici = db.Admin.FirstOrDefault(x => x.UserName == Model.UserName && x.Password == Model.Password);
        //    if(kullanici !=null)
        //    {
        //        Session["UserName"] = kullanici.UserName.ToString();
        //        return RedirectToAction("Index", "Admin");
        //    }
        //    else
        //    {
        //        ViewBag.Hata = "Hatalı Verişi Girişi";
        //        return View();
        //    }
        //}
        //#endregion
        #endregion

        #region makale
        public ActionResult Makale()
        {
            return View();
        }
        #endregion


    }
}
