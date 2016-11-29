using ProjeKulubu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.Entity.Infrastructure;
using PagedList;

namespace ProjeKulubu.Controllers
{
    public class OfficeController : Controller
    {

        db2299D218BEEntities10 db = new db2299D218BEEntities10();

        #region Views Office
        [UserAuthorize] // Index Page
        public ActionResult OfficeIndex(string Sorting_Order, string SearchString, string currentFilter, int? page)
        {
            ViewBag.OfficeName = string.IsNullOrEmpty(Sorting_Order) ? "Ada_Gore" : "";

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

            var kayitlar = from x in db.Office select x;

            if (!String.IsNullOrEmpty(SearchString))
            {
                kayitlar = kayitlar.Where(x => x.OfficeName.Contains(SearchString));
            }

            switch (Sorting_Order)
            {
                case "Ada_Gore":
                    kayitlar = kayitlar.OrderBy(Office => Office.OfficeName);
                    break;
                default:
                    kayitlar = kayitlar.OrderByDescending(Office => Office.ID);
                    break;
            }

            ViewBag.HtmlStr = kayitlar.Count();
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(kayitlar.ToPagedList(pageNumber, pageSize));
        } 

        [UserAuthorize] // Delete Page
        public ActionResult OfficeDelete(int id)
        {
            var data = db.Office.Where(x => x.ID == id).FirstOrDefault();

            return View(data);
        }

        [UserAuthorize] // Update Page
        public ActionResult OfficeUpdate(int id)
        {
            var data = db.Office.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        #endregion

        #region Method Office
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddOffice(string Name, string Content, string AltIcerik, string Zaman, string Telefon, string EMail, string Location, string Adres)
        {
            Office office = new Office();
            Location = "İstanbul,Türkiye";
            Content = Content.Replace("<p>","").Replace("</p>","").Replace("\r","").Replace("\n","");
            AltIcerik = AltIcerik.Replace("<p>", "").Replace("</p>", "").Replace("\r", "").Replace("\n", "");

            office.OfficeName = Name;
            office.OfficeMainContent = Content;
            office.OfficeAltContent = AltIcerik;
            office.OfficeMail = EMail;
            office.OfficeLocation = Adres + " " + Location;
            office.OfficePhone = Telefon;
            office.OfficeWorkingTime = Zaman;
            db.Office.Add(office);
            db.SaveChanges();

            return RedirectToAction("OfficeIndex","Office");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult OfficeDataUpdate(int id ,string Name, string Content, string AltIcerik, string Zaman, string Telefon, string EMail, string Location, string Adres)
        {

            Office officeModel = db.Office.Where(x => x.ID == id).FirstOrDefault();

            Content = Content.Replace("<p>", "").Replace("</p>", "").Replace("\r", "").Replace("\n", "");

            AltIcerik = AltIcerik.Replace("<p>", "").Replace("</p>", "").Replace("\r", "").Replace("\n", "");

            if (Name != null || Content != null || AltIcerik != null)
            {
                officeModel.OfficeName = Name;
                officeModel.OfficeMainContent = Content;
                officeModel.OfficeAltContent = AltIcerik;
                officeModel.OfficeMail = EMail;
                officeModel.OfficeLocation = Adres + " " + Location;
                officeModel.OfficePhone = Telefon;
                officeModel.OfficeWorkingTime = Zaman;
                db.SaveChanges();
            }
            else
            {
                ViewBag.Error = "Serverdan kaynaklı bir hata oluştu,lütfen yetkili biriyle iletişime geçin";
            }
            return RedirectToAction("OfficeIndex", "Office");
        }

        [HttpPost]
        public ActionResult OfficeDataDelete(int id)
        {
            Office removeOffice = db.Office.Find(id);
            db.OfficePictures.Where(x => x.OfficeID == removeOffice.ID).ToList().ForEach(y => db.OfficePictures.Remove(y));
            db.Office.Remove(removeOffice);
            db.SaveChanges();
            return RedirectToAction("OfficeIndex", "Office");
        }

        public ActionResult OfficeMultipleDelete(IEnumerable<int> idler)
        {
            foreach (var item in idler)
            {
                Office removeModel = db.Office.Find(item);
                db.OfficePictures.Where(x => x.OfficeID == removeModel.ID).ToList().ForEach(y => db.OfficePictures.Remove(y));
            }
            db.Office.Where(x => idler.Contains(x.ID)).ToList().ForEach(y => db.Office.Remove(y));
            db.SaveChanges();
            return RedirectToAction("OfficeIndex", "Office");
        }
        #endregion



        #region Views Picture Office
        [UserAuthorize]
        public ActionResult OfficePictureIndex(string Sorting_Order, string SearchString, string currentFilter, int? page)
        {
            ViewBag.PictureOfficeName = string.IsNullOrEmpty(Sorting_Order) ? "Ofise_Gore" : "";

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

            var kayitlar = from x in db.OfficePictures select x;

            if (!String.IsNullOrEmpty(SearchString))
            {
                kayitlar = kayitlar.Where(x => x.Office.OfficeName.Contains(SearchString));
            }

            switch (Sorting_Order)
            {
                case "Ofise_Gore":
                    kayitlar = kayitlar.OrderBy(OfficePictures => OfficePictures.Office.OfficeName);
                    break;
                default:
                    kayitlar = kayitlar.OrderByDescending(OfficePictures => OfficePictures.ID);
                    break;
            }

            ViewBag.HtmlStr = kayitlar.Count();
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(kayitlar.ToPagedList(pageNumber, pageSize));
        }

        [UserAuthorize]
        public ActionResult OfficePictureUpdate(int id)
        {
            var data = db.OfficePictures.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        #endregion

        #region Method Office Picture
        [HttpPost]
        public ActionResult AddOfficePicture(int officeId, HttpPostedFileBase OfficePicture, string OfficePictureSEO)
        {
            OfficePictures officePicture = new OfficePictures();

                string fileMap = Path.GetFileName(OfficePicture.FileName);
                var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
                OfficePicture.SaveAs(loadLocation);

                officePicture.OfficeID = officeId;
                officePicture.PictureSEO = OfficePictureSEO;
                officePicture.PictureURL = fileMap;

                db.OfficePictures.Add(officePicture);
                db.SaveChanges();
            return RedirectToAction("OfficePictureIndex", "Office");
        }

        [HttpPost]
        public ActionResult OfficePictureDataUpdate(int id, int officeId, HttpPostedFileBase OfficePicture, string OfficePictureSEO)
        {
            OfficePictures officePictureModel = db.OfficePictures.Where(x => x.ID == id).FirstOrDefault();

            if(OfficePicture==null)
            {
                officePictureModel.OfficeID = officeId;
                officePictureModel.PictureSEO = OfficePictureSEO;
                db.SaveChanges();
            }
            else
            {
                string fileMap = Path.GetFileName(OfficePicture.FileName);
                var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
                OfficePicture.SaveAs(loadLocation);
                officePictureModel.PictureURL = fileMap;
                officePictureModel.OfficeID = officeId;
                officePictureModel.PictureSEO = OfficePictureSEO;
                db.SaveChanges();

            }
            return RedirectToAction("OfficePictureIndex", "Office");
        }

        public ActionResult PictureMultipleDelete(IEnumerable<int> idler)
        {
            db.OfficePictures.Where(x => idler.Contains(x.ID)).ToList().ForEach(y => db.OfficePictures.Remove(y));
            db.SaveChanges();
            return RedirectToAction("OfficePictureIndex", "Office");
        }
        #endregion












    }
}
