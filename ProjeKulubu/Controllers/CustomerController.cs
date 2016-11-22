using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjeKulubu.Models;
using System.IO;
using System.Web.Security;
using PagedList;

namespace ProjeKulubu.Controllers
{
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/

        db2299D218BEEntities8 db = new db2299D218BEEntities8();


        public ActionResult CustomerIndex(string Sorting_Order,string SearchString,string currentFilter,int? page)
        {
            ViewBag.CustomerName = string.IsNullOrEmpty(Sorting_Order) ? "Ada_Gore" : "";

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

            var kayitlar = from x in db.CustomerComments select x;

            if (!String.IsNullOrEmpty(SearchString))
            {
                kayitlar = kayitlar.Where(x => x.Name.Contains(SearchString));
            }

            switch (Sorting_Order)
            {
                case "Ada_Gore":
                    kayitlar = kayitlar.OrderBy(CustomerComments =>CustomerComments.Name);
                    break;
                default:
                    kayitlar = kayitlar.OrderByDescending(CustomerComments => CustomerComments.ID);
                    break;
            }

            ViewBag.HtmlStr = kayitlar.Count();
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(kayitlar.ToPagedList(pageNumber, pageSize));


        }
        [HttpPost]
        public ActionResult AddCustomer(HttpPostedFileBase CommentPicture, string CustomerName)
        {
            CustomerComments customerModel = new CustomerComments();
            if (CommentPicture != null && CustomerName!=null)
            {
                string fileMap = Path.GetFileName(CommentPicture.FileName);
                var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
                CommentPicture.SaveAs(loadLocation);
                customerModel.CommentsPictureURL = fileMap;
                customerModel.Name = CustomerName;
                db.CustomerComments.Add(customerModel);
                db.SaveChanges();
            }
            else
            {
                ViewBag.Error("Serverdan kaynaklı bir hata oluştu,lütfen yetkili biriyle iletişime geçin");
            }
            return RedirectToAction("CustomerIndex", "Customer");


        }

        [HttpPost]
        public ActionResult CustomerDataUpdate(int id, HttpPostedFileBase CommentPicture,string CustomerName)
        {
            CustomerComments customerModel = db.CustomerComments.Where(x => x.ID == id).FirstOrDefault();
            if(CustomerName!=null && CommentPicture!=null)
            {
                string fileMap = Path.GetFileName(CommentPicture.FileName);
                var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
                CommentPicture.SaveAs(loadLocation);
                customerModel.CommentsPictureURL = fileMap;
                customerModel.Name = CustomerName;
                db.SaveChanges();
                return RedirectToAction("CustomerIndex", "Customer");

            }
            else
        {
                ViewBag.Error("Serverdan kaynaklı bir hata oluştu,lütfen yetkili biriyle iletişime geçin");
            }
            return View();
        }

        [HttpPost]
        public ActionResult CustomerDataDelete(int id)
        {
            CustomerComments customer = db.CustomerComments.Find(id);
            db.CustomerComments.Remove(customer);
            db.SaveChanges();
            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }

        public ActionResult CustomerDelete(int id)
        {
            var data = db.CustomerComments.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        public ActionResult CustomerUpdate(int id)
        {
            var data = db.CustomerComments.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        public ActionResult MultipleDelete(IEnumerable<int> idler)
        {
            db.CustomerComments.Where(x => idler.Contains(x.ID)).ToList().ForEach(y => db.CustomerComments.Remove(y));
            db.SaveChanges();
            return RedirectToAction("CustomerIndex","Customer");
        }


    }
}
