using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjeKulubu.Models;
using PagedList;
namespace ProjeKulubu.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        db2299D218BEEntities8 db = new db2299D218BEEntities8();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Vizyonumuz()
        {
            return View();
        }
        public ActionResult Misyonumuz()
        {
            return View();
        }
        public ActionResult MusteriMemnuniyeti()
        {
            return View();
        }
        public ActionResult MusteriYorumlari()
        {
            return View();
        }
        public ActionResult SikSorular()
        {
            return View();
        }
        public ActionResult BizKimiz()
        {
            return View();
        }
        public ActionResult NelerYapariz()//cfdbad
        {
            return View();
        }
        public ActionResult Iletisim()
        {
            return View();
        }
        public ActionResult Referanslar(int? page)
        {
            var kayitlar = from x in db.Reference select x;
            kayitlar = kayitlar.OrderBy(Reference => Reference.ID);
            int pageSize = 20;
            int pageNumber = (page ?? 1);
            return View(kayitlar.ToPagedList(pageNumber,pageSize));
        }
        public ActionResult AldigimizEgitimler(int? page)
        {
            var kayitlar = from x in db.Education select x;
            kayitlar = kayitlar.OrderByDescending(Education => Education.ID);
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(kayitlar.ToPagedList(pageNumber,pageSize));
        }
        public ActionResult VerdigimizEgitimler(int? page)
        {
            var kayitlar = from x in db.Education select x;
            kayitlar = kayitlar.OrderByDescending(Education => Education.ID);
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            return View(kayitlar.ToPagedList(pageNumber,pageSize));
        }
        public ActionResult Egitimlerimiz()
        {
            return View();
        }
        public ActionResult Egitim(int id)
        {
            var data = db.Education.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        public ActionResult Projeler()
        {
            return View();
        }
        public ActionResult SingleProject()
        {
            return View(); 
        }
        public ActionResult Ekip()
        {
            return View();
        }
        public ActionResult SatisOfis()
        {
            return View();
        }
        public ActionResult SatisOfisSingle()
        {
            return View();
        }

        public ActionResult Blog()
        {
            return View();
        }
        public ActionResult BlogSingle()
        {
            return View();
        }


        public ActionResult Avantajlarimiz()
        {
            return View();
        }
        public ActionResult SatisSonuHizmetlerimiz()
        {
            return View();
        }
        public ActionResult IkinciElSatis()
        {
            return View();
        }
        public ActionResult ProjeGelistirme()
        {
            return View();
        }
        public ActionResult ProjeSatis()
        {
            return View();
        }
        public ActionResult ProjePazarlama()
        {
            return View();
        }

    }
}
