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
    public class GivingEducationController : Controller
    {
        //
        // GET: /GivingEducation/

        db2299D218BEEntities9 db = new db2299D218BEEntities9();
        [UserAuthorize]
        public ActionResult GivingEducationIndex(int? page)
        {
            var list = db.Education.Where(x => x.EducationTypeID == 2).ToList();
            var pageNumber = page ?? 1;
            var onePageOfEducation = list.ToPagedList(pageNumber, 7);
            ViewBag.Show = onePageOfEducation;
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
                eduModel.EducationTypeID = 2;
                db.Education.Add(eduModel);
                db.SaveChanges();
            }
            return RedirectToAction("GivingEducationIndex", "GivingEducation");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GivingEducationDataUpdate(int id,string title,HttpPostedFileBase file,string seo,string content)
        {
            Education education = db.Education.Where(x => x.ID == id).FirstOrDefault();

            content = content.Replace("<p>", "").Replace("</p>", "");

            if (file == null)
            {
                education.EducationTitle = title;
                education.EducationContent = content;
                education.EducationFileSEO = seo;
                education.EducationTypeID = 2;
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
                education.EducationTypeID = 2;
                db.SaveChanges();
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
        [UserAuthorize]
        public ActionResult GivingEducationDelete(int id)
        {
            var data = db.Education.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        
        [HttpPost]
        [UserAuthorize]
        public ActionResult GivingEducationUpdate(int id)
        {
            var data = db.Education.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        [HttpPost]
        [UserAuthorize]
        public ActionResult GivingEducationView(int id)
        {
            var data = db.Education.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

    }
}
