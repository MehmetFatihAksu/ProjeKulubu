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
        db2299D218BEEntities8 db = new db2299D218BEEntities8();
       
        public ActionResult ContactIndex(string Sorting_Order, string SearchString, string currentFilter, int? page)
        {
            ViewBag.ContactName = string.IsNullOrEmpty(Sorting_Order) ? "Ada_Gore" : "";
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

            var kayitlar = from x in db.Contact select x;

            if (!String.IsNullOrEmpty(SearchString))
            {
                kayitlar = kayitlar.Where(x => x.ContactName.Contains(SearchString));
            }

            switch (Sorting_Order)
            {
                case "Ada_Gore":
                    kayitlar = kayitlar.OrderBy(Contact => Contact.ContactName);
                    break;
                default:
                    kayitlar = kayitlar.OrderByDescending(Contact => Contact.ID);
                    break;
            }

            ViewBag.HtmlStr = kayitlar.Count();
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(kayitlar.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult AddContact(string name,string mail,string phone,string content)
        {
            Contact addContact = new Contact();
            if(name!=null && mail!=null && phone!=null && content!=null)
            {
                addContact.ContactMail = mail;
                addContact.ContactName = name;
                addContact.ContactPhone = phone;
                addContact.ContantSubjectContent = content;
                db.Contact.Add(addContact);
                db.SaveChanges();
            }
            return RedirectToAction("Iletisim", "Home");
        }
        public ActionResult ContactView(int id)
        {
            var data = db.Contact.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        public ActionResult MultipleDelete(IEnumerable<int> idler)
        {
            db.Contact.Where(x => idler.Contains(x.ID)).ToList().ForEach(y => db.Contact.Remove(y));
            db.SaveChanges();
            return RedirectToAction("ContactIndex", "Contact");
        }

    }
}
