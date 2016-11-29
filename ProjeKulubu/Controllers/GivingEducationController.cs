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
        db2299D218BEEntities10 db = new db2299D218BEEntities10();

        #region Views
        [UserAuthorize]
        public ActionResult GivingEducationIndex(string Sorting_Order, string SearchString, string currentFilter, int? page)
        {
            ViewBag.EduName = string.IsNullOrEmpty(Sorting_Order) ? "Ada_Gore" : "";
            ViewBag.EduDate = string.IsNullOrEmpty(Sorting_Order) ? "Tarihe_Gore" : "";
            ViewBag.EduView = string.IsNullOrEmpty(Sorting_Order) ? "Goruntulenme_Gore" : "";

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

            var kayitlar = from x in db.Education.Where(x => x.EducationTypeID == 2) select x;

            if (!String.IsNullOrEmpty(SearchString))
            {
                kayitlar = kayitlar.Where(x => x.EducationTitle.Contains(SearchString));
            }

            switch (Sorting_Order)
            {
                case "Ada_Gore":
                    kayitlar = kayitlar.OrderBy(Education => Education.EducationTitle);
                    break;
                case "Tarihe_Gore":
                    kayitlar = kayitlar.OrderByDescending(Education => Education.EducationDate);
                    break;
                case "Goruntulenme_Gore":
                    kayitlar = kayitlar.OrderByDescending(Education => Education.EducationView);
                    break;
                default:
                    kayitlar = kayitlar.OrderByDescending(Education => Education.ID);
                    break;
            }

            ViewBag.HtmlStr = kayitlar.Where(x => x.EducationTypeID == 2).Count();
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(kayitlar.ToPagedList(pageNumber, pageSize));
        }

        [UserAuthorize]
        public ActionResult GivingEducationDelete(int id)
        {
            var data = db.Education.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        [UserAuthorize]
        public ActionResult GivingEducationUpdate(int id)
        {
            var data = db.Education.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        #endregion

        #region Methods
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddGivingEducation(string title, HttpPostedFileBase file, string seo, string content)
        {
            content = content.Replace("<p>", "").Replace("</p>", "");
            Education eduModel = new Education();
            if (file != null)
            {
                string fileMap = Path.GetFileName(file.FileName);
                var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
                file.SaveAs(loadLocation);
                eduModel.EducationFileURL = fileMap;
                eduModel.EducationContent = content;
                eduModel.EducationFileSEO = seo;
                eduModel.EducationTitle = title;
                eduModel.EducationTypeID = 2;
                eduModel.EducationDate = DateTime.Today;
                eduModel.EducationView = 0;
                db.Education.Add(eduModel);
                db.SaveChanges();
            }
            else
            {
                eduModel.EducationContent = content;
                eduModel.EducationFileSEO = seo;
                eduModel.EducationTitle = title;
                eduModel.EducationTypeID = 2;
                eduModel.EducationDate = DateTime.Today;
                eduModel.EducationView = 0;
                db.Education.Add(eduModel);
                db.SaveChanges();
            }
            return RedirectToAction("GivingEducationIndex", "GivingEducation");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult GivingEducationDataUpdate(int id, string title, HttpPostedFileBase file, string seo, string content)
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
            return RedirectToAction("GivingEducationIndex","GivingEducation");
        }

        public ActionResult MultipleDelete(IEnumerable<int> idler)
        {
            db.Education.Where(x => idler.Contains(x.ID)).ToList().ForEach(y => db.Education.Remove(y));
            db.SaveChanges();
            return RedirectToAction("GivingEducationIndex", "GivingEducation");
        }
        #endregion

    }
}
