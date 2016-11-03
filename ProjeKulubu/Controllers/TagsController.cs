using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using ProjeKulubu.Models;
using System.Data.Entity.Infrastructure;

namespace ProjeKulubu.Controllers
{
    public class TagsController : Controller
    {
        //
        // GET: /Tags/
        db2299D218BEEntities8 db = new db2299D218BEEntities8();

        public ActionResult TagsIndex()
        {
            return View();
        }

       [HttpPost]
       public ActionResult AddTags(Tags Model)
        {
            if(Model.TagsName != null)
            {
                db.Tags.Add(Model);
                db.SaveChanges();
            }
            else
            {
                ViewBag.Error = "Lütfen Boş Alanları Doldurun";
            }
            return Json(new { Model = Model });
        }

        [HttpPost]
        public ActionResult TagsDataUpdate(Tags Model)
        {
            if(Model.ID != null)
            {
                var Query = from tag in db.Tags where tag.ID == Model.ID select tag;
                foreach (Tags item in Query)
                {
                    item.TagsName = Model.TagsName;
                }
                db.SaveChanges();
            }
            else
            {
                ViewBag.Error = "Güncelleme Sırasında Hata Oluştu,Lütfen Tekrar Deneyiniz";
            }
            return Json(new { Model = Model });
        }

        [HttpPost]
        public ActionResult TagsDataDelete(int id)
        {
            Tags tag = db.Tags.Find(id);
            db.Tags.Remove(tag);
            db.SaveChanges();
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        public ActionResult TagsDelete(int id)
        {
            var data = db.Tags.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        public ActionResult TagsUpdate(int id)
        {
            var data = db.Tags.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }



    }
}
