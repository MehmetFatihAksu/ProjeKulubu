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
    public class ReferenceController : Controller
    {
        //
        // GET: /Reference/

        db2299D218BEEntities9 db = new db2299D218BEEntities9();
        [UserAuthorize]
        public ActionResult ReferenceIndex(int? page)
        {
            var list = db.Reference.ToList();
            var pageNumber = page ?? 1;
            var onePageOfReference = list.ToPagedList(pageNumber, 10);
            ViewBag.Show = onePageOfReference;
            return View();
        }

        [HttpPost]
        public ActionResult AddReference(HttpPostedFileBase ReferencePicture,string ReferenceName,string ReferencePictureSEO,string ReferenceURL)
        {
            Reference referenceModel = new Reference();
            if(ReferencePicture != null && ReferenceName != null && ReferenceURL != null && ReferencePictureSEO != null)
            {
                string fileMap = Path.GetFileName(ReferencePicture.FileName);
                var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
                ReferencePicture.SaveAs(loadLocation);
                referenceModel.ReferencePictureURL = fileMap;
                referenceModel.ReferenceLink = ReferenceURL;
                referenceModel.ReferenceTitle = ReferenceName;
                referenceModel.ReferencePictureSEO = ReferencePictureSEO;
                db.Reference.Add(referenceModel);
                db.SaveChanges();

            }
            return RedirectToAction("ReferenceIndex", "Reference");
        }


        [HttpPost]
        public ActionResult ReferenceDataUpdate(int Id, HttpPostedFileBase ReferencePicture, string ReferenceName, string ReferenceURL, string ReferencePictureSEO)
        {
            Reference updateModel = db.Reference.Where(x => x.ID == Id).FirstOrDefault();

            if (ReferencePicture == null)
            {
                updateModel.ReferenceLink = ReferenceURL;
                updateModel.ReferencePictureSEO = ReferencePictureSEO;
                updateModel.ReferenceTitle = ReferenceName;
                db.SaveChanges();
            }
            else
            {
                string fileMap = Path.GetFileName(ReferencePicture.FileName);
                var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
                ReferencePicture.SaveAs(loadLocation);
                updateModel.ReferenceLink = ReferenceURL;
                updateModel.ReferencePictureSEO = ReferencePictureSEO;
                updateModel.ReferenceTitle = ReferenceName;
                updateModel.ReferencePictureURL = fileMap;
                db.SaveChanges();
            }
            return RedirectToAction("ReferenceIndex", "Reference");
        }

        [HttpPost]
        public ActionResult ReferenceDataDelete(int id)
        {
            Reference deleteReference = db.Reference.Find(id);
            db.Reference.Remove(deleteReference);
            db.SaveChanges();
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }
        [UserAuthorize]
        public ActionResult ReferenceDelete(int id)
        {
            var data = db.Reference.Where(x => x.ID == id).FirstOrDefault();
               return View(data);
        }
        [UserAuthorize]
        public ActionResult ReferenceUpdate(int id)
        {
            var data = db.Reference.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        [UserAuthorize]
        public ActionResult ReferenceView(int id)
        {
            var data = db.Reference.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

    }
}
