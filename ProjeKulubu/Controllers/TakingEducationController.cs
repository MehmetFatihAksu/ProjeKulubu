using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ProjeKulubu.Models;


namespace ProjeKulubu.Controllers
{
    public class TakingEducationController : Controller
    {
        //
        // GET: /TakingEducation/

        db2299D218BEEntities8 db = new db2299D218BEEntities8();

        public ActionResult TakingEducationIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTakingEducation(string title,HttpPostedFileBase file,string seo,string content)
        {
            Education eduModel = new Education();
            if(title!=null && file!=null && seo!=null && content!=null)
            {
                string fileMap = Path.GetFileName(file.FileName);
                var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
                file.SaveAs(loadLocation);
                eduModel.EducationTitle = title;
                eduModel.EducationFileURL = fileMap;
                eduModel.EducationFileSEO = seo;
                eduModel.EducationContent = content;
                db.Education.Add(eduModel);
                db.SaveChanges();
            }
            return RedirectToAction("TakingEducationIndex", "TakingEducation");
        }

        [HttpPost]
        public ActionResult TakingEducationDataUpdate()
        {
            // doldurulcak
            return View();
        }

        [HttpPost]
        public ActionResult TakingEducationDataDelete(int id)
        {
            Education removeEdu = db.Education.Find(id);
            db.Education.Remove(removeEdu);
            db.SaveChanges();
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        public ActionResult TakingEducationDelete(int id)
        {
            var data = db.Education.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        public ActionResult TakingEducationView(int id)
        {
            var data = db.Education.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        public ActionResult TakingEducationUpdate(int id)
        {
            var data = db.Education.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

    }


  

    
}
