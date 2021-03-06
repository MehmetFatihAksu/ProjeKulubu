﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using ProjeKulubu.Models;
using System.Data.Entity.Infrastructure;
using PagedList;

namespace ProjeKulubu.Controllers
{
    public class TagsController : Controller
    {
        db2299D218BEEntities10 db = new db2299D218BEEntities10();

        #region Views
        [UserAuthorize]
        public ActionResult TagsIndex(string Sorting_Order, string SearchString, string currentFilter, int? page)
        {
            ViewBag.BlogName = string.IsNullOrEmpty(Sorting_Order) ? "Makaleye_Gore" : "";
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

            var kayitlar = from x in db.Tags select x;

            if (!String.IsNullOrEmpty(SearchString))
            {
                kayitlar = kayitlar.Where(x => x.Blog.BlogTitle.Contains(SearchString));
            }

            switch (Sorting_Order)
            {
                case "Ada_Gore":
                    kayitlar = kayitlar.OrderBy(Tags => Tags.Blog.BlogTitle);
                    break;
                default:
                    kayitlar = kayitlar.OrderByDescending(Tags => Tags.ID);
                    break;
            }

            ViewBag.HtmlStr = kayitlar.Count();
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            return View(kayitlar.ToPagedList(pageNumber, pageSize));
        }

        [UserAuthorize]
        public ActionResult TagsDelete(int id)
        {
            var data = db.Tags.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        [UserAuthorize]
        public ActionResult TagsUpdate(int id)
        {
            var data = db.Tags.Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }
        #endregion

        #region Methods
        [HttpPost]
        public ActionResult AddTags(int blogId, string tagname)
        {
            Tags addTags = new Tags();
            addTags.BlogID = blogId;
            addTags.TagsName = tagname;
            db.Tags.Add(addTags);
            db.SaveChanges();
            return RedirectToAction("TagsIndex", "Tags");
        }

        [HttpPost]
        public ActionResult TagsDataUpdate(int id, int blogId, string tagname)
        {
            Tags updateTags = db.Tags.Where(x => x.ID == id).FirstOrDefault();
            updateTags.BlogID = blogId;
            updateTags.TagsName = tagname;
            db.SaveChanges();
            return RedirectToAction("TagsIndex", "Tags");
        }

        [HttpPost]
        public ActionResult TagsDataDelete(int id)
        {
            Tags tag = db.Tags.Find(id);
            db.Tags.Remove(tag);
            db.SaveChanges();
            return RedirectToAction("TagsIndex", "Tags");
        }

        public ActionResult MultipleDelete(IEnumerable<int> idler)
        {
            db.Tags.Where(x => idler.Contains(x.ID)).ToList().ForEach(y => db.Tags.Remove(y));
            db.SaveChanges();
            return RedirectToAction("TagsIndex", "Tags");
        }
        #endregion

    }
}
