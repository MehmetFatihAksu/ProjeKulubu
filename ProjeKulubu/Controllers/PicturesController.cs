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
        db2299D218BEEntities10 db = new db2299D218BEEntities10();

        #region Views
        [UserAuthorize]
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

        [UserAuthorize]
        public ActionResult PicturesDelete(int id)
        {
            var data = db.OurPictures.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        [UserAuthorize]
        public ActionResult PicturesUpdate(int id)
        {
            var data = db.OurPictures.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        #endregion

        #region Methods
        [HttpPost]
        public ActionResult AddPictures(HttpPostedFileBase Picture, string PictureSEO)
        {
            OurPictures picturesModel = new OurPictures();
            string fileMap = Path.GetFileName(Picture.FileName);
            var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
            Picture.SaveAs(loadLocation);
            picturesModel.PictureURL = fileMap;
            picturesModel.PictureSEO = PictureSEO;
            db.OurPictures.Add(picturesModel);
            db.SaveChanges();
            return RedirectToAction("PicturesIndex", "Pictures");
        }

        [HttpPost]
        public ActionResult PicturesDataUpdate(int id, HttpPostedFileBase Picture, string PictureSEO)
        {
            OurPictures pictureModel = db.OurPictures.Where(x => x.ID == id).FirstOrDefault();
            if (Picture == null)
            {
                pictureModel.PictureSEO = PictureSEO;
                db.SaveChanges();
            }
            else
            {
                string fileMap = Path.GetFileName(Picture.FileName);
                var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
                Picture.SaveAs(loadLocation);
                pictureModel.PictureURL = fileMap;
                pictureModel.PictureSEO = PictureSEO;
                db.SaveChanges();
            }
            return RedirectToAction("PicturesIndex", "Pictures");
        }

        [HttpPost]
        public ActionResult PicturesDataDelete(int id)
        {
            OurPictures picture = db.OurPictures.Find(id);
            db.OurPictures.Remove(picture);
            db.SaveChanges();
            return RedirectToAction("PicturesIndex", "Pictures");
        }

        public ActionResult MultipleDelete(IEnumerable<int> idler)
        {
            if(idler!=null)
            {
                db.OurPictures.Where(x => idler.Contains(x.ID)).ToList().ForEach(y => db.OurPictures.Remove(y));
                db.SaveChanges();
                return RedirectToAction("PicturesIndex", "Pictures");
            }
            else
            {
                string olmadi = "olmadi";
                ViewBag.Error = olmadi;
                return RedirectToAction("PicturesIndex", "Pictures");
            }
        }
        #endregion

    }
}
