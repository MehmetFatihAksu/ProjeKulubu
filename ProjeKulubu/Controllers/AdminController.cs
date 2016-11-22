using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjeKulubu.Models;
using System.IO;
using System.Net;

namespace ProjeKulubu.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        db2299D218BEEntities8 db = new db2299D218BEEntities8();

        #region AnaSayfa
        public ActionResult Index()
        {
            return View();
        }

        #endregion

        #region Yardim
        public ActionResult HelpDetail()
        {
            return View();
        } 
        #endregion

        #region CikisYap
        public ActionResult LogOut()
        {
            return View();
        } 
        #endregion

        #region Ayarlar
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


    }
}
