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
    public class BlogController : Controller
    {
        //
        // GET: /Blog/

        db2299D218BEEntities9 db = new db2299D218BEEntities9();
        [UserAuthorize]
        public ActionResult BlogIndex(int ? page)
        {
            ViewBag.BlogName = string.IsNullOrEmpty(Sorting_Order) ? "Ada_Gore" : "";
            ViewBag.BlogDate = string.IsNullOrEmpty(Sorting_Order) ? "Tarihe_Gore" : "";
            ViewBag.BlogView = string.IsNullOrEmpty(Sorting_Order) ? "Goruntulenme_Gore" : "";
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

            var kayitlar = from x in db.Blog select x;

            if (!String.IsNullOrEmpty(SearchString))
            {
                kayitlar = kayitlar.Where(x => x.BlogTitle.Contains(SearchString));
            }

            switch (Sorting_Order)
            {
                case "Ada_Gore":
                    kayitlar = kayitlar.OrderBy(Blog => Blog.BlogTitle);
                    break;
                case "Tarihe_Gore":
                    kayitlar = kayitlar.OrderBy(Blog => Blog.BlogDate);
                    break;
                case "Goruntulenme_Gore":
                    kayitlar = kayitlar.OrderBy(Blog => Blog.BlogViewCount);
                    break;
                default:
                    kayitlar = kayitlar.OrderByDescending(Blog => Blog.ID);
                    break;
            }

            ViewBag.HtmlStr = kayitlar.Count();
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(kayitlar.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddBlog(string title,string tag,HttpPostedFileBase picture,string seo,string content)
        {
            Blog addModel = new Blog();

            content = content.Replace("<p>", "").Replace("</p>", "");
            if(title!=null || content!=null)
            {
                if(picture!=null)
                {
                    string fileMap = Path.GetFileName(picture.FileName);
                    var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
                    picture.SaveAs(loadLocation);
                    addModel.BlogPictureURL = fileMap;
                }
                addModel.BlogContent = content;
                addModel.BlogPictureSEO = seo;
                addModel.BlogTitle = title;
                db.Blog.Add(addModel);
                db.SaveChanges();
            }
            else
            {
                ViewBag.Error("Hatalı Giriş");
            }

            return RedirectToAction("BlogIndex", "Blog");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult BlogDataUpdate(int id,string title,string tag,HttpPostedFileBase picture,string content,string seo)
        {
            Blog updateModel = db.Blog.Where(x => x.ID == id).FirstOrDefault();

            content = content.Replace("<p>", "").Replace("</p>", "");
            if(title!=null && content!=null)
            {
                if(picture!=null)
                {
                    string fileMap = Path.GetFileName(picture.FileName);
                    var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
                    picture.SaveAs(loadLocation);
                    updateModel.BlogPictureURL = fileMap;
                }
                updateModel.BlogContent = content;
                updateModel.BlogPictureSEO = seo;
                updateModel.BlogTitle = title;
                db.SaveChanges();
            }
            else
            {
                ViewBag.Error("Hatalı Giriş");
            }
            return RedirectToAction("BlogIndex", "Blog");
        }


        [HttpPost]
        public ActionResult BlogDataDelete(int id)
        {
            Blog removeModel = db.Blog.Find(id);
            db.Blog.Remove(removeModel);
            db.SaveChanges();
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }
        [UserAuthorize]
        public ActionResult BlogView(int id)
        {
            var data = db.Blog.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        [UserAuthorize]
        public ActionResult BlogUpdate(int id)
        {
            var data = db.Blog.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        [UserAuthorize]
        public ActionResult BlogDelete(int id)
        {
            var data = db.Blog.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        public ActionResult MultipleDelete(IEnumerable<int> idler)
        {
            db.Blog.Where(x => idler.Contains(x.ID)).ToList().ForEach(y => db.Blog.Remove(y));
            db.SaveChanges();
            return RedirectToAction("BlogIndex", "Blog");
        }




    }
}
