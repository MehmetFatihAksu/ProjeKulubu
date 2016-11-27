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
        db2299D218BEEntities10 db = new db2299D218BEEntities10();

        #region Views
        [UserAuthorize]
        public ActionResult ReferenceIndex(string Sorting_Order, string SearchString, string currentFilter, int? page)
        {
            ViewBag.ReferenceName = string.IsNullOrEmpty(Sorting_Order) ? "Ada_Gore" : "";

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

            var kayitlar = from x in db.Reference select x;

            if (!String.IsNullOrEmpty(SearchString))
            {
                kayitlar = kayitlar.Where(x => x.ReferenceTitle.Contains(SearchString));
            }

            switch (Sorting_Order)
            {
                case "Ada_Gore":
                    kayitlar = kayitlar.OrderBy(Reference => Reference.ReferenceTitle);
                    break;
                default:
                    kayitlar = kayitlar.OrderByDescending(Reference => Reference.ID);
                    break;
            }

            ViewBag.HtmlStr = kayitlar.Count();
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(kayitlar.ToPagedList(pageNumber, pageSize));
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
        #endregion

        #region Methods
        [HttpPost]
        public ActionResult AddReference(HttpPostedFileBase ReferencePicture, string ReferenceName, string ReferencePictureSEO, string ReferenceURL)
        {
            Reference referenceModel = new Reference();
            string fileMap = Path.GetFileName(ReferencePicture.FileName);
            var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
            ReferencePicture.SaveAs(loadLocation);
            referenceModel.ReferencePictureURL = fileMap;
            referenceModel.ReferenceLink = "Http://" + ReferenceURL;
            referenceModel.ReferenceTitle = ReferenceName;
            referenceModel.ReferencePictureSEO = ReferencePictureSEO;
            db.Reference.Add(referenceModel);
            db.SaveChanges();
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
            return RedirectToAction("ReferenceIndex", "Reference");
        }

        public ActionResult MultipleDelete(IEnumerable<int> idler)
        {
            db.Reference.Where(x => idler.Contains(x.ID)).ToList().ForEach(y => db.Reference.Remove(y));
            db.SaveChanges();
            return RedirectToAction("ReferenceIndex", "Reference");
        }
        #endregion

    }
}
