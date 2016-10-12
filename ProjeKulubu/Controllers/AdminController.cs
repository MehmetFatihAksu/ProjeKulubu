using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjeKulubu.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

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
    }
}
