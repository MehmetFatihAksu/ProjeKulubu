﻿using System;
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
        db2299D218BEEntities10 db = new db2299D218BEEntities10();

        #region Views
        [UserAuthorize]
        public ActionResult CustomerIndex(string Sorting_Order, string SearchString, string currentFilter, int? page)
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
                    kayitlar = kayitlar.OrderBy(CustomerComments => CustomerComments.Name);
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

        [UserAuthorize]
        public ActionResult CustomerDelete(int id)
        {
            var data = db.CustomerComments.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        [UserAuthorize]
        public ActionResult CustomerUpdate(int id)
        {
            var data = db.CustomerComments.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        #endregion

        #region Methods
        [HttpPost]
        public ActionResult AddCustomer(HttpPostedFileBase CommentPicture, string CustomerName,string seo)
        {
            CustomerComments customerModel = new CustomerComments();
            string fileMap = Path.GetFileName(CommentPicture.FileName);
            var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
            CommentPicture.SaveAs(loadLocation);
            customerModel.PictureSEO = seo;
            customerModel.CommentsPictureURL = fileMap;
            customerModel.Name = CustomerName;
            db.CustomerComments.Add(customerModel);
            db.SaveChanges();
            return RedirectToAction("CustomerIndex", "Customer");
        }

        [HttpPost]
        public ActionResult CustomerDataUpdate(int id, HttpPostedFileBase CommentPicture, string CustomerName,string seo)
        {
            CustomerComments customerModel = db.CustomerComments.Where(x => x.ID == id).FirstOrDefault();

            if (CommentPicture == null)
            {
                customerModel.PictureSEO = seo;
                customerModel.Name = CustomerName;
                db.SaveChanges();
            }
            else
            {
                string fileMap = Path.GetFileName(CommentPicture.FileName);
                var loadLocation = Path.Combine(Server.MapPath("~/Dosyalar"), fileMap);
                CommentPicture.SaveAs(loadLocation);
                customerModel.CommentsPictureURL = fileMap;
                customerModel.PictureSEO = seo;
                customerModel.Name = CustomerName;
                db.SaveChanges();
            }
            return RedirectToAction("CustomerIndex", "Customer");
        }

        [HttpPost]
        public ActionResult CustomerDataDelete(int id)
        {
            CustomerComments customer = db.CustomerComments.Find(id);
            db.CustomerComments.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("CustomerIndex", "Customer");
        }

        public ActionResult MultipleDelete(IEnumerable<int> idler)
        {
            db.CustomerComments.Where(x => idler.Contains(x.ID)).ToList().ForEach(y => db.CustomerComments.Remove(y));
            db.SaveChanges();
            return RedirectToAction("CustomerIndex", "Customer");
        }
        #endregion

    }
}
