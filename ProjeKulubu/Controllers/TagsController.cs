using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using ProjeKulubu.Models;
using System.Data.Entity.Infrastructure;
using PagedList;

namespace ProjeKulubu.Controllers
{
    public class TagsController : Controller
    {
        //
        // GET: /Tags/
        db2299D218BEEntities8 db = new db2299D218BEEntities8();

        [UserAuthorize]
        public ActionResult TagsIndex(string Sorting_Order,string SearchString,string currentFilter,int? page)
        {
            ViewBag.TagsName = string.IsNullOrEmpty(Sorting_Order) ? "Ada_Gore" : "";

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

            var kayitlar = from x in db.Tags select x;

            if (!String.IsNullOrEmpty(SearchString))
            {
                kayitlar = kayitlar.Where(x => x.TagsName.Contains(SearchString));
            }

            switch (Sorting_Order)
            {
                case "Ada_Gore":
                    kayitlar = kayitlar.OrderBy(Tags => Tags.TagsName);
                    break;
                default:
                    kayitlar = kayitlar.OrderByDescending(Tags => Tags.ID);
                    break;
            }

            ViewBag.HtmlStr = kayitlar.Count();
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(kayitlar.ToPagedList(pageNumber, pageSize));
        }    
        
                
        [HttpPost]
       public ActionResult AddTags(string tagname)
        {
            Tags addTags = new Tags();
            if(tagname!=null)
            {
                addTags.TagsName = tagname;
                db.Tags.Add(addTags);
                db.SaveChanges();
            }
            return RedirectToAction("TagsIndex", "Tags");
        }

        [HttpPost]
        public ActionResult TagsDataUpdate(int id,string tagname)
        {
            Tags updateTags = db.Tags.Where(x => x.ID == id).FirstOrDefault();
            if(tagname!=null)
            {
                updateTags.TagsName = tagname;
                db.SaveChanges();
            }
            return RedirectToAction("TagsIndex", "Tags");
        }

        [HttpPost]
        public ActionResult TagsDataDelete(int id)
        {
            Tags tag = db.Tags.Find(id);
            db.Tags.Remove(tag);
            db.SaveChanges();
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }
        [UserAuthorize]
        public ActionResult TagsDelete(int id)
        {
            var data = db.Tags.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        [UserAuthorize]
        public ActionResult TagsUpdate(int id)
        {
            var data = db.Tags.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        public ActionResult MultipleDelete(IEnumerable<int> idler)
        {
            db.Tags.Where(x => idler.Contains(x.ID)).ToList().ForEach(y => db.Tags.Remove(y));
            db.SaveChanges();
            return RedirectToAction("TagsIndex", "Tags");
        }



    }
}
