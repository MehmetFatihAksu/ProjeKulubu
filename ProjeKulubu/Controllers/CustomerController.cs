using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjeKulubu.Models;
using System.IO;
using static System.Net.WebRequestMethods;
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

        //[HttpPost]
        //public ActionResult ResimEkle(HttpPostedFileBase file)
        //{
        //    string imagename = Path.GetFileName(file.FileName);
        //    string path = Server.MapPath("~/addFiles/" + imagename);
        //    file.SaveAs(path);
        //    return View();
        //}

        //public ActionResult AddImage(HttpPostedFileBase uploadFile)
        //{
        //    string imagename = Path.GetFileName(uploadFile.FileName);
        //    string path = Server.MapPath("~/addFiles/" + imagename);
        //    uploadFile.SaveAs(path);
        //    return Json(new { uploadFile = uploadFile });
        //}

        [HttpPost]
        public ActionResult AddCustomer(HttpPostedFileBase file)
        {
            string imagename = Path.GetFileName(file.FileName);
            string path = Server.MapPath("~/addFiles/" + imagename);
            file.SaveAs(path);
            CustomerComments add = new CustomerComments();
            add.Name = Request.Form["Name"];
            add.CommentsPictureURL = Request.Form["CommentsPictureURL"];
            db.CustomerComments.Add(add);
            db.SaveChanges();
            return Json(new { file = file });
        }

        [HttpPost]
        public ActionResult CustomerDataUpdate(CustomerComments Model)
        {
            if(Model.ID !=null)
            {
                var Query = from customer in db.CustomerComments
                            where customer.ID == Model.ID
                            select customer;

                foreach (CustomerComments item in Query)
                {
                    item.Name = Model.Name;
                    item.CommentsPictureURL = Model.Name;
                }
                db.SaveChanges();
                
            }
            else
            {
                ViewBag.Error = "Güncelleme Sırasında Hata Oluştu";
            }
            return Json(new { Model = Model });
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
