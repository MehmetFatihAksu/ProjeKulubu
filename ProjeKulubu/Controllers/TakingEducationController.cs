using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ProjeKulubu.Models;
using System.Data.Entity.Infrastructure;
using PagedList;


namespace ProjeKulubu.Controllers
{
    public class TakingEducationController : Controller
    {
        //
        // GET: /TakingEducation/

        db2299D218BEEntities9 db = new db2299D218BEEntities9();

        [UserAuthorize]
        public ActionResult TakingEducationIndex(int? page)
        {
            var list = db.Education.Where(x=>x.EducationTypeID==1).ToList();
            var pageNumber = page ?? 1;
            var onePageOfEducation = list.ToPagedList(pageNumber, 7);
            ViewBag.Show = onePageOfEducation;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
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
                eduModel.EducationTypeID = 1;
                db.Education.Add(eduModel);
                db.SaveChanges();
            }
            return RedirectToAction("TakingEducationIndex", "TakingEducation");
        }

        [HttpPost]
        public ActionResult TakingEducationDataUpdate(int Id,string title, HttpPostedFileBase file, string seo, string content)
        {
            Education education = db.Education.Where(x => x.ID == Id).FirstOrDefault();

            content = content.Replace("<p>", "").Replace("</p>", "");

            if (file == null)
            {
                education.EducationTitle = title;
                education.EducationContent = content;
                education.EducationFileSEO = seo;
                education.EducationTypeID = 1;
                db.SaveChanges();
            }
            else
            {
                string fileMap = Path.GetFileName(file.FileName);
                var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
                file.SaveAs(loadLocation);
                education.EducationTitle = title;
                education.EducationContent = content;
                education.EducationFileSEO = seo;
                education.EducationFileURL = fileMap;
                education.EducationTypeID = 1;
                db.SaveChanges();
            }
            return RedirectToAction("TakingEducationIndex", "TakingEducation");
        }

        [HttpPost]
        public ActionResult TakingEducationDataDelete(int id)
        {
            Education removeEdu = db.Education.Find(id);
            db.Education.Remove(removeEdu);
            db.SaveChanges();
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }
        [UserAuthorize]
        public ActionResult TakingEducationDelete(int id)
        {
            var data = db.Education.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        [UserAuthorize]
        public ActionResult TakingEducationView(int id)
        {
            var data = db.Education.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        [UserAuthorize]
        public ActionResult TakingEducationUpdate(int id)
        {
            var data = db.Education.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

    }


  

    
}
