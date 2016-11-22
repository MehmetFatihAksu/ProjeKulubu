using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ProjeKulubu.Models;
using PagedList;

namespace ProjeKulubu.Controllers
{
    public class TakingEducationController : Controller
    {
        //
        // GET: /TakingEducation/

        db2299D218BEEntities8 db = new db2299D218BEEntities8();

        public ActionResult TakingEducationIndex(string Sorting_Order, string SearchString, string currentFilter, int? page)
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

            var kayitlar = from x in db.Education select x;

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
                    kayitlar = kayitlar.OrderBy(Education => Education.EducationDate);
                    break;
                case "Goruntulenme_Gore":
                    kayitlar = kayitlar.OrderBy(Education => Education.EducationView);
                    break;
                default:
                    kayitlar = kayitlar.OrderByDescending(Education => Education.ID);
                    break;
            }

            ViewBag.HtmlStr = kayitlar.Count();
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(kayitlar.ToPagedList(pageNumber, pageSize));


        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddTakingEducation(string title,HttpPostedFileBase file,string seo,string content)
        {
            Education eduModel = new Education();
            if(title!=null && seo!=null && content!=null)
            {
                if(file != null)
                {
                    string fileMap = Path.GetFileName(file.FileName);
                    var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
                    file.SaveAs(loadLocation);
                    eduModel.EducationFileURL = fileMap;
                }
                eduModel.EducationTitle = title;
                eduModel.EducationFileSEO = seo;
                eduModel.EducationContent = content;
                db.Education.Add(eduModel);
                db.SaveChanges();
            }
            else
            {
                ViewBag.Error("Serverdan kaynaklı bir sorun oluştu");
            }
            return RedirectToAction("TakingEducationIndex", "TakingEducation");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult TakingEducationDataUpdate(int id,string title, HttpPostedFileBase file, string seo, string content)
        {
            Education updateEdu = db.Education.Where(x => x.ID == id).FirstOrDefault();
            if(title!=null && content!=null && seo!=null)
            {
                if(file!=null)
                {
                    string fileMap = Path.GetFileName(file.FileName);
                    var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
                    file.SaveAs(loadLocation);
                    updateEdu.EducationFileURL = fileMap;
                }
                updateEdu.EducationContent = content;
                updateEdu.EducationFileSEO = seo;
                updateEdu.EducationTitle = title;
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

        public ActionResult TakingEducationDelete(int id)
        {
            var data = db.Education.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        public ActionResult TakingEducationUpdate(int id)
        {
            var data = db.Education.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        public ActionResult MultipleDelete(IEnumerable<int> idler)
        {
            db.Education.Where(x => idler.Contains(x.ID)).ToList().ForEach(y => db.Education.Remove(y));
            db.SaveChanges();
            return RedirectToAction("TakingEducationIndex", "TakingEducation");
        }

    }


  

    
}
