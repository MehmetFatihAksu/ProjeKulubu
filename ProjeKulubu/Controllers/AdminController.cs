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

    

        #region Projeler

        public ActionResult AddProject()
        {
            return View();
        }

        public ActionResult SatisiDevamEdenProjeler()
        {
            return View();
        }

        public ActionResult TamamlananProjeler()
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

        #region Ekip
        public ActionResult Ekip()
        {
            return View();
        }
        #endregion

        #region AnaSayfa Resimler 
       public ActionResult BizdenResimler()
        {
            return View();
        }
        #endregion

        #region S.S.S
        public ActionResult QuestionRequest()
        {
            return View();
        }


        #endregion

        #region Ofis
     
        public ActionResult Ofis()
        {
            return View();
        }





        //Ofis Action'ı eklendi..
        #endregion

        #region Eğitim
        public ActionResult AlinanEgitimler()
        {
            return View();
        }
        public ActionResult VerilenEgitimler()
        {
            return View();
        }

        #endregion


        #region Referans
        public ActionResult Reference()
        {
            return View();
        }
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Reference([Bind(Include = "ID,SiteURL,Baslik,SeoAlt,LogoURL")] Referanslar referans, HttpPostedFileBase uploadFile , int ID)
        //{
        //    if (ModelState.IsValid)
        //    {
        //            db.Referanslar.Add(referans);
        //            db.SaveChanges();
        //            if (uploadFile !=null && uploadFile.ContentLength > 0)
        //            {
                        
        //            }


        //            return RedirectToAction("Reference", "Admin");
        //    }


        //    return View();
        //}
        ////Referans yönetimi  
      

        //public ActionResult ReferansList()
        //{
        //    return View(db.Referanslar.ToList());
        //}


        //public ActionResult ReferansDelete(int? id)
        //{
        //    if(id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Referanslar referans = db.Referanslar.Find(id);
        //    if(referans == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(referans);
        //}

        //[HttpPost,ActionName("ReferansDelete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Referanslar referans = db.Referanslar.Find(id);
        //    db.Referanslar.Remove(referans);
        //    db.SaveChanges();
        //    return RedirectToAction("Reference","Admin");
        //}

        //public ActionResult ReferansEdit(int? id)
        //{
        //    if(id==null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Referanslar referans = db.Referanslar.Find(id);
        //    if(referans ==null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(referans);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ReferansEdit([Bind(Include ="ID,SiteURL,Baslik,SeoAlt,LogoURL")]Referanslar referans)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        db.Entry(referans).State = System.Data.EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Reference", "Admin");
        //    }
        //    return View(referans);
        //}
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

        #region etiket
            public ActionResult Etiket()
        {
            return View();
        }
        #endregion



        #region makale
        public ActionResult Makale()
        {
            return View();
        }
        #endregion

        #region MusteriYorumlari

        public ActionResult MusteriYorumlari()
        {
            return View();
        } 
        #endregion


    }
}
