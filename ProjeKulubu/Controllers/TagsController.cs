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
