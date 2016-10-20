using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjeKulubu.Models;


namespace ProjeKulubu.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        db2299D218BEEntities db = new db2299D218BEEntities();

        public ActionResult Index()
        {
            return View();
        }
        //Index sayfası direkt olarak slider yönetimi sayfasına yönlendirilebilir..Bu Opsiyonlu olarak gerçekleştirilecek...

        public ActionResult QuestionRequest()
        {
            return View();
        }

        //Sadece kendine ait tek bir alan olacak..

        public ActionResult Team()
        {
            return View();
        }

        //Ekip arkadaşlarımız 
        //Çalışanlarımız 
        //Satış Ofislerimiz 


        //şeklinde listelenecek...

        public ActionResult Project()
        {
            return View();
        }

        //Tamamlanan Projeler 
        // Satışı Devam Eden Projeler
        //Yakındaki Projeler

        //şeklinde listelenecek....

        public ActionResult Content()
        {
            return View();
        }

        //İçerik yönetim kısmı müşterinin istekleri doğrultusunda şekillendirilecek...
        //Biz Kimiz ? 
        //Hakkımızda 
        //Müşteri Memnuniyeti 


        public ActionResult Comment()
        {
            return View();
        }
        //Yorum yönetimi tek sayfa halinde olacak... Ve sadece müşteri yorumları eklenip kaldırılacak..



        public ActionResult Education()
        {
            return View();
        }

        //Aldığımız eğitimler 
        //Verdiğimiz eğitimler 
        //şeklinde listelenecek....

        public ActionResult Reference()
        {
            return View();
        }

        //Referans yönetimi 

        //Referans adı ve referans firmasının logosu şeklinde listelenecek...

        //View Henüz Eklenmedi...
        public ActionResult HelpDetail()
        {
            return View();
        }

        //Help Detail için ayrıca bir dökümantasyon hazırlanacak...
        //View Henüz Eklenmedi...
        public ActionResult LogOut()
        {
            return View();
        }

        //LogOut güvenli çıkış şeklinde programlanacak...
        //View Henüz Eklenmedi...
        public ActionResult Settings()
        {
            return View();
        }

        //Duruma göre eklenip kaldırılabilir... (Karar Aşamasında)

        //Genel Not 

        //Model Admin Paneldeki sayfaların ihtiyaçlarına göre eksiksiz şekilde oluşturulacak..

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin Model)
        {
            var kullanici = db.Admin.FirstOrDefault(x => x.UserName == Model.UserName && x.Password == Model.Password);
            if(kullanici !=null)
            {
                Session["UserName"] = kullanici.UserName.ToString();
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ViewBag.Hata = "Hatalı Verişi Girişi";
                return View();
            }
        }

        public ActionResult BitmisProjeler()
        {
            return View();
        }
        public ActionResult DevamEdenProjeler()
        {
            return View();

        }
        public ActionResult YakindakiProjeler()
        {
            return View();
        }

            
    }
}
