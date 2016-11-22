using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjeKulubu.Models;
using PagedList;

namespace ProjeKulubu.Controllers
{
    public class ContactController : Controller
    {
        db2299D218BEEntities9 db = new db2299D218BEEntities9();
        [UserAuthorize]
        public ActionResult ContactIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ContactDataDelete(int id)
        {
            Contact deleteContact = db.Contact.Find(id);
            db.Contact.Remove(deleteContact);
            db.SaveChanges();
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }
        [UserAuthorize]
        public ActionResult ContactDelete(int id)
        {
            var data = db.Contact.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

    }
}
