using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjeKulubu.Models;
using System.IO;
using PagedList;

namespace ProjeKulubu.Controllers
{
    public class PicturesController : Controller
    {
        //
        // GET: /Pictures/

        db2299D218BEEntities8 db = new db2299D218BEEntities8();


        public ActionResult PicturesIndex(string Sorting_Order, string SearchString, string currentFilter, int? page)
        {
            ViewBag.PictureSEO = string.IsNullOrEmpty(Sorting_Order) ? "Seo_Gore" : "";

            ViewBag.CurrentSort = Sorting_Order;
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currentFilter;
            }

            ViewBag.CurrentFilter = SearchString;

            var kayitlar = from x in db.OurPictures select x;

            if (!String.IsNullOrEmpty(SearchString))
            {
                kayitlar = kayitlar.Where(x => x.PictureSEO.Contains(SearchString));
            }

            switch (Sorting_Order)
            {
                case "Seo_Gore":
                    kayitlar = kayitlar.OrderBy(OurPictures => OurPictures.PictureSEO);
                    break;
                default:
                    kayitlar = kayitlar.OrderByDescending(OurPictures => OurPictures.ID);
                    break;
            }

            ViewBag.HtmlStr = kayitlar.Count();
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(kayitlar.ToPagedList(pageNumber, pageSize));
        }


        [HttpPost]
        public ActionResult AddPictures(HttpPostedFileBase Picture,string PictureSEO)
        {
            OurPictures picturesModel = new OurPictures();
            if(Picture !=null && PictureSEO!=null)
            {
                string fileMap = Path.GetFileName(Picture.FileName);
                var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
                Picture.SaveAs(loadLocation);
                picturesModel.PictureURL = fileMap;
                picturesModel.PictureSEO = PictureSEO;
                db.OurPictures.Add(picturesModel);
                db.SaveChanges();
            }
            else
            {
                ViewBag.Error("Serverdan kaynaklı bir hata oluştu,lütfen yetkili biriyle iletişime geçin");
            }
            return RedirectToAction("PicturesIndex", "Pictures");
        }

        [HttpPost]
        public ActionResult PicturesDataUpdate(int id,HttpPostedFileBase Picture,string PictureSEO)
        {
            OurPictures pictureModel = db.OurPictures.Where(x => x.ID == id).FirstOrDefault();
            if(Picture!=null && PictureSEO!=null)
            {
                string fileMap = Path.GetFileName(Picture.FileName);
                var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
                Picture.SaveAs(loadLocation);
                pictureModel.PictureURL = fileMap;
                pictureModel.PictureSEO = PictureSEO;
                db.SaveChanges();
                return RedirectToAction("PicturesIndex", "Pictures");
            }
            else
            {
                ViewBag.Error("Serverdan kaynaklı bir hata oluştu,lütfen yetkili biriyle iletişime geçin");
            }
            return View();
        }

        [HttpPost]
        public ActionResult PicturesDataDelete(int id)
        {
            OurPictures picture = db.OurPictures.Find(id);
            db.OurPictures.Remove(picture);
            db.SaveChanges();
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        public ActionResult PicturesDelete(int id)
        {
            var data = db.OurPictures.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        public ActionResult PicturesUpdate(int id)
        {
            var data = db.OurPictures.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        public ActionResult MultipleDelete(IEnumerable<int> idler)
        {
            db.OurPictures.Where(x => idler.Contains(x.ID)).ToList().ForEach(y => db.OurPictures.Remove(y));
            db.SaveChanges();
            return RedirectToAction("PicturesIndex", "Pictures");
        }


    }
}
