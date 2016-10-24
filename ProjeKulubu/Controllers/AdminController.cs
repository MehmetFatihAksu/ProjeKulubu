﻿using System;
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

        db2299D218BEEntities6 db = new db2299D218BEEntities6();

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
     
        //public ActionResult YOfis()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult YOfis(Proje Model)
        //{
        //   if(ModelState.IsValid)
        //    {
    
        //    }

        //    return View();

        //}





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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reference([Bind(Include = "ID,SiteURL,Baslik,SeoAlt,LogoURL")] Referanslar referans)
        {


            if (ModelState.IsValid)
            {
                    db.Referanslar.Add(referans);
                    db.SaveChanges();
                    return RedirectToAction("Reference", "Admin");
            }


            return View();
        }
        //Referans yönetimi  
      

        public ActionResult ReferansList()
        {
            return View(db.Referanslar.ToList());
        }


        public ActionResult ReferansDelete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Referanslar referans = db.Referanslar.Find(id);
            if(referans == null)
            {
                return HttpNotFound();
            }
            return View(referans);
        }

        [HttpPost,ActionName("ReferansDelete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Referanslar referans = db.Referanslar.Find(id);
            db.Referanslar.Remove(referans);
            db.SaveChanges();
            return RedirectToAction("Reference","Admin");
        }

        public ActionResult ReferansEdit(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Referanslar referans = db.Referanslar.Find(id);
            if(referans ==null)
            {
                return HttpNotFound();
            }
            return View(referans);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReferansEdit([Bind(Include ="ID,SiteURL,Baslik,SeoAlt,LogoURL")]Referanslar referans)
        {
            if(ModelState.IsValid)
            {
                db.Entry(referans).State = System.Data.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Reference", "Admin");
            }
            return View(referans);
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
