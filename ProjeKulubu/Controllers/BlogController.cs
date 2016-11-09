using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjeKulubu.Models;
using System.IO;

namespace ProjeKulubu.Controllers
{
    public class BlogController : Controller
    {
        //
        // GET: /Blog/

        db2299D218BEEntities8 db = new db2299D218BEEntities8();

        public ActionResult BlogIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBlog(string title,string tag,HttpPostedFileBase picture,string seo,string content)
        {
            Blog addModel = new Blog();
            if(title!=null && picture!=null && seo!=null && content!=null)
            {
                string fileMap = Path.GetFileName(picture.FileName);
                var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
                picture.SaveAs(loadLocation);
                addModel.BlogContent = content;
                addModel.BlogPictureSEO = seo;
                addModel.BlogPictureURL = fileMap;
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
        public ActionResult BlogDataUpdate()
        {
            // doldurulcak
            return View();
        }


        [HttpPost]
        public ActionResult BlogDataDelete(int id)
        {
            Blog removeModel = db.Blog.Find(id);
            db.Blog.Remove(removeModel);
            db.SaveChanges();
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        public ActionResult BlogView(int id)
        {
            var data = db.Blog.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        public ActionResult BlogUpdate(int id)
        {
            var data = db.Blog.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        public ActionResult BlogDelete(int id)
        {
            var data = db.Blog.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }





    }
}
