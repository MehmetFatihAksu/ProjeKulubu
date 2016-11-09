using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjeKulubu.Models;
using System.IO;

namespace ProjeKulubu.Controllers
{
    public class ReferenceController : Controller
    {
        //
        // GET: /Reference/

        db2299D218BEEntities8 db = new db2299D218BEEntities8();

        public ActionResult ReferenceIndex()
        {
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
        public ActionResult ReferenceDataUpdate()
        {
            //doldurulcak
            return View();
        }

        [HttpPost]
        public ActionResult ReferenceDataDelete(int id)
        {
            Reference deleteReference = db.Reference.Find(id);
            db.Reference.Remove(deleteReference);
            db.SaveChanges();
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        public ActionResult ReferenceDelete(int id)
        {
            var data = db.Reference.Where(x => x.ID == id).FirstOrDefault();
               return View(data);
        }
        public ActionResult ReferenceUpdate(int id)
        {
            var data = db.Reference.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        public ActionResult ReferenceView(int id)
        {
            var data = db.Reference.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

    }
}
