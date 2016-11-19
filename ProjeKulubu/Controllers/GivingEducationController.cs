using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ProjeKulubu.Models;


namespace ProjeKulubu.Controllers
{
    public class GivingEducationController : Controller
    {
        //
        // GET: /GivingEducation/

        db2299D218BEEntities9 db = new db2299D218BEEntities9();

        public ActionResult GivingEducationIndex()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddGivingEducation(string title,HttpPostedFileBase file,string seo,string content)
        {
            Education eduModel = new Education();
            if(title!=null && file!=null && seo!=null && content!=null)
            {
                string fileMap = Path.GetFileName(file.FileName);
                var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
                file.SaveAs(loadLocation);
                eduModel.EducationContent = content;
                eduModel.EducationFileSEO = seo;
                eduModel.EducationFileURL = fileMap;
                eduModel.EducationTitle = title;
                db.Education.Add(eduModel);
                db.SaveChanges();
            }
            return RedirectToAction("GivingEducationIndex", "GivingEducation");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GivingEducationDataUpdate(int id,string title,HttpPostedFileBase file,string seo,string content)
        {
            Education updateModel = db.Education.Where(x => x.ID == id).FirstOrDefault();
            if(title!=null && content!=null)
            {
                if(file!=null)
                {
                    string fileMap = Path.Combine(file.FileName);
                    var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
                    file.SaveAs(loadLocation);
                    updateModel.EducationTitle = title;
                    updateModel.EducationFileURL = fileMap;
                    updateModel.EducationFileSEO = seo;
                    updateModel.EducationContent = content;
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.Error = "Belirlenemeyen bi hata oluştu";
                }
            }
            return RedirectToAction("GivingEducationIndex", "GivingEducation");

        }

        [HttpPost]
        public ActionResult GivingEducationDataDelete(int id)
        {
            Education removeEdu = db.Education.Find(id);
            db.Education.Remove(removeEdu);
            db.SaveChanges();
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        [HttpPost]
        public ActionResult GivingEducationDelete(int id)
        {
            var data = db.Education.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        [HttpPost]
        public ActionResult GivingEducationUpdate(int id)
        {
            var data = db.Education.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        [HttpPost]
        public ActionResult GivingEducationView(int id)
        {
            var data = db.Education.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

    }
}
