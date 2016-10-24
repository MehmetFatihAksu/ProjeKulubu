﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjeKulubu.Models;
using System.Web.Services;
using System.Web.Script.Services;
using System.Configuration;


namespace ProjeKulubu.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        db2299D218BEEntities2 Db = new db2299D218BEEntities2();

        #region Projeler
        public ActionResult CompletedProjects()
        {
            return View();
        }

        public ActionResult OnGoingSaleProjects()
        {
            return View();
        }

        public ActionResult AddProject()
        {
            return View();
        }


       
        
        #endregion

        #region AnaSayfa
        public ActionResult Index()
        {
            return View();
        }
        
        #endregion

        #region S.S.S
        public ActionResult QuestionRequest()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult AddQuestion()
        //{
            
        //}


        #endregion

        #region Ofis
     
        public ActionResult YOfis()
        {
            return View();
        }
    


        //Ofis Action'ı eklendi..
        #endregion

        #region Eğitim
        public ActionResult ReceivedEducations()
        {
            return View();
        }

        public ActionResult GrandtedEducations()
        {
            return View();
        }

        #endregion


        #region Referans

        public ActionResult Reference()
        {
            return View();
        }

        //Referans yönetimi  
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
            return View();
        }

        #endregion

        #region Takvim
        public ActionResult Takvim()
        {
            return View();
        }
        #endregion

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


    }
}
