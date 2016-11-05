using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjeKulubu.Models;
using System.IO;

namespace ProjeKulubu.Controllers
{
    public class PicturesController : Controller
    {
        //
        // GET: /Pictures/

        db2299D218BEEntities8 db = new db2299D218BEEntities8();


        public ActionResult PicturesIndex()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddPictures(HttpPostedFileBase Picture,string PictureSEO)
        {
            OurPictures picturesModel = new OurPictures();
            if(Picture !=null)
            {
                string fileMap = Path.GetFileName(Picture.FileName);
                var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
                Picture.SaveAs(loadLocation);
                picturesModel.PictureURL = fileMap;
                picturesModel.PictureSEO = PictureSEO;
                db.OurPictures.Add(picturesModel);
                db.SaveChanges();
            }
            return RedirectToAction("PicturesIndex", "Pictures");
        }

        [HttpPost]
        public ActionResult PicturesDataUpdate()
        {
            // Doldurulcak..
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

        public ActionResult PicturesView(int id)
        {
            var data = db.OurPictures.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

    }
}
