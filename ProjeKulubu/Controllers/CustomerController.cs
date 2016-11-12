using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjeKulubu.Models;
using System.IO;
using System.Web.Security;

namespace ProjeKulubu.Controllers
{
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/

        db2299D218BEEntities8 db = new db2299D218BEEntities8();


        public ActionResult CustomerIndex()
        {
            return View();
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
        public ActionResult CustomerView(int id)
        {
            var data = db.CustomerComments.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }





    }
}
