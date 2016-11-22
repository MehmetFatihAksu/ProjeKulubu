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

        db2299D218BEEntities9 db = new db2299D218BEEntities9();
        [UserAuthorize]
        public ActionResult OfficeIndex(int? page)
        {
            var list = db.Office.ToList();
            var pageNumber = page ?? 1;
            var onePageOfOffice = list.ToPagedList(pageNumber, 7);
            ViewBag.Show = onePageOfOffice;
            return View();
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddOffice(string Name, string Content, string AltIcerik, string Zaman, string Telefon, string EMail, string Location, string Adres)
        {
            Office office = new Office();

            Location = "İstanbul,Türkiye";

            Content = Content.Replace("<p>","").Replace("</p>","").Replace("\r","").Replace("\n","");

            AltIcerik = AltIcerik.Replace("<p>", "").Replace("</p>", "").Replace("\r", "").Replace("\n", "");

            if (Name !=null || Content !=null || AltIcerik !=null)
            {
                office.OfficeName = Name;
                office.OfficeMainContent = Content;
                office.OfficeAltContent = AltIcerik;
                office.OfficeMail = EMail;
                office.OfficeLocation = Adres + " " + Location;
                office.OfficePhone = Telefon;
                office.OfficeWorkingTime = Zaman;
                db.Office.Add(office);
                db.SaveChanges();
            }
            else
            {
                ViewBag.Error = "Serverdan kaynaklı bir hata oluştu,lütfen yetkili biriyle iletişime geçin";
            }
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

        [UserAuthorize]
        public ActionResult OfficeDelete(int id)
        {
            var data = db.Office.Where(x => x.ID == id).FirstOrDefault();

            return View(data);
        }
        [UserAuthorize]
        public ActionResult OfficeUpdate(int id)
        {
            var data = db.Office.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        [UserAuthorize]
        public ActionResult OfficeView(int id)
        {
            var data = db.Office.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        [HttpPost]
        public ActionResult OfficeDataDelete(int id)
        {
            Office office = db.Office.Find(id);
            db.Office.Remove(office);
            db.SaveChanges();

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        [UserAuthorize]
        public ActionResult OfficePictureIndex(int? page)
        {
            var list = db.OfficePictures.ToList();
            var pageNumber = page ?? 1;
            var onePageOfOfficePicture = list.ToPagedList(pageNumber, 7);
            ViewBag.Show = onePageOfOfficePicture;
            return View();
        }

        [HttpPost]
        public ActionResult AddOfficePicture(int officeId, HttpPostedFileBase OfficePicture, string OfficePictureSEO, string OfficePictureDesc)
        {
            OfficePictures officePicture = new OfficePictures();

            if (OfficePicture !=null || officeId !=null)
            {
                string fileMap = Path.GetFileName(OfficePicture.FileName);
                var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
                OfficePicture.SaveAs(loadLocation);

                officePicture.OfficeID = officeId;
                officePicture.PictureSEO = OfficePictureSEO;
                officePicture.PictureALT = OfficePictureDesc;
                officePicture.PictureURL = fileMap;

                db.OfficePictures.Add(officePicture);
                db.SaveChanges();
            }
            else
            {
                ViewBag.Error = "Lütfen eksiksiz veri girişi yapınız..";
            }
            return RedirectToAction("OfficePictureIndex", "Office");
        }
        [UserAuthorize]
        public ActionResult OfficePictureUpdate(int id)
        {
            var data = db.OfficePictures.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        public ActionResult OfficePictureDelete(int id)
        {
            var data = db.OfficePictures.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        [HttpPost]
        public ActionResult OfficePictureDataDelete(int id)
        {
            OfficePictures officePicture = db.OfficePictures.Find(id);
            db.OfficePictures.Remove(officePicture);
            db.SaveChanges();

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }


        [HttpPost]
        public ActionResult OfficePictureDataUpdate(int id, int officeId, HttpPostedFileBase OfficePicture, string OfficePictureSEO, string OfficePictureDesc)
        {
            OfficePictures officePictureModel = db.OfficePictures.Where(x => x.ID == id).FirstOrDefault();

            if (officeId !=null || OfficePicture !=null)
            {
                string fileMap = Path.GetFileName(OfficePicture.FileName);
                var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
                OfficePicture.SaveAs(loadLocation);

                
                officePictureModel.PictureSEO = OfficePictureSEO;
                officePictureModel.PictureALT = OfficePictureDesc;
                officePictureModel.OfficeID = officeId;
                officePictureModel.PictureURL = fileMap;
                db.SaveChanges();
            }else{
                ViewBag.Error = "Lütfen eksiksiz veri girişi yapınız..";
            }
            return RedirectToAction("OfficePictureIndex", "Office");
        }

    }
}
